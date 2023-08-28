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
    using CodeHammer.Entities;
    using CodeHammer.Framework;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using FactoryInstaller;
    using Ninject;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// this class GenerateForm
    /// </summary>

    public partial class GenerateForm : Form
    {
        #region Variables

        /// <summary>
        /// The container
        /// </summary>
        private IKernel container;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The fileType
        /// </summary>
        private string fileType = string.Empty;

        /// <summary>
        /// The file name
        /// </summary>
        private string fileName = string.Empty;

        /// <summary>
        /// The child form number
        /// </summary>
        private int childFormNumber = 0;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateForm"/> class.
        /// </summary>
        public GenerateForm()
        {
            InitializeComponent();
            ConfigureContainer();
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        private void ConfigureContainer()
        {
            this.container = new StandardKernel(new InjectionModuleFactory());
            funcTypeFactoryContract = container.Get<FuncTypeFactoryContract>();
        }

        /// <summary>
        /// Shows the new form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = this.fileType.ToUpper() + " Files (*." + this.fileType + ")|*." + this.fileType + "|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveAsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = this.fileType.ToUpper() + " Files (*." + this.fileType + ")|*." + this.fileType + "|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Handles the Click event of the ExitToolsStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// Sets the code editor.
        /// </summary>
        /// <param name="dgmlCode">The DGML code.</param>
        /// <param name="onlyDataContract">if set to <c>true</c> [only data contract].</param>
        /// <param name="filetype">The filetype.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="text">The text.</param>
        /// <param name="bgcolor">The bgcolor.</param>
        public void SetCodeEditor(StringBuilder dgmlCode, bool onlyDataContract, string filetype, string className, string text, string bgcolor)
        {
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);

            if (dgmlCode != null)
            {
                ioManagerContract.CodeEditorBuilder = dgmlCode;
            }

            switch (filetype)
            {
                case "cs":
                    if (onlyDataContract)
                    {
                        fileName = className + SuffixDto.Instance().DataContractTextBox;
                    }
                    else
                    {
                        fileName = className + SuffixDto.Instance().DtoTextBox;
                    }
                    this.fileType = filetype;
                    break;

                case "dgml":
                    fileName = className + "DependencyDiagram";
                    this.fileType = filetype;
                    break;
            }
            syntaxRichTextBox1.Clear();
            syntaxRichTextBox1.AppendText(text);

            // Add the keywords to the list.
            syntaxRichTextBox1.Settings.Keywords.Add("public");
            syntaxRichTextBox1.Settings.Keywords.Add("class");
            syntaxRichTextBox1.Settings.Keywords.Add("using");
            syntaxRichTextBox1.Settings.Keywords.Add("namespace");
            syntaxRichTextBox1.Settings.Keywords.Add("BaseTable");
            syntaxRichTextBox1.Settings.Keywords.Add("DependencyTable");
            syntaxRichTextBox1.Settings.Keywords.Add("-");
            syntaxRichTextBox1.Settings.Keywords.Add("|");

            // Set the comment identifier. For Lua this is two minus-signs after each other (--).
            // For C++ we would set this property to "//".
            syntaxRichTextBox1.Settings.Comment = "///";

            syntaxRichTextBox1.Settings.CommentTags = "//";

            // Set the colors that will be used.
            syntaxRichTextBox1.Settings.KeywordColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(152)))), ((int)(((byte)(203)))));
            syntaxRichTextBox1.Settings.CommentColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(166)))), ((int)(((byte)(74)))));
            syntaxRichTextBox1.Settings.TagsColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(165)))), ((int)(((byte)(73)))));
            //syntaxRichTextBox1.Settings.StringColor = Color.White;
            //syntaxRichTextBox1.Settings.IntegerColor = Color.Red;

            // Let's not process strings and integers.
            syntaxRichTextBox1.Settings.EnableStrings = true;
            syntaxRichTextBox1.Settings.EnableIntegers = false;
            syntaxRichTextBox1.Settings.EnableComments = true;

            // Let's make the settings we just set valid by compiling
            // the keywords to a regular expression.
            syntaxRichTextBox1.CompileKeywords();

            // Load a file and update the syntax highlighting.
            //syntaxRichTextBox1.LoadFile("../script.lua", RichTextBoxStreamType.PlainText);
            syntaxRichTextBox1.ProcessAllLines();

            if (bgcolor == "black")
            {
                syntaxRichTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
                syntaxRichTextBox1.SelectionColor = Color.White;
            }
            else
            {
                syntaxRichTextBox1.BackColor = Color.White;
                syntaxRichTextBox1.SelectionColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the Load event of the GenerateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GenerateForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the copyCodeToolStripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void copyCodeToolStripButton_Click(object sender, EventArgs e)
        {
            syntaxRichTextBox1.SelectAll();
            syntaxRichTextBox1.Copy();
        }

        /// <summary>
        /// Handles the Click event of the exportDtoToolStripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exportDtoToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = this.fileType.ToUpper() + " files (*." + this.fileType + ")|*." + this.fileType;
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = fileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());

                switch (this.fileType)
                {
                    case "dgml":
                        file.WriteLine(ioManagerContract.CodeEditorBuilder);

                        break;

                    case "cs":
                        file.WriteLine(syntaxRichTextBox1.Text);

                        break;
                }
                file.Close();
            }
        }
    }
}