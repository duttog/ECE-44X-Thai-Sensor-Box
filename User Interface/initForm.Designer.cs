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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(initForm));
            beginDataCaptureButton = new Button();
            displayEnvDataButton = new Button();
            imageList1 = new ImageList(components);
            backgroundDataImage = new PictureBox();
            dataDisplayImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)backgroundDataImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataDisplayImage).BeginInit();
            SuspendLayout();
            // 
            // beginDataCaptureButton
            // 
            beginDataCaptureButton.Location = new Point(225, 25);
            beginDataCaptureButton.Name = "beginDataCaptureButton";
            beginDataCaptureButton.Size = new Size(200, 23);
            beginDataCaptureButton.TabIndex = 0;
            beginDataCaptureButton.Text = "Begin Background Data Capture";
            beginDataCaptureButton.UseVisualStyleBackColor = true;
            beginDataCaptureButton.Click += beginDataCaptureButton_Click;
            // 
            // displayEnvDataButton
            // 
            displayEnvDataButton.Location = new Point(850, 25);
            displayEnvDataButton.Name = "displayEnvDataButton";
            displayEnvDataButton.Size = new Size(200, 23);
            displayEnvDataButton.TabIndex = 1;
            displayEnvDataButton.Text = "Display Environmental Data";
            displayEnvDataButton.UseVisualStyleBackColor = true;
            displayEnvDataButton.Click += displayEnvDataButton_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // backgroundDataImage
            // 
            backgroundDataImage.BorderStyle = BorderStyle.FixedSingle;
            backgroundDataImage.Image = (Image)resources.GetObject("backgroundDataImage.Image");
            backgroundDataImage.Location = new Point(25, 80);
            backgroundDataImage.Name = "backgroundDataImage";
            backgroundDataImage.Size = new Size(611, 294);
            backgroundDataImage.SizeMode = PictureBoxSizeMode.AutoSize;
            backgroundDataImage.TabIndex = 3;
            backgroundDataImage.TabStop = false;
            // 
            // dataDisplayImage
            // 
            dataDisplayImage.BorderStyle = BorderStyle.FixedSingle;
            dataDisplayImage.Image = (Image)resources.GetObject("dataDisplayImage.Image");
            dataDisplayImage.Location = new Point(700, 80);
            dataDisplayImage.Name = "dataDisplayImage";
            dataDisplayImage.Size = new Size(465, 281);
            dataDisplayImage.SizeMode = PictureBoxSizeMode.AutoSize;
            dataDisplayImage.TabIndex = 4;
            dataDisplayImage.TabStop = false;
            // 
            // initForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 401);
            Controls.Add(dataDisplayImage);
            Controls.Add(backgroundDataImage);
            Controls.Add(beginDataCaptureButton);
            Controls.Add(displayEnvDataButton);
            Name = "initForm";
            Text = "initForm";
            Load += initForm_Load;
            ((System.ComponentModel.ISupportInitialize)backgroundDataImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataDisplayImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button beginDataCaptureButton;
        private Button displayEnvDataButton;
        private ImageList imageList1;
        private PictureBox backgroundDataImage;
        private PictureBox dataDisplayImage;
    }
}