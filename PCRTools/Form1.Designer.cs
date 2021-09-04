namespace PCRTools
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
            this.menuClearOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPCRArena = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCmds = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRestartAdbServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnectAdbServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTestTap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.menuPCRArenaTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClearOutput,
            this.menuTools,
            this.menuCmds});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1060, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuClearOutput
            // 
            this.menuClearOutput.Name = "menuClearOutput";
            this.menuClearOutput.Size = new System.Drawing.Size(81, 24);
            this.menuClearOutput.Text = "清空输出";
            this.menuClearOutput.Click += new System.EventHandler(this.menuClearOutput_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPCRArena,
            this.menuPCRArenaTimer});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(51, 24);
            this.menuTools.Text = "工具";
            // 
            // menuPCRArena
            // 
            this.menuPCRArena.Name = "menuPCRArena";
            this.menuPCRArena.Size = new System.Drawing.Size(216, 26);
            this.menuPCRArena.Text = "PCR竞技场";
            this.menuPCRArena.Click += new System.EventHandler(this.menuPCRArena_Click);
            // 
            // menuCmds
            // 
            this.menuCmds.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRestartAdbServer,
            this.menuConnectAdbServer,
            this.menuTestTap,
            this.menuScreenshot});
            this.menuCmds.Name = "menuCmds";
            this.menuCmds.Size = new System.Drawing.Size(81, 24);
            this.menuCmds.Text = "常用命令";
            // 
            // menuRestartAdbServer
            // 
            this.menuRestartAdbServer.Name = "menuRestartAdbServer";
            this.menuRestartAdbServer.Size = new System.Drawing.Size(192, 26);
            this.menuRestartAdbServer.Text = "重启AdbServer";
            this.menuRestartAdbServer.Click += new System.EventHandler(this.menuRestartAdbServer_Click);
            // 
            // menuConnectAdbServer
            // 
            this.menuConnectAdbServer.Name = "menuConnectAdbServer";
            this.menuConnectAdbServer.Size = new System.Drawing.Size(192, 26);
            this.menuConnectAdbServer.Text = "连接AdbServer";
            this.menuConnectAdbServer.Click += new System.EventHandler(this.menuConnectAdbServer_Click);
            // 
            // menuTestTap
            // 
            this.menuTestTap.Name = "menuTestTap";
            this.menuTestTap.Size = new System.Drawing.Size(192, 26);
            this.menuTestTap.Text = "测试点击";
            this.menuTestTap.Click += new System.EventHandler(this.menuTestTap_Click);
            // 
            // menuScreenshot
            // 
            this.menuScreenshot.Name = "menuScreenshot";
            this.menuScreenshot.Size = new System.Drawing.Size(192, 26);
            this.menuScreenshot.Text = "截取模拟器界面";
            this.menuScreenshot.Click += new System.EventHandler(this.menuScreenshot_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(0, 28);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(1060, 575);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.Text = "";
            // 
            // menuPCRArenaTimer
            // 
            this.menuPCRArenaTimer.Name = "menuPCRArenaTimer";
            this.menuPCRArenaTimer.Size = new System.Drawing.Size(216, 26);
            this.menuPCRArenaTimer.Text = "PCR竞技场计时器";
            this.menuPCRArenaTimer.Click += new System.EventHandler(this.menuPCRArenaTimer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 603);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuPCRArena;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ToolStripMenuItem menuClearOutput;
        private System.Windows.Forms.ToolStripMenuItem menuCmds;
        private System.Windows.Forms.ToolStripMenuItem menuRestartAdbServer;
        private System.Windows.Forms.ToolStripMenuItem menuConnectAdbServer;
        private System.Windows.Forms.ToolStripMenuItem menuTestTap;
        private System.Windows.Forms.ToolStripMenuItem menuScreenshot;
        private System.Windows.Forms.ToolStripMenuItem menuPCRArenaTimer;
    }
}

