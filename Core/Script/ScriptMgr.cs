using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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


        public Task RunScript(Script script)
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    var tickStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    ScriptTick(script);
                    script.CurExecuteCount++;
                    if (script.CurExecuteCount >= script.MaxExecuteCount)
                        break;
                    var tickEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    var takes = tickEndTime - tickStartTime;
                    var ms = script.Interval - takes;
                    if (ms > 0) Thread.Sleep((int)ms);
                }
            });
            return task;
        }

        private void ScriptTick(Script script)
        {
            var segGroups = script.GetSegmentGroups();
            foreach (var group in segGroups)
            {
                ScriptTickSegments(script, group.Value);
            }
        }

        private void ScriptTickSegments(Script script, List<Segment> segments)
        {
            foreach (var seg in segments)
            {
                foreach (var condition in seg.Conditions)
                {
                    if (condition.UseOpCodes)
                    {
                        script.DoOpCodes(condition.OpCodes);
                    }
                    else
                    {
                        var result = script.TemplateMatch(condition.MatchKey);
                        script.Stack.Push(result.Success);
                        script.AX = result;
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

                }
            }
        }
    }
}
