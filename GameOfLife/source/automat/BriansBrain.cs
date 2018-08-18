using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    class BriansBrain: Algorithm
    {
        public BriansBrain()
        {
            this.statesList.Add(new State() { Name = "Oczekująca", Value = 0 , Color = Brushes.Lavender });
            this.statesList.Add(new State() { Name = "Płonąca", Value = 1, Color = Brushes.Red });
            this.statesList.Add(new State() { Name = "Przetwarzająca", Value = 2, Color = Brushes.Orange });
        }
        public override int Transition(int state, Neighbors neighbors)
        {
            if (state == 1)
                return 2;
            if (state == 2)
                return 0;
            if (neighbors.CountNeighborsOnState(1) == 2)
                return 1;

            return 0;
        }
    }
}
