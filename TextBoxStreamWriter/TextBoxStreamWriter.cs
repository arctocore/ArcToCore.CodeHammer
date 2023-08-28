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
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// this.class TextBoxStreamWriter
    /// </summary>
    public class TextBoxStreamWriter : TextWriter
    {
        private RichTextBox _output = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxStreamWriter"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public TextBoxStreamWriter(RichTextBox output)
        {
            _output = output;
        }

        /// <summary>
        /// When overridden in a derived class, returns the character encoding in which the output is written.
        /// </summary>
        /// <returns>The character encoding in which the output is written.</returns>
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }

        /// <summary>
        /// Writes a character to the text string or stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString());
        }
    }
}