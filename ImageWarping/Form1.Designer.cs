namespace ImageWarping
{
    partial class ImageWarping
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.IDWButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RBFButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ImagePictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 404);
            this.panel1.TabIndex = 1;
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.Location = new System.Drawing.Point(3, 3);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(100, 50);
            this.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadImageButton.Location = new System.Drawing.Point(482, 12);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(83, 23);
            this.LoadImageButton.TabIndex = 2;
            this.LoadImageButton.Text = "导入图像";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // IDWButton
            // 
            this.IDWButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IDWButton.Location = new System.Drawing.Point(483, 70);
            this.IDWButton.Name = "IDWButton";
            this.IDWButton.Size = new System.Drawing.Size(82, 23);
            this.IDWButton.TabIndex = 3;
            this.IDWButton.Text = "IDW";
            this.IDWButton.UseVisualStyleBackColor = true;
            this.IDWButton.Click += new System.EventHandler(this.IDWButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(483, 41);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(82, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "清空控制点";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RBFButton
            // 
            this.RBFButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RBFButton.Location = new System.Drawing.Point(483, 100);
            this.RBFButton.Name = "RBFButton";
            this.RBFButton.Size = new System.Drawing.Size(82, 23);
            this.RBFButton.TabIndex = 5;
            this.RBFButton.Text = "RBF";
            this.RBFButton.UseVisualStyleBackColor = true;
            this.RBFButton.Click += new System.EventHandler(this.RBFButton_Click);
            // 
            // ImageWarping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 428);
            this.Controls.Add(this.RBFButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.IDWButton);
            this.Controls.Add(this.LoadImageButton);
            this.Controls.Add(this.panel1);
            this.Name = "ImageWarping";
            this.Text = "图像扭曲";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.Button IDWButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RBFButton;
    }
}

