using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace GameOfLife
{
    class Board: IEnumerable
    {
        private int[,] logicalMap;
        private int[,] buffer;

        public Board()
        {
            logicalMap = new int[50, 50];
            buffer = new int[50, 50];
        }

        public int Length
        {
            get
            {
                return logicalMap.GetLength(0);
            }
        }

        public void SetCell(int x, int y, int state)
        {
            logicalMap[y, x] = buffer[y,x] = state;
        }

        public int this[int row, int column]
        {
            get
            {
                return logicalMap[row, column];
            }
            set
            {
                buffer[row, column] = value;
            }
        }

        public void Commit()
        {
            int[,] tmp = logicalMap;
            logicalMap = buffer;
            buffer = tmp;
        }
        public void Clear()
        {
            for (int y = 0; y < 50; y++)
                for (int x = 0; x < 50; x++)
                    logicalMap[y, x] = buffer[y, x] = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return logicalMap.GetEnumerator();
        }

        public bool Save(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            for (int x = 0; x < 50; x++)
                            {
                                bw.Write(logicalMap[y, x]);
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Load(string filename)
        {
            try
            {
                using (var sr = new StreamReader(filename))
                {
                    for (int y = 0; y < 50; y++)
                    {
                        for (int x = 0; x < 50; x++)
                        {
                            buffer[y, x] = logicalMap[y,x] = sr.Read();
                        }
                    }
                }

                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader r = new BinaryReader(fs))
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            for (int x = 0; x < 50; x++)
                            {
                                buffer[y, x] = logicalMap[y, x] = r.ReadInt32();
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
