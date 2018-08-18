using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class WireWorld: Algorithm
    {
        public WireWorld()
        {
            this.statesList.Add(new State() { Name = "Brak komórki", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Przód elektronu", Value = 1, Color = Brushes.Red });
            this.statesList.Add(new State() { Name = "Tył elektronu", Value = 2, Color = Brushes.Orange });
            this.statesList.Add(new State() { Name = "Przewodnik", Value = 3, Color = Brushes.Brown });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            switch (state)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    switch (neighbors.CountNeighborsOnState(1))
                    {
                        case 1:
                        case 2:
                            return 1;
                        default:
                            return 3;
                    }
                default:
                    return 0;
            }
        }
    }
}
