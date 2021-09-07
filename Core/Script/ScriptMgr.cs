
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
                    ScriptTick(script);
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

        private void ScriptTick(Script script)
        {
            script.TickStart();
            var segGroups = script.GetSegmentGroups();
            foreach (var group in segGroups)
            {
                ScriptTickSegments(script, group.Value);
            }
            script.CurExecuteCount++;
            script.TickEnd();
        }

        /// <summary>
        /// 同组互斥
        /// </summary>
        /// <param name="script"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        private bool ScriptTickSegments(Script script, List<Segment> segments)
        {
            foreach (var seg in segments)
            {
                foreach (var condition in seg.Conditions)
                {
                    if (!string.IsNullOrWhiteSpace(condition.MatchKey))
                    {
                        var result = script.TemplateMatch(condition.MatchKey);
                        script.Stack.Push(result.Success);
                        script.AX = result;
                    }
                    if (condition.OpCodes != null)
                    {
                        script.DoOpCodes(condition.OpCodes);
                    }
                }
                if (!(script.Stack.Top() is bool))
                    throw new Exception("the top value of stack is not bool value");
                var b = true;
                while (!script.Stack.Empty && script.Stack.Top() is bool)
                {
                    var top = (bool)script.Stack.Pop();
                    b = b && top;
                }
                if (b)
                {
                    foreach (var action in seg.Actions)
                    {
                        script.DoOpCodes(action.OpCodes);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
