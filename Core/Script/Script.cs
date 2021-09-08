
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

        public static readonly string TEMPLATE_MATCH = "TEMPLATE_MATCH";


        public static readonly string CLICK_TEMPLATE = "CLICK_TEMPLATE";

    }

    public class Script
    {
        private static readonly int STACK_CAPACITY = 32;

        private static readonly string[] METHOD_WHITE_LIST = new string[]
        {
            "TemplateMatch",
            "ClickMatchedTemplate",
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

        [JsonIgnore]
        public int CurExecuteCount { get; set; } = 0;

        public int Interval { get; set; } = 2000;

        [JsonIgnore]
        private Stack stack = new Stack(STACK_CAPACITY);

        [JsonIgnore]
        public Stack Stack => stack;

        [JsonIgnore]
        public object AX { get; set; }

        [JsonIgnore]
        public object BX { get; set; }

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
            {
                throw new Exception($"stack value of offset:{offset} is not type of" + typeof(T).Name);
            }
        }

        public void DoOpCodes(IList<string> opCodes)
        {
            for (var i = 0; i < opCodes.Count; i++)
            {
                var opCode = opCodes[i];
                if (opCode == ScriptOps.TEMPLATE_MATCH)
                {
                    var arg3 = stack.Pop();
                    var arg2 = stack.Pop();
                    var arg1 = stack.Pop();
                    var result = Invoke("TemplateMatch", arg1, arg2, arg3);
                    stack.Push(result);
                }
                else if (opCode == ScriptOps.CLICK_TEMPLATE)
                {
                    var arg1 = AX;
                    object arg2;
                    if (CheckStackValueType<PVec2f>(-1))
                        arg2 = stack.Pop();
                    else
                        arg2 = new PVec2f(0, 0);
                    Invoke("ClickMatchedTemplate", arg1, arg2);
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

        public Img GetScreenShot()
        {
            if (_screenshot == null)
                _screenshot = new Img(_emulator.GetScreenCapture());
            return _screenshot;
        }

        public void TickStart()
        {

        }

        public void TickEnd()
        {
            _screenshot = null;
        }

        public ImgMatchResult TemplateMatch(string matchKey)
        {
            Logger.GetInstance().Debug("TemplateMatch", $"matchKey={matchKey}");
            var data = ImageSamplingData.GetWithAspectRatio();

            var screenShot = GetScreenShot();

            var sourceRectKey = matchKey + "_source";
            var rectVec4f = data.RVec4fs.ContainsKey(sourceRectKey) ? data.RVec4fs[sourceRectKey] : new RVec4f(0, 0, 1, 1);
            var sourceRect = screenShot.GetPartial(rectVec4f);
            var templateKey = matchKey + ".png";
            var template = data.GetResizedImg(templateKey, screenShot.Size);
            var threshold = data.GetThreshold(templateKey);

            var result = TemplateMatch(sourceRect, template, threshold);
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
