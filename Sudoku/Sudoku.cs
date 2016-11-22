using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    class Sudoku
    {
        public int dimension;
        public Graph graph;
        public Sudoku(int dimension)
        {
            this.dimension = dimension;
            graph = new Graph(dimension);
            BuildGraph();
            graph.ApplyGraphColoring();
        }
        void BuildGraph()
        {
            int row = 1;
            for (int i = 0; i < dimension*dimension; i++)
            {
                row = (i + dimension) / dimension;
                for (int j = 1; j + i < row * dimension; j++)
                {
                    graph.nodes[i].connectedNodes.Add(i + j);
                    graph.nodes[i + j].connectedNodes.Add(i);
                    graph.adjacencyMatrix[i][i + j] = graph.adjacencyMatrix[i + j][i] = true;
                }
                for (int j = dimension; j + i < dimension * dimension; j+=dimension)
                {
                    graph.nodes[i].connectedNodes.Add(i + j);
                    graph.nodes[i + j].connectedNodes.Add(i);
                    graph.adjacencyMatrix[i][i + j] = graph.adjacencyMatrix[i + j][i] = true;
                }
            }
        }
    }
}
