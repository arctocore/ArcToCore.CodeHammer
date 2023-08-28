/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Log
{
    /// <summary>
    /// this interface LogFuncContract
    /// </summary>

    public interface LogFuncContract
    {
        /// <summary>
        /// Dumps the log.
        /// </summary>
        /// <param name="r">The r.</param>
        void DumpLog(System.IO.StreamReader r);

        /// <summary>
        /// Loggers the specified log message.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        /// <returns></returns>
        bool Logger(string logMessage);
    }
}