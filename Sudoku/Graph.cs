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
    class Graph
    {
        public List<Node> nodes;
        public bool[][] adjacencyMatrix;
        public int[] parent;
        int N;
        public Graph(int N)
        {
            this.N = N;
            int x = 0, y = 0;
            nodes = new List<Node>(N * N);
            adjacencyMatrix = new bool[N * N][];
            parent = new int[N * N];
            for (int i = 0; i < N * N; i++)
            {
                Node node = new Node(i);
                node.x = x;
                node.y = y++;

                parent[i] = i;
                adjacencyMatrix[i] = new bool[N * N];
                node.connectedNodes = new List<int>();
                nodes.Add(node);
                if (y == N)
                {
                    y = 0; x++;
                }
            }
        }
        public void ColorGraph(List<Tuple<int, int>> coloredNodes)
        {
            Color[] colors = new Color[nodes.Count + 1];
            Random rand = new Random();
            for (int i = 1; i <= nodes.Count; i++)
            {
                colors[i] = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            }
            var grouping = coloredNodes.GroupBy(v => v.Item2);
            bool[] usedColors = new bool[nodes.Count + 1];
            foreach (var group in grouping)
            {
                var assignedDigit = group.FirstOrDefault(cell => nodes[cell.Item1].colorValue != -1);
                if (assignedDigit != null)
                {
                    foreach (var graphColoringResult in group)
                    {
                        nodes[graphColoringResult.Item1].colorValue = nodes[assignedDigit.Item1].colorValue;
                        nodes[graphColoringResult.Item1].color = colors[nodes[assignedDigit.Item1].colorValue];
                        usedColors[nodes[assignedDigit.Item1].colorValue] = true;
                    }
                }
            }
            int idx = 1;
            grouping = coloredNodes.GroupBy(v => v.Item2);

            foreach (var group in grouping)
            {
                var assignedDigit = group.FirstOrDefault(cell => nodes[cell.Item1].colorValue != -1);
                if (assignedDigit == null)
                {
                    while (usedColors[idx]) idx++;
                    foreach (var graphColoringResult in group)
                    {
                        nodes[graphColoringResult.Item1].colorValue = idx;
                        nodes[graphColoringResult.Item1].color = colors[idx];
                        usedColors[idx] = true;
                    }
                }
            }
        }
        int SaturatedDegree(int i, List<Tuple<int, int>> currentNodes)
        {
            return currentNodes.Where(n => nodes[i].connectedNodes.Contains(n.Item1)).GroupBy(n => n.Item2).Count();
        }
        int Degree(int i)
        {
            return nodes[i].connectedNodes.Count;
        }
        void ColorNode(int i, List<Tuple<int, int>> currentNodes, ref int colorNumber)
        {
            List<int> colors = currentNodes.Where(n => nodes[i].connectedNodes.Contains(n.Item1)).GroupBy(n => n.Item2).Select(g => g.Key).ToList();

            if (colors.Count == colorNumber)
            {
                colorNumber++;
                currentNodes.Add(new Tuple<int, int>(i, colorNumber));
            }
            else
            {
                int[] usedColors = Enumerable.Range(1, colorNumber).ToArray();
                int colorNum = usedColors.Where(c => !colors.Contains(c)).First();
                currentNodes.Add(new Tuple<int, int>(i, colorNum));
            }
        }
        bool isColored(int i, List<Tuple<int, int>> currentNodes)
        {
            return currentNodes.Any(n => n.Item1 == i);
        }
        public void ApplyGraphColoring()
        {
            int colorNumber = 1;
            int num_ColoredNodes = 0;
            List<Tuple<int, int>> currentNodes = new List<Tuple<int, int>>();

            while (num_ColoredNodes < nodes.Count)
            {
                int maxDegree = -1, idx = -1;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (!isColored(i, currentNodes))
                    {
                        int d = SaturatedDegree(i, currentNodes);
                        if (d > maxDegree || d == maxDegree && Degree(i) > Degree(idx))
                        {
                            maxDegree = d;
                            idx = i;
                        }
                    }
                }
                ColorNode(idx, currentNodes, ref colorNumber);
                num_ColoredNodes++;
            }
            ColorGraph(currentNodes);
        }
        public bool isGraphValid()
        {
            int diagonalCtr = 0, block = (int)Math.Sqrt(N);
            int[] diagonalIndices = new int[N];
            for (int i = 0; i < block; i++)
            {
                diagonalCtr = N * i;
                for (int j = 0; j < block; j++)
                {
                    diagonalIndices[i * block + j] = diagonalCtr++;
                }
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].connectedNodes.Count; j++)
                {
                    if (nodes[i].colorValue == nodes[nodes[i].connectedNodes[j]].colorValue) return false;
                }
                for (int j = 0; j < N; j++)
                {
                    int idx = diagonalIndices[j] + ((nodes[i].x / block) * block) * N + (nodes[i].y / block) * block;
                    if (idx != i && nodes[idx].colorValue == nodes[i].colorValue) return false;
                }
            }
            return true;
        }
    }
}
