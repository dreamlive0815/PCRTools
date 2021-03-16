using System;
using System.Drawing;

namespace Core.Emulators
{
    public abstract class Emulator
    {

        public abstract string Name { get; set; }

        public abstract bool Alive { get; }

        public abstract Rectangle Area { get; }

    }
}
