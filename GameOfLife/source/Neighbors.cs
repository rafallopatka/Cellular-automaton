using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Neighbors
    {
        private int[,] neighbors;
        /// <summary>
        /// Pobiera tablice sąsiadów moora, w porządky [y,x]
        /// </summary>
        public int[,] Neighborhood
        {
            get { return neighbors; }
        }

        public Neighbors(int x, int y, Board board)
        {
            neighbors = new int[3, 3];
            
            //sasiad południowo zachodni
            if (x > 0 && y < board.Length - 1)
                neighbors[2,0] = board[y + 1, x - 1];
            //sasiad zachodni
            if (x > 0)
                neighbors[1,0] = board[y, x - 1];
            //sasiad północno zachodni
            if (x > 0 && y > 0)
                neighbors[0,0] = board[y - 1, x - 1];
            //sasiad północny
            if (y > 0)
                neighbors[0,1] = board[y - 1, x];
            //sasiad połnocno wschodni
            if (x < board.Length - 1 && y > 0)
                neighbors[0,2] = board[y - 1, x + 1];
            //sasiad wschodni
            if (x < board.Length - 1)
                neighbors[1,2] = board[y, x + 1];
            //sasiad południowo wschodni
            if (x < board.Length - 1 && y < board.Length - 1)
                neighbors[2,2] = board[y + 1, x + 1];
            //sasiad poludniowy
            if (y < board.Length - 1)
                neighbors[2,1] = board[y + 1, x];
        }
        public int CountAliveNeighbors
        {
            get
            {
                int count = 0;
                foreach (int item in neighbors)
                    if (item > 0)
                        ++count;

                return count;
            }
        }
        public int CountNeighborsOnState(int state)
        {
            int count = 0;
            foreach (int item in neighbors)
                if (item == state)
                    ++count;

            return count;
        }

        public int N
        {
            get
            {
                return neighbors[0, 1];
            }
        }
        public int W
        {
            get
            {
                return neighbors[1, 0];
            }
        }
        public int S
        {
            get
            {
                return neighbors[2, 1];
            }
        }
        public int E
        {
            get
            {
                return neighbors[1, 2];
            }
        }
        public int NW
        {
            get
            {
                return neighbors[0, 0];
            }
        }
        public int NE
        {
            get
            {
                return neighbors[0, 2];
            }
        }
        public int SW
        {
            get
            {
                return neighbors[2, 0];
            }
        }
        public int SE
        {
            get
            {
                return neighbors[2, 2];
            }
        }
    }
}
