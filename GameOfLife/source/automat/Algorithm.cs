using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    abstract class Algorithm
    {
        protected List<State> statesList = new List<State>();
        public List<State> StatesList
        {
            get { return statesList; }
        }

        public abstract int Transition(int state, Neighbors neighbors);
    }
}
