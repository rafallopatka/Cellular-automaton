using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class DayAndNight:Algorithm
    {
        public DayAndNight()
        {
            this.statesList.Add(new State() { Name = "Martwa", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Żywa", Value = 1, Color = Brushes.Red });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            ///	34678/3678
            if (state == 0)
            {
                switch (neighbors.CountAliveNeighbors)
                {
                    case 3:
                    case 6:
                    case 7:
                    case 8:
                        {
                            return 1;
                        }
                }
            }

            if (state == 1)
            {
                switch (neighbors.CountAliveNeighbors)
                {
                    case 3:
                    case 4:
                    case 6:
                    case 7:
                    case 8:
                        {
                            return 1;
                        }
                }
            }


            return 0;

        }
    }
}
