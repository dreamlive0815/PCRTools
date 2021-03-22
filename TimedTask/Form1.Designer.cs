﻿namespace TimedTask
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
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTestTap = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnAddMinute = new System.Windows.Forms.Button();
            this.btnSubMinute = new System.Windows.Forms.Button();
            this.btnSubSecond = new System.Windows.Forms.Button();
            this.btnAddSecond = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHour
            // 
            this.txtHour.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHour.Location = new System.Drawing.Point(24, 12);
            this.txtHour.Multiline = true;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(115, 44);
            this.txtHour.TabIndex = 0;
            this.txtHour.Text = "11";
            this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMinute
            // 
            this.txtMinute.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinute.Location = new System.Drawing.Point(202, 12);
            this.txtMinute.Multiline = true;
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(115, 44);
            this.txtMinute.TabIndex = 1;
            this.txtMinute.Text = "11";
            this.txtMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSecond
            // 
            this.txtSecond.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecond.Location = new System.Drawing.Point(381, 12);
            this.txtSecond.Multiline = true;
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(115, 44);
            this.txtSecond.TabIndex = 2;
            this.txtSecond.Text = "11";
            this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHour.Location = new System.Drawing.Point(148, 15);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(60, 43);
            this.lblHour.TabIndex = 3;
            this.lblHour.Text = "时";
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinute.Location = new System.Drawing.Point(332, 15);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(60, 43);
            this.lblMinute.TabIndex = 4;
            this.lblMinute.Text = "分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(517, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 43);
            this.label2.TabIndex = 5;
            this.label2.Text = "秒";
            // 
            // btnTestTap
            // 
            this.btnTestTap.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestTap.Location = new System.Drawing.Point(24, 198);
            this.btnTestTap.Name = "btnTestTap";
            this.btnTestTap.Size = new System.Drawing.Size(161, 58);
            this.btnTestTap.TabIndex = 6;
            this.btnTestTap.Text = "测试点击";
            this.btnTestTap.UseVisualStyleBackColor = true;
            this.btnTestTap.Click += new System.EventHandler(this.btnTestTap_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(24, 120);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(161, 58);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始计时";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(28, 73);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 29);
            this.lblMsg.TabIndex = 8;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(202, 120);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(161, 58);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "结束计时";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnAddMinute
            // 
            this.btnAddMinute.Location = new System.Drawing.Point(314, 1);
            this.btnAddMinute.Name = "btnAddMinute";
            this.btnAddMinute.Size = new System.Drawing.Size(75, 24);
            this.btnAddMinute.TabIndex = 10;
            this.btnAddMinute.Text = "+";
            this.btnAddMinute.UseVisualStyleBackColor = true;
            this.btnAddMinute.Click += new System.EventHandler(this.btnAddMinute_Click);
            // 
            // btnSubMinute
            // 
            this.btnSubMinute.Location = new System.Drawing.Point(314, 42);
            this.btnSubMinute.Name = "btnSubMinute";
            this.btnSubMinute.Size = new System.Drawing.Size(75, 24);
            this.btnSubMinute.TabIndex = 11;
            this.btnSubMinute.Text = "-";
            this.btnSubMinute.UseVisualStyleBackColor = true;
            this.btnSubMinute.Click += new System.EventHandler(this.btnSubMinute_Click);
            // 
            // btnSubSecond
            // 
            this.btnSubSecond.Location = new System.Drawing.Point(493, 42);
            this.btnSubSecond.Name = "btnSubSecond";
            this.btnSubSecond.Size = new System.Drawing.Size(75, 24);
            this.btnSubSecond.TabIndex = 13;
            this.btnSubSecond.Text = "-";
            this.btnSubSecond.UseVisualStyleBackColor = true;
            this.btnSubSecond.Click += new System.EventHandler(this.btnSubSecond_Click);
            // 
            // btnAddSecond
            // 
            this.btnAddSecond.Location = new System.Drawing.Point(493, 1);
            this.btnAddSecond.Name = "btnAddSecond";
            this.btnAddSecond.Size = new System.Drawing.Size(75, 24);
            this.btnAddSecond.TabIndex = 12;
            this.btnAddSecond.Text = "+";
            this.btnAddSecond.UseVisualStyleBackColor = true;
            this.btnAddSecond.Click += new System.EventHandler(this.btnAddSecond_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 262);
            this.Controls.Add(this.btnSubSecond);
            this.Controls.Add(this.btnAddSecond);
            this.Controls.Add(this.btnSubMinute);
            this.Controls.Add(this.btnAddMinute);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnTestTap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtMinute);
            this.Controls.Add(this.txtHour);
            this.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestTap;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnAddMinute;
        private System.Windows.Forms.Button btnSubMinute;
        private System.Windows.Forms.Button btnSubSecond;
        private System.Windows.Forms.Button btnAddSecond;
    }
}
