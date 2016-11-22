using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sudoku
{
    class Node
    {
        public int value;
        public int x, y;
        public int colorValue;
        public Color color;
        public List<int> connectedNodes;
        public Node(int value)
        {
            colorValue = -1;
            this.value = value;
        }
    }
}
