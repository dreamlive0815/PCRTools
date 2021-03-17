using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emulators
{
    public class NOXEmulator : Emulator
    {
        public override string Name { get { return "NOXEmulator"; } }

        public override Rectangle Area
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Process GetMainProcess()
        {
            return GetProcessByName("NoxPlayer");
        }
    }
}
