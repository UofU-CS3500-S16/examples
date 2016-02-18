namespace FileAnalyzer
{
    partial class AnalysisWindow
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
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineLabel = new System.Windows.Forms.Label();
            this.wordLabel = new System.Windows.Forms.Label();
            this.charLabel = new System.Windows.Forms.Label();
            this.lineCount = new System.Windows.Forms.TextBox();
            this.wordCount = new System.Windows.Forms.TextBox();
            this.charCount = new System.Windows.Forms.TextBox();
            this.substringLabel = new System.Windows.Forms.Label();
            this.substringCount = new System.Windows.Forms.TextBox();
            this.substringBox = new System.Windows.Forms.TextBox();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.countButton = new System.Windows.Forms.Button();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(383, 28);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newItem,
            this.openItem,
            this.closeItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(44, 24);
            this.fileMenu.Text = "File";
            // 
            // newItem
            // 
            this.newItem.Name = "newItem";
            this.newItem.Size = new System.Drawing.Size(133, 26);
            this.newItem.Text = "New";
            this.newItem.Click += new System.EventHandler(this.NewItem_Click);
            // 
            // openItem
            // 
            this.openItem.Name = "openItem";
            this.openItem.Size = new System.Drawing.Size(133, 26);
            this.openItem.Text = "Open ...";
            this.openItem.Click += new System.EventHandler(this.OpenItem_Click);
            // 
            // closeItem
            // 
            this.closeItem.Name = "closeItem";
            this.closeItem.Size = new System.Drawing.Size(133, 26);
            this.closeItem.Text = "Close";
            this.closeItem.Click += new System.EventHandler(this.CloseItem_Click);
            // 
            // lineLabel
            // 
            this.lineLabel.AutoSize = true;
            this.lineLabel.Location = new System.Drawing.Point(63, 68);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(42, 17);
            this.lineLabel.TabIndex = 1;
            this.lineLabel.Text = "Lines";
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(63, 108);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(49, 17);
            this.wordLabel.TabIndex = 2;
            this.wordLabel.Text = "Words";
            // 
            // charLabel
            // 
            this.charLabel.AutoSize = true;
            this.charLabel.Location = new System.Drawing.Point(63, 152);
            this.charLabel.Name = "charLabel";
            this.charLabel.Size = new System.Drawing.Size(45, 17);
            this.charLabel.TabIndex = 3;
            this.charLabel.Text = "Chars";
            // 
            // lineCount
            // 
            this.lineCount.Location = new System.Drawing.Point(122, 62);
            this.lineCount.Name = "lineCount";
            this.lineCount.Size = new System.Drawing.Size(79, 22);
            this.lineCount.TabIndex = 4;
            // 
            // wordCount
            // 
            this.wordCount.Location = new System.Drawing.Point(122, 103);
            this.wordCount.Name = "wordCount";
            this.wordCount.Size = new System.Drawing.Size(79, 22);
            this.wordCount.TabIndex = 5;
            // 
            // charCount
            // 
            this.charCount.Location = new System.Drawing.Point(122, 147);
            this.charCount.Name = "charCount";
            this.charCount.Size = new System.Drawing.Size(79, 22);
            this.charCount.TabIndex = 6;
            // 
            // substringLabel
            // 
            this.substringLabel.AutoSize = true;
            this.substringLabel.Location = new System.Drawing.Point(38, 235);
            this.substringLabel.Name = "substringLabel";
            this.substringLabel.Size = new System.Drawing.Size(75, 17);
            this.substringLabel.TabIndex = 7;
            this.substringLabel.Text = "Substrings";
            // 
            // substringCount
            // 
            this.substringCount.Location = new System.Drawing.Point(122, 235);
            this.substringCount.Name = "substringCount";
            this.substringCount.Size = new System.Drawing.Size(79, 22);
            this.substringCount.TabIndex = 8;
            // 
            // substringBox
            // 
            this.substringBox.Location = new System.Drawing.Point(38, 201);
            this.substringBox.Name = "substringBox";
            this.substringBox.Size = new System.Drawing.Size(163, 22);
            this.substringBox.TabIndex = 9;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // countButton
            // 
            this.countButton.Location = new System.Drawing.Point(219, 200);
            this.countButton.Name = "countButton";
            this.countButton.Size = new System.Drawing.Size(75, 23);
            this.countButton.TabIndex = 10;
            this.countButton.Text = "Count";
            this.countButton.UseVisualStyleBackColor = true;
            this.countButton.Click += new System.EventHandler(this.CountButton_Click);
            // 
            // AnalysisWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 438);
            this.Controls.Add(this.countButton);
            this.Controls.Add(this.substringBox);
            this.Controls.Add(this.substringCount);
            this.Controls.Add(this.substringLabel);
            this.Controls.Add(this.charCount);
            this.Controls.Add(this.wordCount);
            this.Controls.Add(this.lineCount);
            this.Controls.Add(this.charLabel);
            this.Controls.Add(this.wordLabel);
            this.Controls.Add(this.lineLabel);
            this.Controls.Add(this.menuBar);
            this.MainMenuStrip = this.menuBar;
            this.Name = "AnalysisWindow";
            this.Text = "File Analyzer";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openItem;
        private System.Windows.Forms.ToolStripMenuItem closeItem;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.Label charLabel;
        private System.Windows.Forms.Label substringLabel;
        private System.Windows.Forms.TextBox substringCount;
        private System.Windows.Forms.TextBox substringBox;
        private System.Windows.Forms.TextBox charCount;
        private System.Windows.Forms.TextBox wordCount;
        private System.Windows.Forms.TextBox lineCount;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button countButton;
        private System.Windows.Forms.ToolStripMenuItem newItem;
    }
}

