﻿namespace GetVec
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectAdbInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtKey = new System.Windows.Forms.ToolStripTextBox();
            this.menuSaveImageSample = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefreshInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefreshInfo,
            this.menuCapture,
            this.menuSelectAdbInExplorer,
            this.txtKey,
            this.menuSave});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(722, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuCapture
            // 
            this.menuCapture.Name = "menuCapture";
            this.menuCapture.Size = new System.Drawing.Size(80, 23);
            this.menuCapture.Text = "模拟器截图";
            this.menuCapture.Click += new System.EventHandler(this.menuCapture_Click);
            // 
            // menuSelectAdbInExplorer
            // 
            this.menuSelectAdbInExplorer.Name = "menuSelectAdbInExplorer";
            this.menuSelectAdbInExplorer.Size = new System.Drawing.Size(104, 23);
            this.menuSelectAdbInExplorer.Text = "模拟器Adb目录";
            this.menuSelectAdbInExplorer.Click += new System.EventHandler(this.menuSelectAdbInExplorer_Click);
            // 
            // menuSave
            // 
            this.menuSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveImageSample,
            this.menuPoint,
            this.menuRect});
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(44, 23);
            this.menuSave.Text = "保存";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(722, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // txtKey
            // 
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(100, 23);
            // 
            // menuSaveImageSample
            // 
            this.menuSaveImageSample.Name = "menuSaveImageSample";
            this.menuSaveImageSample.Size = new System.Drawing.Size(152, 22);
            this.menuSaveImageSample.Text = "图例";
            this.menuSaveImageSample.Click += new System.EventHandler(this.menuSaveImageSample_Click);
            // 
            // menuPoint
            // 
            this.menuPoint.Name = "menuPoint";
            this.menuPoint.Size = new System.Drawing.Size(152, 22);
            this.menuPoint.Text = "点";
            this.menuPoint.Click += new System.EventHandler(this.menuPoint_Click);
            // 
            // menuRect
            // 
            this.menuRect.Name = "menuRect";
            this.menuRect.Size = new System.Drawing.Size(152, 22);
            this.menuRect.Text = "矩形";
            this.menuRect.Click += new System.EventHandler(this.menuRect_Click);
            // 
            // menuRefreshInfo
            // 
            this.menuRefreshInfo.Name = "menuRefreshInfo";
            this.menuRefreshInfo.Size = new System.Drawing.Size(44, 23);
            this.menuRefreshInfo.Text = "刷新";
            this.menuRefreshInfo.Click += new System.EventHandler(this.menuRefreshInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(722, 387);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem menuCapture;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAdbInExplorer;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripTextBox txtKey;
        private System.Windows.Forms.ToolStripMenuItem menuSaveImageSample;
        private System.Windows.Forms.ToolStripMenuItem menuPoint;
        private System.Windows.Forms.ToolStripMenuItem menuRect;
        private System.Windows.Forms.ToolStripMenuItem menuRefreshInfo;
    }
}

