using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GameOfLife
{
    struct State
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public Brush Color { get; set; }
    }
}
