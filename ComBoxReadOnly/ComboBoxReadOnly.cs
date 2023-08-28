/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace Presentation.Controls
{
    using System.Windows.Forms;

    /// <summary>
    /// this class ComboBoxReadOnly
    /// </summary>
    public class ComboBoxReadOnly : ComboBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxReadOnly"/> class.
        /// </summary>
        public ComboBoxReadOnly()
        {
            textBox = new TextBox();
            textBox.ReadOnly = true;
            textBox.Visible = false;
        }

        /// <summary>
        /// The text box
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;

                if (readOnly)
                {
                    this.Visible = false;
                    textBox.Text = this.Text;
                    textBox.Location = this.Location;
                    textBox.Size = this.Size;
                    textBox.Visible = true;

                    if (textBox.Parent == null)
                        this.Parent.Controls.Add(textBox);
                }
                else
                {
                    this.Visible = true;
                    this.textBox.Visible = false;
                }
            }
        }
    }
}