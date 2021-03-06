﻿namespace Interrupt
{
    partial class DemoWindow
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
            this.TopOfRange = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartButton1 = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.HailStart = new System.Windows.Forms.TextBox();
            this.HailLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StartButton2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TopOfRange
            // 
            this.TopOfRange.Location = new System.Drawing.Point(158, 40);
            this.TopOfRange.Name = "TopOfRange";
            this.TopOfRange.Size = new System.Drawing.Size(121, 22);
            this.TopOfRange.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Top Of Range";
            // 
            // StartButton1
            // 
            this.StartButton1.Location = new System.Drawing.Point(45, 89);
            this.StartButton1.Name = "StartButton1";
            this.StartButton1.Size = new System.Drawing.Size(233, 27);
            this.StartButton1.TabIndex = 2;
            this.StartButton1.Text = "Start (Version 1)";
            this.StartButton1.UseVisualStyleBackColor = true;
            this.StartButton1.Click += new System.EventHandler(this.StartButton1_Click);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(45, 195);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(233, 27);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Cancel";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // HailStart
            // 
            this.HailStart.Location = new System.Drawing.Point(158, 237);
            this.HailStart.Name = "HailStart";
            this.HailStart.ReadOnly = true;
            this.HailStart.Size = new System.Drawing.Size(120, 22);
            this.HailStart.TabIndex = 4;
            // 
            // HailLength
            // 
            this.HailLength.Location = new System.Drawing.Point(158, 284);
            this.HailLength.Name = "HailLength";
            this.HailLength.ReadOnly = true;
            this.HailLength.Size = new System.Drawing.Size(120, 22);
            this.HailLength.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Longest Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Longest Length";
            // 
            // StartButton2
            // 
            this.StartButton2.Location = new System.Drawing.Point(45, 125);
            this.StartButton2.Name = "StartButton2";
            this.StartButton2.Size = new System.Drawing.Size(233, 27);
            this.StartButton2.TabIndex = 8;
            this.StartButton2.Text = "Start (Version 2)";
            this.StartButton2.UseVisualStyleBackColor = true;
            this.StartButton2.Click += new System.EventHandler(this.StartButton2_Click);
            // 
            // DemoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 381);
            this.Controls.Add(this.StartButton2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HailLength);
            this.Controls.Add(this.HailStart);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TopOfRange);
            this.Name = "DemoWindow";
            this.Text = "Demo Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TopOfRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartButton1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox HailStart;
        private System.Windows.Forms.TextBox HailLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StartButton2;
    }
}

