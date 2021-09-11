
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Core.Common;

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
