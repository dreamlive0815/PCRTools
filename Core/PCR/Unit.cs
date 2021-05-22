using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PCR
{
    public class Unit
    {

        public static Csv GetCsv()
        {
            var path = ResourceMgr.Default.Csv("Unit.csv").AssertExists().Fullpath;
            var csv = Csv.FromFile(path);
            return csv;
        }

        public static List<string> GetAllIds()
        {
            var csv = GetCsv();
            var keys = csv.GetRowKeys();
            return keys;
        }
    }
}
