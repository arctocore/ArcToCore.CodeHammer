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
    using Ambiance;
    using CodeGen.Framework.FunctionArea.Dgml;
    using CodeHammer.Entities;
    using CodeHammer.Framework;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Generators;
    using CodeHammer.Framework.FunctionArea.IndentTextWriter;
    using CodeHammer.Framework.FunctionArea.Log;
    using CodeHammer.Framework.FunctionArea.ProjectManager;
    using CodeHammerHiddenCheckBoxTreeNodeNameSpace;
    using FactoryInstaller;
    using Microsoft.Win32;
    using Ninject;
    using Presentation.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Form used to collect the connection information for the code we're going to generate.
    /// </summary>

    public partial class CodeHammerForm : Form
    {
        #region Variables

        /// <summary>
        /// The fileType
        /// </summary>
        private string fileType = string.Empty;

        /// <summary>
        /// The button tool tip
        /// </summary>
        private ToolTip buttonToolTip = new ToolTip();

        /// <summary>
        /// The only dto
        /// </summary>
        private bool onlyDTO = false;

        /// <summary>
        /// The only data contract
        /// </summary>
        private bool onlyDataContract = false;

        private bool onlyDependencyDTO = false;

        /// <summary>
        /// The bg color
        /// </summary>
        private string bgColor = "black";

        /// <summary>
        /// The current node matches
        /// </summary>
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        /// <summary>
        /// The last node index
        /// </summary>
        private int LastNodeIndex = 0;

        /// <summary>
        /// The last search text
        /// </summary>
        private string LastSearchText;

        /// <summary>
        /// The string generator message
        /// </summary>
        private StringBuilder strGeneratorMessage = new StringBuilder();

        /// <summary>
        /// The string castle windsor
        /// </summary>
        private StringBuilder strCastleWindsor = new StringBuilder();

        /// <summary>
        /// the string data access
        /// </summary>
        private StringBuilder strDataAccess = new StringBuilder();

        /// <summary>
        /// The string fluent n hibernate
        /// </summary>
        private StringBuilder strFluentNHibernate = new StringBuilder();

        /// <summary>
        /// The string URL
        /// </summary>
        private StringBuilder strUrl = new StringBuilder();

        /// <summary>
        /// The SLN
        /// </summary>
        private string sln = string.Empty;

        /// <summary>
        /// The SQL
        /// </summary>
        private string sql = string.Empty;

        /// <summary>
        /// The container
        /// </summary>
        private string container = string.Empty;

        /// <summary>
        /// Lock around target and currentCount
        /// </summary>
        private readonly object stateLock = new object();

        // That's our custom TextWriter class
        private TextWriter _writer = null;

        /// <summary>
        /// The authentication group box
        /// </summary>
        private System.Windows.Forms.GroupBox authenticationGroupBox;

        private ToolStripMenuItem blackBackgroundToolStripMenuItem1;

        /// <summary>
        /// The check all tool strip menu item
        /// </summary>
        private ToolStripMenuItem checkAllToolStripMenuItem;

        /// <summary>
        /// The collapse all tool strip menu item
        /// </summary>
        private ToolStripMenuItem collapseAllToolStripMenuItem;

        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// The connect button
        /// </summary>
        private Button connectButton;

        /// <summary>
        /// The crud tab page
        /// </summary>
        private TabPage crudTabPage;

        /// <summary>
        /// The database ComboBox
        /// </summary>
        private ComboBoxReadOnly databaseComboBox;

        /// <summary>
        /// The database label
        /// </summary>
        private System.Windows.Forms.Label databaseLabel;

        private TextBox dbServerTextBox;
        private CheckBox executeStoredprocedureCheckBox;

        /// <summary>
        /// The expand all tool strip menu item
        /// </summary>
        private ToolStripMenuItem expandAllToolStripMenuItem;

        /// <summary>
        /// The export path
        /// </summary>
        private string exportPath = string.Empty;

        private Button generateButton;
        private CheckBox gridCheckBox;
        private TabPage iocTabPage;

        /// <summary>
        /// The login name label
        /// </summary>
        private System.Windows.Forms.Label loginNameLabel;

        /// <summary>
        /// The login name text box
        /// </summary>
        private System.Windows.Forms.TextBox loginNameTextBox;

        private CodeHammerHiddenCheckBoxTreeNodeNameSpace.CodeHammerMixedCheckBoxesTreeView mixedCheckBoxesTreeView1;

        private Ambiance_TabControl optionsTabControl;
        private string outputDirectory;
        private RichTextBox outputTextBox;

        /// <summary>
        /// The password label
        /// </summary>
        private System.Windows.Forms.Label passwordLabel;

        /// <summary>
        /// The password text box
        /// </summary>
        private System.Windows.Forms.TextBox passwordTextBox;

        /// <summary>
        /// The reset button
        /// </summary>
        private Button ResetButton;

        /// <summary>
        /// The result data options
        /// </summary>
        private List<string> resultDataOptions = new List<string>();

        /// <summary>
        /// The reverse enginner select SP
        /// </summary>
        private string reverseEnginnerSelectSP = string.Empty;

        /// <summary>
        /// The RNG
        /// </summary>
        private Random rng = new Random();

        /// <summary>
        /// The select all button
        /// </summary>
        private Button SelectAllButton;

        /// <summary>
        /// The select tables
        /// </summary>
        private bool selectTables = false;

        /// <summary>
        /// The server label
        /// </summary>
        private System.Windows.Forms.Label serverLabel;

        /// <summary>
        /// The show tables button
        /// </summary>
        private Button ShowTablesButton;

        private SplitContainer splitContainer1;

        /// <summary>
        /// The table menu strip
        /// </summary>
        private ContextMenuStrip tableMenuStrip;

        /// <summary>
        /// The target
        /// </summary>
        private int target;

        private ToolStripSeparator toolStripSeparator1;

        /// <summary>
        /// The uncheck all tool strip menu item
        /// </summary>
        private ToolStripMenuItem uncheckAllToolStripMenuItem;

        private ToolStripMenuItem whiteBackgroundToolStripMenuItem;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Panel panel1;
        private RadioButton fluentNHibernateRadioButton;
        private Button generatorButton;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private GroupBox groupBox4;
        private ImageList imageList1;
        private CheckBox crudCheckBox;
        private PictureBox pictureBox9;
        private GroupBox groupBox6;
        private PictureBox pictureBox8;
        private GroupBox wcfThrottlingGroupBox;
        private PictureBox pictureBox10;
        private ToolStripLabel toolStripLabel4;
        private ToolStripLabel toolStripLabel5;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel toolStripLabel1;
        private ToolStripMenuItem generateDTOToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem generateDtoToolStripMenuItem1;
        private TabPage prefixClassestabPage;
        private GroupBox groupBox3;
        private PictureBox pictureBox6;
        private TextBox dalTextBox;
        private Label label3;
        private TextBox blSuffixTextBox;
        private Label label2;
        private TextBox dtoTextBox;
        private Label label4;
        private TextBox dataContractTextBox;
        private Label label5;
        private TextBox serviceContractTextBox;
        private Label label6;
        private TextBox serviceTextBox;
        private Label label7;
        private Button generateSuffixButton;
        private TextBox solutionTextBox;
        private Label label8;
        private TextBox sqlPrefixTextBox;
        private Label label1;
        private GroupBox groupBox5;
        private PictureBox pictureBox12;
        private ToolStripMenuItem viewTableForeignkeysToolStripMenuItem;
        private ToolStripMenuItem generateDependencyDtoToolStripMenuItem2;
        private TextBox testTextBox;
        private Label label9;
        private CheckBox clearFolderCheckBox;
        private ComboBox iocComboBox;
        private ComboBox wcfCallComboBox;
        private ComboBox unitTestComboBox;
        private ComboBox loggingComboBox;
        private ComboBox wcfPerformanceComboBox;
        private ComboBox sqlOrmComboBox;
        private GroupBox groupBox7;
        private ComboBox streamTypeComboBox;
        private PictureBox pictureBox13;
        private GroupBox groupBox8;
        private ComboBox wcfSecurityComboBox;
        private PictureBox pictureBox14;
        private ToolStripMenuItem generateDataContractToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel9;
        private ToolStripLabel toolStripLabel10;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripLabel toolStripLabel11;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripLabel toolStripLabel12;

        private PictureBox pictureBox7;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value">The value.</param>
        private delegate void StringParameterDelegate(string value);

        /// <summary>
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The indent text writer function contract
        /// </summary>
        private IndentTextWriterFuncContract indentTextWriterFuncContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The code hammer generator contract
        /// </summary>
        private CodeHammerGeneratorContract codeHammerGeneratorContract = null;

        /// <summary>
        /// The database data support adapter contract
        /// </summary>
        private DbDataSupportAdapterContract dbDataSupportAdapterContract = null;

        /// <summary>
        /// The DGML function contract
        /// </summary>
        private DgmlFuncContract dgmlFuncContract = null;

        /// <summary>
        /// The help manager contract
        /// </summary>
        private HelpManagerContract helpManagerContract = null;

        /// <summary>
        /// The project manager contract
        /// </summary>
        private ProjectManagerContract projectManagerContract = null;

        private Label label10;
        private ComboBoxReadOnly providerComboBox;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private GroupBox groupBox9;
        private CheckBox emptyDataLayerCheckBox;
        private Ambiance_ThemeContainer ambiance_ThemeContainer1;
        private Ambiance_ControlBox ambiance_ControlBox1;
        private PictureBox pictureBox5;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel licenseTimeLeftLabel;

        /// <summary>
        /// The container
        /// </summary>
        private IKernel kernelContainer;

        #endregion Variables

        #region Form

        /// <summary>
        /// Configures the container.
        /// </summary>
        private void ConfigureContainer()
        {
            this.kernelContainer = new StandardKernel(new InjectionModuleFactory());
            funcTypeFactoryContract = kernelContainer.Get<FuncTypeFactoryContract>();
            projectManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.PROJECTMANAGERCONTRACT);
            dgmlFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.DGMLFUNCCONTRACT);
            helpManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.HELPMANAGERCONTRACT);
            dbDataSupportAdapterContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.DBDATASUPPORTADAPTER);
            codeHammerGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERGENERATORCONTRACT);
            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            indentTextWriterFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.INDENTTEXTWRITERFUNCCONTRACT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerForm" /> class.
        /// </summary>
        public CodeHammerForm()
        {
            InitializeComponent();
            ConfigureContainer();
            iocComboBox.SelectedIndex = 0;

            wcfCallComboBox.SelectedIndex = 0;

            unitTestComboBox.SelectedIndex = 0;

            loggingComboBox.SelectedIndex = 0;

            wcfPerformanceComboBox.SelectedIndex = 0;

            streamTypeComboBox.SelectedIndex = 0;

            sqlOrmComboBox.SelectedIndex = 0;

            wcfSecurityComboBox.SelectedIndex = 0;

            outputTextBox.Text = "Database not connected";
            EnableControls(false);

            ioManagerContract.InstanceCall = "InstanceContextMode.PerCall";
            container = "castle";
            string systemMessage = string.Empty;

            // Load settings from the Registry
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\CodeHammer"))
            {
                loginNameTextBox.Text = (string)registryKey.GetValue("Login", String.Empty);
                dbServerTextBox.Text = (string)registryKey.GetValue("DBServer", String.Empty);

                blSuffixTextBox.Text = (string)registryKey.GetValue("blSuffixTextBox", String.Empty);
                dalTextBox.Text = (string)registryKey.GetValue("dalTextBox", String.Empty);
                dtoTextBox.Text = (string)registryKey.GetValue("dtoTextBox", String.Empty);
                dataContractTextBox.Text = (string)registryKey.GetValue("dataContractTextBox", String.Empty);
                serviceContractTextBox.Text = (string)registryKey.GetValue("serviceContractTextBox", String.Empty);
                serviceTextBox.Text = (string)registryKey.GetValue("serviceTextBox", String.Empty);
                solutionTextBox.Text = (string)registryKey.GetValue("solutionTextBox", String.Empty);
                sqlPrefixTextBox.Text = (string)registryKey.GetValue("sqlPrefixTextBox", String.Empty);
                solutionTextBox.Text = (string)registryKey.GetValue("solutionTextBox", String.Empty);
                testTextBox.Text = (string)registryKey.GetValue("testTextBox", String.Empty);

                if (((int)registryKey.GetValue("CreateMultipleFiles", 0)) == 1)
                {
                    ////autoExecuteSpToDbCheckBox.Checked = false;
                }

                providerComboBox.Items.Add("Choose provider");
                providerComboBox.Items.Add(ioManagerContract.GetSqlClient);
                // providerComboBox.Items.Add(ioManagerContract.GetOracleClient);

                providerComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(new CodeHammerForm());
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion Form

        #region Events

        /// <summary>
        /// Handles the Click event of the sendFeedbackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void sendFeedbackButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codehammer.eu/Contact-Us");
        }

        /// <summary>
        /// Handles the Click event of the generateDependencyDtoToolStripMenuItem2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void generateDependencyDtoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GenerateDependencyDTO();
        }

        /// <summary>
        /// Handles the Click event of the generateDtoToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void generateDtoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateDTO();
        }

        private void generateDataContractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDataContract();
        }

        private void viewTableForeignkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateTableDependencyDgml();
        }

        /// <summary>
        /// Handles the Click event of the blackBackgroundToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void blackBackgroundToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mixedCheckBoxesTreeView1.BackColor = Color.Black;
            mixedCheckBoxesTreeView1.ForeColor = Color.White;

            bgColor = "black";
        }

        /// <summary>
        /// Handles the Click event of the checkAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll();
        }

        /// <summary>
        /// Handles the Click event of the collapseAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mixedCheckBoxesTreeView1.CollapseAll();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            ConnectToDbServer();
        }

        /// <summary>
        /// Handles the KeyPress event of the databaseComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void databaseComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the DatabaseTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void DatabaseTextBox_TextChanged(object sender, System.EventArgs e)
        {
            //EnableGenerateButton();
        }

        /// <summary>
        /// Handles the Click event of the expandAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mixedCheckBoxesTreeView1.ExpandAll();
        }

        /// <summary>
        /// Handles the Click event of the GenerateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void GenerateButton_Click(object sender, System.EventArgs e)
        {
            // GenerateCodeHammer();
        }

        /// <summary>
        /// gridCheckBox_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            resultDataOptions.Clear();
            resultDataOptions.Add(gridCheckBox.Text);
            if (resultDataOptions.Count == 0)
            {
                this.generateButton.Enabled = false;
                this.generateSuffixButton.Enabled = false;
                this.generatorButton.Enabled = false;
            }
            else
            {
                this.generateButton.Enabled = true;
                this.generateSuffixButton.Enabled = true;
                this.generatorButton.Enabled = true;
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify the contents of this method with
        /// the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeHammerForm));
            this.tableMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateDtoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateDataContractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateDependencyDtoToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTableForeignkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackBackgroundToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.generateDTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.generateButton = new System.Windows.Forms.Button();
            this.ambiance_ThemeContainer1 = new Ambiance.Ambiance_ThemeContainer();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.ambiance_ControlBox1 = new Ambiance.Ambiance_ControlBox();
            this.optionsTabControl = new Ambiance.Ambiance_TabControl();
            this.crudTabPage = new System.Windows.Forms.TabPage();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.authenticationGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.emptyDataLayerCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.providerComboBox = new Presentation.Controls.ComboBoxReadOnly();
            this.panel1 = new System.Windows.Forms.Panel();
            this.executeStoredprocedureCheckBox = new System.Windows.Forms.CheckBox();
            this.dbServerTextBox = new System.Windows.Forms.TextBox();
            this.databaseComboBox = new Presentation.Controls.ComboBoxReadOnly();
            this.connectButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.loginNameTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.serverLabel = new System.Windows.Forms.Label();
            this.ShowTablesButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginNameLabel = new System.Windows.Forms.Label();
            this.crudCheckBox = new System.Windows.Forms.CheckBox();
            this.clearFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.gridCheckBox = new System.Windows.Forms.CheckBox();
            this.prefixClassestabPage = new System.Windows.Forms.TabPage();
            this.generateSuffixButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sqlPrefixTextBox = new System.Windows.Forms.TextBox();
            this.testTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.serviceContractTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataContractTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtoTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dalTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.blSuffixTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.iocTabPage = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.sqlOrmComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.wcfSecurityComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.iocComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.streamTypeComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.wcfCallComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.unitTestComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.loggingComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.wcfThrottlingGroupBox = new System.Windows.Forms.GroupBox();
            this.wcfPerformanceComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fluentNHibernateRadioButton = new System.Windows.Forms.RadioButton();
            this.generatorButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mixedCheckBoxesTreeView1 = new CodeHammerHiddenCheckBoxTreeNodeNameSpace.CodeHammerMixedCheckBoxesTreeView();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.licenseTimeLeftLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.tableMenuStrip.SuspendLayout();
            this.ambiance_ThemeContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.optionsTabControl.SuspendLayout();
            this.crudTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.authenticationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.prefixClassestabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.iocTabPage.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.wcfThrottlingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMenuStrip
            // 
            this.tableMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.generateDtoToolStripMenuItem1,
            this.generateDataContractToolStripMenuItem,
            this.generateDependencyDtoToolStripMenuItem2,
            this.viewTableForeignkeysToolStripMenuItem,
            this.blackBackgroundToolStripMenuItem1,
            this.whiteBackgroundToolStripMenuItem});
            this.tableMenuStrip.Name = "tableMenuStrip";
            this.tableMenuStrip.Size = new System.Drawing.Size(225, 230);
            this.tableMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.tableMenuStrip_Opening);
            this.tableMenuStrip.Click += new System.EventHandler(this.tableMenuStrip_Click);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.checkAllToolStripMenuItem.Text = "Check all";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.uncheckAllToolStripMenuItem.Text = "Uncheck all";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse all";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // generateDtoToolStripMenuItem1
            // 
            this.generateDtoToolStripMenuItem1.Name = "generateDtoToolStripMenuItem1";
            this.generateDtoToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.generateDtoToolStripMenuItem1.Text = "Generate Domain";
            this.generateDtoToolStripMenuItem1.Click += new System.EventHandler(this.generateDtoToolStripMenuItem1_Click);
            // 
            // generateDataContractToolStripMenuItem
            // 
            this.generateDataContractToolStripMenuItem.Name = "generateDataContractToolStripMenuItem";
            this.generateDataContractToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.generateDataContractToolStripMenuItem.Text = "Generate DataContract";
            this.generateDataContractToolStripMenuItem.Click += new System.EventHandler(this.generateDataContractToolStripMenuItem_Click);
            // 
            // generateDependencyDtoToolStripMenuItem2
            // 
            this.generateDependencyDtoToolStripMenuItem2.Name = "generateDependencyDtoToolStripMenuItem2";
            this.generateDependencyDtoToolStripMenuItem2.Size = new System.Drawing.Size(224, 22);
            this.generateDependencyDtoToolStripMenuItem2.Text = "Generate denpency Domain ";
            this.generateDependencyDtoToolStripMenuItem2.Visible = false;
            this.generateDependencyDtoToolStripMenuItem2.Click += new System.EventHandler(this.generateDependencyDtoToolStripMenuItem2_Click);
            // 
            // viewTableForeignkeysToolStripMenuItem
            // 
            this.viewTableForeignkeysToolStripMenuItem.Name = "viewTableForeignkeysToolStripMenuItem";
            this.viewTableForeignkeysToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.viewTableForeignkeysToolStripMenuItem.Text = "View table dependencies";
            this.viewTableForeignkeysToolStripMenuItem.Click += new System.EventHandler(this.viewTableForeignkeysToolStripMenuItem_Click);
            // 
            // blackBackgroundToolStripMenuItem1
            // 
            this.blackBackgroundToolStripMenuItem1.Name = "blackBackgroundToolStripMenuItem1";
            this.blackBackgroundToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.blackBackgroundToolStripMenuItem1.Text = "Black background";
            this.blackBackgroundToolStripMenuItem1.Visible = false;
            this.blackBackgroundToolStripMenuItem1.Click += new System.EventHandler(this.blackBackgroundToolStripMenuItem1_Click);
            // 
            // whiteBackgroundToolStripMenuItem
            // 
            this.whiteBackgroundToolStripMenuItem.Name = "whiteBackgroundToolStripMenuItem";
            this.whiteBackgroundToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.whiteBackgroundToolStripMenuItem.Text = "White background";
            this.whiteBackgroundToolStripMenuItem.Visible = false;
            this.whiteBackgroundToolStripMenuItem.Click += new System.EventHandler(this.whiteBackgroundToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.png");
            this.imageList1.Images.SetKeyName(1, "Data-Information.png");
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(87, 22);
            this.toolStripLabel4.Text = "Color code:";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStripLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel5.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(107, 22);
            this.toolStripLabel5.Text = "[Syntax error]";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStripLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(101, 22);
            this.toolStripLabel6.Text = "[Primarykey]";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(139)))), ((int)(((byte)(206)))));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(98, 22);
            this.toolStripLabel1.Text = "[Foreignkey]";
            this.toolStripLabel1.Visible = false;
            // 
            // generateDTOToolStripMenuItem
            // 
            this.generateDTOToolStripMenuItem.Name = "generateDTOToolStripMenuItem";
            this.generateDTOToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // generateButton
            // 
            this.generateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.generateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateButton.Font = new System.Drawing.Font("Tahoma", 16F);
            this.generateButton.ForeColor = System.Drawing.Color.White;
            this.generateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generateButton.Location = new System.Drawing.Point(3, 214);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(898, 34);
            this.generateButton.TabIndex = 12;
            this.generateButton.Text = "Generate";
            this.generateButton.UseCompatibleTextRendering = true;
            this.generateButton.UseVisualStyleBackColor = false;
            this.generateButton.Click += new System.EventHandler(this.StartThread);
            // 
            // ambiance_ThemeContainer1
            // 
            this.ambiance_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ambiance_ThemeContainer1.Controls.Add(this.pictureBox5);
            this.ambiance_ThemeContainer1.Controls.Add(this.ambiance_ControlBox1);
            this.ambiance_ThemeContainer1.Controls.Add(this.optionsTabControl);
            this.ambiance_ThemeContainer1.Controls.Add(this.splitContainer1);
            this.ambiance_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ambiance_ThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ambiance_ThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.ambiance_ThemeContainer1.Name = "ambiance_ThemeContainer1";
            this.ambiance_ThemeContainer1.Padding = new System.Windows.Forms.Padding(10, 70, 10, 9);
            this.ambiance_ThemeContainer1.RoundCorners = false;
            this.ambiance_ThemeContainer1.Sizable = false;
            this.ambiance_ThemeContainer1.Size = new System.Drawing.Size(933, 695);
            this.ambiance_ThemeContainer1.SmartBounds = false;
            this.ambiance_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ambiance_ThemeContainer1.TabIndex = 36;
            this.ambiance_ThemeContainer1.Text = "CodeHammer";
            // 
            // pictureBox5
            // 
            this.pictureBox5.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.ErrorImage")));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.InitialImage")));
            this.pictureBox5.Location = new System.Drawing.Point(10, 9);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(26, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 44;
            this.pictureBox5.TabStop = false;
            // 
            // ambiance_ControlBox1
            // 
            this.ambiance_ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_ControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            this.ambiance_ControlBox1.Location = new System.Drawing.Point(883, 7);
            this.ambiance_ControlBox1.Name = "ambiance_ControlBox1";
            this.ambiance_ControlBox1.Size = new System.Drawing.Size(46, 22);
            this.ambiance_ControlBox1.TabIndex = 35;
            // 
            // optionsTabControl
            // 
            this.optionsTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.optionsTabControl.Controls.Add(this.crudTabPage);
            this.optionsTabControl.Controls.Add(this.prefixClassestabPage);
            this.optionsTabControl.Controls.Add(this.iocTabPage);
            this.optionsTabControl.Font = new System.Drawing.Font("Tahoma", 10F);
            this.optionsTabControl.ImeMode = System.Windows.Forms.ImeMode.On;
            this.optionsTabControl.ItemSize = new System.Drawing.Size(80, 24);
            this.optionsTabControl.Location = new System.Drawing.Point(10, 41);
            this.optionsTabControl.Multiline = true;
            this.optionsTabControl.Name = "optionsTabControl";
            this.optionsTabControl.SelectedIndex = 0;
            this.optionsTabControl.Size = new System.Drawing.Size(913, 288);
            this.optionsTabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.optionsTabControl.TabIndex = 34;
            // 
            // crudTabPage
            // 
            this.crudTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.crudTabPage.Controls.Add(this.pictureBox8);
            this.crudTabPage.Controls.Add(this.authenticationGroupBox);
            this.crudTabPage.Controls.Add(this.crudCheckBox);
            this.crudTabPage.Controls.Add(this.clearFolderCheckBox);
            this.crudTabPage.Controls.Add(this.gridCheckBox);
            this.crudTabPage.Controls.Add(this.generateButton);
            this.crudTabPage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crudTabPage.ForeColor = System.Drawing.Color.White;
            this.crudTabPage.Location = new System.Drawing.Point(4, 28);
            this.crudTabPage.Name = "crudTabPage";
            this.crudTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.crudTabPage.Size = new System.Drawing.Size(905, 256);
            this.crudTabPage.TabIndex = 0;
            this.crudTabPage.Text = "Database Design";
            // 
            // pictureBox8
            // 
            this.pictureBox8.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox8.ErrorImage")));
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox8.InitialImage")));
            this.pictureBox8.Location = new System.Drawing.Point(3, 0);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(35, 38);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 37;
            this.pictureBox8.TabStop = false;
            // 
            // authenticationGroupBox
            // 
            this.authenticationGroupBox.Controls.Add(this.pictureBox2);
            this.authenticationGroupBox.Controls.Add(this.emptyDataLayerCheckBox);
            this.authenticationGroupBox.Controls.Add(this.label10);
            this.authenticationGroupBox.Controls.Add(this.providerComboBox);
            this.authenticationGroupBox.Controls.Add(this.panel1);
            this.authenticationGroupBox.Controls.Add(this.executeStoredprocedureCheckBox);
            this.authenticationGroupBox.Controls.Add(this.dbServerTextBox);
            this.authenticationGroupBox.Controls.Add(this.databaseComboBox);
            this.authenticationGroupBox.Controls.Add(this.connectButton);
            this.authenticationGroupBox.Controls.Add(this.passwordTextBox);
            this.authenticationGroupBox.Controls.Add(this.databaseLabel);
            this.authenticationGroupBox.Controls.Add(this.SelectAllButton);
            this.authenticationGroupBox.Controls.Add(this.loginNameTextBox);
            this.authenticationGroupBox.Controls.Add(this.ResetButton);
            this.authenticationGroupBox.Controls.Add(this.serverLabel);
            this.authenticationGroupBox.Controls.Add(this.ShowTablesButton);
            this.authenticationGroupBox.Controls.Add(this.passwordLabel);
            this.authenticationGroupBox.Controls.Add(this.loginNameLabel);
            this.authenticationGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.authenticationGroupBox.ForeColor = System.Drawing.Color.White;
            this.authenticationGroupBox.Location = new System.Drawing.Point(3, 34);
            this.authenticationGroupBox.Name = "authenticationGroupBox";
            this.authenticationGroupBox.Size = new System.Drawing.Size(898, 176);
            this.authenticationGroupBox.TabIndex = 2;
            this.authenticationGroupBox.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(808, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(84, 83);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 40;
            this.pictureBox2.TabStop = false;
            // 
            // emptyDataLayerCheckBox
            // 
            this.emptyDataLayerCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.emptyDataLayerCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.emptyDataLayerCheckBox.Location = new System.Drawing.Point(454, 74);
            this.emptyDataLayerCheckBox.Name = "emptyDataLayerCheckBox";
            this.emptyDataLayerCheckBox.Size = new System.Drawing.Size(284, 17);
            this.emptyDataLayerCheckBox.TabIndex = 8;
            this.emptyDataLayerCheckBox.Text = "No Data Repository";
            this.emptyDataLayerCheckBox.UseVisualStyleBackColor = false;
            this.emptyDataLayerCheckBox.CheckedChanged += new System.EventHandler(this.emptyDataLayerCheckBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(13, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 20);
            this.label10.TabIndex = 45;
            this.label10.Text = "Provider:";
            // 
            // providerComboBox
            // 
            this.providerComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.providerComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.providerComboBox.ForeColor = System.Drawing.Color.Black;
            this.providerComboBox.FormattingEnabled = true;
            this.providerComboBox.Location = new System.Drawing.Point(105, 111);
            this.providerComboBox.Name = "providerComboBox";
            this.providerComboBox.ReadOnly = false;
            this.providerComboBox.Size = new System.Drawing.Size(321, 22);
            this.providerComboBox.TabIndex = 4;
            this.providerComboBox.SelectedIndexChanged += new System.EventHandler(this.providerComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(432, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 168);
            this.panel1.TabIndex = 21;
            // 
            // executeStoredprocedureCheckBox
            // 
            this.executeStoredprocedureCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.executeStoredprocedureCheckBox.Checked = true;
            this.executeStoredprocedureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.executeStoredprocedureCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.executeStoredprocedureCheckBox.Location = new System.Drawing.Point(454, 51);
            this.executeStoredprocedureCheckBox.Name = "executeStoredprocedureCheckBox";
            this.executeStoredprocedureCheckBox.Size = new System.Drawing.Size(284, 17);
            this.executeStoredprocedureCheckBox.TabIndex = 7;
            this.executeStoredprocedureCheckBox.Text = "Execute Stored Procedures to database";
            this.executeStoredprocedureCheckBox.UseVisualStyleBackColor = false;
            // 
            // dbServerTextBox
            // 
            this.dbServerTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.dbServerTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.dbServerTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dbServerTextBox.Location = new System.Drawing.Point(105, 80);
            this.dbServerTextBox.Name = "dbServerTextBox";
            this.dbServerTextBox.Size = new System.Drawing.Size(321, 22);
            this.dbServerTextBox.TabIndex = 3;
            // 
            // databaseComboBox
            // 
            this.databaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.databaseComboBox.ForeColor = System.Drawing.Color.Black;
            this.databaseComboBox.FormattingEnabled = true;
            this.databaseComboBox.Location = new System.Drawing.Point(532, 17);
            this.databaseComboBox.Name = "databaseComboBox";
            this.databaseComboBox.ReadOnly = false;
            this.databaseComboBox.Size = new System.Drawing.Size(360, 22);
            this.databaseComboBox.TabIndex = 6;
            this.databaseComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.databaseComboBox_KeyPress);
            // 
            // connectButton
            // 
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.connectButton.Image = ((System.Drawing.Image)(resources.GetObject("connectButton.Image")));
            this.connectButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectButton.Location = new System.Drawing.Point(5, 140);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(421, 27);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.AcceptsReturn = true;
            this.passwordTextBox.BackColor = System.Drawing.Color.White;
            this.passwordTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.passwordTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.passwordTextBox.Location = new System.Drawing.Point(105, 49);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(321, 22);
            this.passwordTextBox.TabIndex = 2;
            // 
            // databaseLabel
            // 
            this.databaseLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.databaseLabel.Location = new System.Drawing.Point(451, 20);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(75, 20);
            this.databaseLabel.TabIndex = 1;
            this.databaseLabel.Text = "Database:";
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SelectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAllButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SelectAllButton.ForeColor = System.Drawing.Color.White;
            this.SelectAllButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectAllButton.Image")));
            this.SelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectAllButton.Location = new System.Drawing.Point(599, 140);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(135, 27);
            this.SelectAllButton.TabIndex = 10;
            this.SelectAllButton.Text = "All";
            this.SelectAllButton.UseCompatibleTextRendering = true;
            this.SelectAllButton.UseVisualStyleBackColor = false;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // loginNameTextBox
            // 
            this.loginNameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.loginNameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.loginNameTextBox.BackColor = System.Drawing.Color.White;
            this.loginNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.loginNameTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.loginNameTextBox.Location = new System.Drawing.Point(105, 19);
            this.loginNameTextBox.Name = "loginNameTextBox";
            this.loginNameTextBox.Size = new System.Drawing.Size(321, 22);
            this.loginNameTextBox.TabIndex = 1;
            this.loginNameTextBox.TextChanged += new System.EventHandler(this.LoginNameTextBox_TextChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.ResetButton.ForeColor = System.Drawing.Color.White;
            this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
            this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetButton.Location = new System.Drawing.Point(758, 140);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(135, 27);
            this.ResetButton.TabIndex = 11;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseCompatibleTextRendering = true;
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // serverLabel
            // 
            this.serverLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.serverLabel.Location = new System.Drawing.Point(13, 82);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(62, 21);
            this.serverLabel.TabIndex = 0;
            this.serverLabel.Text = "Server:";
            // 
            // ShowTablesButton
            // 
            this.ShowTablesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ShowTablesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowTablesButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.ShowTablesButton.ForeColor = System.Drawing.Color.White;
            this.ShowTablesButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowTablesButton.Image")));
            this.ShowTablesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ShowTablesButton.Location = new System.Drawing.Point(439, 140);
            this.ShowTablesButton.Name = "ShowTablesButton";
            this.ShowTablesButton.Size = new System.Drawing.Size(135, 27);
            this.ShowTablesButton.TabIndex = 9;
            this.ShowTablesButton.Text = "  Tables";
            this.ShowTablesButton.UseCompatibleTextRendering = true;
            this.ShowTablesButton.UseVisualStyleBackColor = false;
            this.ShowTablesButton.Click += new System.EventHandler(this.ShowTablesButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.Location = new System.Drawing.Point(13, 51);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(74, 20);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password:";
            // 
            // loginNameLabel
            // 
            this.loginNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginNameLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.loginNameLabel.ForeColor = System.Drawing.Color.White;
            this.loginNameLabel.Location = new System.Drawing.Point(13, 21);
            this.loginNameLabel.Name = "loginNameLabel";
            this.loginNameLabel.Size = new System.Drawing.Size(86, 21);
            this.loginNameLabel.TabIndex = 2;
            this.loginNameLabel.Text = "Login Name:";
            // 
            // crudCheckBox
            // 
            this.crudCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.crudCheckBox.Checked = true;
            this.crudCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.crudCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.crudCheckBox.Location = new System.Drawing.Point(221, 12);
            this.crudCheckBox.Name = "crudCheckBox";
            this.crudCheckBox.Size = new System.Drawing.Size(64, 16);
            this.crudCheckBox.TabIndex = 42;
            this.crudCheckBox.Text = "CRUD";
            this.crudCheckBox.UseVisualStyleBackColor = false;
            this.crudCheckBox.Visible = false;
            this.crudCheckBox.CheckedChanged += new System.EventHandler(this.crudCheckBox_CheckedChanged);
            // 
            // clearFolderCheckBox
            // 
            this.clearFolderCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.clearFolderCheckBox.Enabled = false;
            this.clearFolderCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.clearFolderCheckBox.Location = new System.Drawing.Point(312, 13);
            this.clearFolderCheckBox.Name = "clearFolderCheckBox";
            this.clearFolderCheckBox.Size = new System.Drawing.Size(268, 19);
            this.clearFolderCheckBox.TabIndex = 43;
            this.clearFolderCheckBox.Text = "Clear folder before generate";
            this.clearFolderCheckBox.UseVisualStyleBackColor = false;
            this.clearFolderCheckBox.Visible = false;
            // 
            // gridCheckBox
            // 
            this.gridCheckBox.AutoSize = true;
            this.gridCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridCheckBox.Location = new System.Drawing.Point(602, 13);
            this.gridCheckBox.Name = "gridCheckBox";
            this.gridCheckBox.Size = new System.Drawing.Size(145, 18);
            this.gridCheckBox.TabIndex = 28;
            this.gridCheckBox.Text = "GridView Bootstrap";
            this.gridCheckBox.UseVisualStyleBackColor = true;
            this.gridCheckBox.Visible = false;
            this.gridCheckBox.CheckedChanged += new System.EventHandler(this.gridCheckBox_CheckedChanged);
            // 
            // prefixClassestabPage
            // 
            this.prefixClassestabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.prefixClassestabPage.Controls.Add(this.generateSuffixButton);
            this.prefixClassestabPage.Controls.Add(this.groupBox3);
            this.prefixClassestabPage.Controls.Add(this.pictureBox6);
            this.prefixClassestabPage.Location = new System.Drawing.Point(4, 28);
            this.prefixClassestabPage.Name = "prefixClassestabPage";
            this.prefixClassestabPage.Size = new System.Drawing.Size(905, 256);
            this.prefixClassestabPage.TabIndex = 2;
            this.prefixClassestabPage.Text = "Prefix/Suffix settings";
            // 
            // generateSuffixButton
            // 
            this.generateSuffixButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.generateSuffixButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateSuffixButton.Font = new System.Drawing.Font("Tahoma", 16F);
            this.generateSuffixButton.ForeColor = System.Drawing.Color.White;
            this.generateSuffixButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generateSuffixButton.Location = new System.Drawing.Point(4, 214);
            this.generateSuffixButton.Name = "generateSuffixButton";
            this.generateSuffixButton.Size = new System.Drawing.Size(898, 34);
            this.generateSuffixButton.TabIndex = 10;
            this.generateSuffixButton.Text = "Generate";
            this.generateSuffixButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.generateSuffixButton.UseCompatibleTextRendering = true;
            this.generateSuffixButton.UseVisualStyleBackColor = false;
            this.generateSuffixButton.Click += new System.EventHandler(this.StartThread);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sqlPrefixTextBox);
            this.groupBox3.Controls.Add(this.testTextBox);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.solutionTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.serviceTextBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.serviceContractTextBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.dataContractTextBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.dtoTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dalTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.blSuffixTextBox);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(4, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(898, 176);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            // 
            // sqlPrefixTextBox
            // 
            this.sqlPrefixTextBox.Enabled = false;
            this.sqlPrefixTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.sqlPrefixTextBox.Location = new System.Drawing.Point(611, 32);
            this.sqlPrefixTextBox.Name = "sqlPrefixTextBox";
            this.sqlPrefixTextBox.Size = new System.Drawing.Size(277, 22);
            this.sqlPrefixTextBox.TabIndex = 6;
            // 
            // testTextBox
            // 
            this.testTextBox.Enabled = false;
            this.testTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.testTextBox.Location = new System.Drawing.Point(161, 136);
            this.testTextBox.Name = "testTextBox";
            this.testTextBox.Size = new System.Drawing.Size(278, 22);
            this.testTextBox.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(7, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 14);
            this.label9.TabIndex = 62;
            this.label9.Text = "Test:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(463, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 61;
            this.label1.Text = "Stored procedure:";
            // 
            // solutionTextBox
            // 
            this.solutionTextBox.Enabled = false;
            this.solutionTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.solutionTextBox.Location = new System.Drawing.Point(161, 32);
            this.solutionTextBox.Name = "solutionTextBox";
            this.solutionTextBox.Size = new System.Drawing.Size(278, 22);
            this.solutionTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 14);
            this.label8.TabIndex = 57;
            this.label8.Text = "Solution:";
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.Enabled = false;
            this.serviceTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.serviceTextBox.Location = new System.Drawing.Point(611, 110);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.Size = new System.Drawing.Size(277, 22);
            this.serviceTextBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(463, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 14);
            this.label7.TabIndex = 55;
            this.label7.Text = "Service:";
            // 
            // serviceContractTextBox
            // 
            this.serviceContractTextBox.Enabled = false;
            this.serviceContractTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.serviceContractTextBox.Location = new System.Drawing.Point(611, 84);
            this.serviceContractTextBox.Name = "serviceContractTextBox";
            this.serviceContractTextBox.Size = new System.Drawing.Size(277, 22);
            this.serviceContractTextBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(463, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 14);
            this.label6.TabIndex = 53;
            this.label6.Text = "ServiceContract:";
            // 
            // dataContractTextBox
            // 
            this.dataContractTextBox.Enabled = false;
            this.dataContractTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dataContractTextBox.Location = new System.Drawing.Point(611, 58);
            this.dataContractTextBox.Name = "dataContractTextBox";
            this.dataContractTextBox.Size = new System.Drawing.Size(277, 22);
            this.dataContractTextBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(463, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 14);
            this.label5.TabIndex = 51;
            this.label5.Text = "DataContract:";
            // 
            // dtoTextBox
            // 
            this.dtoTextBox.Enabled = false;
            this.dtoTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dtoTextBox.Location = new System.Drawing.Point(161, 110);
            this.dtoTextBox.Name = "dtoTextBox";
            this.dtoTextBox.Size = new System.Drawing.Size(278, 22);
            this.dtoTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 14);
            this.label4.TabIndex = 49;
            this.label4.Text = "Domain Object:";
            // 
            // dalTextBox
            // 
            this.dalTextBox.Enabled = false;
            this.dalTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.dalTextBox.Location = new System.Drawing.Point(161, 84);
            this.dalTextBox.Name = "dalTextBox";
            this.dalTextBox.Size = new System.Drawing.Size(278, 22);
            this.dalTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(7, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 47;
            this.label3.Text = "Data Access:";
            // 
            // blSuffixTextBox
            // 
            this.blSuffixTextBox.Enabled = false;
            this.blSuffixTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.blSuffixTextBox.Location = new System.Drawing.Point(161, 58);
            this.blSuffixTextBox.Name = "blSuffixTextBox";
            this.blSuffixTextBox.Size = new System.Drawing.Size(278, 22);
            this.blSuffixTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 14);
            this.label2.TabIndex = 45;
            this.label2.Text = "Business Logic:";
            // 
            // pictureBox6
            // 
            this.pictureBox6.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.ErrorImage")));
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.InitialImage")));
            this.pictureBox6.Location = new System.Drawing.Point(4, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(39, 37);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 3;
            this.pictureBox6.TabStop = false;
            // 
            // iocTabPage
            // 
            this.iocTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.iocTabPage.Controls.Add(this.groupBox9);
            this.iocTabPage.Controls.Add(this.pictureBox1);
            this.iocTabPage.Controls.Add(this.fluentNHibernateRadioButton);
            this.iocTabPage.Controls.Add(this.generatorButton);
            this.iocTabPage.ForeColor = System.Drawing.Color.White;
            this.iocTabPage.Location = new System.Drawing.Point(4, 28);
            this.iocTabPage.Name = "iocTabPage";
            this.iocTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.iocTabPage.Size = new System.Drawing.Size(905, 256);
            this.iocTabPage.TabIndex = 1;
            this.iocTabPage.Text = "Architectural Design";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.groupBox2);
            this.groupBox9.Controls.Add(this.groupBox8);
            this.groupBox9.Controls.Add(this.groupBox1);
            this.groupBox9.Controls.Add(this.groupBox7);
            this.groupBox9.Controls.Add(this.groupBox4);
            this.groupBox9.Controls.Add(this.groupBox6);
            this.groupBox9.Controls.Add(this.groupBox5);
            this.groupBox9.Controls.Add(this.wcfThrottlingGroupBox);
            this.groupBox9.Location = new System.Drawing.Point(4, 34);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(898, 176);
            this.groupBox9.TabIndex = 28;
            this.groupBox9.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.sqlOrmComboBox);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(308, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 45);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.ErrorImage")));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.InitialImage")));
            this.pictureBox4.Location = new System.Drawing.Point(246, 11);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(29, 29);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 35;
            this.pictureBox4.TabStop = false;
            // 
            // sqlOrmComboBox
            // 
            this.sqlOrmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sqlOrmComboBox.Enabled = false;
            this.sqlOrmComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.sqlOrmComboBox.ForeColor = System.Drawing.Color.Black;
            this.sqlOrmComboBox.FormattingEnabled = true;
            this.sqlOrmComboBox.Items.AddRange(new object[] {
            "SQL"});
            this.sqlOrmComboBox.Location = new System.Drawing.Point(6, 15);
            this.sqlOrmComboBox.Name = "sqlOrmComboBox";
            this.sqlOrmComboBox.Size = new System.Drawing.Size(229, 22);
            this.sqlOrmComboBox.TabIndex = 2;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.wcfSecurityComboBox);
            this.groupBox8.Controls.Add(this.pictureBox14);
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(308, 123);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(285, 45);
            this.groupBox8.TabIndex = 40;
            this.groupBox8.TabStop = false;
            // 
            // wcfSecurityComboBox
            // 
            this.wcfSecurityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wcfSecurityComboBox.Enabled = false;
            this.wcfSecurityComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.wcfSecurityComboBox.ForeColor = System.Drawing.Color.Black;
            this.wcfSecurityComboBox.FormattingEnabled = true;
            this.wcfSecurityComboBox.Items.AddRange(new object[] {
            "Choose Wcf Security",
            "Certificate"});
            this.wcfSecurityComboBox.Location = new System.Drawing.Point(5, 15);
            this.wcfSecurityComboBox.Name = "wcfSecurityComboBox";
            this.wcfSecurityComboBox.Size = new System.Drawing.Size(229, 22);
            this.wcfSecurityComboBox.TabIndex = 8;
            this.wcfSecurityComboBox.SelectedIndexChanged += new System.EventHandler(this.wcfSecurityComboBox_SelectedIndexChanged);
            // 
            // pictureBox14
            // 
            this.pictureBox14.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox14.ErrorImage")));
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox14.InitialImage")));
            this.pictureBox14.Location = new System.Drawing.Point(245, 11);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(32, 32);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox14.TabIndex = 38;
            this.pictureBox14.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.iocComboBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(5, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 45);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(240, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 14);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // iocComboBox
            // 
            this.iocComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iocComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.iocComboBox.ForeColor = System.Drawing.Color.Black;
            this.iocComboBox.FormattingEnabled = true;
            this.iocComboBox.Items.AddRange(new object[] {
            "Choose IoC container",
            "No IoC container",
            "Castle Wcf Facility"});
            this.iocComboBox.Location = new System.Drawing.Point(6, 15);
            this.iocComboBox.Name = "iocComboBox";
            this.iocComboBox.Size = new System.Drawing.Size(229, 22);
            this.iocComboBox.TabIndex = 1;
            this.iocComboBox.SelectedIndexChanged += new System.EventHandler(this.iocComboBox_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.streamTypeComboBox);
            this.groupBox7.Controls.Add(this.pictureBox13);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(608, 66);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(285, 45);
            this.groupBox7.TabIndex = 39;
            this.groupBox7.TabStop = false;
            // 
            // streamTypeComboBox
            // 
            this.streamTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.streamTypeComboBox.Enabled = false;
            this.streamTypeComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.streamTypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.streamTypeComboBox.FormattingEnabled = true;
            this.streamTypeComboBox.Items.AddRange(new object[] {
            "Json",
            "Xml"});
            this.streamTypeComboBox.Location = new System.Drawing.Point(6, 15);
            this.streamTypeComboBox.Name = "streamTypeComboBox";
            this.streamTypeComboBox.Size = new System.Drawing.Size(229, 22);
            this.streamTypeComboBox.TabIndex = 6;
            this.streamTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.streamTypeComboBox_SelectedIndexChanged);
            // 
            // pictureBox13
            // 
            this.pictureBox13.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox13.ErrorImage")));
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox13.InitialImage")));
            this.pictureBox13.Location = new System.Drawing.Point(243, 10);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(32, 32);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox13.TabIndex = 38;
            this.pictureBox13.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.wcfCallComboBox);
            this.groupBox4.Controls.Add(this.pictureBox7);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(308, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 45);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            // 
            // wcfCallComboBox
            // 
            this.wcfCallComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wcfCallComboBox.Enabled = false;
            this.wcfCallComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.wcfCallComboBox.ForeColor = System.Drawing.Color.Black;
            this.wcfCallComboBox.FormattingEnabled = true;
            this.wcfCallComboBox.Items.AddRange(new object[] {
            "Per-Call",
            "Per-Session",
            "Singleton"});
            this.wcfCallComboBox.Location = new System.Drawing.Point(5, 15);
            this.wcfCallComboBox.Name = "wcfCallComboBox";
            this.wcfCallComboBox.Size = new System.Drawing.Size(229, 22);
            this.wcfCallComboBox.TabIndex = 5;
            this.wcfCallComboBox.SelectedIndexChanged += new System.EventHandler(this.wcfCallComboBox_SelectedIndexChanged);
            // 
            // pictureBox7
            // 
            this.pictureBox7.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.ErrorImage")));
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.InitialImage")));
            this.pictureBox7.Location = new System.Drawing.Point(249, 10);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(24, 29);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox7.TabIndex = 37;
            this.pictureBox7.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.unitTestComboBox);
            this.groupBox6.Controls.Add(this.pictureBox9);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(6, 123);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(284, 45);
            this.groupBox6.TabIndex = 34;
            this.groupBox6.TabStop = false;
            // 
            // unitTestComboBox
            // 
            this.unitTestComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitTestComboBox.Enabled = false;
            this.unitTestComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.unitTestComboBox.ForeColor = System.Drawing.Color.Black;
            this.unitTestComboBox.FormattingEnabled = true;
            this.unitTestComboBox.Items.AddRange(new object[] {
            "Choose unit test",
            "NUnit",
            "VisualStudio UnitTest"});
            this.unitTestComboBox.Location = new System.Drawing.Point(6, 15);
            this.unitTestComboBox.Name = "unitTestComboBox";
            this.unitTestComboBox.Size = new System.Drawing.Size(228, 22);
            this.unitTestComboBox.TabIndex = 7;
            this.unitTestComboBox.SelectedIndexChanged += new System.EventHandler(this.unitTestComboBox_SelectedIndexChanged);
            // 
            // pictureBox9
            // 
            this.pictureBox9.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox9.ErrorImage")));
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox9.InitialImage")));
            this.pictureBox9.Location = new System.Drawing.Point(258, 13);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(8, 24);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox9.TabIndex = 38;
            this.pictureBox9.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.loggingComboBox);
            this.groupBox5.Controls.Add(this.pictureBox12);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(608, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(285, 45);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            // 
            // loggingComboBox
            // 
            this.loggingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loggingComboBox.Enabled = false;
            this.loggingComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.loggingComboBox.ForeColor = System.Drawing.Color.Black;
            this.loggingComboBox.FormattingEnabled = true;
            this.loggingComboBox.Items.AddRange(new object[] {
            "Choose logging",
            "Log4Net"});
            this.loggingComboBox.Location = new System.Drawing.Point(6, 15);
            this.loggingComboBox.Name = "loggingComboBox";
            this.loggingComboBox.Size = new System.Drawing.Size(229, 22);
            this.loggingComboBox.TabIndex = 3;
            this.loggingComboBox.SelectedIndexChanged += new System.EventHandler(this.loggingComboBox_SelectedIndexChanged);
            // 
            // pictureBox12
            // 
            this.pictureBox12.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox12.ErrorImage")));
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox12.InitialImage")));
            this.pictureBox12.Location = new System.Drawing.Point(243, 10);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(32, 32);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox12.TabIndex = 38;
            this.pictureBox12.TabStop = false;
            // 
            // wcfThrottlingGroupBox
            // 
            this.wcfThrottlingGroupBox.Controls.Add(this.wcfPerformanceComboBox);
            this.wcfThrottlingGroupBox.Controls.Add(this.pictureBox10);
            this.wcfThrottlingGroupBox.ForeColor = System.Drawing.Color.White;
            this.wcfThrottlingGroupBox.Location = new System.Drawing.Point(6, 66);
            this.wcfThrottlingGroupBox.Name = "wcfThrottlingGroupBox";
            this.wcfThrottlingGroupBox.Size = new System.Drawing.Size(284, 45);
            this.wcfThrottlingGroupBox.TabIndex = 40;
            this.wcfThrottlingGroupBox.TabStop = false;
            // 
            // wcfPerformanceComboBox
            // 
            this.wcfPerformanceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wcfPerformanceComboBox.Enabled = false;
            this.wcfPerformanceComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.wcfPerformanceComboBox.ForeColor = System.Drawing.Color.Black;
            this.wcfPerformanceComboBox.FormattingEnabled = true;
            this.wcfPerformanceComboBox.Items.AddRange(new object[] {
            "Choose wcf performance",
            "Throttling"});
            this.wcfPerformanceComboBox.Location = new System.Drawing.Point(7, 16);
            this.wcfPerformanceComboBox.Name = "wcfPerformanceComboBox";
            this.wcfPerformanceComboBox.Size = new System.Drawing.Size(227, 22);
            this.wcfPerformanceComboBox.TabIndex = 4;
            this.wcfPerformanceComboBox.SelectedIndexChanged += new System.EventHandler(this.wcfPerformanceComboBox_SelectedIndexChanged);
            // 
            // pictureBox10
            // 
            this.pictureBox10.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.ErrorImage")));
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.InitialImage")));
            this.pictureBox10.Location = new System.Drawing.Point(246, 10);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(32, 32);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox10.TabIndex = 38;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // fluentNHibernateRadioButton
            // 
            this.fluentNHibernateRadioButton.AutoSize = true;
            this.fluentNHibernateRadioButton.BackColor = System.Drawing.Color.Red;
            this.fluentNHibernateRadioButton.Enabled = false;
            this.fluentNHibernateRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.fluentNHibernateRadioButton.ForeColor = System.Drawing.Color.White;
            this.fluentNHibernateRadioButton.Location = new System.Drawing.Point(135, 9);
            this.fluentNHibernateRadioButton.Name = "fluentNHibernateRadioButton";
            this.fluentNHibernateRadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fluentNHibernateRadioButton.Size = new System.Drawing.Size(225, 18);
            this.fluentNHibernateRadioButton.TabIndex = 3;
            this.fluentNHibernateRadioButton.Text = "Coming soon (FluentNHibernate)";
            this.fluentNHibernateRadioButton.UseVisualStyleBackColor = false;
            this.fluentNHibernateRadioButton.Visible = false;
            this.fluentNHibernateRadioButton.CheckedChanged += new System.EventHandler(this.fluentNHibernateRadioButton_CheckedChanged);
            // 
            // generatorButton
            // 
            this.generatorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.generatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generatorButton.Font = new System.Drawing.Font("Tahoma", 16F);
            this.generatorButton.ForeColor = System.Drawing.Color.White;
            this.generatorButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generatorButton.Location = new System.Drawing.Point(4, 214);
            this.generatorButton.Name = "generatorButton";
            this.generatorButton.Size = new System.Drawing.Size(898, 34);
            this.generatorButton.TabIndex = 34;
            this.generatorButton.Text = "Generate";
            this.generatorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.generatorButton.UseCompatibleTextRendering = true;
            this.generatorButton.UseVisualStyleBackColor = false;
            this.generatorButton.Click += new System.EventHandler(this.StartThread);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(10, 333);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.splitContainer1.Panel1.Controls.Add(this.mixedCheckBoxesTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel2.Controls.Add(this.outputTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(913, 353);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 35;
            // 
            // mixedCheckBoxesTreeView1
            // 
            this.mixedCheckBoxesTreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.mixedCheckBoxesTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mixedCheckBoxesTreeView1.CheckBoxes = true;
            this.mixedCheckBoxesTreeView1.ContextMenuStrip = this.tableMenuStrip;
            this.mixedCheckBoxesTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixedCheckBoxesTreeView1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.mixedCheckBoxesTreeView1.ForeColor = System.Drawing.Color.White;
            this.mixedCheckBoxesTreeView1.LineColor = System.Drawing.Color.White;
            this.mixedCheckBoxesTreeView1.Location = new System.Drawing.Point(0, 0);
            this.mixedCheckBoxesTreeView1.Name = "mixedCheckBoxesTreeView1";
            this.mixedCheckBoxesTreeView1.ShowNodeToolTips = true;
            this.mixedCheckBoxesTreeView1.Size = new System.Drawing.Size(909, 267);
            this.mixedCheckBoxesTreeView1.TabIndex = 27;
            this.mixedCheckBoxesTreeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.mixedCheckBoxesTreeView1_AfterCheck);
            // 
            // outputTextBox
            // 
            this.outputTextBox.AutoWordSelection = true;
            this.outputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputTextBox.BulletIndent = 1;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.outputTextBox.ForeColor = System.Drawing.Color.White;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.outputTextBox.Size = new System.Drawing.Size(909, 49);
            this.outputTextBox.TabIndex = 33;
            this.outputTextBox.Text = "";
            this.outputTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.outputTextBox_LinkClicked);
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseTimeLeftLabel,
            this.toolStripSeparator3,
            this.toolStripLabel9,
            this.toolStripLabel10,
            this.toolStripSeparator4,
            this.toolStripLabel11,
            this.toolStripSeparator5,
            this.toolStripLabel12});
            this.toolStrip1.Location = new System.Drawing.Point(0, 49);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(909, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // licenseTimeLeftLabel
            // 
            this.licenseTimeLeftLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.licenseTimeLeftLabel.Name = "licenseTimeLeftLabel";
            this.licenseTimeLeftLabel.Size = new System.Drawing.Size(57, 22);
            this.licenseTimeLeftLabel.Text = "Time left:";
            this.licenseTimeLeftLabel.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel9.Text = "Color code:";
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.toolStripLabel10.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(88, 22);
            this.toolStripLabel10.Text = "[Unsupported]";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.toolStripLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel11.Text = "[Primarykey]";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel12
            // 
            this.toolStripLabel12.BackColor = System.Drawing.Color.Black;
            this.toolStripLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(123)))), ((int)(((byte)(172)))));
            this.toolStripLabel12.Name = "toolStripLabel12";
            this.toolStripLabel12.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel12.Text = "[Foreignkey]";
            this.toolStripLabel12.Visible = false;
            // 
            // CodeHammerForm
            // 
            this.AcceptButton = this.generateButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(933, 695);
            this.Controls.Add(this.ambiance_ThemeContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(261, 65);
            this.Name = "CodeHammerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeHammer";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.CodeHammerForm_Load);
            this.tableMenuStrip.ResumeLayout(false);
            this.ambiance_ThemeContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.optionsTabControl.ResumeLayout(false);
            this.crudTabPage.ResumeLayout(false);
            this.crudTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.authenticationGroupBox.ResumeLayout(false);
            this.authenticationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.prefixClassestabPage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.iocTabPage.ResumeLayout(false);
            this.iocTabPage.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.wcfThrottlingGroupBox.ResumeLayout(false);
            this.wcfThrottlingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Handles the KeyPress event of the languageComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="KeyPressEventArgs" /> instance containing the event data.
        /// </param>
        private void languageComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Handles the DatabaseCounted event of the Generator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="DataTierGenerator.CodeHammerCountEventArgs" /> instance containing the
        /// event data.
        /// </param>
        /// <summary>
        /// Handles the TextChanged event of the LoginNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void LoginNameTextBox_TextChanged(object sender, System.EventArgs e)
        {
            //EnableGenerateButton();
        }

        /// <summary>
        /// Handles the AfterCheck event of the mixedCheckBoxesTreeView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="TreeViewEventArgs" /> instance containing the event data.
        /// </param>
        private void mixedCheckBoxesTreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked && e.Node.Tag.Equals("col") && !e.Node.Parent.Checked)
            {
                if (e.Node.FullPath.Contains("Please setup primarykey for table"))
                {
                    // MessageBox.Show("Please choose the table " + e.Node.Parent.Text);
                    e.Node.Checked = false;
                }

                if (e.Node.FullPath.Contains("Avoid using spaces"))
                {
                    // MessageBox.Show("Please choose the table " + e.Node.Parent.Text);
                    e.Node.Checked = false;
                }
            }
            else if (!e.Node.Checked && e.Node.Tag.Equals("Table"))
            {
                foreach (TreeNode nodeItem in e.Node.Nodes)
                {
                    nodeItem.Checked = false;
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the namespaceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void namespaceTextBox_TextChanged(object sender, System.EventArgs e)
        {
            //EnableGenerateButton();
        }

        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetList();
        }

        /// <summary>
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            bool stopProcess;
            CallRecursive(true, true, false, out stopProcess, mixedCheckBoxesTreeView1);

            CleanLogReader();
        }

        /// <summary>
        /// Prints the recursive.
        /// </summary>
        /// <param name="noLog">if set to <c>true</c> [no log].</param>
        /// <param name="nocheck">if set to <c>true</c> [nocheck].</param>
        /// <param name="treeNode">The tree node.</param>
        private void PrintRecursive(bool noLog, bool nocheck, TreeNode treeNode)
        {
            if (treeNode.Tag.Equals("error"))
            {
                if (treeNode.Text.Contains("Please"))
                {
                    treeNode.Checked = false;
                    // if (noLog)

                    //  Console.WriteLine(treeNode.ToolTipText);
                }

                if (treeNode.ToolTipText.Contains("Avoid"))
                {
                    treeNode.Checked = false;
                    if (noLog)

                        Console.WriteLine(treeNode.ToolTipText);
                }

                if (treeNode.Parent != null)
                {
                    treeNode.Parent.Checked = false;
                    treeNode.Parent.ForeColor = Color.Red;
                    if (noLog)

                        Console.WriteLine(treeNode.ToolTipText);
                }

                return;
            }
            else
            {
                if (nocheck)
                    treeNode.Checked = true;
            }

            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(noLog, nocheck, tn);
            }
        }

        /// <summary>
        /// Checks all.
        /// </summary>
        private void CheckAll()
        {
            if (mixedCheckBoxesTreeView1.SelectedNode != null)
            {
                mixedCheckBoxesTreeView1.SelectedNode.Checked = true;

                foreach (TreeNode colItem in mixedCheckBoxesTreeView1.SelectedNode.Nodes)
                {
                    if (colItem.Tag != null && (colItem.Tag.Equals("PK") || colItem.Tag.Equals("NOTNULLCOLUMN")))
                    {
                        if (!colItem.Tag.Equals("error"))
                        {
                            colItem.Checked = true;
                        }
                    }
                    else
                    {
                        if (!colItem.Tag.Equals("error"))
                        {
                            colItem.Checked = true;
                        }
                    }
                }

                if (fluentNHibernateRadioButton.Checked)
                {
                    NhibernateCheckForKeyRelations(mixedCheckBoxesTreeView1.SelectedNode.Text);
                }
            }
        }

        // Call the procedure using the TreeView.
        /// <summary>
        /// Calls the recursive.
        /// </summary>
        /// <param name="noLog">if set to <c>true</c> [no log].</param>
        /// <param name="nocheck">if set to <c>true</c> [nocheck].</param>
        /// <param name="stopCheck">if set to <c>true</c> [stop check].</param>
        /// <param name="treeView">The tree view.</param>
        private void CallRecursive(bool noLog, bool nocheck, bool stopCheck, out bool stopProcess, TreeView treeView)
        {
            stopProcess = false;
            // Print each node recursively.
            outputTextBox.Clear();
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Tag.Equals("error"))
                {
                    n.Checked = false;
                    n.ForeColor = Color.Red;

                    if (noLog)

                        Console.WriteLine(n.ToolTipText);
                    if (stopCheck)
                    {
                        stopProcess = true;
                    }

                    if (n.Parent != null)
                    {
                        n.Parent.ForeColor = Color.Red;
                        n.Parent.Checked = false;
                        //if (noLog)
                        //{
                        //    Console.WriteLine(n.Parent.ToolTipText);
                        //}
                        if (stopCheck)
                        {
                            stopProcess = true;
                        }
                    }
                }

                PrintRecursive(noLog, nocheck, n);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the serverComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void serverComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the serverComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void serverComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dbServerTextBox.Text))
            {
                string systemMessage = string.Empty;
                string innersystemMessage = string.Empty;
                List<string> dbList = null;

                if (!dbDataSupportAdapterContract.PopulateDataBaseSqlServer(loginNameTextBox.Text, passwordTextBox.Text, dbServerTextBox.Text, out dbList, out innersystemMessage))
                {
                    systemMessage = innersystemMessage;

                    Console.WriteLine(ioManagerContract.OutputError);

                    return;
                }

                foreach (var item in dbList)
                {
                    databaseComboBox.Items.Add(item);
                }

                databaseComboBox.Items.Add("Select database");
                databaseComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ServerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void ServerTextBox_TextChanged(object sender, System.EventArgs e)
        {
            //EnableGenerateButton();
        }

        /// <summary>
        /// Setups the database connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void SetupDatabaseConnection(out string connectionString)
        {
            connectionString = string.Empty;

            if (ioManagerContract.DBProvider.Equals("Oracle.ManagedDataAccess.Client"))
            {
                //User Id=codehammer; Password=neetsneets; Data Source=localhost:1521;

                connectionString = "Data Source=" + dbServerTextBox.Text + "; User ID=" + loginNameTextBox.Text + "; Password=" + passwordTextBox.Text + ";";
            }

            if (ioManagerContract.DBProvider.Equals("System.Data.SqlClient"))
            {
                connectionString = "Server=" + dbServerTextBox.Text + "; Database=" + databaseComboBox.SelectedItem.ToString() + "; User ID=" + loginNameTextBox.Text + "; Password=" + passwordTextBox.Text + ";";
            }

            ioManagerContract.DatabaseName = databaseComboBox.SelectedItem.ToString();
            ioManagerContract.DbConnection = connectionString;
        }

        /// <summary>
        /// Handles the Click event of the ShowTablesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void ShowTablesButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            string connectionString;
            try
            {
                gridCheckBox.Checked = false;
                selectTables = true;

                for (int i = 0; i < mixedCheckBoxesTreeView1.Nodes.Count; i++)
                {
                    mixedCheckBoxesTreeView1.Nodes[i].Checked = false;
                }
                ShowTablesButton.Enabled = false;
                SetupDatabaseConnection(out connectionString);
                codeHammerDataUtilContract.connectionStringDB = connectionString;
                //// Retrieve Data Tables
                codeHammerDataUtilContract.DataTableNames = codeHammerGeneratorContract.CodeHammerRetrievePrimaryKeys();
                mixedCheckBoxesTreeView1.Nodes.Clear();
                TreeNode tableNode = null;
                string lastTable = null;
                string lastColNamePrimaryKey = null;
                List<string> primaryKeysList = new List<string>();
                primaryKeysList.Clear();

                foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[1].Rows)
                {
                    foreach (DataRow dataRowItemPKTemp in codeHammerDataUtilContract.DataTableNames.Tables[0].Rows)
                    {
                        if (dataRowItemPKTemp.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[1].ToString()))
                        {
                            if (!primaryKeysList.Any(x => x.Equals(dataRowItemPK.ItemArray[1].ToString())))
                            {
                                primaryKeysList.Add(dataRowItemPK.ItemArray[1].ToString());
                            }
                        }
                    }
                }

                List<Tuple<string, string>> pkAndTb = new List<Tuple<string, string>>();
                pkAndTb.Clear();
                foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[1].Rows)
                {
                    //0 = tablename, 1 = colname
                    pkAndTb.Add(new Tuple<string, string>(dataRowItemPK.ItemArray[0].ToString(), dataRowItemPK.ItemArray[1].ToString()));
                }

                foreach (DataRow dataRowItem in codeHammerDataUtilContract.DataTableNames.Tables[0].Rows)
                {
                    string tablename = dataRowItem.ItemArray[2].ToString();
                    string colname = dataRowItem.ItemArray[3].ToString();
                    string colPK = string.Empty;

                    List<string> pkList = primaryKeysList.Distinct().ToList();
                    if (pkList.Any(x => x.Equals(dataRowItem.ItemArray[3].ToString())))
                    {
                        if (pkAndTb.Any(x => x.Item1.Equals(dataRowItem.ItemArray[2].ToString())))
                        {
                            colPK = dataRowItem.ItemArray[4].ToString();
                        }
                    }

                    string colDataType = dataRowItem.ItemArray[7].ToString();

                    //if(colname == "StockedQty")
                    //{
                    //}

                    string tableNameX = dataRowItem.ItemArray[2].ToString();

                    CodeHammerHiddenCheckBoxTreeNode colsNoCheckBoxDataType = new CodeHammerHiddenCheckBoxTreeNode();

                    //foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[2].Rows)
                    //{
                    //    if (dataRowItem.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[2]))
                    //    {
                    //        colsNoCheckBoxDataType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(156)))), ((int)(((byte)(214)))));
                    //        break;
                    //    }
                    //}

                    if (lastTable != dataRowItem.ItemArray[2].ToString())
                    {
                        lastTable = dataRowItem.ItemArray[2].ToString();
                        lastColNamePrimaryKey = dataRowItem.ItemArray[3].ToString();

                        tableNode = new TreeNode(lastTable);
                        tableNode.Tag = "Table";

                        switch (colDataType)
                        {
                            case "geography":
                            case "hierarchyid":
                            case "geometry":
                            case "time":
                            case "xml":
                            case "numeric":
                                tableNode.Text = colname;// + ": Datatype " + colDataType + " not supported";
                                tableNode.ToolTipText = "Tablename " + tablename + ": " + colname + ": Datatype " + colDataType + " not supported";

                                tableNode.Tag = "error";
                                tableNode.Name = "NOPK";
                                tableNode.ForeColor = Color.Red;
                                colsNoCheckBoxDataType.Tag = "error";
                                if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                    tableNode.Nodes.Add(colsNoCheckBoxDataType);
                                //tableNode.Expand();

                                break;

                            default:

                                if (!pkList.Any(x => x.Equals(lastColNamePrimaryKey)))
                                {
                                    tableNode.Text = tablename + ": Please setup primarykey";
                                    tableNode.ToolTipText = "Table " + tablename + ": Please setup primarykey";
                                    tableNode.Tag = "error";
                                    tableNode.Name = "NOPK";
                                    tableNode.ForeColor = Color.Red;
                                    tableNode.Nodes.Clear();
                                }

                                break;
                        }
                        if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                            mixedCheckBoxesTreeView1.Nodes.Add(tableNode);
                    }

                    TreeNode cols = new TreeNode();
                    CodeHammerHiddenCheckBoxTreeNode colsNoCheckBox = new CodeHammerHiddenCheckBoxTreeNode();

                    //foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[2].Rows)
                    //{
                    //    if (dataRowItem.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[2]))
                    //    {
                    //        colsNoCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(156)))), ((int)(((byte)(214)))));
                    //        break;
                    //    }
                    //}

                    if (colPK.Equals("1"))
                    {
                        switch (colDataType)
                        {
                            case "geography":
                            case "hierarchyid":
                            case "geometry":
                            case "xml":
                            case "time":
                            case "numeric":

                                cols.Checked = false;
                                colsNoCheckBox.Tag = "error";
                                colsNoCheckBox.Text = "Tablename " + tablename + ": " + colname;// + ": " + dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";
                                colsNoCheckBox.ToolTipText = dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";

                                colsNoCheckBox.ForeColor = Color.Red;
                                if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                    tableNode.Nodes.Add(colsNoCheckBox);
                                //tableNode.Expand();

                                break;

                            default:

                                cols.Checked = true;
                                colsNoCheckBox.Tag = "PK";
                                colsNoCheckBox.Text = dataRowItem.ItemArray[3].ToString();
                                colsNoCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
                                //colsNoCheckBox.ImageIndex = 1;
                                if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                    tableNode.Nodes.Add(colsNoCheckBox);

                                break;
                        }
                    }
                    else
                    {
                        ////Check for not NULL columns
                        CodeHammerHiddenCheckBoxTreeNode colsNoCheckBoxNotNull = new CodeHammerHiddenCheckBoxTreeNode();

                        ////Based on multiple pk keys
                        foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[2].Rows)
                        {
                            if (dataRowItem.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[2]))
                            {
                                // colsNoCheckBoxNotNull.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(156)))), ((int)(((byte)(214)))));
                                colsNoCheckBoxNotNull.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(210)))), ((int)(((byte)(138)))));
                                break;
                            }
                        }

                        if (dataRowItem.ItemArray[6].ToString() == "NO")
                        {
                            if (colname.Contains(" "))
                            {
                                colsNoCheckBox.Text = colname;// +": Avoid using spaces in names even if the system allows";
                                colsNoCheckBox.ToolTipText = "Tablename " + tablename + ": " + colname + ": Spaces in names not supported";
                                colsNoCheckBox.Tag = "error";
                                colsNoCheckBox.Name = "NOPK";
                                colsNoCheckBox.ForeColor = Color.Red;
                                if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                    tableNode.Nodes.Add(colsNoCheckBox);

                                //tableNode.Expand();
                            }
                            else
                            {
                                //foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[2].Rows)
                                //{
                                //    if (dataRowItem.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[2]))
                                //    {
                                //        colsNoCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(156)))), ((int)(((byte)(214)))));
                                //        break;
                                //    }
                                //}

                                switch (colDataType)
                                {
                                    case "geography":
                                    case "hierarchyid":
                                    case "geometry":
                                    case "xml":
                                    case "time":
                                    case "numeric":
                                        cols.Checked = false;
                                        colsNoCheckBox.Tag = "error";
                                        colsNoCheckBox.Text = colname;// + ": " + dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";
                                        colsNoCheckBox.ToolTipText = "Tablename " + tablename + ": " + dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";

                                        colsNoCheckBox.ForeColor = Color.Red;
                                        if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                            tableNode.Nodes.Add(colsNoCheckBox);
                                        //tableNode.Expand();
                                        break;
                                }

                                colsNoCheckBoxNotNull.Tag = "NOTNULLCOLUMN";
                                colsNoCheckBoxNotNull.Text = dataRowItem.ItemArray[3].ToString();
                                colsNoCheckBoxNotNull.ToolTipText = "Not allow NULL";
                                if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                    tableNode.Nodes.Add(colsNoCheckBoxNotNull);
                            }
                        }
                        else
                        {
                            switch (colDataType)
                            {
                                case "geography":
                                case "hierarchyid":
                                case "geometry":
                                case "xml":
                                case "time":
                                case "numeric":

                                    colsNoCheckBoxNotNull.Text = colname;// + ": " + dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";
                                    colsNoCheckBoxNotNull.ToolTipText = "Tablename " + tablename + ": " + dataRowItem.ItemArray[3].ToString() + " Datatype " + colDataType + " not supported";

                                    colsNoCheckBoxNotNull.Name = "error";
                                    colsNoCheckBoxNotNull.Tag = "error";
                                    colsNoCheckBoxNotNull.ForeColor = Color.Red;
                                    if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                        tableNode.Nodes.Add(colsNoCheckBoxNotNull);
                                    //tableNode.Expand();

                                    break;

                                default:

                                    /*Prepare for constrain check Orange color*/
                                    //foreach (DataRow dataRowItemPK in codeHammerDataUtilContract.DataTableNames.Tables[2].Rows)
                                    //{
                                    //    if (dataRowItem.ItemArray[3].ToString().Equals(dataRowItemPK.ItemArray[2]))
                                    //    {
                                    //        cols.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(156)))), ((int)(((byte)(214)))));
                                    //        break;
                                    //    }
                                    //}

                                    cols.Text = dataRowItem.ItemArray[3].ToString();
                                    cols.Tag = "col";
                                    if (pkAndTb.Any(x => x.Item1.Equals(tablename)))
                                        tableNode.Nodes.Add(cols);
                                    break;
                            }
                        }
                    }

                    if (tableNameX.Contains(" "))
                    {
                        tableNode.Tag = "error";
                        tableNode.Name = "NOPK";
                        tableNode.ForeColor = Color.Red;
                        tableNode.ToolTipText = "Tablename " + tableNameX + ": Spaces in names not supported";
                    }
                }
                bool stopProcess;
                CallRecursive(false, false, false, out stopProcess, mixedCheckBoxesTreeView1);
            }
            catch
            {
                Console.WriteLine(ioManagerContract.OutputError);
                //throw new Exception(ex);
            }
            finally
            {
                ShowTablesButton.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the SqlServerAuthenticationRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void SqlServerAuthenticationRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            loginNameLabel.Enabled = true;
            loginNameTextBox.Enabled = true;
            ////loginNameTextBox.BackColor = SystemColors.Window;

            passwordLabel.Enabled = true;
            passwordTextBox.Enabled = true;
            ////passwordTextBox.BackColor = SystemColors.Window;

            //EnableGenerateButton();
        }

        /// <summary>
        /// Starts the thread.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StartThread(object sender, EventArgs e)
        {
            outputTextBox.Clear();

            if (databaseComboBox.SelectedItem.Equals("Select database"))
            {
                Console.WriteLine("Database name is required");
                return;
            }

            if (string.IsNullOrEmpty(solutionTextBox.Text))
            {
                Console.WriteLine("Solution name is required");
                return;
            }

            if (string.IsNullOrEmpty(blSuffixTextBox.Text))
            {
                Console.WriteLine("Business suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(dalTextBox.Text))
            {
                Console.WriteLine("Data access suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(dtoTextBox.Text))
            {
                Console.WriteLine("Domain object suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(testTextBox.Text))
            {
                Console.WriteLine("Test suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(sqlPrefixTextBox.Text))
            {
                Console.WriteLine("Stored procedure prefix name is required");
                return;
            }

            if (string.IsNullOrEmpty(dataContractTextBox.Text))
            {
                Console.WriteLine("Data contract suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(serviceContractTextBox.Text))
            {
                Console.WriteLine("Service contract suffix name is required");
                return;
            }

            if (string.IsNullOrEmpty(serviceTextBox.Text))
            {
                Console.WriteLine("Service suffix name is required");
                return;
            }

            bool stopProcess;
            onlyDTO = false;
            if (mixedCheckBoxesTreeView1.SelectedNode != null && mixedCheckBoxesTreeView1.SelectedNode.ForeColor.Equals(Color.Red))
            {
                CallRecursive(true, false, true, out stopProcess, mixedCheckBoxesTreeView1);

                CleanLogReader();

                if (stopProcess)
                {
                    return;
                }
            }
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\CodeHammer"))
            {
                registryKey.SetValue("AuthenticationType", 2, RegistryValueKind.DWord);
                registryKey.SetValue("Login", loginNameTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("CreateMultipleFiles", 0, RegistryValueKind.DWord);

                registryKey.SetValue("DBServer", dbServerTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("blSuffixTextBox", blSuffixTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("dalTextBox", dalTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("dtoTextBox", dtoTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("dataContractTextBox", dataContractTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("serviceContractTextBox", serviceContractTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("serviceTextBox", serviceTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("solutionTextBox", solutionTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("sqlPrefixTextBox", sqlPrefixTextBox.Text, RegistryValueKind.String);
                registryKey.SetValue("testTextBox", testTextBox.Text, RegistryValueKind.String);
            }

            outputTextBox.Clear();
            ioManagerContract.LogBuilder = new StringBuilder();
            ioManagerContract.LogBuilder.Length = 0;
            ioManagerContract.LogBuilder.Capacity = 0;
            ioManagerContract.LogBuilder.AppendLine();
            ioManagerContract.LogBuilder.AppendLine("CodeHammer Log: " + DateTime.Now);
            ioManagerContract.LogBuilder.AppendLine();
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select an output directory";
                dialog.SelectedPath = outputDirectory;
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    outputDirectory = dialog.SelectedPath;
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\CodeHammer"))
                    {
                        registryKey.SetValue("OutputDirectory", outputDirectory, RegistryValueKind.String);
                    }
                    exportPath = outputDirectory;
                    Console.Write("CodeHammer generating...\n");
                   
                }
                else
                {
                    EnableControls(true);
                    return;
                }
            }

            // GenerateCodeHammer();

            lock (stateLock)
            {
                target = rng.Next(100);
            }
            Thread t = new Thread(new ThreadStart(GenerateCodeHammer));
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// Handles the Click event of the tableMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tableMenuStrip_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Opening event of the tableMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.
        /// </param>
        private void tableMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mixedCheckBoxesTreeView1.SelectedNode != null)
            {
                if (mixedCheckBoxesTreeView1.SelectedNode.Tag != null)
                {
                    if (mixedCheckBoxesTreeView1.SelectedNode.Tag.Equals("PK") || mixedCheckBoxesTreeView1.SelectedNode.Tag.Equals("NOTNULLCOLUMN"))
                    {
                        e.Cancel = true;
                    }
                }
                if (mixedCheckBoxesTreeView1.SelectedNode.Tag == null)
                {
                    e.Cancel = true;
                }
            }

            //CallRecursive(mixedCheckBoxesTreeView1);
        }

        /// <summary>
        /// Handles the Click event of the uncheckAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void uncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mixedCheckBoxesTreeView1.SelectedNode != null)
            {
                mixedCheckBoxesTreeView1.SelectedNode.Checked = false;

                foreach (TreeNode colItem in mixedCheckBoxesTreeView1.SelectedNode.Nodes)
                {
                    if (colItem.Tag != null && (colItem.Tag.Equals("PK") || colItem.Tag.Equals("NOTNULLCOLUMN")))
                    {
                        colItem.Checked = true;
                    }
                    else
                    {
                        colItem.Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the whiteBackgroundToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void whiteBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mixedCheckBoxesTreeView1.BackColor = Color.White;
            mixedCheckBoxesTreeView1.ForeColor = Color.Black;

            bgColor = "white";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the WindowsAuthenticationRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void WindowsAuthenticationRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            loginNameLabel.Enabled = false;
            loginNameTextBox.Enabled = false;
            loginNameTextBox.BackColor = SystemColors.InactiveBorder;
            loginNameTextBox.Text = "";

            passwordLabel.Enabled = false;
            passwordTextBox.Enabled = false;
            passwordTextBox.BackColor = SystemColors.InactiveBorder;
            passwordTextBox.Text = "";

            // EnableGenerateButton();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the castleWcfRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void castleWcfRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            container = "castle";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the unityRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void unityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            container = "unity";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ninjectWcfRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ninjectWcfRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            container = "ninject";
        }

        /// <summary>
        /// Handles the LinkClicked event of the outputTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkClickedEventArgs" /> instance containing the event data.</param>
        private void outputTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                if (e.LinkText.Contains(".sln"))
                {
                    //file://CodeHammer.sln
                    string link = outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox + "\\" + sln;
                    var info = System.Diagnostics.Process.Start(link);
                }

                if (e.LinkText.Contains(".sql"))
                {
                    //file://Log4NetSqlScript.sql
                    string link = outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox + ioManagerContract.CodeHammerLoggingLog4Net + "\\Log4NetSqlScript.sql";
                    var info = System.Diagnostics.Process.Start(link);
                }

                if (e.LinkText.Contains("http://localhost:8081/"))
                {
                    var info = System.Diagnostics.Process.Start(e.LinkText);
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the Load event of the CodeHammerForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CodeHammerForm_Load(object sender, EventArgs e)
        {
            // Instantiate the writer
            _writer = new TextBoxStreamWriter(outputTextBox);
            // Redirect the out Console stream
            Console.SetOut(_writer);

            buttonToolTip.ToolTipTitle = "Info";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;

            buttonToolTip.ShowAlways = true;

            buttonToolTip.AutoPopDelay = 6000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;

            buttonToolTip.SetToolTip(iocComboBox, helpManagerContract.CodeHammerIoCCastleWcfHelp());
            buttonToolTip.SetToolTip(wcfCallComboBox, helpManagerContract.CodeHammerWcfCallHelp());

            buttonToolTip.SetToolTip(wcfSecurityComboBox, helpManagerContract.CodeHammerWcfSecurityHelp());

            buttonToolTip.SetToolTip(unitTestComboBox, helpManagerContract.CodeHammerUnitTestHelp());

            buttonToolTip.SetToolTip(wcfPerformanceComboBox, helpManagerContract.CodeHammerWcfThrottlingHelp());
            buttonToolTip.SetToolTip(loggingComboBox, helpManagerContract.CodeHammerLog4NetHelp());

            buttonToolTip.SetToolTip(streamTypeComboBox, helpManagerContract.CodeHammerDataStreamHelp());

            buttonToolTip.SetToolTip(sqlOrmComboBox, helpManagerContract.CodeHammerOrmHelp());

            buttonToolTip.SetToolTip(pictureBox3, "IoC container.");

            buttonToolTip.SetToolTip(pictureBox4, "Stored Procedure.");

            buttonToolTip.SetToolTip(pictureBox7, "WCF instance management.");

            buttonToolTip.SetToolTip(pictureBox13, "ResponseFormat: Json/Xml.");

            buttonToolTip.SetToolTip(pictureBox10, "WCF performance.");

            buttonToolTip.SetToolTip(pictureBox9, "Unit test.");

            buttonToolTip.SetToolTip(pictureBox12, "Logging.");

            buttonToolTip.Active = true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the fluentNHibernateRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void fluentNHibernateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ResetList();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the wcfCallComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void wcfCallComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (wcfCallComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.InstanceCall = "InstanceContextMode.PerCall";
                    break;

                case 1:
                    ioManagerContract.InstanceCall = "InstanceContextMode.PerSession";
                    break;

                case 2:
                    ioManagerContract.InstanceCall = "InstanceContextMode.Single";
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the wcfSecurityComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void wcfSecurityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (wcfSecurityComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.WcfSecurityEnum = ioManagerContract.WcfSecurityEnum = IOManager.WcfSecurity.None;

                    break;

                case 1:

                    ioManagerContract.WcfSecurityEnum = ioManagerContract.WcfSecurityEnum = IOManager.WcfSecurity.CertificateAuthentication;

                    CertificatDialog cfr = new CertificatDialog();
                    cfr.ShowDialog(this);

                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the unitTestComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void unitTestComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (unitTestComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.UnitTest = false;
                    break;

                case 1:
                    ioManagerContract.UnitTest = true;
                    ioManagerContract.UnitTestTypeEnum = IOManager.UnitTestEnum.NUnit;
                    break;

                case 2:
                    ioManagerContract.UnitTest = true;
                    ioManagerContract.UnitTestTypeEnum = IOManager.UnitTestEnum.VSUnitTest;
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the loggingComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loggingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (loggingComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.Log4Net = false;
                    break;

                case 1:
                    ioManagerContract.Log4Net = true;
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the streamTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void streamTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (streamTypeComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.StreamFormatTypeEnum = IOManager.StreamFormat.Json;
                    break;

                case 1:
                    ioManagerContract.StreamFormatTypeEnum = IOManager.StreamFormat.Xml;
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the wcfPerformanceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void wcfPerformanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (wcfPerformanceComboBox.SelectedIndex)
            {
                case 0:
                    ioManagerContract.WcfPerformance = false;
                    break;

                case 1:
                    ioManagerContract.WcfPerformance = true;
                    break;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the crudCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void crudCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ioManagerContract.Crud = crudCheckBox.Checked;

            executeStoredprocedureCheckBox.Enabled = crudCheckBox.Checked;
            optionsTabControl.TabPages[2].Enabled = crudCheckBox.Checked;

            if (!crudCheckBox.Checked)
            {
                executeStoredprocedureCheckBox.Checked = crudCheckBox.Checked;
                iocComboBox.SelectedIndex = 0;
                wcfCallComboBox.SelectedIndex = 0;
                wcfSecurityComboBox.SelectedIndex = 0;
                unitTestComboBox.SelectedIndex = 0;
                loggingComboBox.SelectedIndex = 0;
                streamTypeComboBox.SelectedIndex = 0;
                wcfPerformanceComboBox.SelectedIndex = 0;
                sqlOrmComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the outputTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {
            int lastIndex = outputTextBox.SelectionStart;
            int lastLength = outputTextBox.SelectionLength;
            SyntaxHighlightFromRegex();
            outputTextBox.Select(lastIndex, lastLength);
            this.outputTextBox.SelectionColor = Color.White;
            this.outputTextBox.Invalidate();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the iocComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void iocComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Castle Wcf Facility
            if (iocComboBox.SelectedItem.ToString().Equals("Castle Wcf Facility"))
            {
                ioManagerContract.UseIoC = true;
                container = "castle";
            }

            if (iocComboBox.SelectedItem.ToString().Equals("No IoC container"))
            {
                ioManagerContract.UseIoC = true;
                container = "noioc";
            }

            if (iocComboBox.SelectedItem.ToString().Equals("Choose IoC container"))
            {
                ioManagerContract.UseIoC = false;
            }
        }

        private void loginAuthButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the LinkClicked event of the loginLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void loginLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the providerComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void providerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (providerComboBox.SelectedItem.ToString().Equals(ioManagerContract.GetSqlClient))
            {
                ioManagerContract.DBProvider = providerComboBox.SelectedItem.ToString();
            }

            if (providerComboBox.SelectedItem.ToString().Equals(ioManagerContract.GetOracleClient))
            {
                ioManagerContract.DBProvider = providerComboBox.SelectedItem.ToString();
            }
        }

        private void emptyDataLayerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (emptyDataLayerCheckBox.Checked)
            {
                executeStoredprocedureCheckBox.Checked = false;
                executeStoredprocedureCheckBox.Enabled = false;
            }
            else
            {
                executeStoredprocedureCheckBox.Enabled = true;
            }
        }

        #endregion Events

        #region Methods

        private void PullData()
        {
            if (!dgmlFuncContract.PullData())
            {
                Console.Write("Could not retrieve data");
                return;
            }
        }

        /// <summary>
        /// Generates the table dependency DGML.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private void GenerateTableDependencyDgml()
        {
            if (mixedCheckBoxesTreeView1.SelectedNode == null)
            {
                Console.Write("\nPlease choose table");
                return;
            }

            ResetList();

            CheckAll();

            bool stopProcess;
            if (mixedCheckBoxesTreeView1.SelectedNode != null && mixedCheckBoxesTreeView1.SelectedNode.ForeColor.Equals(Color.Red))
            {
                CallRecursive(true, false, true, out stopProcess, mixedCheckBoxesTreeView1);

                CleanLogReader();

                if (stopProcess)
                {
                    return;
                }
            }

            onlyDTO = true;
            outputTextBox.Clear();
            Console.Write("CodeHammer generating table dependencies...\n");

            lock (stateLock)
            {
                target = rng.Next(100);
            }

            ioManagerContract.SelectTableName = mixedCheckBoxesTreeView1.SelectedNode.Text;

            Thread t = new Thread(new ThreadStart(PullData));
            t.IsBackground = true;
            t.Start();
            t.Join(1000);

            //dgmlFuncContract.PullData();

            if (ioManagerContract.CodeEditorBuilder == null || ioManagerContract.CodeEditorBuilder.Length == 0)
            {
                outputTextBox.Clear();
                Console.Write("\n" + ioManagerContract.SelectTableName + " has no dependencies...\n");
                return;
            }

            GenerateForm gf = new GenerateForm();
            int runFaults = 0;
            bool run = true;
            while (run)
            {
                if (runFaults == 20)
                {
                    run = false;
                    outputTextBox.Clear();
                    Console.Write("\nPlease try generating again...\n");
                    return;
                }

                if (ioManagerContract.CodeEditorBuilder.Length > 0)
                {
                    run = false;
                }
                runFaults++;
                Thread.Sleep(1000);
            }

            runFaults = 0;

            try
            {
                outputTextBox.Clear();
                Console.Write("\nCodeHammer done generating table dependencies...");
                fileType = "dgml";

                gf.SetCodeEditor(ioManagerContract.CodeEditorBuilder, false, fileType, mixedCheckBoxesTreeView1.SelectedNode.Text, ioManagerContract.DependencyTableBuilder.ToString(), bgColor);
                gf.ShowDialog(this);
            }
            catch { }

            onlyDTO = false;

            generateButton.Enabled = true;
            generatorButton.Enabled = true;
            generateSuffixButton.Enabled = true;

            connectButton.Enabled = true;
            ShowTablesButton.Enabled = true;
            SelectAllButton.Enabled = true;
            ResetButton.Enabled = true;
            databaseComboBox.Enabled = true;
            executeStoredprocedureCheckBox.Enabled = true;
            emptyDataLayerCheckBox.Enabled = true;

            iocComboBox.Enabled = true;
            wcfPerformanceComboBox.Enabled = true;
            unitTestComboBox.Enabled = true;
            sqlOrmComboBox.Enabled = true;
            wcfCallComboBox.Enabled = true;
            wcfSecurityComboBox.Enabled = true;
            loggingComboBox.Enabled = true;
            streamTypeComboBox.Enabled = true;

            solutionTextBox.Enabled = true;
            blSuffixTextBox.Enabled = true;
            dalTextBox.Enabled = true;
            dtoTextBox.Enabled = true;
            testTextBox.Enabled = true;
            sqlPrefixTextBox.Enabled = true;
            dataContractTextBox.Enabled = true;
            serviceContractTextBox.Enabled = true;
            serviceTextBox.Enabled = true;
        }

        private void GenerateDependencyDTO()
        {
            if (mixedCheckBoxesTreeView1.SelectedNode == null)
            {
                Console.Write("\nPlease choose table");
                return;
            }

            ResetList();

            CheckAll();

            bool stopProcess;
            if (mixedCheckBoxesTreeView1.SelectedNode != null && mixedCheckBoxesTreeView1.SelectedNode.ForeColor.Equals(Color.Red))
            {
                CallRecursive(true, false, true, out stopProcess, mixedCheckBoxesTreeView1);

                CleanLogReader();

                if (stopProcess)
                {
                    return;
                }
            }

            onlyDTO = true;
            onlyDependencyDTO = true;
            outputTextBox.Clear();
            Console.Write("CodeHammer generating dependency Domain Object...\n");

            //lock (stateLock)
            //{
            //    target = rng.Next(100);
            //}
            //Thread t = new Thread(new ThreadStart(GenerateCodeHammer));
            //t.IsBackground = true;
            //t.Start();
            //t.Join(1000);
            GenerateCodeHammer();

            GenerateForm gf = new GenerateForm();
            int runFaults = 0;
            bool run = true;
            while (run)
            {
                if (runFaults == 200)
                {
                    run = false;
                    Console.Write("\nPlease try generating again...");
                }

                if (ioManagerContract.CodeEditorBuilder.Length > 0)
                {
                    run = false;
                }
                runFaults++;
                Thread.Sleep(1000);
            }

            runFaults = 0;

            try
            {
                outputTextBox.Clear();
                Console.Write("\nCodeHammer done generating dependency Domain Object...");
                fileType = "cs";

                gf.SetCodeEditor(null, false, fileType, mixedCheckBoxesTreeView1.SelectedNode.Text, ioManagerContract.CodeEditorBuilder.ToString(), bgColor);
                gf.ShowDialog(this);
            }
            catch { }

            onlyDTO = false;
            onlyDependencyDTO = false;

            generateButton.Enabled = true;
            generatorButton.Enabled = true;
            generateSuffixButton.Enabled = true;

            connectButton.Enabled = true;
            ShowTablesButton.Enabled = true;
            SelectAllButton.Enabled = true;
            ResetButton.Enabled = true;
            databaseComboBox.Enabled = true;
            executeStoredprocedureCheckBox.Enabled = true;
            emptyDataLayerCheckBox.Enabled = true;

            iocComboBox.Enabled = true;
            wcfPerformanceComboBox.Enabled = true;
            unitTestComboBox.Enabled = true;
            sqlOrmComboBox.Enabled = true;
            wcfCallComboBox.Enabled = true;
            wcfSecurityComboBox.Enabled = true;
            loggingComboBox.Enabled = true;
            streamTypeComboBox.Enabled = true;

            solutionTextBox.Enabled = true;
            blSuffixTextBox.Enabled = true;
            dalTextBox.Enabled = true;
            dtoTextBox.Enabled = true;
            testTextBox.Enabled = true;
            sqlPrefixTextBox.Enabled = true;
            dataContractTextBox.Enabled = true;
            serviceContractTextBox.Enabled = true;
            serviceTextBox.Enabled = true;
        }

        /// <summary>
        /// Generates the data contract.
        /// </summary>
        private void GenerateDataContract()
        {
            ioManagerContract.CodeEditorBuilder = new StringBuilder();
            ioManagerContract.CodeEditorBuilder.Clear();

            if (mixedCheckBoxesTreeView1.SelectedNode == null)
            {
                Console.Write("\nPlease choose table");
                return;
            }

            ResetList();

            CheckAll();

            bool stopProcess;
            if (mixedCheckBoxesTreeView1.SelectedNode != null && mixedCheckBoxesTreeView1.SelectedNode.ForeColor.Equals(Color.Red))
            {
                CallRecursive(true, false, true, out stopProcess, mixedCheckBoxesTreeView1);

                CleanLogReader();

                if (stopProcess)
                {
                    return;
                }
            }

            onlyDTO = true;
            onlyDataContract = true;
            outputTextBox.Clear();
            Console.Write("CodeHammer generating DataContract...\n");

            //lock (stateLock)
            //{
            //    target = rng.Next(100);
            //}
            //Thread t = new Thread(new ThreadStart(GenerateCodeHammer));
            //t.IsBackground = true;
            //t.Start();

            //t.Join(1000);
            GenerateCodeHammer();

            GenerateForm gf = new GenerateForm();
            int runFaults = 0;
            bool run = true;
            while (run)
            {
                if (runFaults == 200)
                {
                    run = false;
                    Console.Write("\nPlease try generating again...");
                }

                if (ioManagerContract.CodeEditorBuilder.Length > 0)
                {
                    run = false;
                }
                runFaults++;
                Thread.Sleep(1000);
            }

            runFaults = 0;

            try
            {
                outputTextBox.Clear();
                Console.Write("\nCodeHammer done generating DataContract...");
                fileType = "cs";

                gf.SetCodeEditor(null, true, fileType, mixedCheckBoxesTreeView1.SelectedNode.Text, ioManagerContract.CodeEditorBuilder.ToString(), bgColor);
                gf.ShowDialog(this);
            }
            catch
            {
                Console.Write("\nCodeHammer could not generate DataContract. Please try again!");
            }

            onlyDTO = false;
            onlyDataContract = false;

            generateButton.Enabled = true;
            generatorButton.Enabled = true;
            generateSuffixButton.Enabled = true;

            connectButton.Enabled = true;
            ShowTablesButton.Enabled = true;
            SelectAllButton.Enabled = true;
            ResetButton.Enabled = true;
            databaseComboBox.Enabled = true;
            executeStoredprocedureCheckBox.Enabled = true;
            emptyDataLayerCheckBox.Enabled = true;

            iocComboBox.Enabled = true;
            wcfPerformanceComboBox.Enabled = true;
            unitTestComboBox.Enabled = true;
            sqlOrmComboBox.Enabled = true;
            wcfCallComboBox.Enabled = true;
            wcfSecurityComboBox.Enabled = true;
            loggingComboBox.Enabled = true;
            streamTypeComboBox.Enabled = true;

            solutionTextBox.Enabled = true;
            blSuffixTextBox.Enabled = true;
            dalTextBox.Enabled = true;
            dtoTextBox.Enabled = true;
            testTextBox.Enabled = true;
            sqlPrefixTextBox.Enabled = true;
            dataContractTextBox.Enabled = true;
            serviceContractTextBox.Enabled = true;
            serviceTextBox.Enabled = true;
        }

        /// <summary>
        /// Generates the dto.
        /// </summary>
        private void GenerateDTO()
        {
            ioManagerContract.CodeEditorBuilder = new StringBuilder();
            ioManagerContract.CodeEditorBuilder.Clear();

            if (mixedCheckBoxesTreeView1.SelectedNode == null)
            {
                Console.Write("\nPlease choose table");
                return;
            }

            ResetList();

            CheckAll();

            bool stopProcess;
            if (mixedCheckBoxesTreeView1.SelectedNode != null && mixedCheckBoxesTreeView1.SelectedNode.ForeColor.Equals(Color.Red))
            {
                CallRecursive(true, false, true, out stopProcess, mixedCheckBoxesTreeView1);

                CleanLogReader();

                if (stopProcess)
                {
                    return;
                }
            }

            onlyDTO = true;
            outputTextBox.Clear();
            Console.Write("CodeHammer generating Domain Object...\n");

            //lock (stateLock)
            //{
            //    target = rng.Next(100);
            //}
            //Thread t = new Thread(new ThreadStart(GenerateCodeHammer));
            //t.IsBackground = true;
            //t.Start();

            GenerateCodeHammer();

            GenerateForm gf = new GenerateForm();
            int runFaults = 0;
            bool run = true;
            while (run)
            {
                if (runFaults == 200)
                {
                    run = false;
                    Console.Write("\nPlease try generating again...");
                }

                if (ioManagerContract.CodeEditorBuilder.Length > 0)
                {
                    run = false;
                }
                runFaults++;
                Thread.Sleep(1000);
            }

            runFaults = 0;

            try
            {
                outputTextBox.Clear();
                Console.Write("\nCodeHammer done generating Domain Object...");
                fileType = "cs";

                gf.SetCodeEditor(null, false, fileType, mixedCheckBoxesTreeView1.SelectedNode.Text, ioManagerContract.CodeEditorBuilder.ToString(), bgColor);
                gf.ShowDialog(this);
            }
            catch
            {
                Console.Write("\nCodeHammer could not generate Domain Object. Please try again!");
            }

            onlyDTO = false;

            generateButton.Enabled = true;
            generatorButton.Enabled = true;
            generateSuffixButton.Enabled = true;

            connectButton.Enabled = true;
            ShowTablesButton.Enabled = true;
            SelectAllButton.Enabled = true;
            ResetButton.Enabled = true;
            databaseComboBox.Enabled = true;
            executeStoredprocedureCheckBox.Enabled = true;
            emptyDataLayerCheckBox.Enabled = true;

            iocComboBox.Enabled = true;
            wcfPerformanceComboBox.Enabled = true;
            unitTestComboBox.Enabled = true;
            sqlOrmComboBox.Enabled = true;
            wcfCallComboBox.Enabled = true;
            wcfSecurityComboBox.Enabled = true;
            loggingComboBox.Enabled = true;
            streamTypeComboBox.Enabled = true;

            solutionTextBox.Enabled = true;
            blSuffixTextBox.Enabled = true;
            dalTextBox.Enabled = true;
            dtoTextBox.Enabled = true;
            testTextBox.Enabled = true;
            sqlPrefixTextBox.Enabled = true;
            dataContractTextBox.Enabled = true;
            serviceContractTextBox.Enabled = true;
            serviceTextBox.Enabled = true;
        }

        /// <summary>
        /// Cleans the log reader.
        /// </summary>
        private void CleanLogReader()
        {
            if (outputTextBox.Find("Avoid using spaces") >= 0)
            {
                outputTextBox.Lines = outputTextBox.Lines.Distinct().ToArray();
                outputTextBox.Text = Regex.Replace(outputTextBox.Text, @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline);
            }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void EnableControls(bool state)
        {
            databaseComboBox.Enabled = state;
            executeStoredprocedureCheckBox.Enabled = state;
            emptyDataLayerCheckBox.Enabled = state;
            crudCheckBox.Enabled = state;
            ShowTablesButton.Enabled = state;
            SelectAllButton.Enabled = state;
            ResetButton.Enabled = state;
            generateButton.Enabled = state;
            generateSuffixButton.Enabled = state;
            generatorButton.Enabled = state;

            //connectButton.Enabled = state;

            iocComboBox.Enabled = state;
            // fluentNHibernateRadioButton.Enabled = state;
            wcfCallComboBox.Enabled = state;
            wcfSecurityComboBox.Enabled = state;
            unitTestComboBox.Enabled = state;
            wcfPerformanceComboBox.Enabled = state;
            sqlOrmComboBox.Enabled = state;
            wcfCallComboBox.Enabled = state;
            wcfSecurityComboBox.Enabled = state;
            loggingComboBox.Enabled = state;
            streamTypeComboBox.Enabled = state;

            blSuffixTextBox.Enabled = state;
            dalTextBox.Enabled = state;
            dtoTextBox.Enabled = state;
            dataContractTextBox.Enabled = state;
            serviceContractTextBox.Enabled = state;
            serviceTextBox.Enabled = state;
            testTextBox.Enabled = state;
            solutionTextBox.Enabled = state;
            sqlPrefixTextBox.Enabled = state;

            loggingComboBox.Enabled = state;
            streamTypeComboBox.Enabled = state;
            clearFolderCheckBox.Enabled = state;
        }

        /// <summary>
        /// Updates the count.
        /// </summary>

        /// <summary>
        /// Connects to database server.
        /// </summary>
        private void ConnectToDbServer()
        {
            try
            {
                if (!string.IsNullOrEmpty(dbServerTextBox.Text))
                {
                    //connect to db
                    string systemMessage = string.Empty;
                    string innersystemMessage = string.Empty;
                    List<string> dbList = null;

                    if (providerComboBox.SelectedIndex == -1)
                    {
                        Console.WriteLine("Provider not selected!");
                    }

                    if (!dbDataSupportAdapterContract.PopulateDataBaseSqlServer(loginNameTextBox.Text, passwordTextBox.Text, dbServerTextBox.Text, out dbList, out innersystemMessage))
                    {
                        Console.WriteLine("Database not connected");
                        return;
                    }

                    databaseComboBox.Items.Clear();
                    dbList.Sort();
                    databaseComboBox.Items.Add("Select database");
                    foreach (var item in dbList)
                    {
                        databaseComboBox.Items.Add(item);
                    }

                    databaseComboBox.SelectedIndex = 0;
                    EnableControls(true);

                    databaseComboBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
                    databaseComboBox.Refresh();
                    Thread.Sleep(1000);
                    databaseComboBox.BackColor = Color.White;
                    Console.WriteLine("Database connected");

                    if (databaseComboBox.Items.Count == 0)
                    {
                        databaseComboBox.BackColor = Color.Red;
                        databaseComboBox.Refresh();
                        Thread.Sleep(1000);
                        databaseComboBox.BackColor = Color.White;
                        databaseComboBox.Items.Clear();
                        mixedCheckBoxesTreeView1.Nodes.Clear();
                        Console.WriteLine("Database not connected");
                    }
                }
                else
                {
                    databaseComboBox.BackColor = Color.Red;
                    databaseComboBox.Refresh();
                    Thread.Sleep(1000);
                    databaseComboBox.BackColor = Color.White;
                    databaseComboBox.Items.Clear();
                    mixedCheckBoxesTreeView1.Nodes.Clear();

                    Console.WriteLine("Please choose server");
                }
            }
            catch
            {
                databaseComboBox.BackColor = Color.Red;
                databaseComboBox.Refresh();
                Thread.Sleep(1000);
                databaseComboBox.BackColor = Color.White;
                databaseComboBox.Items.Clear();
                mixedCheckBoxesTreeView1.Nodes.Clear();

                Console.WriteLine("Database not connected");
            }
        }

        /// <summary>
        /// Nhibernates the check for key relations.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        private void NhibernateCheckForKeyRelations(string tableName)
        {
            ioManagerContract.PkKeysHoldRelations.Clear();
            List<string> messageList = new List<string>();
            messageList.Clear();
            List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos = null;
            codeHammerGeneratorContract.InitNHibernateDTOGeneration(tableName, ioManagerContract.DbConnection, out CodeHammerPropAndValueDtos, false);
            string parentTableName = string.Empty;
            strFluentNHibernate.AppendLine("Found missing foreign key relationships");

            if (ioManagerContract.PkKeys.Count > 1)
            {
                foreach (CodeHammerPkClass item in ioManagerContract.PkKeys.Distinct().ToList())
                {
                    ioManagerContract.PkKeysHoldRelations.Add(item);
                }

                // StartHere:
                List<CodeHammerPkClass> tempList = new List<CodeHammerPkClass>();
                foreach (CodeHammerPkClass item in ioManagerContract.PkKeys.Distinct().ToList())
                {
                    tempList.Add(item);
                }

                foreach (CodeHammerPkClass pkClass in tempList)
                {
                    if (!string.IsNullOrEmpty(pkClass.Name) && !string.IsNullOrEmpty(pkClass.ForeignKeyRef) && !string.IsNullOrEmpty(pkClass.ForeignKeyRefID))
                    {
                        if (!messageList.Any(x => x.Equals("Table: " + pkClass.Name + " -> Table: " + pkClass.ForeignKeyRef + " Column: " + pkClass.ForeignKeyRefID)))
                        {
                            messageList.Add("Table: " + pkClass.Name + " -> Table: " + pkClass.ForeignKeyRef + " Column: " + pkClass.ForeignKeyRefID);
                            strFluentNHibernate.AppendLine("Table: " + pkClass.Name + " -> Table: " + pkClass.ForeignKeyRef + " Column: " + pkClass.ForeignKeyRefID);
                            ioManagerContract.PkKeysHoldRelations.Add(pkClass);
                        }
                    }
                }

                ////Used for finding foreign key relations in development ------
                ////foreach (CodeHammerPkClass pkClass in ioManagerContract.PkKeysRelations.Distinct().ToList())
                ////{
                ////    if (string.IsNullOrEmpty(parentTableName))
                ////    {
                ////        parentTableName = pkClass.ForeignKeyRef;
                ////        //ioManagerContract.PkKeys.Clear();
                ////        ioManagerContract.PkKeysHoldRelations.Add(pkClass);
                ////        ioManagerContract.PkKeysRelations.Remove(pkClass);

                ////        codeHammerGeneratorContract.InitNHibernateDTOGneration(parentTableName, ioManagerContract.dbConnection, out CodeHammerPropAndValueDtos);
                ////        goto StartHere;
                ////    }
                ////    else if (parentTableName != pkClass.ForeignKeyRef)
                ////    {
                ////        parentTableName = pkClass.ForeignKeyRef;
                ////        //ioManagerContract.PkKeys.Clear();
                ////        ioManagerContract.PkKeysHoldRelations.Add(pkClass);
                ////        ioManagerContract.PkKeysRelations.Remove(pkClass);
                ////        codeHammerGeneratorContract.InitNHibernateDTOGneration(parentTableName, ioManagerContract.dbConnection, out CodeHammerPropAndValueDtos);
                ////        goto StartHere;
                ////    }
                ////}
            }
            messageList.Clear();
            Console.WriteLine(strFluentNHibernate.ToString());
        }

        /// <summary>
        /// Generates the code hammer.
        /// </summary>
        private void GenerateCodeHammer()
        {
            if (databaseComboBox.Items.Count == 0)
            {
                outputTextBox.Clear();
                Console.WriteLine("Select database");
                return;
            }

            if (fluentNHibernateRadioButton.Checked)
            {
                ioManagerContract.UseOrm = fluentNHibernateRadioButton.Checked;
            }
            else
            {
                ioManagerContract.UseOrm = false;
            }

            ioManagerContract.Crud = crudCheckBox.Checked;

            string connectionString;
            string configConnection = string.Empty;
            try
            {
                generateButton.Enabled = false;
                generatorButton.Enabled = false;
                generateSuffixButton.Enabled = false;

                connectButton.Enabled = false;
                ShowTablesButton.Enabled = false;
                SelectAllButton.Enabled = false;
                ResetButton.Enabled = false;
                databaseComboBox.Enabled = false;
                executeStoredprocedureCheckBox.Enabled = false;
                emptyDataLayerCheckBox.Enabled = false;

                iocComboBox.Enabled = false;
                wcfPerformanceComboBox.Enabled = false;
                unitTestComboBox.Enabled = false;
                sqlOrmComboBox.Enabled = false;
                wcfCallComboBox.Enabled = false;
                wcfSecurityComboBox.Enabled = false;
                loggingComboBox.Enabled = false;
                streamTypeComboBox.Enabled = false;

                solutionTextBox.Enabled = false;
                blSuffixTextBox.Enabled = false;
                dalTextBox.Enabled = false;
                dtoTextBox.Enabled = false;
                testTextBox.Enabled = false;
                sqlPrefixTextBox.Enabled = false;
                dataContractTextBox.Enabled = false;
                serviceContractTextBox.Enabled = false;
                serviceTextBox.Enabled = false;

                configConnection = "|" + dbServerTextBox.Text + "|" + databaseComboBox.SelectedItem.ToString() + "|" + loginNameTextBox.Text + "|" + passwordTextBox.Text + ";";
                connectionString = "Server=" + dbServerTextBox.Text + "; Database=" + databaseComboBox.SelectedItem.ToString() + "; User ID=" + loginNameTextBox.Text + "; Password=" + passwordTextBox.Text + ";";

                //// Let the user select where to create the C# and SQL code

                Dictionary<string, List<string>> tablesAndColumnDic = new Dictionary<string, List<string>>();
                List<string> tableNames = new List<string>();

                StringBuilder strError = new StringBuilder();
                strError.Length = 0;
                strError.Capacity = 0;
                tablesAndColumnDic.Clear();
                tableNames.Clear();
                foreach (TreeNode tr in mixedCheckBoxesTreeView1.Nodes)
                {
                    tableNames.Clear();
                    if (tr.Checked)
                    {
                        foreach (TreeNode childItem in tr.Nodes)
                        {
                            if (childItem.FullPath.Contains("Avoid using spaces"))
                            {
                                outputTextBox.Clear();
                                strError.AppendLine("Error: " + childItem.FullPath.Replace(@"\", string.Empty));
                                break;
                            }
                            else if (childItem.FullPath.Contains("Please setup primarykey for table"))
                            {
                                outputTextBox.Clear();
                                strError.AppendLine("Error: " + childItem.FullPath.Replace(@"\", string.Empty));
                                break;
                            }

                            if (childItem.Tag != null && (childItem.Tag.Equals("PK") || childItem.Tag.Equals("NOTNULLCOLUMN")))
                            {
                                if (childItem.PrevNode != null && childItem.PrevNode.Text.Contains("not supported"))
                                {
                                    if (childItem.PrevNode.Checked)
                                    {
                                        childItem.Checked = false;
                                        generateButton.Enabled = true;
                                        generatorButton.Enabled = true;
                                        generateSuffixButton.Enabled = true;

                                        connectButton.Enabled = true;
                                        ShowTablesButton.Enabled = true;
                                        SelectAllButton.Enabled = true;
                                        ResetButton.Enabled = true;
                                        databaseComboBox.Enabled = true;
                                        executeStoredprocedureCheckBox.Enabled = true;
                                        emptyDataLayerCheckBox.Enabled = true;

                                        iocComboBox.Enabled = true;
                                        wcfPerformanceComboBox.Enabled = true;
                                        unitTestComboBox.Enabled = true;
                                        sqlOrmComboBox.Enabled = true;
                                        wcfCallComboBox.Enabled = true;
                                        wcfSecurityComboBox.Enabled = true;
                                        loggingComboBox.Enabled = true;
                                        streamTypeComboBox.Enabled = true;

                                        solutionTextBox.Enabled = true;
                                        blSuffixTextBox.Enabled = true;
                                        dalTextBox.Enabled = true;
                                        dtoTextBox.Enabled = true;
                                        testTextBox.Enabled = true;
                                        sqlPrefixTextBox.Enabled = true;
                                        dataContractTextBox.Enabled = true;
                                        serviceContractTextBox.Enabled = true;
                                        serviceTextBox.Enabled = true;

                                        outputTextBox.Clear();
                                        strError.AppendLine("Error: SQL Server data type : " + childItem.PrevNode.FullPath.Replace(@"\", string.Empty));
                                        break;
                                    }
                                    //throw new Exception("SQL Server data type : " + childItem.PrevNode.Text + " not supported");
                                }
                                tableNames.Add(childItem.Text);
                            }

                            if (childItem.Checked)
                            {
                                if (childItem.PrevNode != null && childItem.PrevNode.Text.Contains("not supported"))
                                {
                                    if (childItem.PrevNode.Checked)
                                    {
                                        childItem.Checked = false;
                                        generateButton.Enabled = true;
                                        generatorButton.Enabled = true;
                                        generateSuffixButton.Enabled = true;

                                        connectButton.Enabled = true;
                                        ShowTablesButton.Enabled = true;
                                        SelectAllButton.Enabled = true;
                                        ResetButton.Enabled = true;
                                        databaseComboBox.Enabled = true;
                                        executeStoredprocedureCheckBox.Enabled = true;
                                        emptyDataLayerCheckBox.Enabled = true;

                                        iocComboBox.Enabled = true;
                                        wcfPerformanceComboBox.Enabled = true;
                                        unitTestComboBox.Enabled = true;
                                        sqlOrmComboBox.Enabled = true;
                                        wcfCallComboBox.Enabled = true;
                                        wcfSecurityComboBox.Enabled = true;
                                        loggingComboBox.Enabled = true;
                                        streamTypeComboBox.Enabled = true;

                                        solutionTextBox.Enabled = true;
                                        blSuffixTextBox.Enabled = true;
                                        dalTextBox.Enabled = true;
                                        dtoTextBox.Enabled = true;
                                        testTextBox.Enabled = true;
                                        sqlPrefixTextBox.Enabled = true;
                                        dataContractTextBox.Enabled = true;
                                        serviceContractTextBox.Enabled = true;
                                        serviceTextBox.Enabled = true;

                                        outputTextBox.Clear();
                                        strError.AppendLine("Error: SQL Server data type : " + childItem.PrevNode.FullPath.Replace(@"\", string.Empty));
                                        break;
                                    }
                                    //throw new Exception("SQL Server data type : " + childItem.PrevNode.Text + " not supported");
                                }

                                tableNames.Add(childItem.Text);
                            }
                        }
                        List<string> tempTable = new List<string>();
                        tempTable.AddRange(tableNames);
                        if (!tr.Name.Equals("NOPK"))
                        {
                            if (tr.PrevNode != null && tr.PrevNode.Text.Contains("not supported"))
                            {
                                if (tr.PrevNode.Checked)
                                {
                                    tr.Checked = false;
                                    generateButton.Enabled = true;
                                    generatorButton.Enabled = true;
                                    generateSuffixButton.Enabled = true;

                                    connectButton.Enabled = true;
                                    ShowTablesButton.Enabled = true;
                                    SelectAllButton.Enabled = true;
                                    ResetButton.Enabled = true;
                                    databaseComboBox.Enabled = true;
                                    executeStoredprocedureCheckBox.Enabled = true;
                                    emptyDataLayerCheckBox.Enabled = true;

                                    iocComboBox.Enabled = true;
                                    wcfPerformanceComboBox.Enabled = true;
                                    unitTestComboBox.Enabled = true;
                                    sqlOrmComboBox.Enabled = true;
                                    wcfCallComboBox.Enabled = true;
                                    wcfSecurityComboBox.Enabled = true;
                                    loggingComboBox.Enabled = true;
                                    streamTypeComboBox.Enabled = true;

                                    solutionTextBox.Enabled = true;
                                    blSuffixTextBox.Enabled = true;
                                    dalTextBox.Enabled = true;
                                    dtoTextBox.Enabled = true;
                                    testTextBox.Enabled = true;
                                    sqlPrefixTextBox.Enabled = true;
                                    dataContractTextBox.Enabled = true;
                                    serviceContractTextBox.Enabled = true;
                                    serviceTextBox.Enabled = true;

                                    outputTextBox.Clear();
                                    strError.AppendLine("Error: SQL Server data type : " + tr.PrevNode.FullPath.Replace(@"\", string.Empty));
                                    break;
                                }

                                //throw new Exception("SQL Server data type : " + tr.PrevNode.Text + " not supported");
                            }

                            tablesAndColumnDic.Add(tr.Text, tempTable);
                        }
                        else
                        {
                            if (tr.PrevNode != null && tr.PrevNode.Text.Contains("not supported"))
                            {
                                if (tr.PrevNode.Checked)
                                {
                                    tr.Checked = false;
                                    generateButton.Enabled = true;
                                    generatorButton.Enabled = true;
                                    generateSuffixButton.Enabled = true;

                                    connectButton.Enabled = true;
                                    ShowTablesButton.Enabled = true;
                                    SelectAllButton.Enabled = true;
                                    ResetButton.Enabled = true;
                                    databaseComboBox.Enabled = true;
                                    executeStoredprocedureCheckBox.Enabled = true;
                                    emptyDataLayerCheckBox.Enabled = true;

                                    iocComboBox.Enabled = true;
                                    wcfPerformanceComboBox.Enabled = true;
                                    unitTestComboBox.Enabled = true;
                                    sqlOrmComboBox.Enabled = true;
                                    wcfCallComboBox.Enabled = true;
                                    wcfSecurityComboBox.Enabled = true;
                                    loggingComboBox.Enabled = true;
                                    streamTypeComboBox.Enabled = true;

                                    solutionTextBox.Enabled = true;
                                    blSuffixTextBox.Enabled = true;
                                    dalTextBox.Enabled = true;
                                    dtoTextBox.Enabled = true;
                                    testTextBox.Enabled = true;
                                    sqlPrefixTextBox.Enabled = true;
                                    dataContractTextBox.Enabled = true;
                                    serviceContractTextBox.Enabled = true;
                                    serviceTextBox.Enabled = true;

                                    outputTextBox.Clear();
                                    strError.AppendLine("Error: SQL Server data type : " + tr.PrevNode.FullPath.Replace(@"\", string.Empty));
                                    break;
                                }
                                //throw new Exception("SQL Server data type : " + tr.PrevNode.Text + " not supported");
                            }
                        }
                    }
                }

                if (strError.Length != 0)
                {
                    outputTextBox.Clear();
                    ioManagerContract.LogBuilder.AppendLine(strError.ToString());
                    ioManagerContract.LogBuilder.AppendLine();
                }

                if (crudCheckBox.Checked)
                {
                    if (tablesAndColumnDic.Count == 0)
                    {
                        outputTextBox.Clear();

                        Console.WriteLine("Please choose tables");
                        EnableControls(true);
                        return;
                    }
                }

                ////Copy Project to destination and unzip.

                string name = string.Empty;

                SuffixDto.Instance().SolutionTextBox = !string.IsNullOrEmpty(blSuffixTextBox.Text) ? solutionTextBox.Text : "CodeHammer";

                SuffixDto.Instance().TestTextBox = !string.IsNullOrEmpty(testTextBox.Text) ? testTextBox.Text : "Test";

                SuffixDto.Instance().BlSuffixTextBox = !string.IsNullOrEmpty(blSuffixTextBox.Text) ? blSuffixTextBox.Text : "Bl";

                SuffixDto.Instance().DalTextBox = !string.IsNullOrEmpty(dalTextBox.Text) ? dalTextBox.Text : "Dal";

                SuffixDto.Instance().DataContractTextBox = !string.IsNullOrEmpty(dataContractTextBox.Text) ? dataContractTextBox.Text : "DataContract";

                SuffixDto.Instance().DtoTextBox = !string.IsNullOrEmpty(dtoTextBox.Text) ? dtoTextBox.Text : "Domain";

                SuffixDto.Instance().ServiceContractTextBox = !string.IsNullOrEmpty(serviceContractTextBox.Text) ? serviceContractTextBox.Text : "ServiceContract";

                SuffixDto.Instance().ServiceTextBox = !string.IsNullOrEmpty(serviceTextBox.Text) ? serviceTextBox.Text : "Service";

                SuffixDto.Instance().SolutionTextBox = !string.IsNullOrEmpty(solutionTextBox.Text) ? solutionTextBox.Text : "CodeHammer";

                SuffixDto.Instance().SqlPrefixTextBox = !string.IsNullOrEmpty(sqlPrefixTextBox.Text) ? sqlPrefixTextBox.Text : "SP";

                //if (vs2012RadioButton.Checked)
                //{
                if (!onlyDTO && !onlyDataContract)
                {
                    codeHammerDataUtilContract.CopyCodeHammerGetCodeHammerProject("VS2012", outputDirectory + @"\" + ioManagerContract.CodeHammerProjectNameZip);
                    Thread.Sleep(1500);
                    codeHammerDataUtilContract.Zip(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectNameZip, outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text);

                    SuffixDto.Instance().CodeHammerPath = outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text;

                    Thread.Sleep(2500);

                    if (!File.Exists(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectNameZip))
                    {
                        Console.Clear();
                        Console.WriteLine("CodeHammerServiceProject is missing. Please try again!");
                        return;
                    }

                    string oldFilePath = outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + "\\" + @"CodeHammer.sln"; // Full path of old file
                    string newFilePath = outputDirectory + @"\" + @"\" + SuffixDto.Instance().SolutionTextBox + "\\" + solutionTextBox.Text + ".sln"; // Full path of new file

                    if (!SuffixDto.Instance().SolutionTextBox.Equals("CodeHammer"))
                    {
                        File.Move(oldFilePath, newFilePath);
                    }

                    Thread.Sleep(1500);

                    File.Delete(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectNameZip);
                    //}
                }

                ioManagerContract.EmptyDataLayerCheckBox = emptyDataLayerCheckBox.Checked;

                //// Generate the SQL and C# code
                codeHammerGeneratorContract.CodeHammerGenerate(onlyDependencyDTO, onlyDataContract, onlyDTO, container, selectTables, tablesAndColumnDic, crudCheckBox.Checked, resultDataOptions, outputDirectory, connectionString, configConnection, true, "CodeHammerRepository", "CodeHammerBusinessLogic", "Domain", "", executeStoredprocedureCheckBox.Checked, ioManagerContract.InstanceCall, ioManagerContract.WcfPerformance, (int)ioManagerContract.WcfSecurityEnum);

                //// Save the current state to the Registry
            }
            catch (System.IO.IOException se)
            {
                try
                {
                    File.Delete(outputDirectory + "\\CodeHammerServiceProject.zip");
                }
                catch { }

                outputTextBox.Clear();
                ioManagerContract.LogBuilder.AppendLine("Remember:");
                ioManagerContract.LogBuilder.AppendLine("1: Close the generated solution.");
                ioManagerContract.LogBuilder.AppendLine("2: Choose new folder name if already exists.");
                ioManagerContract.LogBuilder.AppendLine("3: See log for further information.");
                ioManagerContract.LogBuilder.AppendLine();

                Console.WriteLine(ioManagerContract.LogBuilder.ToString());

                generateButton.Enabled = true;
                generatorButton.Enabled = true;
                generateSuffixButton.Enabled = true;

                connectButton.Enabled = true;
                ShowTablesButton.Enabled = true;
                SelectAllButton.Enabled = true;
                ResetButton.Enabled = true;
                databaseComboBox.Enabled = true;
                executeStoredprocedureCheckBox.Enabled = true;

                iocComboBox.Enabled = true;
                wcfPerformanceComboBox.Enabled = true;
                unitTestComboBox.Enabled = true;
                sqlOrmComboBox.Enabled = true;
                wcfCallComboBox.Enabled = true;
                wcfSecurityComboBox.Enabled = true;
                loggingComboBox.Enabled = true;
                streamTypeComboBox.Enabled = true;

                solutionTextBox.Enabled = true;
                blSuffixTextBox.Enabled = true;
                dalTextBox.Enabled = true;
                dtoTextBox.Enabled = true;
                testTextBox.Enabled = true;
                sqlPrefixTextBox.Enabled = true;
                dataContractTextBox.Enabled = true;
                serviceContractTextBox.Enabled = true;
                serviceTextBox.Enabled = true;

                // throw new Exception(ex);
                using (StreamWriter streamWriter = new StreamWriter(outputDirectory + "\\CodeHammer.Log", true))
                {
                    streamWriter.WriteLine(ioManagerContract.LogBuilder.ToString());
                    streamWriter.WriteLine(se);
                }

                return;
            }
            catch (Exception ex)
            {
                outputTextBox.Clear();
                Console.WriteLine(ex.Message);
                ioManagerContract.LogBuilder.AppendLine(ex.ToString());
                ioManagerContract.LogBuilder.AppendLine();
                generateButton.Enabled = true;
                generateSuffixButton.Enabled = true;
                generatorButton.Enabled = true;

                connectButton.Enabled = true;
                ShowTablesButton.Enabled = true;
                SelectAllButton.Enabled = true;
                ResetButton.Enabled = true;
                databaseComboBox.Enabled = true;
                executeStoredprocedureCheckBox.Enabled = true;

                iocComboBox.Enabled = true;
                wcfPerformanceComboBox.Enabled = true;
                unitTestComboBox.Enabled = true;
                sqlOrmComboBox.Enabled = true;
                wcfCallComboBox.Enabled = true;
                wcfSecurityComboBox.Enabled = true;
                loggingComboBox.Enabled = true;
                streamTypeComboBox.Enabled = true;

                solutionTextBox.Enabled = true;
                blSuffixTextBox.Enabled = true;
                dalTextBox.Enabled = true;
                dtoTextBox.Enabled = true;
                testTextBox.Enabled = true;
                sqlPrefixTextBox.Enabled = true;
                dataContractTextBox.Enabled = true;
                serviceContractTextBox.Enabled = true;
                serviceTextBox.Enabled = true;

                using (StreamWriter streamWriter = new StreamWriter(outputDirectory + "\\CodeHammer.Log", true))
                {
                    streamWriter.WriteLine(ioManagerContract.LogBuilder.ToString());
                }
                // throw new Exception(ex);
                return;
            }
            finally
            {
            }

            if (!onlyDTO)
            {
                //projectManagerContract.AddFileToCodeHammerBusinessLogicProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerDataAccessProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerRepositoryMappingProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerRepositoryHelperProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerDtoProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerServiceLibraryProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                //projectManagerContract.AddFileToCodeHammerServiceLibraryHost(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);

                if (!crudCheckBox.Checked)
                {
                    //projectManagerContract.AddFileToCodeHammerLoggingProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                    //projectManagerContract.AddFileToCodeHammerTestProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                }

                //  projectManagerContract.AddFileToCodeHammerTestProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);

                if (ioManagerContract.Log4Net)
                {
                    //    projectManagerContract.AddFileToCodeHammerLoggingProject(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox);
                }

                Thread.Sleep(1000);

                strGeneratorMessage.Length = 0;
                strGeneratorMessage.Capacity = 0;
                outputTextBox.Clear();

                codeHammerDataUtilContract.FindSolution(outputDirectory + "\\" + SuffixDto.Instance().SolutionTextBox, out sln);
                strGeneratorMessage.Append("\nCodeHammer done generating...\n");
                strGeneratorMessage.AppendLine("\nClick this link to open the solution: " + @"file://" + sln);

                Console.WriteLine(strGeneratorMessage.ToString());

                if (ioManagerContract.UseIoC && container.Equals("castle"))
                {
                    strCastleWindsor.Length = 0;
                    strCastleWindsor.Capacity = 0;
                    //strCastleWindsor.AppendLine();

                    strCastleWindsor.AppendLine("To install Castle Windsor WCF integration facility");
                    strCastleWindsor.AppendLine("1. Right click on the Service host Project.");
                    strCastleWindsor.AppendLine("2. Choose Manage NuGet packages.");
                    strCastleWindsor.AppendLine("3. Paste Castle.WcfIntegrationFacility inside the search textfield.");
                    strCastleWindsor.AppendLine("4. Press install.");
                    strCastleWindsor.AppendLine("5. Press Close.");
                    Console.WriteLine(strCastleWindsor.ToString());
                }

                if (!ioManagerContract.UseIoC)
                {
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.ICodeHammerRepositoryFolder);
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.ICodeHammerBusinessLogicFolder);
                }

                if (ioManagerContract.EmptyDataLayerCheckBox)
                {
                    strDataAccess.Length = 0;
                    strDataAccess.Capacity = 0;

                    strDataAccess.AppendLine("Choose your own data repository.");
                    Console.WriteLine(strDataAccess.ToString());

                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.CodeHammerRepositoryDatabaseManagementFolder);
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.CodeHammerStoredProcedureFolder);
                }

                if (ioManagerContract.UseIoC && container.Equals("noioc"))
                {
                    strCastleWindsor.Length = 0;
                    strCastleWindsor.Capacity = 0;
                    //strCastleWindsor.AppendLine();

                    strCastleWindsor.AppendLine("Choose your own IoC container and register in Global.asax.cs.");
                    Console.WriteLine(strCastleWindsor.ToString());
                }

                if (!ioManagerContract.UnitTest)
                {
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.VisualstudioSolutionBusinessTestProjectFolder);
                    Thread.Sleep(2000);
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.VisualstudioSolutionDataRepositoryTestProjectFolder);
                    Thread.Sleep(2000);
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.VisualstudioSolutionServiceLibraryTestProjectFolder);
                }

                if (ioManagerContract.UnitTest)
                {
                    ioManagerContract.UnitTestBuilder = new StringBuilder();

                    if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                    {
                        ioManagerContract.UnitTestBuilder.Length = 0;
                        ioManagerContract.UnitTestBuilder.Capacity = 0;
                        ioManagerContract.UnitTestBuilder.AppendLine("To install NUnit");
                        ioManagerContract.UnitTestBuilder.AppendLine("1. Right click on the Solution Test Project.");
                        ioManagerContract.UnitTestBuilder.AppendLine("2. Choose Manage NuGet packages.");
                        ioManagerContract.UnitTestBuilder.AppendLine("3. Paste NUnit inside the search textfield.");
                        ioManagerContract.UnitTestBuilder.AppendLine("4. Press install.");
                        ioManagerContract.UnitTestBuilder.AppendLine("5. Press Close.");
                        Console.WriteLine(ioManagerContract.UnitTestBuilder.ToString());
                    }

                    if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                    {
                        ioManagerContract.VSUnitTestBuilder = new StringBuilder();
                        ioManagerContract.VSUnitTestBuilder.Length = 0;
                        ioManagerContract.VSUnitTestBuilder.Capacity = 0;
                        ioManagerContract.VSUnitTestBuilder.AppendLine("To install Microsoft.VisualStudio.QualityTools.UnitTestFramework");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("1. Right click on the Solution Test Project's References.");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("2. Add Reference.");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("3. Paste Microsoft.VisualStudio.QualityTools.UnitTestFramework inside the Search Assembly textfield.");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("4. Choose the checkbox and click.");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("5. Press OK.");
                        ioManagerContract.VSUnitTestBuilder.AppendLine("6. Now start building your quality tests.");
                        Console.WriteLine(ioManagerContract.VSUnitTestBuilder.ToString());
                    }
                }

                if (ioManagerContract.Log4Net)
                {
                    Console.WriteLine(ioManagerContract.CreateLog4NetSql());

                    ////Write file to folder
                }

                if (!ioManagerContract.Log4Net)
                {
                    codeHammerDataUtilContract.ClearFolder(outputDirectory + @"\" + ioManagerContract.CodeHammerProjectName + solutionTextBox.Text + ioManagerContract.CodeHammerLoggingLog4Net);
                }

                if (fluentNHibernateRadioButton.Checked)
                {
                    strFluentNHibernate.Length = 0;
                    strFluentNHibernate.Capacity = 0;
                    // strFluentNHibernate.AppendLine();
                    strFluentNHibernate.AppendLine("To install FluentNHibernate");
                    strFluentNHibernate.AppendLine("1. Right click on the Data Project.");
                    strFluentNHibernate.AppendLine("2. Choose Manage NuGet packages.");
                    strFluentNHibernate.AppendLine("3. Paste FluentNHibernate inside the search textfield.");
                    strFluentNHibernate.AppendLine("4. Press install.");
                    strFluentNHibernate.AppendLine("5. Press Close.");
                    Console.WriteLine(strFluentNHibernate.ToString());
                }
                else
                {
                    if (crudCheckBox.Checked)
                    {
                        strUrl.Length = 0;
                        strUrl.Capacity = 0;
                        strUrl.Append("\n1. Startup your Host project.\n");
                        strUrl.AppendLine("\n2. Click the url below.\n");

                        strUrl.AppendLine(codeHammerDataUtilContract.MakeHelpPage.ToString());
                        Console.WriteLine();
                        Console.WriteLine(strUrl.ToString());
                    }
                }

                generateButton.Enabled = true;
                generateSuffixButton.Enabled = true;
                generatorButton.Enabled = true;

                connectButton.Enabled = true;
                ShowTablesButton.Enabled = true;
                SelectAllButton.Enabled = true;
                ResetButton.Enabled = true;
                databaseComboBox.Enabled = true;
                executeStoredprocedureCheckBox.Enabled = true;

                iocComboBox.Enabled = true;
                wcfPerformanceComboBox.Enabled = true;
                unitTestComboBox.Enabled = true;
                sqlOrmComboBox.Enabled = true;
                wcfCallComboBox.Enabled = true;
                wcfSecurityComboBox.Enabled = true;
                loggingComboBox.Enabled = true;
                streamTypeComboBox.Enabled = true;

                solutionTextBox.Enabled = true;
                blSuffixTextBox.Enabled = true;
                dalTextBox.Enabled = true;
                dtoTextBox.Enabled = true;
                testTextBox.Enabled = true;
                sqlPrefixTextBox.Enabled = true;
                dataContractTextBox.Enabled = true;
                serviceContractTextBox.Enabled = true;
                serviceTextBox.Enabled = true;
            }

           
        }

        /// <summary>
        /// Resets the list.
        /// </summary>
        private void ResetList()
        {
            outputTextBox.Clear();
            for (int i = 0; i < mixedCheckBoxesTreeView1.Nodes.Count; i++)
            {
                this.mixedCheckBoxesTreeView1.Nodes[i].Checked = false;

                foreach (TreeNode childItem in this.mixedCheckBoxesTreeView1.Nodes[i].Nodes)
                {
                    if (childItem.Tag != null && (childItem.Tag.Equals("PK") || childItem.Tag.Equals("NOTNULLCOLUMN")))
                    {
                        childItem.Checked = true;
                    }
                    else
                    {
                        childItem.Checked = false;
                    }
                }
            }

            gridCheckBox.CheckState = CheckState.Unchecked;
        }

        /// <summary>
        /// Syntaxes the highlight from regex.
        /// </summary>
        private void SyntaxHighlightFromRegex()
        {
            this.outputTextBox.SuspendLayout();

            //string REG_EX_KEYWORDS = @"\bTablename\b|\bFROM\b|\bWHERE\b|\bCONTAINS\b|\bIN\b|\bIS\b|\bLIKE\b|\bNONE\b|\bNOT\b|\bNULL\b|\bOR\b";
            string REG_EX_KEYWORDS = @"\bTablename\b|\bTo install Castle Windsor WCF integration facility\b|\bTo install NUnit\b|\bTo install FluentNHibernate\b|\bTo install Log4Net\b|\bTo install Microsoft.VisualStudio.QualityTools.UnitTestFramework\b|\bLog4NetSqlScript.sql\b";

            MatchRExpression(this.outputTextBox, REG_EX_KEYWORDS, Color.Green);
        }

        /// <summary>
        /// Matches the r expression.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="regexpression">The regexpression.</param>
        /// <param name="color">The color.</param>
        private void MatchRExpression(RichTextBox textBox, string regexpression, Color color)
        {
            System.Text.RegularExpressions.MatchCollection matches = Regex.Matches(this.outputTextBox.Text, regexpression, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                textBox.Select(match.Index, match.Length);
                textBox.SelectionColor = color;
            }
        }

        #endregion Methods
    }
}