namespace CodeHammer.Framework.FunctionArea.IndentTextWriter
{
    /// <summary>
    /// this interface IndentTextWriterFuncContract
    /// </summary>
    public interface IndentTextWriterFuncContract
    {
        /// <summary>
        /// Reads the file and format.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>return formated text if success</returns>
        string ReadFileAndFormat(string path);
    }
}