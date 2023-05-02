namespace test_project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            backButton = new Button();
            listView1 = new ListView();
            goButton = new Button();
            listView = new ListView();
            iconList = new ImageList(components);
            filePathTextBox = new TextBox();
            fileNameTB = new Label();
            fileNameLabel = new Label();
            fileTypeTB = new Label();
            fileTypeLabel = new Label();
            errorBox = new TextBox();
            selectFileButton = new Button();
            cancelFileButton = new Button();
            SuspendLayout();
            // 
            // backButton
            // 
            backButton.Location = new Point(9, 10);
            backButton.Margin = new Padding(1, 3, 1, 3);
            backButton.Name = "backButton";
            backButton.Size = new Size(186, 54);
            backButton.TabIndex = 0;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(290, 143);
            listView1.Margin = new Padding(1, 3, 1, 3);
            listView1.Name = "listView1";
            listView1.Size = new Size(5, 7);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // goButton
            // 
            goButton.Location = new Point(1430, 10);
            goButton.Margin = new Padding(1, 3, 1, 3);
            goButton.Name = "goButton";
            goButton.Size = new Size(186, 54);
            goButton.TabIndex = 2;
            goButton.Text = "Go";
            goButton.UseVisualStyleBackColor = true;
            goButton.Click += goButton_Click;
            // 
            // listView
            // 
            listView.LargeImageList = iconList;
            listView.Location = new Point(12, 86);
            listView.Margin = new Padding(1, 3, 1, 3);
            listView.Name = "listView";
            listView.Size = new Size(1603, 657);
            listView.SmallImageList = iconList;
            listView.TabIndex = 3;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Tile;
            listView.ItemSelectionChanged += listView_ItemSelectionChanged;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // 
            // iconList
            // 
            iconList.ColorDepth = ColorDepth.Depth24Bit;
            iconList.ImageStream = (ImageListStreamer)resources.GetObject("iconList.ImageStream");
            iconList.TransparentColor = Color.Transparent;
            iconList.Images.SetKeyName(0, "folder.png");
            iconList.Images.SetKeyName(1, "check_mark.png");
            iconList.Images.SetKeyName(2, "red_x.png");
            // 
            // filePathTextBox
            // 
            filePathTextBox.Location = new Point(213, 10);
            filePathTextBox.Margin = new Padding(1, 3, 1, 3);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.Size = new Size(1186, 39);
            filePathTextBox.TabIndex = 4;
            // 
            // fileNameTB
            // 
            fileNameTB.AutoSize = true;
            fileNameTB.Location = new Point(25, 774);
            fileNameTB.Margin = new Padding(1, 0, 1, 0);
            fileNameTB.Name = "fileNameTB";
            fileNameTB.Size = new Size(115, 32);
            fileNameTB.TabIndex = 5;
            fileNameTB.Text = "FileName";
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new Point(135, 774);
            fileNameLabel.Margin = new Padding(1, 0, 1, 0);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new Size(72, 32);
            fileNameLabel.TabIndex = 6;
            fileNameLabel.Text = "blank";
            // 
            // fileTypeTB
            // 
            fileTypeTB.AutoSize = true;
            fileTypeTB.Location = new Point(25, 838);
            fileTypeTB.Margin = new Padding(1, 0, 1, 0);
            fileTypeTB.Name = "fileTypeTB";
            fileTypeTB.Size = new Size(102, 32);
            fileTypeTB.TabIndex = 7;
            fileTypeTB.Text = "FileType";
            // 
            // fileTypeLabel
            // 
            fileTypeLabel.AutoSize = true;
            fileTypeLabel.Location = new Point(135, 838);
            fileTypeLabel.Margin = new Padding(1, 0, 1, 0);
            fileTypeLabel.Name = "fileTypeLabel";
            fileTypeLabel.Size = new Size(72, 32);
            fileTypeLabel.TabIndex = 8;
            fileTypeLabel.Text = "blank";
            // 
            // errorBox
            // 
            errorBox.Location = new Point(428, 774);
            errorBox.Margin = new Padding(1, 3, 1, 3);
            errorBox.Multiline = true;
            errorBox.Name = "errorBox";
            errorBox.Size = new Size(879, 91);
            errorBox.TabIndex = 9;
            errorBox.Text = "General Message Text Box";
            // 
            // selectFileButton
            // 
            selectFileButton.Location = new Point(1338, 774);
            selectFileButton.Margin = new Padding(5, 6, 5, 6);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(269, 49);
            selectFileButton.TabIndex = 10;
            selectFileButton.Text = "Select File";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += selectFileButton_Click;
            // 
            // cancelFileButton
            // 
            cancelFileButton.Location = new Point(1338, 822);
            cancelFileButton.Margin = new Padding(5, 6, 5, 6);
            cancelFileButton.Name = "cancelFileButton";
            cancelFileButton.Size = new Size(269, 49);
            cancelFileButton.TabIndex = 11;
            cancelFileButton.Text = "Cancel";
            cancelFileButton.UseVisualStyleBackColor = true;
            cancelFileButton.Click += cancelFileButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1629, 899);
            Controls.Add(cancelFileButton);
            Controls.Add(selectFileButton);
            Controls.Add(errorBox);
            Controls.Add(fileTypeLabel);
            Controls.Add(fileTypeTB);
            Controls.Add(fileNameLabel);
            Controls.Add(fileNameTB);
            Controls.Add(filePathTextBox);
            Controls.Add(listView);
            Controls.Add(goButton);
            Controls.Add(listView1);
            Controls.Add(backButton);
            Margin = new Padding(1, 3, 1, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button backButton;
        private ListView listView1;
        private Button goButton;
        private ListView listView;
        private TextBox filePathTextBox;
        private Label fileNameTB;
        private Label fileNameLabel;
        private Label fileTypeTB;
        private Label fileTypeLabel;
        private ImageList iconList;
        private TextBox errorBox;
        private Button selectFileButton;
        private Button cancelFileButton;
    }
}