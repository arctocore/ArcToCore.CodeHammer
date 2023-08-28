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
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.Generators.BusinessGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using System.IO;

    /// <summary>
    /// this class CodeHammerBusinessLogicGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerBusinessLogicGeneratorTests
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
        /// The code hammer dal class name
        /// </summary>
        private string codeHammerDALClassName = "Test";

        /// <summary>
        /// The table
        /// </summary>
        private CodeHammerTableDto table = new CodeHammerTableDto()
        {
            CodeHammerColumns = new System.Collections.Generic.List<CodeHammerColumn>()
            {
            },

            CodeHammerForeignKeys = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>()
            {
            },

            CodeHammerName = "test",
            CodeHammerNullableFields = new System.Collections.Generic.Dictionary<string, string>()
            {
            },

            CodeHammerPrimaryKeys = new System.Collections.Generic.List<CodeHammerColumn>()
            {
            },

            CodeHammerSchemaName = "dbo"
        };

        /// <summary>
        /// The stream writer
        /// </summary>
        private StreamWriter streamWriter = new StreamWriter("tmp.txt", true);

        /// <summary>
        /// Codes the hammer create bl delete method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateBLDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBLDeleteMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create bl insert method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateBLInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBLInsertMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create bl select all method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateBLSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBLSelectAllMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create bl select method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateBLSelectMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBLSelectMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create bl update method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateBLUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBLUpdateMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create ibl delete method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateIBLDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateIBLDeleteMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create ibl insert method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateIBLInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateIBLInsertMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create ibl select all method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateIBLSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateIBLSelectAllMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create ibl select method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateIBLSelectMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateIBLSelectMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }

        /// <summary>
        /// Codes the hammer create ibl update method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateIBLUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);

            bool state = codeHammerBusinessLogicGeneratorContract.CodeHammerCreateIBLUpdateMethod(codeHammerDALClassName, table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
        }
    }
}