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
            warningListIcon = new ListView();
            warningImages = new ImageList(components);
            label1 = new Label();
            label2 = new Label();
            warningInformation = new Label();
            backgroundDataButton = new Button();
            newFileButton = new Button();
            refreshButton = new Button();
            SuspendLayout();
            // 
            // tempGraph
            // 
            tempGraph.Location = new Point(35, 30);
            tempGraph.Margin = new Padding(4, 3, 4, 3);
            tempGraph.Name = "tempGraph";
            tempGraph.Size = new Size(630, 540);
            tempGraph.TabIndex = 0;
            // 
            // wndspdGraph
            // 
            wndspdGraph.Location = new Point(35, 630);
            wndspdGraph.Margin = new Padding(4, 3, 4, 3);
            wndspdGraph.Name = "wndspdGraph";
            wndspdGraph.Padding = new Padding(0, 0, 0, 10);
            wndspdGraph.Size = new Size(630, 540);
            wndspdGraph.TabIndex = 1;
            // 
            // wtrlvlGraph
            // 
            wtrlvlGraph.Location = new Point(735, 630);
            wtrlvlGraph.Margin = new Padding(4, 3, 4, 3);
            wtrlvlGraph.Name = "wtrlvlGraph";
            wtrlvlGraph.Size = new Size(630, 540);
            wtrlvlGraph.TabIndex = 2;
            // 
            // humGraph
            // 
            humGraph.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            humGraph.Location = new Point(735, 30);
            humGraph.Margin = new Padding(4, 3, 4, 3);
            humGraph.MaximumSize = new Size(630, 540);
            humGraph.MinimumSize = new Size(162, 141);
            humGraph.Name = "humGraph";
            humGraph.Size = new Size(630, 540);
            humGraph.TabIndex = 3;
            // 
            // warningListIcon
            // 
            warningListIcon.Location = new Point(1395, 70);
            warningListIcon.Margin = new Padding(2, 1, 2, 1);
            warningListIcon.Name = "warningListIcon";
            warningListIcon.Size = new Size(500, 482);
            warningListIcon.SmallImageList = warningImages;
            warningListIcon.TabIndex = 4;
            warningListIcon.UseCompatibleStateImageBehavior = false;
            warningListIcon.View = View.SmallIcon;
            warningListIcon.ItemSelectionChanged += warningListIcon_ItemSelectionChanged;
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
            // label1
            // 
            label1.BackColor = Color.Yellow;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(1395, 53);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(500, 18);
            label1.TabIndex = 5;
            label1.Text = "Warning List";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.DeepSkyBlue;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(1395, 581);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 0, 15, 0);
            label2.Size = new Size(500, 18);
            label2.TabIndex = 6;
            label2.Text = "Warning Information";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // warningInformation
            // 
            warningInformation.BackColor = SystemColors.Window;
            warningInformation.BorderStyle = BorderStyle.FixedSingle;
            warningInformation.Location = new Point(1395, 597);
            warningInformation.Margin = new Padding(2, 50, 50, 0);
            warningInformation.Name = "warningInformation";
            warningInformation.Padding = new Padding(0, 0, 100, 0);
            warningInformation.Size = new Size(500, 320);
            warningInformation.TabIndex = 7;
            // 
            // backgroundDataButton
            // 
            backgroundDataButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            backgroundDataButton.Location = new Point(1395, 1076);
            backgroundDataButton.Name = "backgroundDataButton";
            backgroundDataButton.Size = new Size(500, 50);
            backgroundDataButton.TabIndex = 8;
            backgroundDataButton.Text = "Begin Background Data Reading";
            backgroundDataButton.UseVisualStyleBackColor = true;
            backgroundDataButton.Click += backgroundDataButton_Click;
            // 
            // newFileButton
            // 
            newFileButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            newFileButton.Location = new Point(1395, 1007);
            newFileButton.Name = "newFileButton";
            newFileButton.Size = new Size(500, 50);
            newFileButton.TabIndex = 9;
            newFileButton.Text = "Choose New File";
            newFileButton.UseVisualStyleBackColor = true;
            newFileButton.Click += newFileButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            refreshButton.Location = new Point(1395, 936);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(500, 50);
            refreshButton.TabIndex = 10;
            refreshButton.Text = "Refresh Data";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // dataForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1930, 1199);
            Controls.Add(refreshButton);
            Controls.Add(newFileButton);
            Controls.Add(backgroundDataButton);
            Controls.Add(warningInformation);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(warningListIcon);
            Controls.Add(humGraph);
            Controls.Add(wtrlvlGraph);
            Controls.Add(wndspdGraph);
            Controls.Add(tempGraph);
            Margin = new Padding(2, 1, 2, 1);
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
        private ListView warningListIcon;
        private Label label1;
        private Label label2;
        private Label warningInformation;
        private ImageList warningImages;
        private Button backgroundDataButton;
        private Button newFileButton;
        private Button refreshButton;
    }
}