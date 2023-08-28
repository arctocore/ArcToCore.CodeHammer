/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.Tests
{
    using CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    /// <summary>
    /// this class CodeHammerLoggingGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerLoggingGeneratorTests
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private IKernel kernel = null;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            kernel = new StandardKernel(new InjectionModuleFactory());
        }

        /// <summary>
        /// Codes the hammer logging test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerLoggingTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerLoggingGeneratorContract codeHammerLoggingGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);

            bool state = codeHammerLoggingGeneratorContract.CodeHammerLogging(string.Empty);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Codes the hammer log4 net SQL script test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerLog4NetSqlScriptTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerLoggingGeneratorContract codeHammerLoggingGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);

            bool state = codeHammerLoggingGeneratorContract.CodeHammerLog4NetSqlScript(string.Empty);
            Assert.IsFalse(state);
        }
    }
}