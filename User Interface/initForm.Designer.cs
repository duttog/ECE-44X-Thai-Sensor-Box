namespace test_project
{
    partial class initForm
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
            beginDataCaptureButton = new Button();
            displayEnvDataButton = new Button();
            splitter1 = new Splitter();
            imageList1 = new ImageList(components);
            backgroundDataImage = new PictureBox();
            dataDisplayImage = new PictureBox();
            logoImage = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)backgroundDataImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataDisplayImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logoImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // beginDataCaptureButton
            // 
            beginDataCaptureButton.Location = new Point(150, 100);
            beginDataCaptureButton.Name = "beginDataCaptureButton";
            beginDataCaptureButton.Size = new Size(200, 23);
            beginDataCaptureButton.TabIndex = 0;
            beginDataCaptureButton.Text = "Begin Background Data Capture";
            beginDataCaptureButton.UseVisualStyleBackColor = true;
            beginDataCaptureButton.Click += beginDataCaptureButton_Click;
            // 
            // displayEnvDataButton
            // 
            displayEnvDataButton.Location = new Point(650, 100);
            displayEnvDataButton.Name = "displayEnvDataButton";
            displayEnvDataButton.Size = new Size(200, 23);
            displayEnvDataButton.TabIndex = 1;
            displayEnvDataButton.Text = "Display Environmental Data";
            displayEnvDataButton.UseVisualStyleBackColor = true;
            displayEnvDataButton.Click += displayEnvDataButton_Click;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(500, 461);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // backgroundDataImage
            // 
            backgroundDataImage.Location = new Point(100, 150);
            backgroundDataImage.Name = "backgroundDataImage";
            backgroundDataImage.Size = new Size(300, 150);
            backgroundDataImage.TabIndex = 3;
            backgroundDataImage.TabStop = false;
            // 
            // dataDisplayImage
            // 
            dataDisplayImage.Location = new Point(600, 150);
            dataDisplayImage.Name = "dataDisplayImage";
            dataDisplayImage.Size = new Size(300, 150);
            dataDisplayImage.TabIndex = 4;
            dataDisplayImage.TabStop = false;
            // 
            // logoImage
            // 
            logoImage.Location = new Point(380, 340);
            logoImage.Name = "logoImage";
            logoImage.Size = new Size(96, 96);
            logoImage.TabIndex = 5;
            logoImage.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(520, 340);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(96, 96);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // initForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(pictureBox1);
            Controls.Add(logoImage);
            Controls.Add(dataDisplayImage);
            Controls.Add(backgroundDataImage);
            Controls.Add(beginDataCaptureButton);
            Controls.Add(splitter1);
            Controls.Add(displayEnvDataButton);
            Name = "initForm";
            Text = "initForm";
            Load += initForm_Load;
            ((System.ComponentModel.ISupportInitialize)backgroundDataImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataDisplayImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)logoImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button beginDataCaptureButton;
        private Button displayEnvDataButton;
        private Splitter splitter1;
        private ImageList imageList1;
        private PictureBox backgroundDataImage;
        private PictureBox dataDisplayImage;
        private PictureBox logoImage;
        private PictureBox pictureBox1;
    }
}