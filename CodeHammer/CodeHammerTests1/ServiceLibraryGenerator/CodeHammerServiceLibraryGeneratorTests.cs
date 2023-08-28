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
    using CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// this class CodeHammerServiceLibraryGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerServiceLibraryGeneratorTests
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
        /// The dic
        /// </summary>
        private Dictionary<string, List<Dictionary<string, string>>> dic = new Dictionary<string, List<Dictionary<string, string>>>();

        /// <summary>
        /// The ct
        /// </summary>
        private CodeHammer.Entities.CodeHammerTableDto ct = new CodeHammer.Entities.CodeHammerTableDto()
        {
            CodeHammerName = "codehammer"
        };

        /// <summary>
        /// The stream writer
        /// </summary>
        private StreamWriter streamWriter;

        /// <summary>
        /// The path
        /// </summary>
        private string path = "c:\\tmp";

        private List<CodeHammer.Entities.CodeHammerTableDto> tableList = new List<CodeHammerTableDto>();

        private Dictionary<string, List<Dictionary<string, string>>> dicList = new Dictionary<string, List<Dictionary<string, string>>>();

        private CodeHammer.Entities.CodeHammerTableDto tb = new CodeHammerTableDto() { CodeHammerName = "codehammer" };

        private List<string> options = new List<string>();

        /// <summary>
        /// Codes the hammer service contract library test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceContractLibraryTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceContractLibrary(true, dicList, "codehammer", tb, true, options, path);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Services the library test.
        /// </summary>
        [TestMethod()]
        public void ServiceLibraryTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            bool state = codeHammerServiceLibraryGeneratorContract.ServiceLibrary(tableList, "", true, dicList, "codehammer", tb, true, options, path, "instanceCall");
            Assert.IsFalse(state);
        }

        [TestMethod()]
        public void CodeHammerIServiceCreateDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerIServiceCreateDeleteMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer i service create insert method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerIServiceCreateInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerIServiceCreateInsertMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer i service create select all method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerIServiceCreateSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerIServiceCreateSelectAllMethod("classname", tb, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer i service create select by identifier test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerIServiceCreateSelectByIDTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerIServiceCreateSelectByID("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer i service create update method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerIServiceCreateUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerIServiceCreateUpdateMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer service create delete method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceCreateDeleteMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceCreateDeleteMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer service create insert method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceCreateInsertMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceCreateInsertMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer service create select all method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceCreateSelectAllMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceCreateSelectAllMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer service create select by identifier method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceCreateSelectByIDMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceCreateSelectByIDMethod("classname", tb, streamWriter);
            Assert.IsFalse(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }

        /// <summary>
        /// Codes the hammer service create update method test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerServiceCreateUpdateMethodTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);

            string g = Guid.NewGuid().ToString();
            streamWriter = new StreamWriter("c:\\" + g + ".txt", true);
            bool state = codeHammerServiceLibraryGeneratorContract.CodeHammerServiceCreateUpdateMethod("classname", tb, streamWriter);
            Assert.IsTrue(state);
            streamWriter.Close();
            Thread.Sleep(2000);
            File.Delete("c:\\" + g + ".txt");
        }
    }
}