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
    using CodeHammer.Framework.FunctionArea.Generators.DtoGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using System.Collections.Generic;

    /// <summary>
    /// this class CodeHammerDTOGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerDTOGeneratorTests
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
        /// Codes the hammer create data transfer class test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDataTransferClassTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDTOGeneratorContract codeHammerDTOGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDTOGENERATORCONTRACT);

            bool state = codeHammerDTOGeneratorContract.CodeHammerCreateDataTransferClass(false, false,
                true,
                new Dictionary<string, List<Dictionary<string, string>>>(),
                new CodeHammer.Entities.CodeHammerTableDto() { CodeHammerName = "codehammer" },
                "",
                "",
                new List<string>(),
                "",
                "",
                "",
                "",
                "",
                "",
                false, 0);

            Assert.IsFalse(state);
        }
    }
}