using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class Csv
    {


        private Dictionary<string, int> headerIndexes;


        public List<CsvHeader> Headers { get; private set; }

        private void SetHeaders(List<string> headers)
        {

        }
        
    }

    public class CsvHeader
    {
        public string Value { get; set; }
    }
}
