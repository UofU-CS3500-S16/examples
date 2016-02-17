namespace TipCalculator
{
    partial class TipCalc
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
            this.CheckBox = new System.Windows.Forms.TextBox();
            this.TipBox = new System.Windows.Forms.TextBox();
            this.CalcButton = new System.Windows.Forms.Button();
            this.TotalBox = new System.Windows.Forms.TextBox();
            this.CheckLabel = new System.Windows.Forms.Label();
            this.TipLabel = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CheckBox
            // 
            this.CheckBox.Location = new System.Drawing.Point(126, 44);
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Size = new System.Drawing.Size(100, 22);
            this.CheckBox.TabIndex = 0;
            // 
            // TipBox
            // 
            this.TipBox.Location = new System.Drawing.Point(126, 93);
            this.TipBox.Name = "TipBox";
            this.TipBox.Size = new System.Drawing.Size(100, 22);
            this.TipBox.TabIndex = 1;
            // 
            // CalcButton
            // 
            this.CalcButton.Location = new System.Drawing.Point(126, 144);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(100, 23);
            this.CalcButton.TabIndex = 2;
            this.CalcButton.Text = "Calculate";
            this.CalcButton.UseVisualStyleBackColor = true;
            this.CalcButton.Click += new System.EventHandler(this.CalcButton_Click);
            this.CalcButton.MouseEnter += new System.EventHandler(this.CalcButton_MouseEnter);
            // 
            // TotalBox
            // 
            this.TotalBox.Location = new System.Drawing.Point(126, 196);
            this.TotalBox.Name = "TotalBox";
            this.TotalBox.ReadOnly = true;
            this.TotalBox.Size = new System.Drawing.Size(100, 22);
            this.TotalBox.TabIndex = 3;
            // 
            // CheckLabel
            // 
            this.CheckLabel.AutoSize = true;
            this.CheckLabel.Location = new System.Drawing.Point(12, 47);
            this.CheckLabel.Name = "CheckLabel";
            this.CheckLabel.Size = new System.Drawing.Size(99, 17);
            this.CheckLabel.TabIndex = 4;
            this.CheckLabel.Text = "Check Amount";
            // 
            // TipLabel
            // 
            this.TipLabel.AutoSize = true;
            this.TipLabel.Location = new System.Drawing.Point(67, 93);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(44, 17);
            this.TipLabel.TabIndex = 5;
            this.TipLabel.Text = "Tip %";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(71, 199);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(40, 17);
            this.TotalLabel.TabIndex = 6;
            this.TotalLabel.Text = "Total";
            // 
            // TipCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.TipLabel);
            this.Controls.Add(this.CheckLabel);
            this.Controls.Add(this.TotalBox);
            this.Controls.Add(this.CalcButton);
            this.Controls.Add(this.TipBox);
            this.Controls.Add(this.CheckBox);
            this.Name = "TipCalc";
            this.Text = "Tip Calculator";
            this.Load += new System.EventHandler(this.TipCalc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CheckBox;
        private System.Windows.Forms.TextBox TipBox;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.TextBox TotalBox;
        private System.Windows.Forms.Label CheckLabel;
        private System.Windows.Forms.Label TipLabel;
        private System.Windows.Forms.Label TotalLabel;
    }
}

