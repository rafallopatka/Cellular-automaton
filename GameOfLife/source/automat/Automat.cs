using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    partial class Automat
    {

        private Dictionary<string, Algorithm> algorithms;

        public Automat()
        {
            algorithms = new Dictionary<string, Algorithm>();
            RegisteAlgorithms();
        }

        private void RegisteAlgorithms()
        {
            algorithms.Add("Conway Game of Life", new Conway());
            algorithms.Add("HighLife", new HighLife());
            algorithms.Add("Brian's Brain", new BriansBrain());
            algorithms.Add("Coral", new Coral());
            algorithms.Add("Maze", new Amazing());
            algorithms.Add("Replicator", new Replicator());
            algorithms.Add("Wire-World", new WireWorld());
            algorithms.Add("Walled Cities", new WalledCities());
            algorithms.Add("Day and Night", new DayAndNight());
            algorithms.Add("Gnarl", new Gnarl());
        }

        public List<string> MethodList
        {
            get
            {
                return algorithms.Keys.ToList();
            }
        }

        public List<State> GetStateList(string algorithm)
        {
            return algorithms[algorithm].StatesList;
        }

        public bool Execute(string algorithm, ref Board board)
        {
            if(algorithm == null || algorithms.Keys.Contains(algorithm) == false)
                        return false;

            board.Commit();

            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    board[y,x] = algorithms[algorithm].Transition(board[y,x], new Neighbors(x, y, board));
                }
            }            

            return true;
        }
    }
}
