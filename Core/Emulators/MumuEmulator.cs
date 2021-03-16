using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emulators
{
    class MumuEmulator : Emulator
    {
        public override string Name { get; set; } = "MumuEmulator";

        public override bool Alive
        {
            get { return false; }
        }

        public override Rectangle Area
        {
            get { return new Rectangle(); }
        }

        
    }
}
