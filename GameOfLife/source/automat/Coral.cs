using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class Coral: Algorithm
    {
        public Coral()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }

        public override int Transition(int state, Neighbors neighbors)
        {
            switch (neighbors.CountAliveNeighbors)
            {
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return state;
                case 3:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
