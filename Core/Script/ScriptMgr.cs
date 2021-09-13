
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Core.Common;
using EventSystem;

namespace Core.Script
{
    public class ScriptMgr
    {

        private static ScriptMgr instance;

        public static ScriptMgr GetInstance()
        {
            if (instance == null)
            {
                instance = new ScriptMgr();
            }
            return instance;
        }

        private ScriptMgr()
        {
            if (ScriptMetaInfos.Count == 0)
            {
                LoadDefaultScripts();
            }
            SortScriptMetaInfos();
        }

        public void LoadDefaultScripts()
        {
            var dirPath = ResourceManager.Default.GetFullPath("${G}/Script");
            AddScriptsFromDirectory(dirPath);
        }

        public void SortScriptMetaInfos()
        {
            ScriptMetaInfos.Sort((a, b) =>
            {
                //if (a.Enabled != b.Enabled)
                //    return -a.Enabled.CompareTo(b.Enabled);
                return -a.Priority.CompareTo(b.Priority);
            });
        }

        public List<ScriptMetaInfo> ScriptMetaInfos
        {
            get { return ConfigMgr.GetConfig().ScriptMetaInfos; }
            set { ConfigMgr.GetConfig().ScriptMetaInfos = value; }
        }

        private IDictionary<string, Script> _scripts = new Dictionary<string, Script>();

        private ScriptMetaInfo GetScriptMetaInfoByIdentity(string identity)
        {
            foreach (var info in ScriptMetaInfos)
            {
                if (info.Identity == identity)
                    return info;
            }
            return null;
        }

        private ScriptMetaInfo GetScriptMetaInfoByFilePath(string filePath)
        {
            foreach (var info in ScriptMetaInfos)
            {
                if (info.FilePath == filePath)
                    return info;
            }
            return null;
        }

        public Script GetScript(string identity)
        {
            if (_scripts.ContainsKey(identity))
                return _scripts[identity];
            var info = GetScriptMetaInfoByIdentity(identity);
            if (info != null)
            {
                var script = Script.FromFile(info.FilePath);
                _scripts[script.Identity] = script;
                return script;
            }
            return null;
        }

        public Script ReloadScript(string identity)
        {
            _scripts.Remove(identity);
            return GetScript(identity);
        }

        private void AddScriptFunc(string filePath)
        {
            var sameFilePath = GetScriptMetaInfoByFilePath(filePath);
            if (sameFilePath != null)
            {
                Logger.GetInstance().Warn("AddScript", $"script of filePath:{filePath} already loaded");
                return;
            }
            var script = Script.FromFile(filePath);
            var sameIdentity = GetScriptMetaInfoByIdentity(script.Identity);
            if (sameIdentity != null)
            {
                Logger.GetInstance().Warn("AddScript", $"script of identity:{script.Identity} already loaded");
                return;
            }
            var newMetaInfo = new ScriptMetaInfo()
            {
                FilePath = filePath,
                Identity = script.Identity,
            };
            ScriptMetaInfos.Add(newMetaInfo);
            _scripts[script.Identity] = script;
        }

        public void AddScript(string filePath)
        {
            AddScriptFunc(filePath);
            SortScriptMetaInfos();
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        public void AddScripts(IList<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                AddScriptFunc(filePath);
            }
            SortScriptMetaInfos();
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        public void AddScriptsFromDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Logger.GetInstance().Warn("AddScriptsFromDirectory", $"dirPath:{dirPath} not exist");
                return;
            }
            var filePaths = Directory.GetFiles(dirPath, "*" + Script.FileExt);
            foreach (var filePath in filePaths)
            {
                AddScriptFunc(filePath);
            }
            SortScriptMetaInfos();
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        private ScriptController defaultScriptController;

        public bool IsDefaultRunning => defaultScriptController != null && defaultScriptController.IsRunning;

        public ScriptController RunDefaultScript(Script script)
        {
            if (IsDefaultRunning)
                throw new Exception("another task is running");
            defaultScriptController = RunScript(script);
            return defaultScriptController;
        }

        public void StopDefaultScript()
        {
            if (!IsDefaultRunning)
                return;
            defaultScriptController.Stop();
        }

        public ScriptController RunScript(Script script)
        {
            var tokenSource = new CancellationTokenSource();
            var task = Task.Run(() =>
            {
                while (true)
                {
                    if (tokenSource.Token.IsCancellationRequested)
                    {
                        Logger.GetInstance().Info("RunScript", $"script: {script.Name} is terminated");
                        return;
                    }
                    var tickStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    try
                    {
                        Logger.GetInstance().Info("ScriptTick", $"Tick{script.CurExecuteCount}");
                        script.Tick();
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().Error("ScriptTick", Utils.GetErrorDescription(e));
                        if (script.StopWhenException)
                            break;
                    }
                    if (script.CurExecuteCount >= script.MaxExecuteCount)
                        break;
                    var tickEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    var takes = tickEndTime - tickStartTime;
                    var ms = script.Interval - takes;
                    if (ms > 0) Thread.Sleep((int)ms);
                }
            });
            task.ContinueWith((t) =>
            {
                if (t.Exception != null)
                {
                    Logger.GetInstance().Error("RunScript", Utils.GetErrorDescription(t.Exception));
                }
            });
            return new ScriptController(script, task, tokenSource);
        }
    }

    public class ScriptController
    {
        private Task _scriptTask;
        private CancellationTokenSource _scriptTaskTokenSource;

        public ScriptController(Script script, Task task, CancellationTokenSource tokenSource)
        {
            Script = script;
            _scriptTask = task;
            _scriptTaskTokenSource = tokenSource;
        }

        public Script Script { get; }

        public bool IsRunning => _scriptTask != null && _scriptTask.Status == TaskStatus.Running;

        public void Stop()
        {
            _scriptTaskTokenSource.Cancel();
        }
    }
}
