﻿namespace GetVec
{
    partial class FrmInput
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
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(69, 17);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(418, 28);
            this.txtKey.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "键名";
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(54, 5);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(67, 28);
            this.txtThreshold.TabIndex = 7;
            this.txtThreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThreshold_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "阈值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "%";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtThreshold);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(15, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 34);
            this.panel1.TabIndex = 9;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(17, 96);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(475, 35);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 143);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmInput";
            this.Load += new System.EventHandler(this.FrmInput_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
    }
}