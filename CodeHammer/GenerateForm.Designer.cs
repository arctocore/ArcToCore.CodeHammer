namespace CodeHammer
{
    partial class GenerateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateForm));
            this.syntaxRichTextBox1 = new SyntaxHighlighter.SyntaxRichTextBox();
            this.ambiance_ThemeContainer1 = new Ambiance.Ambiance_ThemeContainer();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.ambiance_ControlBox1 = new Ambiance.Ambiance_ControlBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.copyCodeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDtoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ambiance_ThemeContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxRichTextBox1
            // 
            this.syntaxRichTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxRichTextBox1.Location = new System.Drawing.Point(10, 79);
            this.syntaxRichTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxRichTextBox1.Name = "syntaxRichTextBox1";
            this.syntaxRichTextBox1.ReadOnly = true;
            this.syntaxRichTextBox1.Size = new System.Drawing.Size(710, 481);
            this.syntaxRichTextBox1.TabIndex = 6;
            this.syntaxRichTextBox1.Text = "";
            // 
            // ambiance_ThemeContainer1
            // 
            this.ambiance_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ambiance_ThemeContainer1.Controls.Add(this.pictureBox5);
            this.ambiance_ThemeContainer1.Controls.Add(this.ambiance_ControlBox1);
            this.ambiance_ThemeContainer1.Controls.Add(this.toolStrip1);
            this.ambiance_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ambiance_ThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ambiance_ThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.ambiance_ThemeContainer1.Name = "ambiance_ThemeContainer1";
            this.ambiance_ThemeContainer1.Padding = new System.Windows.Forms.Padding(20, 56, 20, 16);
            this.ambiance_ThemeContainer1.RoundCorners = false;
            this.ambiance_ThemeContainer1.Sizable = false;
            this.ambiance_ThemeContainer1.Size = new System.Drawing.Size(730, 570);
            this.ambiance_ThemeContainer1.SmartBounds = true;
            this.ambiance_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ambiance_ThemeContainer1.TabIndex = 10;
            this.ambiance_ThemeContainer1.Text = "ambiance_ThemeContainer1";
            // 
            // pictureBox5
            // 
            this.pictureBox5.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.ErrorImage")));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.InitialImage")));
            this.pictureBox5.Location = new System.Drawing.Point(10, 12);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(26, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 45;
            this.pictureBox5.TabStop = false;
            // 
            // ambiance_ControlBox1
            // 
            this.ambiance_ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_ControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            this.ambiance_ControlBox1.Location = new System.Drawing.Point(680, 11);
            this.ambiance_ControlBox1.Name = "ambiance_ControlBox1";
            this.ambiance_ControlBox1.Size = new System.Drawing.Size(44, 23);
            this.ambiance_ControlBox1.TabIndex = 9;
            this.ambiance_ControlBox1.Text = "ambiance_ControlBox1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCodeToolStripButton,
            this.toolStripSeparator1,
            this.exportDtoToolStripButton,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(20, 56);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(690, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // copyCodeToolStripButton
            // 
            this.copyCodeToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.copyCodeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyCodeToolStripButton.ForeColor = System.Drawing.Color.White;
            this.copyCodeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyCodeToolStripButton.Image")));
            this.copyCodeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyCodeToolStripButton.Name = "copyCodeToolStripButton";
            this.copyCodeToolStripButton.Size = new System.Drawing.Size(109, 22);
            this.copyCodeToolStripButton.Text = "Select all and copy";
            this.copyCodeToolStripButton.Click += new System.EventHandler(this.copyCodeToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.Red;
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.White;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // exportDtoToolStripButton
            // 
            this.exportDtoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportDtoToolStripButton.ForeColor = System.Drawing.Color.White;
            this.exportDtoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportDtoToolStripButton.Image")));
            this.exportDtoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportDtoToolStripButton.Name = "exportDtoToolStripButton";
            this.exportDtoToolStripButton.Size = new System.Drawing.Size(44, 22);
            this.exportDtoToolStripButton.Text = "Export";
            this.exportDtoToolStripButton.Click += new System.EventHandler(this.exportDtoToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 570);
            this.Controls.Add(this.syntaxRichTextBox1);
            this.Controls.Add(this.ambiance_ThemeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(261, 65);
            this.Name = "GenerateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.GenerateForm_Load);
            this.ambiance_ThemeContainer1.ResumeLayout(false);
            this.ambiance_ThemeContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private SyntaxHighlighter.SyntaxRichTextBox syntaxRichTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton copyCodeToolStripButton;
        private System.Windows.Forms.ToolStripButton exportDtoToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Ambiance.Ambiance_ThemeContainer ambiance_ThemeContainer1;
        private Ambiance.Ambiance_ControlBox ambiance_ControlBox1;
        private System.Windows.Forms.PictureBox pictureBox5;

    }
}



