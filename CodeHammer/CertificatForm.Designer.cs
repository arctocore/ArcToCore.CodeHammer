namespace CodeHammer
{
    partial class CertificatDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificatDialog));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ambiance_ThemeContainer1 = new Ambiance.Ambiance_ThemeContainer();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.ambiance_ControlBox1 = new Ambiance.Ambiance_ControlBox();
            this.pfxGroupBox = new System.Windows.Forms.GroupBox();
            this.txtPasswordPfx = new System.Windows.Forms.TextBox();
            this.saveAsPfxButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.storeGroupBox = new System.Windows.Forms.GroupBox();
            this.saveStoreButton = new System.Windows.Forms.Button();
            this.cboStoreName = new System.Windows.Forms.ComboBox();
            this.storeLabel = new System.Windows.Forms.Label();
            this.cboStoreLocation = new System.Windows.Forms.ComboBox();
            this.locationLabel = new System.Windows.Forms.Label();
            this.certGroupBox = new System.Windows.Forms.GroupBox();
            this.dtpValidTo = new System.Windows.Forms.DateTimePicker();
            this.dtpValid = new System.Windows.Forms.DateTimePicker();
            this.dateToLabel = new System.Windows.Forms.Label();
            this.dateFromlabel = new System.Windows.Forms.Label();
            this.keySizeLabel = new System.Windows.Forms.Label();
            this.cboKeySize = new System.Windows.Forms.ComboBox();
            this.certLabel = new System.Windows.Forms.Label();
            this.txtDN = new System.Windows.Forms.TextBox();
            this.certTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.ambiance_ThemeContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.pfxGroupBox.SuspendLayout();
            this.storeGroupBox.SuspendLayout();
            this.certGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ambiance_ThemeContainer1
            // 
            this.ambiance_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ambiance_ThemeContainer1.Controls.Add(this.pictureBox5);
            this.ambiance_ThemeContainer1.Controls.Add(this.ambiance_ControlBox1);
            this.ambiance_ThemeContainer1.Controls.Add(this.pfxGroupBox);
            this.ambiance_ThemeContainer1.Controls.Add(this.storeGroupBox);
            this.ambiance_ThemeContainer1.Controls.Add(this.certGroupBox);
            this.ambiance_ThemeContainer1.Controls.Add(this.certTextBox);
            this.ambiance_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ambiance_ThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ambiance_ThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.ambiance_ThemeContainer1.Name = "ambiance_ThemeContainer1";
            this.ambiance_ThemeContainer1.Padding = new System.Windows.Forms.Padding(20, 56, 20, 16);
            this.ambiance_ThemeContainer1.RoundCorners = false;
            this.ambiance_ThemeContainer1.Sizable = false;
            this.ambiance_ThemeContainer1.Size = new System.Drawing.Size(469, 565);
            this.ambiance_ThemeContainer1.SmartBounds = true;
            this.ambiance_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ambiance_ThemeContainer1.TabIndex = 70;
            this.ambiance_ThemeContainer1.Text = "Cert settings";
            // 
            // pictureBox5
            // 
            this.pictureBox5.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.ErrorImage")));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.InitialImage")));
            this.pictureBox5.Location = new System.Drawing.Point(9, 10);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(26, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 71;
            this.pictureBox5.TabStop = false;
            // 
            // ambiance_ControlBox1
            // 
            this.ambiance_ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_ControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            this.ambiance_ControlBox1.Location = new System.Drawing.Point(414, 11);
            this.ambiance_ControlBox1.Name = "ambiance_ControlBox1";
            this.ambiance_ControlBox1.Size = new System.Drawing.Size(46, 23);
            this.ambiance_ControlBox1.TabIndex = 70;
            this.ambiance_ControlBox1.Text = "ambiance_ControlBox1";
            // 
            // pfxGroupBox
            // 
            this.pfxGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pfxGroupBox.Controls.Add(this.txtPasswordPfx);
            this.pfxGroupBox.Controls.Add(this.saveAsPfxButton);
            this.pfxGroupBox.Controls.Add(this.passwordLabel);
            this.pfxGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pfxGroupBox.ForeColor = System.Drawing.Color.White;
            this.pfxGroupBox.Location = new System.Drawing.Point(16, 217);
            this.pfxGroupBox.Name = "pfxGroupBox";
            this.pfxGroupBox.Size = new System.Drawing.Size(437, 88);
            this.pfxGroupBox.TabIndex = 42;
            this.pfxGroupBox.TabStop = false;
            this.pfxGroupBox.Text = "Save as PFX";
            // 
            // txtPasswordPfx
            // 
            this.txtPasswordPfx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtPasswordPfx.Location = new System.Drawing.Point(145, 16);
            this.txtPasswordPfx.Name = "txtPasswordPfx";
            this.txtPasswordPfx.PasswordChar = '*';
            this.txtPasswordPfx.Size = new System.Drawing.Size(278, 22);
            this.txtPasswordPfx.TabIndex = 67;
            // 
            // saveAsPfxButton
            // 
            this.saveAsPfxButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.saveAsPfxButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAsPfxButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.saveAsPfxButton.ForeColor = System.Drawing.Color.White;
            this.saveAsPfxButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAsPfxButton.Image")));
            this.saveAsPfxButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveAsPfxButton.Location = new System.Drawing.Point(279, 44);
            this.saveAsPfxButton.Name = "saveAsPfxButton";
            this.saveAsPfxButton.Size = new System.Drawing.Size(144, 23);
            this.saveAsPfxButton.TabIndex = 45;
            this.saveAsPfxButton.Text = "Save as PFX";
            this.saveAsPfxButton.UseCompatibleTextRendering = true;
            this.saveAsPfxButton.UseVisualStyleBackColor = false;
            this.saveAsPfxButton.Click += new System.EventHandler(this.saveAsPfxButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.Location = new System.Drawing.Point(9, 21);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(70, 14);
            this.passwordLabel.TabIndex = 43;
            this.passwordLabel.Text = "Password:";
            // 
            // storeGroupBox
            // 
            this.storeGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.storeGroupBox.Controls.Add(this.saveStoreButton);
            this.storeGroupBox.Controls.Add(this.cboStoreName);
            this.storeGroupBox.Controls.Add(this.storeLabel);
            this.storeGroupBox.Controls.Add(this.cboStoreLocation);
            this.storeGroupBox.Controls.Add(this.locationLabel);
            this.storeGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.storeGroupBox.ForeColor = System.Drawing.Color.White;
            this.storeGroupBox.Location = new System.Drawing.Point(16, 321);
            this.storeGroupBox.Name = "storeGroupBox";
            this.storeGroupBox.Size = new System.Drawing.Size(437, 112);
            this.storeGroupBox.TabIndex = 68;
            this.storeGroupBox.TabStop = false;
            this.storeGroupBox.Text = "Save to certificate store";
            // 
            // saveStoreButton
            // 
            this.saveStoreButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.saveStoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveStoreButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.saveStoreButton.ForeColor = System.Drawing.Color.White;
            this.saveStoreButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveStoreButton.Location = new System.Drawing.Point(279, 80);
            this.saveStoreButton.Name = "saveStoreButton";
            this.saveStoreButton.Size = new System.Drawing.Size(144, 23);
            this.saveStoreButton.TabIndex = 68;
            this.saveStoreButton.Text = "Save";
            this.saveStoreButton.UseCompatibleTextRendering = true;
            this.saveStoreButton.UseVisualStyleBackColor = false;
            this.saveStoreButton.Click += new System.EventHandler(this.saveStoreButton_Click);
            // 
            // cboStoreName
            // 
            this.cboStoreName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStoreName.FormattingEnabled = true;
            this.cboStoreName.Items.AddRange(new object[] {
            "2048",
            "4096",
            "8192",
            "16384"});
            this.cboStoreName.Location = new System.Drawing.Point(145, 53);
            this.cboStoreName.Name = "cboStoreName";
            this.cboStoreName.Size = new System.Drawing.Size(278, 22);
            this.cboStoreName.TabIndex = 69;
            // 
            // storeLabel
            // 
            this.storeLabel.AutoSize = true;
            this.storeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.storeLabel.Location = new System.Drawing.Point(9, 58);
            this.storeLabel.Name = "storeLabel";
            this.storeLabel.Size = new System.Drawing.Size(45, 14);
            this.storeLabel.TabIndex = 68;
            this.storeLabel.Text = "Store:";
            // 
            // cboStoreLocation
            // 
            this.cboStoreLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStoreLocation.FormattingEnabled = true;
            this.cboStoreLocation.Items.AddRange(new object[] {
            "2048",
            "4096",
            "8192",
            "16384"});
            this.cboStoreLocation.Location = new System.Drawing.Point(145, 19);
            this.cboStoreLocation.Name = "cboStoreLocation";
            this.cboStoreLocation.Size = new System.Drawing.Size(278, 22);
            this.cboStoreLocation.TabIndex = 67;
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.locationLabel.Location = new System.Drawing.Point(9, 24);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(64, 14);
            this.locationLabel.TabIndex = 43;
            this.locationLabel.Text = "Location:";
            // 
            // certGroupBox
            // 
            this.certGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.certGroupBox.Controls.Add(this.dtpValidTo);
            this.certGroupBox.Controls.Add(this.dtpValid);
            this.certGroupBox.Controls.Add(this.dateToLabel);
            this.certGroupBox.Controls.Add(this.dateFromlabel);
            this.certGroupBox.Controls.Add(this.keySizeLabel);
            this.certGroupBox.Controls.Add(this.cboKeySize);
            this.certGroupBox.Controls.Add(this.certLabel);
            this.certGroupBox.Controls.Add(this.txtDN);
            this.certGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.certGroupBox.ForeColor = System.Drawing.Color.White;
            this.certGroupBox.Location = new System.Drawing.Point(16, 46);
            this.certGroupBox.Name = "certGroupBox";
            this.certGroupBox.Size = new System.Drawing.Size(437, 155);
            this.certGroupBox.TabIndex = 41;
            this.certGroupBox.TabStop = false;
            this.certGroupBox.Text = "Cert settings";
            // 
            // dtpValidTo
            // 
            this.dtpValidTo.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dtpValidTo.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dtpValidTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidTo.Location = new System.Drawing.Point(145, 122);
            this.dtpValidTo.Name = "dtpValidTo";
            this.dtpValidTo.Size = new System.Drawing.Size(278, 22);
            this.dtpValidTo.TabIndex = 65;
            // 
            // dtpValid
            // 
            this.dtpValid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dtpValid.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValid.Location = new System.Drawing.Point(145, 88);
            this.dtpValid.Name = "dtpValid";
            this.dtpValid.Size = new System.Drawing.Size(278, 22);
            this.dtpValid.TabIndex = 66;
            // 
            // dateToLabel
            // 
            this.dateToLabel.AutoSize = true;
            this.dateToLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dateToLabel.Location = new System.Drawing.Point(8, 127);
            this.dateToLabel.Name = "dateToLabel";
            this.dateToLabel.Size = new System.Drawing.Size(58, 14);
            this.dateToLabel.TabIndex = 63;
            this.dateToLabel.Text = "Valid to:";
            // 
            // dateFromlabel
            // 
            this.dateFromlabel.AutoSize = true;
            this.dateFromlabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dateFromlabel.Location = new System.Drawing.Point(8, 94);
            this.dateFromlabel.Name = "dateFromlabel";
            this.dateFromlabel.Size = new System.Drawing.Size(70, 14);
            this.dateFromlabel.TabIndex = 64;
            this.dateFromlabel.Text = "Vald from:";
            // 
            // keySizeLabel
            // 
            this.keySizeLabel.AutoSize = true;
            this.keySizeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.keySizeLabel.Location = new System.Drawing.Point(8, 57);
            this.keySizeLabel.Name = "keySizeLabel";
            this.keySizeLabel.Size = new System.Drawing.Size(59, 14);
            this.keySizeLabel.TabIndex = 62;
            this.keySizeLabel.Text = "Key size:";
            // 
            // cboKeySize
            // 
            this.cboKeySize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeySize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.cboKeySize.FormattingEnabled = true;
            this.cboKeySize.Items.AddRange(new object[] {
            "384",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384"});
            this.cboKeySize.Location = new System.Drawing.Point(145, 54);
            this.cboKeySize.Name = "cboKeySize";
            this.cboKeySize.Size = new System.Drawing.Size(278, 22);
            this.cboKeySize.TabIndex = 61;
            // 
            // certLabel
            // 
            this.certLabel.AutoSize = true;
            this.certLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.certLabel.Location = new System.Drawing.Point(9, 23);
            this.certLabel.Name = "certLabel";
            this.certLabel.Size = new System.Drawing.Size(132, 14);
            this.certLabel.TabIndex = 60;
            this.certLabel.Tag = "cn=";
            this.certLabel.Text = "X.500 Name:      cn=";
            // 
            // txtDN
            // 
            this.txtDN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtDN.Location = new System.Drawing.Point(145, 19);
            this.txtDN.Name = "txtDN";
            this.txtDN.Size = new System.Drawing.Size(278, 22);
            this.txtDN.TabIndex = 59;
            // 
            // certTextBox
            // 
            this.certTextBox.AutoWordSelection = true;
            this.certTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.certTextBox.BulletIndent = 1;
            this.certTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.certTextBox.ForeColor = System.Drawing.Color.White;
            this.certTextBox.Location = new System.Drawing.Point(9, 442);
            this.certTextBox.Name = "certTextBox";
            this.certTextBox.ReadOnly = true;
            this.certTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.certTextBox.Size = new System.Drawing.Size(450, 114);
            this.certTextBox.TabIndex = 69;
            this.certTextBox.Text = "";
            // 
            // CertificatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(469, 565);
            this.Controls.Add(this.ambiance_ThemeContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(261, 65);
            this.Name = "CertificatDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cert settings";
            this.toolTip.SetToolTip(this, "Certificate setup");
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CertificatForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ambiance_ThemeContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.pfxGroupBox.ResumeLayout(false);
            this.pfxGroupBox.PerformLayout();
            this.storeGroupBox.ResumeLayout(false);
            this.storeGroupBox.PerformLayout();
            this.certGroupBox.ResumeLayout(false);
            this.certGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox certGroupBox;
        private System.Windows.Forms.TextBox txtDN;
        private System.Windows.Forms.Label certLabel;
        private System.Windows.Forms.DateTimePicker dtpValidTo;
        private System.Windows.Forms.DateTimePicker dtpValid;
        private System.Windows.Forms.Label dateToLabel;
        private System.Windows.Forms.Label dateFromlabel;
        private System.Windows.Forms.Label keySizeLabel;
        private System.Windows.Forms.ComboBox cboKeySize;
        private System.Windows.Forms.GroupBox pfxGroupBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button saveAsPfxButton;
        private System.Windows.Forms.TextBox txtPasswordPfx;
        private System.Windows.Forms.GroupBox storeGroupBox;
        private System.Windows.Forms.ComboBox cboStoreLocation;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.ComboBox cboStoreName;
        private System.Windows.Forms.Label storeLabel;
        private System.Windows.Forms.Button saveStoreButton;
        private System.Windows.Forms.RichTextBox certTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTip;
        private Ambiance.Ambiance_ThemeContainer ambiance_ThemeContainer1;
        private Ambiance.Ambiance_ControlBox ambiance_ControlBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}