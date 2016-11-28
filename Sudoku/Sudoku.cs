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
        public Sudoku(int dimension, int[][] initialValues, bool isGreedy)
        {
            this.dimension = dimension;
            graph = new Graph(dimension);
            fillGraph(initialValues);
            BuildGraph();
            if(isGreedy)
                graph.ApplyGraphColoring();
            else
                DominationCovering.ApplyGraphColoring(graph.nodes, graph.adjacencyMatrix);
        }
        public void fillGraph(int[][] initialGraph)
        {
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (initialGraph[i][j] != -1)
                    {
                        graph.nodes[i * dimension + j].colorValue = initialGraph[i][j];
                    }
                }
            }
        }
        void BuildGraph()
        {
            int[] horizontalIndices = new int[dimension];
            int[] verticalIndices = new int[dimension];
            int[] diagonalIndices = new int[dimension];
            int diagonalCtr = 0, block = (int)Math.Sqrt(dimension);

            for (int i = 0; i < dimension; i++)
            {
                horizontalIndices[i] = i;
                verticalIndices[i] = i * dimension;
            }
            for (int i = 0; i < block; i++)
            {
                diagonalCtr = dimension * i;
                for (int j = 0; j < block; j++)
                {
                    diagonalIndices[i * block + j] = diagonalCtr++;
                }
            }
            int idx = 0;
            for (int i = 0; i < dimension * dimension; i++)
            {
                int r = graph.nodes[i].x;
                int c = graph.nodes[i].y;
                for (int j = 0; j < dimension; j++)
                {
                    idx = horizontalIndices[j] + r * dimension;
                    if (idx != i)
                    {
                        if (!graph.nodes[i].connectedNodes.Contains(idx))
                            graph.nodes[i].connectedNodes.Add(idx);
                        graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                    }
                    idx = verticalIndices[j] + c;
                    if (idx != i)
                    {
                        if (!graph.nodes[i].connectedNodes.Contains(idx))
                            graph.nodes[i].connectedNodes.Add(idx);
                        graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                    }
                    idx = diagonalIndices[j] + ((r / block) * block) * dimension + (c / block) * block;
                    if (idx != i)
                    {
                        if (!graph.nodes[i].connectedNodes.Contains(idx))
                            graph.nodes[i].connectedNodes.Add(idx);
                        graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                    }
                }
            }
            
            for (int i = 0; i < dimension*dimension; i++)
            {
                if (graph.nodes[i].colorValue == -1) continue;
                int[] differentNodes = graph.nodes.Where(n => n.colorValue != -1 && n.colorValue != graph.nodes[i].colorValue).GroupBy(n=>n.value).Select(g=>g.Key).ToArray();
                for (int j = 0; j < differentNodes.Length; j++)
                {
                    if (!graph.nodes[i].connectedNodes.Contains(differentNodes[j]))
                    {
                        graph.nodes[i].connectedNodes.Add(differentNodes[j]);
                        graph.nodes[differentNodes[j]].connectedNodes.Add(i);
                    }
                    graph.adjacencyMatrix[i][differentNodes[j]] = graph.adjacencyMatrix[differentNodes[j]][i] = true;
                }
                int[] similarNodes = graph.nodes.Where(n => n.value != graph.nodes[i].value && n.colorValue == graph.nodes[i].colorValue).GroupBy(n => n.value).Select(g => g.Key).ToArray();
                for (int j = 0; j < similarNodes.Length; j++)
                {
                    int r = graph.nodes[similarNodes[j]].x;
                    int c = graph.nodes[similarNodes[j]].y;
                    for (int k = 0; k < dimension; k++)
                    {
                        idx = horizontalIndices[k] + r * dimension;
                        if (idx != similarNodes[j])
                        {
                            if (!graph.nodes[i].connectedNodes.Contains(idx))
                            {
                                graph.nodes[i].connectedNodes.Add(idx);
                                graph.nodes[idx].connectedNodes.Add(i);
                            }
                            graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                        }
                        idx = verticalIndices[k] + c;
                        if (idx != similarNodes[j])
                        {
                            if (!graph.nodes[i].connectedNodes.Contains(idx))
                            {
                                graph.nodes[i].connectedNodes.Add(idx);
                                graph.nodes[idx].connectedNodes.Add(i);
                            }
                            graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                        }
                        idx = diagonalIndices[k] + ((r / block) * block) * dimension + (c / block) * block;
                        if (idx != similarNodes[j])
                        {
                            if (!graph.nodes[i].connectedNodes.Contains(idx))
                            {
                                graph.nodes[i].connectedNodes.Add(idx);
                                graph.nodes[idx].connectedNodes.Add(i);
                            }
                            graph.adjacencyMatrix[i][idx] = graph.adjacencyMatrix[idx][i] = true;
                        }
                    }
                }
            }
        }
    }
}
