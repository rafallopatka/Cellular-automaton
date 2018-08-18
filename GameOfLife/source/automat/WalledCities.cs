using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class WalledCities:Algorithm
    {
        public WalledCities()
        {
            this.statesList.Add(new State() { Name = "Brak komórki", Value = 0, Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Budowniczy", Value = 1, Color = Brushes.Red });
            this.statesList.Add(new State() { Name = "Ściana", Value = 2, Color = Brushes.Orange });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            if (state != 0)
                switch (neighbors.CountAliveNeighbors)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return 1;
                }
            else
                switch (neighbors.CountAliveNeighbors)
                {
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        return 2;
                }

            return 0;
        }
    }
}
