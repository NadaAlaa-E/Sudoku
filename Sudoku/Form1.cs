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
        bool isGreedy;
        public Form1()
        {
            isGreedy = true;
            InitializeComponent();
        }

        int[][] initialize()
        {
            int dimension_ = int.Parse(dimension.Text);
            int[][] initialValues = new int[dimension_][];
            for (int i = 0; i < dimension_; i++)
            {
                initialValues[i] = new int[dimension_];
                for (int j = 0; j < dimension_; j++)
                {
                    if (sudokuGrid.Rows[i].Cells[j].Value == null)
                        initialValues[i][j] = -1;
                    else
                        initialValues[i][j] = int.Parse(sudokuGrid.Rows[i].Cells[j].Value.ToString());
                }
            }
            return initialValues;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (dimension.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Dimension.");
                return;
            }

            panel1.Refresh();

            int[][] initialValues = initialize();

            sudoku = new Sudoku(Convert.ToInt32(dimension.Text), initialValues, isGreedy);
            if (sudoku.graph.isGraphValid())
            {
                GenerateGraph();
                GenerateGrid();
            }
            else
            {
                MessageBox.Show("Invalid Sudoku Initial Values.");
            }
        }

        void GenerateGrid()
        {
            if (sudoku == null) return;

            for (int i = 0; i < sudoku.dimension; i++)
            {
                for (int j = 0; j < sudoku.dimension; j++)
                {
                    sudokuGrid.Rows[i].Cells[j].Value = sudoku.graph.nodes[i * sudoku.dimension + j].colorValue.ToString();
                }
            }
        }

        void GenerateGraph()
        {
            if (sudoku == null) return;

            Graphics g = panel1.CreateGraphics();
            Font font = new Font("Arial", 15);
            float x = panel1.Width/2, y = panel1.Height/2, r = 25;
            int n = sudoku.dimension*sudoku.dimension, c = 230;
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
                g.DrawString((sudoku.graph.nodes[idx].colorValue).ToString(), font, Brushes.AntiqueWhite, posX-r, posY-r);
                idx++;
            }
            g.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dimension_TextChanged(object sender, EventArgs e)
        {
            if (dimension.Text.Length == 0) return;

            int dimension_ = int.Parse(dimension.Text);
            sudokuGrid.Rows.Clear();
            sudokuGrid.RowCount = dimension_ + 1;
            sudokuGrid.ColumnCount = dimension_;
            for (int i = 0; i < sudokuGrid.ColumnCount; i++)
            {
                sudokuGrid.Columns[i].Width = (sudokuGrid.Width / (sudokuGrid.ColumnCount + 3));
                sudokuGrid.Rows[i].Height = (sudokuGrid.Height / (sudokuGrid.RowCount + 3));
            }

            if (dimension_ == 9 && !isGreedy) setInitialValues(dimension_);
        }

        private void setInitialValues(int dimension_)
        {
            int[,] grid = {
                         { 5, 3, 0, 0, 7, 0, 9, 1, 2 },
                         { 6, 0, 0, 1, 9, 5, 3, 4, 0 },
                         { 0, 9, 0, 0, 0, 0, 0, 6, 0 },
                         { 8, 0, 9, 0, 6, 0, 0, 0, 3 },
                         { 4, 2, 0, 8, 0, 3, 0, 0, 1 },
                         { 7, 1, 0, 0, 2, 0, 0, 5, 6 },
                         { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                         { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                         { 3, 0, 5, 2, 8, 0, 0, 7, 9 } };
            for (int i = 0; i < dimension_; i++)
            {
                for (int j = 0; j < dimension_; j++)
                {
                    if (grid[i, j] != 0) sudokuGrid.Rows[i].Cells[j].Value = grid[i, j].ToString();
                }
            }
        }

        private void domBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (domBtn.Checked) isGreedy = false;
            else isGreedy = true;
            dimension_TextChanged(sender, e);
        }

        private void greedyBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (greedyBtn.Checked) isGreedy = true;
            else isGreedy = false;
            dimension_TextChanged(sender, e);
        }
    }
}
