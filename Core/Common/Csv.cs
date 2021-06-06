using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class Csv : IEnumerable<CsvRow>
    {


        private static List<string> SplitVals(string line)
        {
            var arr = line.Split(new char[] { ',' });
            return new List<string>(arr);
        }

        public static Csv FromFile(string filePath)
        {
            var csv = new Csv();
            var lines = File.ReadAllLines(filePath);
            var headerLine = lines[0];
            var headerVals = SplitVals(headerLine);
            csv.SetHeaders(headerVals);
            for (int i = 1; i < lines.Length; i++)
            {
                var dataLine = lines[i];
                var dataVals = SplitVals(dataLine);
                csv.AddRow(dataVals);
            }
            return csv;
        }

        public List<CsvHeader> Headers { get; private set; }

        private Dictionary<string, int> columnIndexes;

        private void SetHeaders(List<string> headerVals)
        {
            var headers = new List<CsvHeader>();
            columnIndexes = new Dictionary<string, int>();
            for (var i = 0; i < headerVals.Count; i++)
            {
                var val = headerVals[i];
                headers.Add(new CsvHeader() { Value = val });
                columnIndexes[val] = i;
            }
            Headers = headers;
        }

        public bool HasHeader(string headerVal)
        {
            return columnIndexes.ContainsKey(headerVal);
        }

        public bool TryAddHeader(string headerVal)
        {
            if (HasHeader(headerVal))
                return false;
            columnIndexes[headerVal] = Headers.Count;
            Headers.Add(new CsvHeader() { Value = headerVal });
            return true;
        }

        public int GetColumnIndex(string headerVal)
        {
            return columnIndexes[headerVal];
        }

        private List<CsvRow> rows = new List<CsvRow>();

        private Dictionary<string, CsvRow> rowsDict = new Dictionary<string, CsvRow>();


        public int Count
        {
            get { return rows.Count; }
        }

        private int GetKeyColumnIndex()
        {
            return 0;
        }

        public void AddRow(List<string> dataVals)
        {
            var row = new CsvRow(this, dataVals);
            rows.Add(row);
            var key = dataVals[GetKeyColumnIndex()];
            rowsDict[key] = row;
        }

        public CsvRow this[int index]
        {
            get
            {
                return rows[index];
            }
        }

        public CsvRow this[string key]
        {
            get
            {
                if (!rowsDict.ContainsKey(key))
                    return null;
                var row = rowsDict[key];
                return row;
            }
        }


        public List<string> GetRowKeys()
        {
            var r = new List<string>();
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                r.Add(row[GetKeyColumnIndex()]);
            }
            return r;
        }

        public IEnumerator<CsvRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CsvHeader
    {
        public string Value { get; set; }
    }

    public class CsvRow
    {
        private Csv csv;
        private List<string> vals;

        public CsvRow(Csv csv, List<string> vals)
        {
            this.csv = csv;
            this.vals = vals;
        }

        public string this[int index]
        {
            get
            {
                return vals[index];
            }
        }

        public string this[string header]
        {
            get
            {
                var index = csv.GetColumnIndex(header);
                return vals[index];
            }
            set
            {
                var index = csv.GetColumnIndex(header);
                vals[index] = value;
            }
        }
    }
}
