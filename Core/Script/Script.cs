﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;

using Core.Common;
using Core.Emulators;
using Core.Model;


namespace Core.Script
{

    public static class ScriptOps
    {

        public static readonly string NOT = "NOT";

        public static readonly string MOVE_TO_AX = "MOVE_TO_AX";

        public static readonly string MOVE_TO_BX = "MOVE_TO_BX";

        public static readonly string MOVE_TO_CX = "MOVE_TO_CX";

        public static readonly string MOVE_TO_DX = "MOVE_TO_DX";

        public static readonly string PARSE_INT = "PARSE_INT";

        public static readonly string PARSE_PVEC2F = "PARSE_PVEC2F";

        public static readonly string CLICK_TEMPLATE = "CLICK_TEMPLATE";

        public static readonly string DO_DRAG = "DO_DRAG";

        //public static readonly string ASSERT_TRUE = "ASSERT_TRUE";

        public static readonly string TEMPLATE_MATCH = "TEMPLATE_MATCH";

    }

    public class Script
    {
        private static readonly int STACK_CAPACITY = 32;

        private static readonly string[] METHOD_WHITE_LIST = new string[]
        {
            "TemplateMatch",
            "ClickMatchedTemplate",
            "DoDrag",
        };


        public string Identity { get; set; }

        public string Name { get; set; }

        public List<Segment> Segments { get; set; } = new List<Segment>();

        [JsonIgnore]
        private IDictionary<string, List<Segment>> _segGroups;

        public IDictionary<string, List<Segment>> GetSegmentGroups()
        {
            if (_segGroups != null)
                return _segGroups;
            _segGroups = new SortedDictionary<string, List<Segment>>();
            foreach (var seg in Segments)
            {
                var groupKey = seg.Group;
                if (!_segGroups.ContainsKey(groupKey)) _segGroups.Add(groupKey, new List<Segment>());
                _segGroups[groupKey].Add(seg);
            }
            foreach (var pair in _segGroups)
            {
                pair.Value.Sort((x, y) => -x.Priority.CompareTo(y.Priority));
            }
            return _segGroups;
        }

        public int MaxExecuteCount { get; set; } = int.MaxValue;

        public bool StopWhenException { get; set; } = true;

        [JsonIgnore]
        public int CurExecuteCount { get; set; } = 0;

        public int Interval { get; set; } = 2000;

        [JsonIgnore]
        private Stack stack = new Stack(STACK_CAPACITY);

        //[JsonIgnore]
        //public Stack Stack => stack;

        [JsonIgnore]
        private object AX { get; set; }

        [JsonIgnore]
        private object BX { get; set; }

        [JsonIgnore]
        private object CX { get; set; }

        [JsonIgnore]
        private object DX { get; set; }

        public void Reset()
        {
            _screenshot?.Dispose();
            _segGroups = null;
            _emulator = null;
            _screenshot = null;
            stack.Reset();
        }

        private bool CheckStackValueType<T>(int offset)
        {
            var value = stack[offset];
            return value is T;
        }

        private void AssertStackValueType<T>(int offset)
        {
            var value = stack[offset];
            if (!(value is T))
                throw new Exception($"stack value of offset:{offset} is not type of" + typeof(T).Name);
        }

        private void Assert(bool b, string prompt)
        {
            if (!b)
                throw new Exception(prompt);
        }

        private void AssertType<T>(object value)
        {
            Assert(value is T, $"value:{value} is not type of" + typeof(T).Name);
        }

        public void DoOpCodes(IList<string> opCodes)
        {
            for (var i = 0; i < opCodes.Count; i++)
            {
                var opCode = opCodes[i];
                if (false)
                {

                }
                else if (opCode == ScriptOps.NOT)
                {
                    var top = stack.Pop();
                    AssertType<bool>(top);
                    stack.Push(!(bool)top);
                }
                else if (opCode == ScriptOps.MOVE_TO_AX)
                {
                    var top = stack.Pop();
                    AX = top;
                }
                else if (opCode == ScriptOps.MOVE_TO_BX)
                {
                    var top = stack.Pop();
                    BX = top;
                }
                else if (opCode == ScriptOps.MOVE_TO_CX)
                {
                    var top = stack.Pop();
                    CX = top;
                }
                else if (opCode == ScriptOps.MOVE_TO_DX)
                {
                    var top = stack.Pop();
                    DX = top;
                }
                else if (opCode == ScriptOps.PARSE_INT)
                {
                    var n = int.Parse(opCodes[++i]);
                    stack.Push(n);
                }
                else if (opCode == ScriptOps.PARSE_PVEC2F)
                {
                    var pVec2f = PVec2f.Parse(opCodes[++i]);
                    stack.Push(pVec2f);
                }
                else if (opCode == ScriptOps.CLICK_TEMPLATE)
                {
                    var arg1 = AX;
                    AssertType<ImgMatchResult>(arg1);
                    object arg2 = BX is PVec2f ? BX : new PVec2f(0, 0);
                    Invoke("ClickMatchedTemplate", arg1, arg2);
                }
                else if (opCode == ScriptOps.DO_DRAG)
                {
                    var arg1 = AX;
                    AssertType<PVec2f>(arg1);
                    var arg2 = BX;
                    AssertType<PVec2f>(arg2);
                    var arg3 = CX;
                    AssertType<int>(arg3);
                    Invoke("DoDrag", arg1, arg2, arg3);
                }
                //else if (opCode == ScriptOps.ASSERT_TRUE)
                //{
                //    var top = stack.Top();
                //    Assert(top is bool && (bool)top, "assert stack top value true failed");
                //}
                else if (opCode == ScriptOps.TEMPLATE_MATCH)
                {
                    var arg3 = stack.Pop();
                    var arg2 = stack.Pop();
                    var arg1 = stack.Pop();
                    var result = Invoke("TemplateMatch", arg1, arg2, arg3);
                    stack.Push(result);
                }
            }
        }

        private object Invoke(string funcName, params object[] args)
        {
            if (Array.IndexOf(METHOD_WHITE_LIST, funcName) == -1)
                throw new Exception("funcName not in white list");
            var type = GetType();
            var method = type.GetMethod(funcName, BindingFlags.NonPublic | BindingFlags.Instance);
            var ret = method.Invoke(this, args);
            return ret;
        }

        [JsonIgnore]
        private Emulator _emulator;

        public void SetEmulator(Emulator emulator)
        {
            _emulator = emulator;
        }

        [JsonIgnore]
        private Img _screenshot;

        private Img GetScreenShot()
        {
            if (_screenshot == null)
                _screenshot = new Img(_emulator.GetScreenCapture());
            return _screenshot;
        }

        [JsonIgnore]
        private IDictionary<string, ImgMatchResult> _templateMatchResults;

        private void TickStart()
        {
            AX = BX = CX = DX = null;
            _screenshot = null;
            _templateMatchResults = new Dictionary<string, ImgMatchResult>();
        }

        public void Tick()
        {
            TickStart();
            var segGroups = GetSegmentGroups();
            foreach (var group in segGroups)
            {
                ScriptTickSegments(group.Value);
            }
            CurExecuteCount++;
            TickEnd();
        }

        [JsonIgnore]
        private bool _breakSegment;

        /// <summary>
        /// 同组互斥
        /// </summary>
        /// <param name="script"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        private bool ScriptTickSegments(List<Segment> segments)
        {
            foreach (var seg in segments)
            {
                _breakSegment = false;
                foreach (var condition in seg.Conditions)
                {
                    if (!string.IsNullOrWhiteSpace(condition.MatchKey))
                    {
                        var result = TemplateMatchByKey(condition.MatchKey);
                        stack.Push(result.Success);
                        AX = result;
                    }
                    if (condition.OpCodes != null)
                    {
                        DoOpCodes(condition.OpCodes);
                    }
                    if (_breakSegment)
                        break;
                }
                if (!(stack.Top() is bool))
                    throw new Exception("the top value of stack is not bool value");
                var b = true;
                while (!stack.Empty && stack.Top() is bool)
                {
                    var top = (bool)stack.Pop();
                    b = b && top;
                }
                if (_breakSegment)
                    continue;
                if (b)
                {
                    foreach (var action in seg.Actions)
                    {
                        DoOpCodes(action.OpCodes);
                    }
                    return true;
                }
            }
            return false;
        }

        private void TickEnd()
        {
        }

        private ImgMatchResult TemplateMatchByKey(string matchKey)
        {
            Logger.GetInstance().Debug("TemplateMatch", $"matchKey={matchKey}");

            if (_templateMatchResults.ContainsKey(matchKey))
            {
                return _templateMatchResults[matchKey];
            }

            var data = ImageSamplingData.GetWithAspectRatio();

            var screenShot = GetScreenShot();

            var sourceRectKey = matchKey + "_source";
            var rectVec4f = data.RVec4fs.ContainsKey(sourceRectKey) ? data.RVec4fs[sourceRectKey] : new RVec4f(0, 0, 1, 1);
            var sourceRect = screenShot.GetPartial(rectVec4f);
            var templateKey = matchKey + ".png";
            var template = data.GetResizedImg(templateKey, screenShot.Size);
            var threshold = data.GetThreshold(templateKey);

            var result = TemplateMatch(sourceRect, template, threshold);
            _templateMatchResults[matchKey] = result;
            return result;
        }

        private ImgMatchResult TemplateMatch(Img source, Img template, double threshold)
        {
            var result = source.Match(template, threshold);
            return result;
        }

        private void ClickMatchedTemplate(ImgMatchResult result, PVec2f offset)
        {
            if (!result.Success)
                return;
            var pVec2f = result.GetMatchedRectCenterVec2f();
            pVec2f += offset;
            _emulator.DoTap(pVec2f);
        }

        private void DoDrag(PVec2f start, PVec2f end, int milliSeconds)
        {
            _emulator.DoDrag(start, end, milliSeconds);
        }

        public void Save(string filePath)
        {
            var jsonStr = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, jsonStr);
        }
    }

    public class Stack
    {
        private int _capacity;
        private object[] container;
        private int topIndex;

        public Stack(int capacity)
        {
            _capacity = capacity;
            Reset();
        }

        public void Reset()
        {
            container = new object[_capacity];
            topIndex = -1;
        }

        //public int TopIndex => topIndex;

        public bool Empty => topIndex <= -1;

        public bool Full => topIndex + 1 >= _capacity;

        public object Top()
        {
            if (Empty)
                throw new Exception("Script Stack Empty");
            else
                return container[topIndex];
        }

        public void Push(object obj)
        {
            if (Full)
                throw new Exception("Script Stack Overflow");
            container[++topIndex] = obj;
        }

        public object Pop()
        {
            if (Empty)
                return null;
            var obj = container[topIndex--];
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset">-1栈顶向下减小 1栈底向上增加</param>
        /// <returns></returns>
        public object this[int offset]
        {
            get
            {
                var index = -1;
                if (offset < 0)
                    index = topIndex + offset + 1;
                else if (offset > 0)
                    index = offset - 1;
                if (index < 0 || index >= _capacity)
                    return null;
                return container[index];
            }
        }
    }


    public class Segment
    {

        public string Group { get; set; } = "default";

        public int Priority { get; set; } = 0;

        public List<Condition> Conditions { get; set; } = new List<Condition>();

        public List<Action> Actions { get; set; } = new List<Action>();

        public string Comment { get; set; }

    }

    public class Condition
    {
        public string MatchKey { get; set; }

        public List<string> OpCodes { get; set; }
    }

    public class Action
    {
        public List<string> OpCodes { get; set; } = new List<string>();
    }
}
