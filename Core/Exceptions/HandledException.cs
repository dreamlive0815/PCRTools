using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class HandledException : Exception, IHandled
    {
        public HandledException(string msg) : base(msg) { }
    }

    public interface IHandled
    {
    }
}
