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
    public partial class Form1 : Form
    {
        Sudoku sudoku;
        public Form1()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku(Convert.ToInt32(dimension.Text));
            panel1.Refresh();
            panel2.Refresh();
            GenerateGraph();
            GenerateGrid();
        }

        void GenerateGrid()
        {
            if (sudoku == null) return;

            Graphics g = panel2.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 1);
            Font font = new Font("Arial", 20);
            int lines = Convert.ToInt32(dimension.Text), N = lines*lines;
            float x = 0.0f, y = 0.0f, xSpace = panel2.Width/lines, ySpace = panel2.Height/lines;

            //Vertical Lines
            for (int i = 0; i < lines + 1; i++)
            {
                g.DrawLine(pen, x, y, x, panel2.Height);
                x += xSpace;
            }          
            //Horizontal Lines
            x = 0.0f;
            for (int i = 0; i < lines + 1; i++)
            {
                g.DrawLine(pen, x, y, panel2.Width, y);
                y += ySpace;
            }
            for (int i = 0; i < N; i++)
            {
                x = sudoku.graph.nodes[i].x; 
                y = sudoku.graph.nodes[i].y;
                g.DrawString((sudoku.graph.nodes[i].colorValue+1).ToString(), font, Brushes.Black, x*xSpace, y*ySpace);
            }
        }

        void GenerateGraph()
        {
            if (sudoku == null) return;

            Graphics g = panel1.CreateGraphics();
            Font font = new Font("Arial", 15);
            float x = panel1.Width/2, y = panel1.Height/2, r = 30;
            int n = sudoku.dimension*sudoku.dimension, c = 10*n;
            int idx = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (sudoku.graph.adjacencyMatrix[i][j])
                    {
                        Pen pen = new Pen(Color.Black);
                        float posX1 = (float)(x + c * Math.Cos(2 * Math.PI * i / n) - r/2);
                        float posY1 = (float)(y + c * Math.Sin(2 * Math.PI * i / n) - r/2);
                        float posX2 = (float)(x + c * Math.Cos(2 * Math.PI * j / n) - r/2);
                        float posY2 = (float)(y + c * Math.Sin(2 * Math.PI * j / n) - r/2);
                        g.DrawLine(pen, posX1, posY1, posX2, posY2);
                    }
                }
            }
            while (idx < sudoku.dimension * sudoku.dimension)
            {
                SolidBrush s = new SolidBrush(sudoku.graph.nodes[idx].color);
                float posX = (float)(x + c * Math.Cos(2 * Math.PI * idx / n));
                float posY = (float)(y + c * Math.Sin(2 * Math.PI * idx / n));
                g.FillEllipse(s, posX-r, posY-r, r, r);
                g.DrawString((sudoku.graph.nodes[idx].colorValue + 1).ToString(), font, Brushes.AntiqueWhite, posX-r, posY-r);
                idx++;
            }
            g.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
