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
            this.menuScripts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStopScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTestScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmulatorInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPCRArena = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPCRArenaTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCmds = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRestartAdbServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnectAdbServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTestTap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClearOutput,
            this.menuScripts,
            this.menuTools,
            this.menuCmds});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(795, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuClearOutput
            // 
            this.menuClearOutput.Name = "menuClearOutput";
            this.menuClearOutput.Size = new System.Drawing.Size(68, 21);
            this.menuClearOutput.Text = "清空输出";
            this.menuClearOutput.Click += new System.EventHandler(this.menuClearOutput_Click);
            // 
            // menuScripts
            // 
            this.menuScripts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStopScript,
            this.menuTestScript});
            this.menuScripts.Name = "menuScripts";
            this.menuScripts.Size = new System.Drawing.Size(44, 21);
            this.menuScripts.Text = "脚本";
            // 
            // menuStopScript
            // 
            this.menuStopScript.Name = "menuStopScript";
            this.menuStopScript.Size = new System.Drawing.Size(124, 22);
            this.menuStopScript.Text = "停止脚本";
            this.menuStopScript.Click += new System.EventHandler(this.menuStopScript_Click);
            // 
            // menuTestScript
            // 
            this.menuTestScript.Name = "menuTestScript";
            this.menuTestScript.Size = new System.Drawing.Size(124, 22);
            this.menuTestScript.Text = "脚本测试";
            this.menuTestScript.Click += new System.EventHandler(this.menuTestScript_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEmulatorInfo,
            this.menuPCRArena,
            this.menuPCRArenaTimer});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(44, 21);
            this.menuTools.Text = "工具";
            // 
            // menuEmulatorInfo
            // 
            this.menuEmulatorInfo.Name = "menuEmulatorInfo";
            this.menuEmulatorInfo.Size = new System.Drawing.Size(171, 22);
            this.menuEmulatorInfo.Text = "模拟器实时信息";
            this.menuEmulatorInfo.Click += new System.EventHandler(this.menuEmulatorInfo_Click);
            // 
            // menuPCRArena
            // 
            this.menuPCRArena.Name = "menuPCRArena";
            this.menuPCRArena.Size = new System.Drawing.Size(171, 22);
            this.menuPCRArena.Text = "PCR竞技场";
            this.menuPCRArena.Click += new System.EventHandler(this.menuPCRArena_Click);
            // 
            // menuPCRArenaTimer
            // 
            this.menuPCRArenaTimer.Name = "menuPCRArenaTimer";
            this.menuPCRArenaTimer.Size = new System.Drawing.Size(171, 22);
            this.menuPCRArenaTimer.Text = "PCR竞技场计时器";
            this.menuPCRArenaTimer.Click += new System.EventHandler(this.menuPCRArenaTimer_Click);
            // 
            // menuCmds
            // 
            this.menuCmds.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRestartAdbServer,
            this.menuConnectAdbServer,
            this.menuTestTap,
            this.menuScreenshot});
            this.menuCmds.Name = "menuCmds";
            this.menuCmds.Size = new System.Drawing.Size(68, 21);
            this.menuCmds.Text = "常用命令";
            // 
            // menuRestartAdbServer
            // 
            this.menuRestartAdbServer.Name = "menuRestartAdbServer";
            this.menuRestartAdbServer.Size = new System.Drawing.Size(161, 22);
            this.menuRestartAdbServer.Text = "重启AdbServer";
            this.menuRestartAdbServer.Click += new System.EventHandler(this.menuRestartAdbServer_Click);
            // 
            // menuConnectAdbServer
            // 
            this.menuConnectAdbServer.Name = "menuConnectAdbServer";
            this.menuConnectAdbServer.Size = new System.Drawing.Size(161, 22);
            this.menuConnectAdbServer.Text = "连接AdbServer";
            this.menuConnectAdbServer.Click += new System.EventHandler(this.menuConnectAdbServer_Click);
            // 
            // menuTestTap
            // 
            this.menuTestTap.Name = "menuTestTap";
            this.menuTestTap.Size = new System.Drawing.Size(161, 22);
            this.menuTestTap.Text = "测试点击";
            this.menuTestTap.Click += new System.EventHandler(this.menuTestTap_Click);
            // 
            // menuScreenshot
            // 
            this.menuScreenshot.Name = "menuScreenshot";
            this.menuScreenshot.Size = new System.Drawing.Size(161, 22);
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
            this.txtOutput.Location = new System.Drawing.Point(0, 25);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(795, 457);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 482);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
        private System.Windows.Forms.ToolStripMenuItem menuEmulatorInfo;
        private System.Windows.Forms.ToolStripMenuItem menuScripts;
        private System.Windows.Forms.ToolStripMenuItem menuStopScript;
        private System.Windows.Forms.ToolStripMenuItem menuTestScript;
    }
}

