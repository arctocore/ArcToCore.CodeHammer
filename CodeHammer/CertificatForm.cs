/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer
{
    using CodeHammer.Crypto;
    using CodeHammer.Entities;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Linq;

    /// <summary>
    /// this class Certificat
    /// </summary>
    public partial class CertificatDialog : Form
    {
        #region Variables

        /// <summary>
        /// Lock around target and currentCount
        /// </summary>
        private readonly object stateLock = new object();

        /// <summary>
        /// The target
        /// </summary>
        private int target;

        /// <summary>
        /// The RNG
        /// </summary>
        private Random rng = new Random();

        /// <summary>
        /// Gets or sets the cert store location.
        /// </summary>
        /// <value>
        /// The cert store location.
        /// </value>
        private StoreLocation CertStoreLocation { get; set; }

        /// <summary>
        /// Gets or sets the name of the cert store.
        /// </summary>
        /// <value>
        /// The name of the cert store.
        /// </value>
        private StoreName CertStoreName { get; set; }

        /// <summary>
        /// Gets or sets the certificate.
        /// </summary>
        /// <value>
        /// The certificate.
        /// </value>
        private X509Certificate2 Certificate { get; set; }

        /// <summary>
        /// Gets the current thread identifier.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern int GetCurrentThreadId();

        /// <summary>
        /// Opens the thread.
        /// </summary>
        /// <param name="desiredAccess">The desired access.</param>
        /// <param name="inheritHandle">if set to <c>true</c> [inherit handle].</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr OpenThread(int desiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inheritHandle, int threadId);

        /// <summary>
        /// Terminates the thread.
        /// </summary>
        /// <param name="hThread">The h thread.</param>
        /// <param name="exitCode">The exit code.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TerminateThread(IntPtr hThread, int exitCode);

        /// <summary>
        /// Closes the handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Gets or sets the cert properties.
        /// </summary>
        /// <value>
        /// The cert properties.
        /// </value>
        private SelfSignedCertProperties CertProperties { get; set; }

        /// <summary>
        /// The background worker running
        /// </summary>
        private bool backgroundWorkerRunning;

        /// <summary>
        /// The background thread identifier
        /// </summary>
        private volatile int backgroundThreadId;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificatDialog"/> class.
        /// </summary>
        public CertificatDialog()
        {
            InitializeComponent();

           // LoadSettings();
            LoadStoreDropdownLists();
        }

        #region Methods

        /// <summary>
        /// Loads the store dropdown lists.
        /// </summary>
        private void LoadStoreDropdownLists()
        {
            cboStoreLocation.Items.Clear();
            foreach (StoreLocation storeLocation in Enum.GetValues(typeof(StoreLocation)))
            {
                int index = cboStoreLocation.Items.Add(storeLocation);
                if (StoreLocation.LocalMachine == storeLocation)
                    cboStoreLocation.SelectedIndex = index;
            }

            cboStoreName.Items.Clear();
            foreach (StoreName storeName in Enum.GetValues(typeof(StoreName)))
            {
                int index = cboStoreName.Items.Add(storeName);
                if (StoreName.My == storeName)
                    cboStoreName.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            LoadDefaultSettings();

            XDocument doc = null;
            try
            {
                doc = XDocument.Load(SettingsFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (null != doc)
                LoadSettings(doc);
        }

        /// <summary>
        /// Loads the default settings.
        /// </summary>
        private void LoadDefaultSettings()
        {
            try
            {
                DateTime today = DateTime.Today;

                txtDN.Text = "localhost";
                cboKeySize.Text = "4096";
                dtpValid.Value = today.AddDays(-7); // just to be safe
                dtpValidTo.Value = today.AddYears(10);
                //chkExportablePrivateKey.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSettings(XDocument doc)
        {
            try
            {
                txtDN.Text = GetSetting(doc, "dn", "");
                cboKeySize.Text = GetSetting(doc, "keySize", "4096");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        private string GetSetting(XDocument doc, string elementName, string defaultValue)
        {
            XElement dnElement = doc.Root.Element(elementName);
            return (null != dnElement) ? dnElement.Value : defaultValue;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            XDocument doc = new XDocument(
                new XElement("CodeHammerSettings",
                    new XElement("dn", txtDN.Text),
                    new XElement("keySize", cboKeySize.Text),
                    new XElement("exportPrivateKey", true)
                    ));
            try
            {
                doc.Save(SettingsFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets the settings file.
        /// </summary>
        /// <value>
        /// The settings file.
        /// </value>
        private string SettingsFile
        {
            get
            {
                return Path.Combine(Application.LocalUserAppDataPath, "CodeHammerSettings.xml");
            }
        }

        /// <summary>
        /// Validates the cert properties.
        /// </summary>
        /// <returns></returns>
        private bool ValidateCertProperties(out string error)
        {
            error = string.Empty;
            if (!ValidateDN(out error))
            {
                txtDN.SelectAll();
                txtDN.Focus();
                return false;
            }
            if (!ValidateKeySize())
            {
                cboKeySize.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Generates the cert.
        /// </summary>
        /// <returns></returns>
        private X509Certificate2 GenerateCert()
        {
            try
            {
                certTextBox.AppendText("\nGenerating certificate please wait.");

                // use a form to initiate a cancellable background worker
                // BackgroundCertGenForm form = new BackgroundCertGenForm();
                CertProperties = new SelfSignedCertProperties()
                {
                    Name = new X500DistinguishedName(txtDN.Text),
                    ValidFrom = dtpValid.Value,
                    ValidTo = dtpValidTo.Value,
                    KeyBitLength = int.Parse(cboKeySize.Text),
                    IsPrivateKeyExportable = true,
                };

                using (CryptContext ctx = new CryptContext())
                {
                    ctx.Open();

                    Certificate = ctx.CreateSelfSignedCertificate(CertProperties);
                }

                Thread.Sleep(5000);

                certTextBox.AppendText("\nDone generating certificate.");
                return Certificate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private bool ValidateDN(out string error)
        {
            error = string.Empty;
            try
            {
                new X500DistinguishedName(txtDN.Text);
                errorProvider.SetError(txtDN, "");
                return true;
            }
            catch (CryptographicException x)
            {
                error = "Error: " + txtDN + " " + x.Message;
                errorProvider.SetError(txtDN, x.Message);
            }
            return false;
        }

        private bool ValidateKeySize()
        {
            string errorMsg = "";

            int keySize;
            if (int.TryParse(cboKeySize.Text, out keySize))
            {
                switch (keySize)
                {
                    case 384:
                    case 512:
                    case 1024:
                    case 2048:
                    case 4096:
                    case 8192:
                    case 16384:
                        break;

                    default:
                        errorMsg = "Invalid key size.";
                        break;
                }
            }
            else errorMsg = "Key size must be an int value.";
            errorProvider.SetError(cboKeySize, errorMsg);

            return "" == errorMsg;
        }

        /// <summary>
        /// Backrounds the worker finished.
        /// </summary>
        private void BackroundWorkerFinished()
        {
            backgroundWorkerRunning = false;
            Close();
        }

        private void GeneratePFX()
        {
            try
            {
                txtDN.Text = certLabel.Tag.ToString() + txtDN.Text;
                certTextBox.Clear();
                string error = string.Empty;
                if (!ValidateCertProperties(out error))
                {
                    certTextBox.Text = error;
                    certGroupBox.Enabled = true;
                    pfxGroupBox.Enabled = true;
                    storeGroupBox.Enabled = true;
                  
                    return;
                }

                X509Certificate2 cert = GenerateCert();
                if (null == cert)
                    return; // user must have cancelled the operation

                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = "PFX file (*.pfx)|*.pfx";

                if (DialogResult.OK == fileDialog.ShowDialog(this))
                {
                    using (Stream outputStream = File.Create(fileDialog.FileName))
                    {
                        byte[] pfx = cert.Export(X509ContentType.Pfx, txtPasswordPfx.Text.Length > 0 ? txtPasswordPfx.Text : null);
                        outputStream.Write(pfx, 0, pfx.Length);
                        outputStream.Close();
                    }

                    certTextBox.AppendText("\nCodeHammer successfully saved a signed certificate to " + Path.GetFileName(fileDialog.FileName));
                }

                certGroupBox.Enabled = true;
                pfxGroupBox.Enabled = true;
                storeGroupBox.Enabled = true;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Generates the cert.
        /// </summary>
        private void GenerateCERT()
        {
            try { 
            certTextBox.Clear();
            X509Store store = new X509Store((StoreName)cboStoreName.SelectedItem, (StoreLocation)cboStoreLocation.SelectedItem);
            store.Open(OpenFlags.ReadWrite);

            X509Certificate2 cert = GenerateCert();
            if (null != cert)
            {
                // I've not been able to figure out what property isn't getting copied into the store,
                // but IIS can't find the private key when I simply add the cert directly to the store
                // in this fashion:  store.Add(cert);
                // The extra two lines of code here does seem to make IIS happy though.
                // I got this idea from here: http://www.derkeiler.com/pdf/Newsgroups/microsoft.public.inetserver.iis.security/2008-03/msg00020.pdf
                //  (written by David Wang at blogs.msdn.com/David.Wang)
                byte[] pfx = cert.Export(X509ContentType.Pfx);
                cert = new X509Certificate2(pfx, (string)null, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                // NOTE: it's not clear to me at this point if this will work if you want to save to StoreLocation.CurrentUser
                //       given that there's also an X509KeyStorageFlags.UserKeySet. That could be DPAPI related though, and not cert store related.

                store.Add(cert);
            }
            store.Close();

            if (null != cert)
            {
                CertInformation.Instance().StoreLocation = cboStoreLocation.SelectedItem.ToString();
                CertInformation.Instance().StoreName = cboStoreName.SelectedItem.ToString();
                CertInformation.Instance().X509FindType = "FindBySubjectName";
                string fv = txtDN.Text.Replace("cn=", string.Empty).Trim();
                CertInformation.Instance().FindValue = fv;

                Certificate = cert;
                CertStoreLocation = (StoreLocation)cboStoreLocation.SelectedItem;
                CertStoreName = (StoreName)cboStoreName.SelectedItem;

                certTextBox.AppendText("\nCertificate FriendlyName: " + cert.SubjectName.Name);
                certTextBox.AppendText("\nStoreLocation: " + CertStoreLocation.ToString());
                certTextBox.AppendText("\nStoreName: " + CertStoreName.ToString());

                saveAsPfxButton.Enabled = true;

                //new CertificatForm
                //{
                //    Certificate = cert,
                //    CertStoreLocation = (StoreLocation)cboStoreLocation.SelectedItem,
                //    CertStoreName = (StoreName)cboStoreName.SelectedItem,
                //}.ShowDialog();
            }

            certGroupBox.Enabled = true;
            pfxGroupBox.Enabled = true;
            storeGroupBox.Enabled = true;

        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion Methods

        /// <summary>
        /// Handles the Click event of the saveAsPfxButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveAsPfxButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPasswordPfx.Text))
            {
                MessageBox.Show("Password cannot be empty!");
                return;
            }

            try { 
            certGroupBox.Enabled = false;
            pfxGroupBox.Enabled = false;
            storeGroupBox.Enabled = false;

           

            lock (stateLock)
            {
                target = rng.Next(100);
            }
            Thread t = new Thread(new ThreadStart(GeneratePFX));
            t.IsBackground = true;
            t.Start();
            t.Join(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the saveStoreButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveStoreButton_Click(object sender, EventArgs e)
        {
            try { 
            certGroupBox.Enabled = false;
            pfxGroupBox.Enabled = false;
            storeGroupBox.Enabled = false;

         

            lock (stateLock)
            {
                target = rng.Next(100);
            }
            Thread t = new Thread(new ThreadStart(GenerateCERT));
            t.IsBackground = true;
            t.Start();
            t.Join(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            backgroundThreadId = GetCurrentThreadId();

            using (CryptContext ctx = new CryptContext())
            {
                ctx.Open();

                Certificate = ctx.CreateSelfSignedCertificate(CertProperties);
            }

            BeginInvoke(new Action(BackroundWorkerFinished), null);
        }

        private void CertificatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (backgroundWorkerRunning)
            {
                // do our best to whack the background thread
                // note that this would be a very bad idea in a long-lived server process,
                // but in a client app, it's a nice way to give the user back her CPU.
                IntPtr hThread = OpenThread(1, false, backgroundThreadId);
                if (IntPtr.Zero != hThread)
                {
                    TerminateThread(hThread, 0);
                    CloseHandle(hThread);
                }
                this.Close();
            }
        }
    }
}