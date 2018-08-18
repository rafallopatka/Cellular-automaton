using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class Conway: Algorithm
    {
        public Conway()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender});
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }

        public override int Transition(int state, Neighbors neighbors)
        {
            if (state == 0 && neighbors.CountAliveNeighbors == 3)
                return 1;

            if (state == 1 && (neighbors.CountAliveNeighbors == 2 || neighbors.CountAliveNeighbors == 3))
                return 1;

            return 0;
        }
    }
}
