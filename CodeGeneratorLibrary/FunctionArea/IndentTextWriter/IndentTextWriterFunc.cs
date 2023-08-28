namespace CodeHammer.Framework.FunctionArea.IndentTextWriter
{
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.CodeDom.Compiler;
    using System.IO;

    /// <summary>
    /// this class IndentTextWriterFunc
    /// </summary>

    public class IndentTextWriterFunc : CodeHammer.Framework.FunctionArea.IndentTextWriter.IndentTextWriterFuncContract
    {
        #region Variables

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="IndentTextWriterFunc"/> class.
        /// </summary>
        /// <param name="logFuncContract">The log function contract.</param>
        public IndentTextWriterFunc(LogFuncContract logFuncContract)
        {
            this.logFuncContract = logFuncContract;
        }

        /// <summary>
        /// Reads the file and format.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>return formated string</returns>
        public string ReadFileAndFormat(string path)
        {
            try
            {
                string content = string.Empty;
                string formatedContent = string.Empty;

                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] cSharpFiles = dir.GetDirectories("*", SearchOption.AllDirectories);

                foreach (DirectoryInfo csFile in cSharpFiles)
                {
                    foreach (var item in csFile.GetFiles())
                    {
                        FileInfo info = new FileInfo(item.FullName);

                        if (info.Extension.Equals(".cs"))
                        {
                            using (StreamReader sr = new StreamReader(item.FullName))
                            {
                                content = sr.ReadToEnd();
                            }

                            if (!string.IsNullOrEmpty(content))
                            {
                                formatedContent = CreateMultilevelIndentString(content);
                            }

                            if (!File.Exists(path))
                            {
                                File.Create(path).Close();
                            }
                            File.WriteAllText(path, formatedContent);
                        }
                    }
                }

                return formatedContent;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creates the multilevel indent string.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>return formated text if success</returns>
        private string CreateMultilevelIndentString(string contents)
        {
            // Creates a TextWriter to use as the base output writer.
            System.IO.StringWriter baseTextWriter = new System.IO.StringWriter();

            // Create an IndentedTextWriter and set the tab string to use
            // as the indentation string for each indentation level.
            System.CodeDom.Compiler.IndentedTextWriter indentWriter = new IndentedTextWriter(baseTextWriter, "    ");

            // Sets the indentation level.
            indentWriter.Indent = 0;

            // Output test strings at stepped indentations through a recursive loop method.
            WriteLevel(contents, indentWriter, 0, 5);

            // Return the resulting string from the base StringWriter.
            return baseTextWriter.ToString();
        }

        /// <summary>
        /// Writes the level.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="indentWriter">The indent writer.</param>
        /// <param name="level">The level.</param>
        /// <param name="totalLevels">The total levels.</param>
        private void WriteLevel(string contents, IndentedTextWriter indentWriter, int level, int totalLevels)
        {
            // Output a test string with a new-line character at the end.
            indentWriter.WriteLine(contents + level.ToString());

            // If not yet at the highest recursion level, call this output method for the next level of indentation.
            if (level < totalLevels)
            {
                // Increase the indentation count for the next level of indented output.
                indentWriter.Indent++;

                // Call the WriteLevel method to write test output for the next level of indentation.
                WriteLevel(contents, indentWriter, level + 1, totalLevels);

                // Restores the indentation count for this level after the recursive branch method has returned.
                indentWriter.Indent--;
            }
            else
                // Outputs a string using the WriteLineNoTabs method.
                //indentWriter.WriteLineNoTabs("This is a test phrase written with the IndentTextWriter.WriteLineNoTabs method.");

                // Outputs a test string with a new-line character at the end.
                indentWriter.WriteLine(contents + level.ToString());
        }
    }
}