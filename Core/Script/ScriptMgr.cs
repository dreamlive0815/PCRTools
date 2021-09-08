
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

        private Task defaultTask;


        public bool IsDefaultRunning => defaultTask != null && defaultTask.Status == TaskStatus.Running;

        public Task RunDefaultScript(Script script)
        {
            if (IsDefaultRunning)
                throw new Exception("another task is running");
            defaultTask = RunScript(script);
            return defaultTask;
        }

        public void StopDefaultScript()
        {
            defaultTask?.Dispose();
            defaultTask = null;
        }

        public Task RunScript(Script script)
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    var tickStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    try
                    {
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
            return task;
        }
    }
}
