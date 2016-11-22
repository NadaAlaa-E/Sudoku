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
        public Graph(int N)
        {
            int x = 0, y = 0;
            nodes = new List<Node>(N*N);
            adjacencyMatrix = new bool[N*N][];
            parent = new int[N*N];
            for (int i = 0; i < N*N; i++)
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
        int getCoverageOfNode(int nodeIdx, List<int> currentNodes)
        {
            int coverageNodeIdx = nodeIdx;
            int max_ConnectedNodes = 0;
            for (int i = 0; i < currentNodes.Count; i++)
            {
                if (adjacencyMatrix[nodeIdx][currentNodes[i]] || currentNodes[i] == nodeIdx) continue;

                int num_ConnectedNodes = 0;
                int commonNodes = 0, currentConnectedNodes = 0;
                for (int j = 0; j < currentNodes.Count; j++)
                {
                    if (adjacencyMatrix[currentNodes[i]][currentNodes[j]] && adjacencyMatrix[nodeIdx][currentNodes[j]]) commonNodes++;
                    if (adjacencyMatrix[nodeIdx][currentNodes[j]]) num_ConnectedNodes++;
                    if (adjacencyMatrix[currentNodes[i]][currentNodes[j]]) currentConnectedNodes++;
                }
                if (commonNodes == num_ConnectedNodes && currentConnectedNodes >= max_ConnectedNodes)
                {
                    max_ConnectedNodes = currentConnectedNodes;
                    coverageNodeIdx = currentNodes[i];
                }
            }
            return coverageNodeIdx;
        }
        bool isCompleteGraph(List<int> currentNodes)
        {
            int num_AllConnection = (currentNodes.Count*(currentNodes.Count-1));
            for (int i = 0; i < currentNodes.Count; i++)
            {
                for (int j = 0; j < currentNodes.Count; j++)
                {
                    if (adjacencyMatrix[currentNodes[i]][currentNodes[j]]) num_AllConnection--;
                }
            }
            return (num_AllConnection == 0);
        }
        public void ColorNode(int i)
        {
            int N = nodes.Count;
            bool[] vis = new bool[N];
            int j = 0;
            for (j = 0; j < nodes[i].connectedNodes.Count; j++)
            {
                if (nodes[nodes[i].connectedNodes[j]].colorValue != -1)
                    vis[nodes[nodes[i].connectedNodes[j]].colorValue] = true;
            }
            j = 0;
            while (vis[j]) j++;
            nodes[i].colorValue = j;
        }
        public void ColorGraph()
        {
            Color[] colors = new Color[nodes.Count];
            Random rand = new Random();
            for (int i = 0; i < nodes.Count; i++)
            {
                colors[i] = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].colorValue == -1)
                    ColorNode(i);
                nodes[i].color = colors[nodes[i].colorValue];
            }
        }
        public void ApplyGraphColoring()
        {
            bool[] vis = new bool[nodes.Count];
            List<int> currentNodes = new List<int>();
            int N = nodes.Count;
            for (int i = 0; i < nodes.Count; i++)
            {
                parent[i] = i;
                currentNodes.Add(i);
            }
            for (int i = 0; i < currentNodes.Count; i++)
            {
                if (isCompleteGraph(currentNodes))
                {
                    break;
                }
                int coverage = getCoverageOfNode(currentNodes[i], currentNodes);
                if (coverage != currentNodes[i])
                {
                    vis[currentNodes[i]] = true;
                    parent[currentNodes[i]] = coverage;
                    currentNodes.RemoveAt(i);
                    i--;
                }
                else if (i == currentNodes.Count - 1)
                {
                    int j = 0;
                    while (j < N && vis[j]) j++;
                    ColorNode(j);
                    vis[j] = true;
                    currentNodes.Remove(j);
                    i = -1;
                }
            }
            ColorGraph();
        }
    }
}
