
using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using Core.Common;
using Core.Emulators;
using Core.Model;


namespace Core.Script
{

    public static class ScriptOps
    {
        public static readonly string TEMPLATE_MATCH = "TEMPLATE_MATCH";


    }

    public class Script
    {
        private static readonly int STACK_CAPACITY = 32;

        private static readonly string[] METHOD_WHITE_LIST = new string[]
        {
            "TemplateMatch",
        };


        public string Identity { get; set; }

        public string Name { get; set; }

        public List<Segment> Segments { get; set; } = new List<Segment>(); 

        public int MaxExecuteCount { get; set; } = int.MaxValue;

        [JsonIgnore]
        public int CurExecuteCount { get; set; } = 0;

        public int Interval { get; set; } = 2000;

        [JsonIgnore]
        private Stack stack = new Stack(STACK_CAPACITY);

        public void StackPush(object obj)
        {
            stack.Push(obj);
        }

        public void Reset()
        {
            stack.Reset();
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
            }
        }

        public object Invoke(string funcName, params object[] args)
        {
            if (Array.IndexOf(METHOD_WHITE_LIST, funcName) == -1)
                throw new Exception("funcName not in white list");
            var type = GetType();
            var method = type.GetMethod(funcName);
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
        private Img _img;

        public Img GetScreenShot()
        {
            if (_img == null)
                _img = new Img(_emulator.GetScreenCapture());
            return _img;
        }

        public void TickStart()
        {

        }

        public void TickEnd()
        {
            _img = null;
        }

        public ImgMatchResult TemplateMatch(string matchKey)
        {
            var data = ImageSamplingData.GetWithAspectRatio();

            var screenShot = GetScreenShot();

            var rectVec4f = data.RVec4fs.ContainsKey(matchKey) ? data.RVec4fs[matchKey] : new RVec4f(0, 0, 1, 1);
            var rect = screenShot.GetPartial(rectVec4f);
            var template = data.GetResizedImg(matchKey, screenShot.Size);
            var threshold = data.GetThreshold(matchKey);

            var result = TemplateMatch(rect, template, threshold);
            return result;
        }


        private ImgMatchResult TemplateMatch(Img source, Img template, double threshold)
        {
            var result = source.Match(template, threshold);
            return result;
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

        public virtual bool UseOpCodes { get; set; } = false;

        public List<string> OpCodes { get; set; } = new List<string>();
    }

    public class Action
    {

    }
}
