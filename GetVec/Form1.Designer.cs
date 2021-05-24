namespace GetVec
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFrmInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveImageSample = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetRect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenAdbDir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenResDir = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuRectSizeOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThreshold = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFrmInfo,
            this.menuCapture,
            this.menuSave,
            this.menuSetRect,
            this.menuOpenDir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(963, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFrmInfo
            // 
            this.menuFrmInfo.Name = "menuFrmInfo";
            this.menuFrmInfo.Size = new System.Drawing.Size(51, 24);
            this.menuFrmInfo.Text = "信息";
            this.menuFrmInfo.Click += new System.EventHandler(this.menuRefreshInfo_Click);
            // 
            // menuCapture
            // 
            this.menuCapture.Name = "menuCapture";
            this.menuCapture.Size = new System.Drawing.Size(51, 24);
            this.menuCapture.Text = "捕获";
            this.menuCapture.Click += new System.EventHandler(this.menuCapture_Click);
            // 
            // menuSave
            // 
            this.menuSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveImageSample,
            this.menuThreshold,
            this.menuPoint,
            this.menuRectSizeOnly,
            this.menuRect});
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(51, 24);
            this.menuSave.Text = "保存";
            // 
            // menuSaveImageSample
            // 
            this.menuSaveImageSample.Name = "menuSaveImageSample";
            this.menuSaveImageSample.Size = new System.Drawing.Size(216, 26);
            this.menuSaveImageSample.Text = "图例";
            this.menuSaveImageSample.Click += new System.EventHandler(this.menuSaveImageSample_Click);
            // 
            // menuPoint
            // 
            this.menuPoint.Name = "menuPoint";
            this.menuPoint.Size = new System.Drawing.Size(216, 26);
            this.menuPoint.Text = "点";
            this.menuPoint.Click += new System.EventHandler(this.menuPoint_Click);
            // 
            // menuRect
            // 
            this.menuRect.Name = "menuRect";
            this.menuRect.Size = new System.Drawing.Size(216, 26);
            this.menuRect.Text = "矩形(位置和大小)";
            this.menuRect.Click += new System.EventHandler(this.menuRect_Click);
            // 
            // menuSetRect
            // 
            this.menuSetRect.Name = "menuSetRect";
            this.menuSetRect.Size = new System.Drawing.Size(111, 24);
            this.menuSetRect.Text = "设置矩形区域";
            this.menuSetRect.Click += new System.EventHandler(this.menuSetRect_Click);
            // 
            // menuOpenDir
            // 
            this.menuOpenDir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenAdbDir,
            this.menuOpenResDir});
            this.menuOpenDir.Name = "menuOpenDir";
            this.menuOpenDir.Size = new System.Drawing.Size(51, 24);
            this.menuOpenDir.Text = "目录";
            // 
            // menuOpenAdbDir
            // 
            this.menuOpenAdbDir.Name = "menuOpenAdbDir";
            this.menuOpenAdbDir.Size = new System.Drawing.Size(115, 26);
            this.menuOpenAdbDir.Text = "Adb";
            this.menuOpenAdbDir.Click += new System.EventHandler(this.menuOpenAdbDir_Click);
            // 
            // menuOpenResDir
            // 
            this.menuOpenResDir.Name = "menuOpenResDir";
            this.menuOpenResDir.Size = new System.Drawing.Size(115, 26);
            this.menuOpenResDir.Text = "资源";
            this.menuOpenResDir.Click += new System.EventHandler(this.menuOpenResDir_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(963, 456);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuRectSizeOnly
            // 
            this.menuRectSizeOnly.Name = "menuRectSizeOnly";
            this.menuRectSizeOnly.Size = new System.Drawing.Size(216, 26);
            this.menuRectSizeOnly.Text = "矩形(仅大小)";
            this.menuRectSizeOnly.Click += new System.EventHandler(this.menuRectSizeOnly_Click);
            // 
            // menuThreshold
            // 
            this.menuThreshold.Name = "menuThreshold";
            this.menuThreshold.Size = new System.Drawing.Size(216, 26);
            this.menuThreshold.Text = "阈值";
            this.menuThreshold.Click += new System.EventHandler(this.menuThreshold_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(963, 484);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem menuCapture;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDir;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveImageSample;
        private System.Windows.Forms.ToolStripMenuItem menuPoint;
        private System.Windows.Forms.ToolStripMenuItem menuRect;
        private System.Windows.Forms.ToolStripMenuItem menuFrmInfo;
        private System.Windows.Forms.ToolStripMenuItem menuOpenAdbDir;
        private System.Windows.Forms.ToolStripMenuItem menuOpenResDir;
        private System.Windows.Forms.ToolStripMenuItem menuSetRect;
        private System.Windows.Forms.ToolStripMenuItem menuRectSizeOnly;
        private System.Windows.Forms.ToolStripMenuItem menuThreshold;
    }
}

