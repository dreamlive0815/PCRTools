namespace PCRTools
{
    partial class FrmPCRArenaTimer
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
            this.components = new System.ComponentModel.Container();
            this.btnSubSecond = new System.Windows.Forms.Button();
            this.btnAddSecond = new System.Windows.Forms.Button();
            this.btnSubMinute = new System.Windows.Forms.Button();
            this.btnAddMinute = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblSecond = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.lblOffsetMS = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnAdd5Minutes = new System.Windows.Forms.Button();
            this.btnTestTap = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnTapBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSubSecond
            // 
            this.btnSubSecond.Location = new System.Drawing.Point(405, 46);
            this.btnSubSecond.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubSecond.Name = "btnSubSecond";
            this.btnSubSecond.Size = new System.Drawing.Size(56, 19);
            this.btnSubSecond.TabIndex = 24;
            this.btnSubSecond.Text = "-";
            this.btnSubSecond.UseVisualStyleBackColor = true;
            this.btnSubSecond.Click += new System.EventHandler(this.btnSubSecond_Click);
            // 
            // btnAddSecond
            // 
            this.btnAddSecond.Location = new System.Drawing.Point(405, 4);
            this.btnAddSecond.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddSecond.Name = "btnAddSecond";
            this.btnAddSecond.Size = new System.Drawing.Size(56, 19);
            this.btnAddSecond.TabIndex = 23;
            this.btnAddSecond.Text = "+";
            this.btnAddSecond.UseVisualStyleBackColor = true;
            this.btnAddSecond.Click += new System.EventHandler(this.btnAddSecond_Click);
            // 
            // btnSubMinute
            // 
            this.btnSubMinute.Location = new System.Drawing.Point(255, 46);
            this.btnSubMinute.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubMinute.Name = "btnSubMinute";
            this.btnSubMinute.Size = new System.Drawing.Size(56, 19);
            this.btnSubMinute.TabIndex = 22;
            this.btnSubMinute.Text = "-";
            this.btnSubMinute.UseVisualStyleBackColor = true;
            this.btnSubMinute.Click += new System.EventHandler(this.btnSubMinute_Click);
            // 
            // btnAddMinute
            // 
            this.btnAddMinute.Location = new System.Drawing.Point(255, 4);
            this.btnAddMinute.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddMinute.Name = "btnAddMinute";
            this.btnAddMinute.Size = new System.Drawing.Size(56, 19);
            this.btnAddMinute.TabIndex = 21;
            this.btnAddMinute.Text = "+";
            this.btnAddMinute.UseVisualStyleBackColor = true;
            this.btnAddMinute.Click += new System.EventHandler(this.btnAddMinute_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(17, 67);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 19);
            this.lblMsg.TabIndex = 20;
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecond.Location = new System.Drawing.Point(405, 17);
            this.lblSecond.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(48, 34);
            this.lblSecond.TabIndex = 19;
            this.lblSecond.Text = "秒";
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinute.Location = new System.Drawing.Point(255, 17);
            this.lblMinute.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(48, 34);
            this.lblMinute.TabIndex = 18;
            this.lblMinute.Text = "分";
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHour.Location = new System.Drawing.Point(105, 17);
            this.lblHour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(48, 34);
            this.lblHour.TabIndex = 17;
            this.lblHour.Text = "时";
            // 
            // txtSecond
            // 
            this.txtSecond.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecond.Location = new System.Drawing.Point(315, 18);
            this.txtSecond.Margin = new System.Windows.Forms.Padding(2);
            this.txtSecond.Multiline = true;
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(87, 39);
            this.txtSecond.TabIndex = 16;
            this.txtSecond.Text = "0";
            this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMinute
            // 
            this.txtMinute.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinute.Location = new System.Drawing.Point(165, 17);
            this.txtMinute.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinute.Multiline = true;
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(87, 39);
            this.txtMinute.TabIndex = 15;
            this.txtMinute.Text = "0";
            this.txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHour
            // 
            this.txtHour.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHour.Location = new System.Drawing.Point(15, 17);
            this.txtHour.Margin = new System.Windows.Forms.Padding(2);
            this.txtHour.Multiline = true;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(87, 39);
            this.txtHour.TabIndex = 14;
            this.txtHour.Text = "0";
            this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOffsetMS
            // 
            this.lblOffsetMS.AutoSize = true;
            this.lblOffsetMS.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffsetMS.Location = new System.Drawing.Point(219, 104);
            this.lblOffsetMS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOffsetMS.Name = "lblOffsetMS";
            this.lblOffsetMS.Size = new System.Drawing.Size(81, 34);
            this.lblOffsetMS.TabIndex = 27;
            this.lblOffsetMS.Text = "毫秒";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffset.Location = new System.Drawing.Point(15, 104);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(81, 34);
            this.lblOffset.TabIndex = 26;
            this.lblOffset.Text = "偏移";
            // 
            // txtOffset
            // 
            this.txtOffset.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOffset.Location = new System.Drawing.Point(100, 104);
            this.txtOffset.Margin = new System.Windows.Forms.Padding(2);
            this.txtOffset.Multiline = true;
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(115, 39);
            this.txtOffset.TabIndex = 25;
            this.txtOffset.Text = "0";
            this.txtOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(15, 226);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(121, 46);
            this.btnStop.TabIndex = 29;
            this.btnStop.Text = "结束计时";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(15, 160);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 46);
            this.btnStart.TabIndex = 28;
            this.btnStart.Text = "开始计时";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAdd5Minutes
            // 
            this.btnAdd5Minutes.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd5Minutes.Location = new System.Drawing.Point(315, 226);
            this.btnAdd5Minutes.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd5Minutes.Name = "btnAdd5Minutes";
            this.btnAdd5Minutes.Size = new System.Drawing.Size(146, 46);
            this.btnAdd5Minutes.TabIndex = 31;
            this.btnAdd5Minutes.Text = "设置5分钟后";
            this.btnAdd5Minutes.UseVisualStyleBackColor = true;
            this.btnAdd5Minutes.Click += new System.EventHandler(this.btnAdd5Minutes_Click);
            // 
            // btnTestTap
            // 
            this.btnTestTap.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestTap.Location = new System.Drawing.Point(340, 160);
            this.btnTestTap.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestTap.Name = "btnTestTap";
            this.btnTestTap.Size = new System.Drawing.Size(121, 46);
            this.btnTestTap.TabIndex = 30;
            this.btnTestTap.Text = "测试点击";
            this.btnTestTap.UseVisualStyleBackColor = true;
            this.btnTestTap.Click += new System.EventHandler(this.btnTestTap_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(290, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 23);
            this.label1.TabIndex = 32;
            this.label1.Text = "1.00s";
            // 
            // btnTapBack
            // 
            this.btnTapBack.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTapBack.Location = new System.Drawing.Point(194, 160);
            this.btnTapBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnTapBack.Name = "btnTapBack";
            this.btnTapBack.Size = new System.Drawing.Size(142, 46);
            this.btnTapBack.TabIndex = 33;
            this.btnTapBack.Text = "点击返回键";
            this.btnTapBack.UseVisualStyleBackColor = true;
            this.btnTapBack.Visible = false;
            this.btnTapBack.Click += new System.EventHandler(this.btnTapBack_Click);
            // 
            // FrmPCRArenaTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 292);
            this.Controls.Add(this.btnTapBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd5Minutes);
            this.Controls.Add(this.btnTestTap);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblOffsetMS);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.btnSubSecond);
            this.Controls.Add(this.btnAddSecond);
            this.Controls.Add(this.btnSubMinute);
            this.Controls.Add(this.btnAddMinute);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtMinute);
            this.Controls.Add(this.txtHour);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmPCRArenaTimer";
            this.Text = "FrmPCRArenaTimer";
            this.Load += new System.EventHandler(this.FrmPCRArenaTimer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubSecond;
        private System.Windows.Forms.Button btnAddSecond;
        private System.Windows.Forms.Button btnSubMinute;
        private System.Windows.Forms.Button btnAddMinute;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label lblOffsetMS;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAdd5Minutes;
        private System.Windows.Forms.Button btnTestTap;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTapBack;
    }
}