using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class Gnarl:Algorithm
    {
        public Gnarl()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            if (neighbors.CountAliveNeighbors == 1)
                return 1;
            else
                return 0;
        }
    }
}
