using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class NotPrintStacktraceException : Exception, INotPrintStacktrace
    {
        public NotPrintStacktraceException(string msg) : base(msg)
        {
        }
    }

    public interface INotPrintStacktrace
    {
    }
}
