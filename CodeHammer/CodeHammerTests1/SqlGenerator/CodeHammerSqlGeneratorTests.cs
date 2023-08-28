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
    using CodeHammer.Framework.FunctionArea.Generators.SqlGenerator;
    using FactoryInstaller;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    /// <summary>
    /// this class CodeHammerSqlGeneratorTests
    /// </summary>
    [TestClass()]
    public class CodeHammerSqlGeneratorTests
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
        /// The path
        /// </summary>
        private string path = "c:\\tmp";

        /// <summary>
        /// The dbname
        /// </summary>
        private string dbname = "codehammer";

        /// <summary>
        /// Codes the hammer create delete stored procedure test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateDeleteStoredProcedureTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);

            bool state = codeHammerSqlGeneratorContract.CodeHammerCreateDeleteStoredProcedure(dbname, table, path, true);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Codes the hammer create insert stored procedure test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateInsertStoredProcedureTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);

            bool state = codeHammerSqlGeneratorContract.CodeHammerCreateInsertStoredProcedure(dbname, table, path, true);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Codes the hammer create select all stored procedure test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateSelectAllStoredProcedureTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);

            bool state = codeHammerSqlGeneratorContract.CodeHammerCreateSelectAllStoredProcedure("10", dbname, table, path, true);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Codes the hammer create select stored procedure test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateSelectStoredProcedureTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);

            bool state = codeHammerSqlGeneratorContract.CodeHammerCreateSelectStoredProcedure(dbname, table, path, true);
            Assert.IsFalse(state);
        }

        /// <summary>
        /// Codes the hammer create update stored procedure test.
        /// </summary>
        [TestMethod()]
        public void CodeHammerCreateUpdateStoredProcedureTest()
        {
            FuncTypeFactoryContract iFuncTypeFactory = kernel.Get<FuncTypeFactoryContract>();
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = iFuncTypeFactory.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);

            bool state = codeHammerSqlGeneratorContract.CodeHammerCreateUpdateStoredProcedure(dbname, table, path, true);
            Assert.IsFalse(state);
        }
    }
}