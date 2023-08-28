/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator
{
    /// <summary>
    /// this interface CodeHammerLoggingGeneratorContract
    /// </summary>

    public interface CodeHammerLoggingGeneratorContract
    {
        /// <summary>
        /// Codes the hammer log4 net SQL script.
        /// </summary>
        /// <param name="csPath">The cs path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerLog4NetSqlScript(string csPath);

        /// <summary>
        /// Codes the hammer logging.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerLogging(string path);
    }
}