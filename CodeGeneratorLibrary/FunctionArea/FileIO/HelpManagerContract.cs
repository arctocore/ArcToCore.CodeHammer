/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.FileIO
{
    /// <summary>
    /// this interface HelpManagerContract
    /// </summary>

    public interface HelpManagerContract
    {
        /// <summary>
        /// Codes the hammer data stream help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerDataStreamHelp();

        /// <summary>
        /// Codes the hammer io c castle WCF help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerIoCCastleWcfHelp();

        /// <summary>
        /// Codes the hammer log4 net help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerLog4NetHelp();

        /// <summary>
        /// Codes the hammer orm help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerOrmHelp();

        /// <summary>
        /// Codes the hammer unit test help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerUnitTestHelp();

        /// <summary>
        /// Codes the hammer WCF call help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerWcfCallHelp();

        /// <summary>
        /// Codes the hammer WCF security help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerWcfSecurityHelp();

        /// <summary>
        /// Codes the hammer WCF throttling help.
        /// </summary>
        /// <returns>if success return true</returns>
        string CodeHammerWcfThrottlingHelp();
    }
}