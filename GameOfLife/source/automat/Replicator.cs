using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    partial class Replicator:Algorithm
    {
        public Replicator()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            switch (neighbors.CountAliveNeighbors)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                    return 1;
            }

            return 0;
        }
    }
}
