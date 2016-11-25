namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dimension = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Generate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sudokuGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.sudokuGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dimension
            // 
            this.dimension.Location = new System.Drawing.Point(251, 21);
            this.dimension.Name = "dimension";
            this.dimension.Size = new System.Drawing.Size(168, 31);
            this.dimension.TabIndex = 0;
            this.dimension.TextChanged += new System.EventHandler(this.dimension_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Grid\'s Dimension";
            // 
            // Generate
            // 
            this.Generate.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Generate.Location = new System.Drawing.Point(251, 80);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(168, 42);
            this.Generate.TabIndex = 2;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(883, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1383, 1111);
            this.panel1.TabIndex = 3;
            // 
            // sudokuGrid
            // 
            this.sudokuGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sudokuGrid.Location = new System.Drawing.Point(11, 296);
            this.sudokuGrid.Name = "sudokuGrid";
            this.sudokuGrid.RowTemplate.Height = 33;
            this.sudokuGrid.Size = new System.Drawing.Size(858, 786);
            this.sudokuGrid.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2278, 1135);
            this.Controls.Add(this.sudokuGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dimension);
            this.Name = "Form1";
            this.Text = "Sudoku";
            ((System.ComponentModel.ISupportInitialize)(this.sudokuGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dimension;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView sudokuGrid;
    }
}

