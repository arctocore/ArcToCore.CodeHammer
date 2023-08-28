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
    using CodeHammer.Framework.FunctionArea.Generators.DataGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// this class CodeHammerDataAccessLayerGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerDataAccessLayerGeneratorTests
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
        private StreamWriter streamWriter;

        /// <summary>
        /// The selecttables
        /// </summary>
        private bool selecttables = true;

        /// <summary>
        /// The dic
        /// </summary>
        private Dictionary<string, List<Dictionary<string, string>>> dic = new Dictionary<string, List<Dictionary<string, string>>>();

        /// <summary>
        /// The dbname
        /// </summary>
        private string dbname = "codehammer";

        /// <summary>
        /// The ct
        /// </summary>
        private CodeHammer.Entities.CodeHammerTableDto ct = new CodeHammer.Entities.CodeHammerTableDto()
        {
            CodeHammerName = "codehammer"
        };

        /// <summary>
        /// The crud
        /// </summary>
        private bool crud = true;

        /// <summary>
        /// The result data options
        /// </summary>
        private List<string> resultDataOptions = new List<string>();

        /// <summary>
        /// The name space
        /// </summary>
        private string nameSpace = "DAL";

        /// <summary>
        /// The path
        /// </summary>
        private string path = "c:\\tmp";

        /// <summary>
        /// Codes the hammer create data access class test. Test that directory is not found
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDataAccessClassTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataAccessClass(selecttables, dic, dbname, ct, crud, resultDataOptions, nameSpace, path);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create data access interface test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDataAccessInterfaceTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataAccessInterface(selecttables, dic, dbname, ct, crud, resultDataOptions, nameSpace, path);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create data management interface test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDataManagementInterfaceTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataManagementInterface(path, true);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create delete method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDeleteMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create insert method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateInsertMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create select all method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateSelectAllMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create select method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateSelectMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateSelectMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer create update method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateUpdateMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// is the code hammer create delete method test.
        /// </summary>
        [TestMethod()]
        public void ICodeHammerCreateDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.ICodeHammerCreateDeleteMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// is the code hammer create insert method test.
        /// </summary>
        [TestMethod()]
        public void ICodeHammerCreateInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.ICodeHammerCreateInsertMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// is the code hammer create select all method test.
        /// </summary>
        [TestMethod()]
        public void ICodeHammerCreateSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.ICodeHammerCreateSelectAllMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// is the code hammer create select method test.
        /// </summary>
        [TestMethod()]
        public void ICodeHammerCreateSelectMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.ICodeHammerCreateSelectMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// is the code hammer create update method test.
        /// </summary>
        [TestMethod()]
        public void ICodeHammerCreateUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerDataAccessLayerGeneratorContract.ICodeHammerCreateUpdateMethod(table, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }
    }
}