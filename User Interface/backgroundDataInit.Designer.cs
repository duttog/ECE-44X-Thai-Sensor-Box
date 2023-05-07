namespace test_project
{
    partial class backgroundDataInit
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
            fileTitleBox = new TextBox();
            fileNameButton = new Button();
            usbImageList = new ImageList(components);
            comLabel = new Label();
            fileNameLabel = new Label();
            fileErrorLabel = new Label();
            comPortList = new ListView();
            SuspendLayout();
            // 
            // fileTitleBox
            // 
            fileTitleBox.Location = new Point(50, 53);
            fileTitleBox.Name = "fileTitleBox";
            fileTitleBox.Size = new Size(520, 23);
            fileTitleBox.TabIndex = 0;
            // 
            // fileNameButton
            // 
            fileNameButton.Location = new Point(597, 53);
            fileNameButton.Name = "fileNameButton";
            fileNameButton.Size = new Size(137, 23);
            fileNameButton.TabIndex = 1;
            fileNameButton.Text = "Select";
            fileNameButton.UseVisualStyleBackColor = true;
            fileNameButton.MouseClick += fileNameButton_MouseClick;
            // 
            // usbImageList
            // 
            usbImageList.ColorDepth = ColorDepth.Depth8Bit;
            usbImageList.ImageSize = new Size(96, 96);
            usbImageList.TransparentColor = Color.Transparent;
            // 
            // comLabel
            // 
            comLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            comLabel.Location = new Point(50, 127);
            comLabel.Name = "comLabel";
            comLabel.Size = new Size(684, 28);
            comLabel.TabIndex = 3;
            comLabel.Text = "Available COM Ports";
            comLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // fileNameLabel
            // 
            fileNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fileNameLabel.Location = new Point(50, 27);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new Size(520, 23);
            fileNameLabel.TabIndex = 4;
            fileNameLabel.Text = "File Title";
            fileNameLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // fileErrorLabel
            // 
            fileErrorLabel.Location = new Point(50, 79);
            fileErrorLabel.Name = "fileErrorLabel";
            fileErrorLabel.Size = new Size(520, 48);
            fileErrorLabel.TabIndex = 5;
            fileErrorLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // comPortList
            // 
            comPortList.Location = new Point(50, 158);
            comPortList.Name = "comPortList";
            comPortList.Size = new Size(684, 254);
            comPortList.SmallImageList = usbImageList;
            comPortList.TabIndex = 6;
            comPortList.UseCompatibleStateImageBehavior = false;
            comPortList.ItemSelectionChanged += listView1_ItemSelectionChanged;
            comPortList.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // backgroundDataInit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comPortList);
            Controls.Add(fileErrorLabel);
            Controls.Add(fileNameLabel);
            Controls.Add(comLabel);
            Controls.Add(fileNameButton);
            Controls.Add(fileTitleBox);
            Name = "backgroundDataInit";
            Text = "backgroundDataInit";
            Shown += backgroundDataInit_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox fileTitleBox;
        private Button fileNameButton;
        private ImageList usbImageList;
        private Label comLabel;
        private Label fileNameLabel;
        private Label fileErrorLabel;
        private ListView comPortList;
    }
}