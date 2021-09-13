namespace PCRTools
{
    partial class FrmScriptManager
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSwitchEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.columnPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuReloadScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenScriptInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIdentity,
            this.columnName,
            this.columnEnabled,
            this.columnPriority,
            this.columnFilePath});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(715, 412);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnIdentity
            // 
            this.columnIdentity.Text = "标识符";
            this.columnIdentity.Width = 150;
            // 
            // columnName
            // 
            this.columnName.Text = "名称";
            this.columnName.Width = 120;
            // 
            // columnEnabled
            // 
            this.columnEnabled.Text = "是否可用";
            this.columnEnabled.Width = 100;
            // 
            // columnFilePath
            // 
            this.columnFilePath.Text = "文件路径";
            this.columnFilePath.Width = 600;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdd,
            this.menuMoveUp,
            this.menuMoveDown,
            this.menuSwitchEnabled,
            this.menuReloadScript,
            this.menuOpenScriptInExplorer});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAdd
            // 
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(51, 24);
            this.menuAdd.Text = "添加";
            this.menuAdd.Click += new System.EventHandler(this.menuAdd_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "脚本文件|*.script.json";
            // 
            // menuMoveUp
            // 
            this.menuMoveUp.Name = "menuMoveUp";
            this.menuMoveUp.Size = new System.Drawing.Size(51, 24);
            this.menuMoveUp.Text = "上移";
            this.menuMoveUp.Click += new System.EventHandler(this.menuMoveUp_Click);
            // 
            // menuMoveDown
            // 
            this.menuMoveDown.Name = "menuMoveDown";
            this.menuMoveDown.Size = new System.Drawing.Size(51, 24);
            this.menuMoveDown.Text = "下移";
            this.menuMoveDown.Click += new System.EventHandler(this.menuMoveDown_Click);
            // 
            // menuSwitchEnabled
            // 
            this.menuSwitchEnabled.Name = "menuSwitchEnabled";
            this.menuSwitchEnabled.Size = new System.Drawing.Size(111, 24);
            this.menuSwitchEnabled.Text = "切换可用状态";
            this.menuSwitchEnabled.Click += new System.EventHandler(this.menuSwitchEnabled_Click);
            // 
            // columnPriority
            // 
            this.columnPriority.Text = "优先级";
            // 
            // menuReloadScript
            // 
            this.menuReloadScript.Name = "menuReloadScript";
            this.menuReloadScript.Size = new System.Drawing.Size(81, 24);
            this.menuReloadScript.Text = "重载脚本";
            this.menuReloadScript.Click += new System.EventHandler(this.menuReloadScript_Click);
            // 
            // menuOpenScriptInExplorer
            // 
            this.menuOpenScriptInExplorer.Name = "menuOpenScriptInExplorer";
            this.menuOpenScriptInExplorer.Size = new System.Drawing.Size(156, 24);
            this.menuOpenScriptInExplorer.Text = "在文件管理器中打开";
            this.menuOpenScriptInExplorer.Click += new System.EventHandler(this.menuOpenScriptInExplorer_Click);
            // 
            // FrmScriptManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 440);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmScriptManager";
            this.Text = "FrmScriptManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmScriptManager_FormClosed);
            this.Load += new System.EventHandler(this.FrmScriptManager_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ColumnHeader columnIdentity;
        private System.Windows.Forms.ColumnHeader columnEnabled;
        private System.Windows.Forms.ColumnHeader columnFilePath;
        private System.Windows.Forms.ToolStripMenuItem menuAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ToolStripMenuItem menuMoveUp;
        private System.Windows.Forms.ToolStripMenuItem menuMoveDown;
        private System.Windows.Forms.ToolStripMenuItem menuSwitchEnabled;
        private System.Windows.Forms.ColumnHeader columnPriority;
        private System.Windows.Forms.ToolStripMenuItem menuReloadScript;
        private System.Windows.Forms.ToolStripMenuItem menuOpenScriptInExplorer;
    }
}