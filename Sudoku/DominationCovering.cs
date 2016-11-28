using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Sudoku
{
    class DominationCovering
    {
        static int getCoverageOfNode(int nodeIdx, List<int> currentNodes, bool[][] adjacencyMatrix)
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
        static bool isCompleteGraph(List<int> currentNodes, bool[][] adjacencyMatrix)
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
        static void ColorNode(int i, List<Node> nodes)
        {
            int N = nodes.Count;
            bool[] vis = new bool[N + 1];
            int j = 0;
            for (j = 0; j < nodes[i].connectedNodes.Count; j++)
            {
                if (nodes[nodes[i].connectedNodes[j]].colorValue != -1)
                    vis[nodes[nodes[i].connectedNodes[j]].colorValue] = true;
            }
            j = 1;
            while (vis[j]) j++;
            nodes[i].colorValue = j;
        }
        static void ColorGraph(List<Node> nodes)
        {
            Color[] colors = new Color[nodes.Count + 1];
            Random rand = new Random();
            for (int i = 1; i <= nodes.Count; i++)
            {
                colors[i] = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].colorValue == -1)
                    ColorNode(i, nodes);
                nodes[i].color = colors[nodes[i].colorValue];
            }
        }
        static int GetDegree(int i, List<int> currentNodes, bool[][] adjacencyMatrix)
        {
            int degree = 0;
            for (int j = 0; j < currentNodes.Count; j++)
            {
                if(adjacencyMatrix[i][currentNodes[j]]) degree++;
            }
            return degree;
        }
        static public void ApplyGraphColoring(List<Node> nodes, bool[][] adjacencyMatrix)
        {
            bool[] vis = new bool[nodes.Count];
            List<int> currentNodes = new List<int>();
            int N = nodes.Count;
            for (int i = 0; i < nodes.Count; i++)
            {
                //parent[i] = i;
                currentNodes.Add(i);
            }
            for (int i = 0; i < currentNodes.Count; i++)
            {
                if (isCompleteGraph(currentNodes, adjacencyMatrix))
                {
                    break;
                }
                int coverage = getCoverageOfNode(currentNodes[i], currentNodes, adjacencyMatrix);
                if (coverage != currentNodes[i])
                {
                    vis[currentNodes[i]] = true;
                    //parent[currentNodes[i]] = coverage;
                    currentNodes.RemoveAt(i);
                    i--;
                }
                else if (i == currentNodes.Count - 1)
                {
                    int j = 0, maxDegree = 0, maxIdx = 0;
                    for (; j < currentNodes.Count; j++)
                    {
                        int degree = GetDegree(currentNodes[j], currentNodes, adjacencyMatrix);
                        if (degree > maxDegree)
                        {
                            maxDegree = degree;
                            maxIdx = j;
                        }
                    }
                    ColorNode(maxIdx, nodes);
                    vis[maxIdx] = true;
                    currentNodes.RemoveAt(maxIdx);
                    i = -1;
                }
            }
            ColorGraph(nodes);
        }
    }
}
