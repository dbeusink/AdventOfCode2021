using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class SubmarineNavigator
    {
        public int HorizontalPosition { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public int Product => HorizontalPosition * Depth;

        public void Move(SubmarineMovement movement, int units)
        {
            switch (movement)
            {
                case SubmarineMovement.Forward: HorizontalPosition += units; break;
                case SubmarineMovement.Up: Depth -= units; break;
                case SubmarineMovement.Down: Depth += units; break;
            }
        }
    }

    internal enum SubmarineMovement
    {
        Forward,
        Up,
        Down,
    }
}
