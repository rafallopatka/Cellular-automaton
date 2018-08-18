using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class HighLife:Algorithm
    {
        public HighLife()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            switch (neighbors.CountAliveNeighbors)
            {
                case 2:
                    return state;
                case 3:
                    return 1;
                case 6:
                    return (state == 0) ? 1 : 0;
                default:
                    return 0;
            }
        }
    }
}
