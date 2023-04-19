namespace test_project
{
    partial class dataForm
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
            tempGraph = new ScottPlot.FormsPlot();
            wndspdGraph = new ScottPlot.FormsPlot();
            wtrlvlGraph = new ScottPlot.FormsPlot();
            humGraph = new ScottPlot.FormsPlot();
            SuspendLayout();
            // 
            // tempGraph
            // 
            tempGraph.Location = new Point(50, 50);
            tempGraph.Margin = new Padding(5);
            tempGraph.Name = "tempGraph";
            tempGraph.Size = new Size(900, 900);
            tempGraph.TabIndex = 0;
            tempGraph.Load += tempGraph_Load;
            // 
            // wndspdGraph
            // 
            wndspdGraph.Location = new Point(50, 1050);
            wndspdGraph.Margin = new Padding(6, 5, 6, 5);
            wndspdGraph.Name = "wndspdGraph";
            wndspdGraph.Size = new Size(900, 900);
            wndspdGraph.TabIndex = 1;
            wndspdGraph.Load += wndspdGraph_Load;
            // 
            // wtrlvlGraph
            // 
            wtrlvlGraph.Location = new Point(1050, 1050);
            wtrlvlGraph.Margin = new Padding(6, 5, 6, 5);
            wtrlvlGraph.Name = "wtrlvlGraph";
            wtrlvlGraph.Size = new Size(900, 900);
            wtrlvlGraph.TabIndex = 2;
            wtrlvlGraph.Load += wtrlvlGraph_Load;
            // 
            // humGraph
            // 
            humGraph.Location = new Point(1050, 50);
            humGraph.Margin = new Padding(6, 5, 6, 5);
            humGraph.Name = "humGraph";
            humGraph.Size = new Size(900, 900);
            humGraph.TabIndex = 3;
            humGraph.Load += humGraph_Load;
            // 
            // dataForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2478, 1944);
            Controls.Add(humGraph);
            Controls.Add(wtrlvlGraph);
            Controls.Add(wndspdGraph);
            Controls.Add(tempGraph);
            Margin = new Padding(2);
            Name = "dataForm";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.FormsPlot tempGraph;
        private ScottPlot.FormsPlot wndspdGraph;
        private ScottPlot.FormsPlot wtrlvlGraph;
        private ScottPlot.FormsPlot humGraph;
    }
}