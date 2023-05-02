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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dataForm));
            tempGraph = new ScottPlot.FormsPlot();
            wndspdGraph = new ScottPlot.FormsPlot();
            wtrlvlGraph = new ScottPlot.FormsPlot();
            humGraph = new ScottPlot.FormsPlot();
            listView1 = new ListView();
            label1 = new Label();
            label2 = new Label();
            warningInformation = new Label();
            warningImages = new ImageList(components);
            SuspendLayout();
            // 
            // tempGraph
            // 
            tempGraph.Location = new Point(65, 64);
            tempGraph.Margin = new Padding(6);
            tempGraph.Name = "tempGraph";
            tempGraph.Size = new Size(1170, 1152);
            tempGraph.TabIndex = 0;
            // 
            // wndspdGraph
            // 
            wndspdGraph.Location = new Point(65, 1344);
            wndspdGraph.Margin = new Padding(8, 6, 8, 6);
            wndspdGraph.Name = "wndspdGraph";
            wndspdGraph.Size = new Size(1170, 1152);
            wndspdGraph.TabIndex = 1;
            // 
            // wtrlvlGraph
            // 
            wtrlvlGraph.Location = new Point(1365, 1344);
            wtrlvlGraph.Margin = new Padding(8, 6, 8, 6);
            wtrlvlGraph.Name = "wtrlvlGraph";
            wtrlvlGraph.Size = new Size(1170, 1152);
            wtrlvlGraph.TabIndex = 2;
            // 
            // humGraph
            // 
            humGraph.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            humGraph.Location = new Point(1365, 64);
            humGraph.Margin = new Padding(8, 6, 8, 6);
            humGraph.MaximumSize = new Size(1170, 1152);
            humGraph.MinimumSize = new Size(300, 300);
            humGraph.Name = "humGraph";
            humGraph.Size = new Size(1170, 1152);
            humGraph.TabIndex = 3;
            // 
            // listView1
            // 
            listView1.Location = new Point(2620, 151);
            listView1.Name = "listView1";
            listView1.Size = new Size(561, 1024);
            listView1.SmallImageList = warningImages;
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Window;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(2620, 116);
            label1.Name = "label1";
            label1.Size = new Size(561, 36);
            label1.TabIndex = 5;
            label1.Text = "Warning List";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.Window;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(2620, 1245);
            label2.Name = "label2";
            label2.Size = new Size(561, 36);
            label2.TabIndex = 6;
            label2.Text = "Warning Information";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // warningInformation
            // 
            warningInformation.BackColor = SystemColors.Window;
            warningInformation.BorderStyle = BorderStyle.FixedSingle;
            warningInformation.Location = new Point(2620, 1281);
            warningInformation.Name = "warningInformation";
            warningInformation.Size = new Size(561, 579);
            warningInformation.TabIndex = 7;
            // 
            // warningImages
            // 
            warningImages.ColorDepth = ColorDepth.Depth16Bit;
            warningImages.ImageStream = (ImageListStreamer)resources.GetObject("warningImages.ImageStream");
            warningImages.TransparentColor = Color.Transparent;
            warningImages.Images.SetKeyName(0, "check_mark.png");
            warningImages.Images.SetKeyName(1, "red_x.png");
            warningImages.Images.SetKeyName(2, "warning_triangle.png");
            // 
            // dataForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(3221, 2119);
            Controls.Add(warningInformation);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(humGraph);
            Controls.Add(wtrlvlGraph);
            Controls.Add(wndspdGraph);
            Controls.Add(tempGraph);
            Name = "dataForm";
            Text = "Form2";
            Shown += dataForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.FormsPlot tempGraph;
        private ScottPlot.FormsPlot wndspdGraph;
        private ScottPlot.FormsPlot wtrlvlGraph;
        private ScottPlot.FormsPlot humGraph;
        private ListView listView1;
        private Label label1;
        private Label label2;
        private Label warningInformation;
        private ImageList warningImages;
    }
}