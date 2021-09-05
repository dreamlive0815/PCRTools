
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Core.Script
{
    public class ScriptData
    {

        public string Identity { get; set; }

        public string Name { get; set; }

        public int MaxExecuteCount { get; set; } = int.MaxValue;

        [JsonIgnore]
        public int CurExecuteCount { get; set; } = 0;

        public int Interval { get; set; } = 2000;
    }


    public class Segment
    {

        public int Priority { get; set; }



    }

    public class Condition
    {



    }

    public class Action
    {

    }
}
