/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

using CodeHammer.Framework.FunctionArea.DataUtil;

namespace CodeHammer.Framework.FunctionArea.FileIO
{
    /// <summary>
    /// this class HelpManager
    /// </summary>

    public class HelpManager : CodeHammer.Framework.FunctionArea.FileIO.HelpManagerContract
    {
        #region Variables

        /// <summary>
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerServiceLibraryGenerator"/> class.
        /// </summary>
        /// <param name="codeHammerDataUtilContract">The code hammer data utility contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        public HelpManager(CodeHammerDataUtilContract codeHammerDataUtilContract,

            FuncTypeFactoryContract funcTypeFactoryContract)
        {
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;

            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
        }

        /// <summary>
        /// Codes the hammer io c castle WCF help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerIoCCastleWcfHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.IoCCastleWcf.txt");
        }

        /// <summary>
        /// Codes the hammer WCF per call help.
        /// </summary>
        /// <returnsif success return true></returns>
        public string CodeHammerWcfCallHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.wcfCall.txt");
        }

        /// <summary>
        /// Codes the hammer WCF security help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerWcfSecurityHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.wcfSecurity.txt");
        }

        /// <summary>
        /// Codes the hammer unit test help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerUnitTestHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.unitTest.txt");
        }

        /// <summary>
        /// Codes the hammer WCF throttling help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerWcfThrottlingHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.wcfThrottling.txt");
        }

        /// <summary>
        /// Codes the hammer log4 net help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerLog4NetHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.Log4Net.txt");
        }

        /// <summary>
        /// Codes the hammer data stream help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerDataStreamHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.DataStream.txt");
        }

        /// <summary>
        /// Codes the hammer orm help.
        /// </summary>
        /// <returns>if success return true</returns>
        public string CodeHammerOrmHelp()
        {
            return codeHammerDataUtilContract.CodeHammerGetResource("CodeHammer.Framework.Resources.Help.SqlOrm.txt");
        }
    }
}