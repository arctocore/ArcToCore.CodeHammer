/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators
{
    using CodeHammer.Entities;
    using CodeHammer.Framework;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Generators.BusinessGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DataGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DtoGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGeneratorPENDING;
    using CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.SqlGenerator;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Generates C# and SQL code for accessing a database.
    /// </summary>

    public class CodeHammerGenerator : CodeHammer.Framework.FunctionArea.Generators.CodeHammerGeneratorContract
    {
        #region Variables

        /// <summary>
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The database data support adapter contract
        /// </summary>
        private DbDataSupportAdapterContract dbDataSupportAdapterContract = null;

        /// <summary>
        /// The fluent n hibernate generator contract
        /// </summary>
        private FluentNHibernateGeneratorContract fluentNHibernateGeneratorContract = null;

        /// <summary>
        /// The code hammer SQL generator contract
        /// </summary>
        private CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract = null;

        /// <summary>
        /// The code hammer dto generator contract
        /// </summary>
        private CodeHammerDTOGeneratorContract codeHammerDTOGeneratorContract = null;

        /// <summary>
        /// The code hammer data access layer generator contract
        /// </summary>
        private CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract = null;

        /// <summary>
        /// The code hammer business logic generator contract
        /// </summary>
        private CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract = null;

        /// <summary>
        /// The code hammer service library generator contract
        /// </summary>
        private CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract = null;

        /// <summary>
        /// The code hammer logging generator contract
        /// </summary>
        private CodeHammerLoggingGeneratorContract codeHammerLoggingGeneratorContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerServiceLibraryGenerator"/> class.
        /// </summary>
        /// <param name="codeHammerDataUtilContract">The code hammer data utility contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        public CodeHammerGenerator(CodeHammerDataUtilContract codeHammerDataUtilContract,
            IOManagerContract ioManagerContract,
             LogFuncContract logFuncContract,
            FuncTypeFactoryContract funcTypeFactoryContract,
            DbDataSupportAdapterContract dbDataSupportAdapterContract,
            FluentNHibernateGeneratorContract fluentNHibernateGeneratorContract,
            CodeHammerSqlGeneratorContract codeHammerSqlGeneratorContract,
            CodeHammerDTOGeneratorContract codeHammerDTOGeneratorContract,

            CodeHammerDataAccessLayerGeneratorContract codeHammerDataAccessLayerGeneratorContract,
            CodeHammerBusinessLogicGeneratorContract codeHammerBusinessLogicGeneratorContract,
            CodeHammerServiceLibraryGeneratorContract codeHammerServiceLibraryGeneratorContract,
            CodeHammerLoggingGeneratorContract codeHammerLoggingGeneratorContract)
        {
            this.codeHammerLoggingGeneratorContract = codeHammerLoggingGeneratorContract;
            this.codeHammerServiceLibraryGeneratorContract = codeHammerServiceLibraryGeneratorContract;
            this.codeHammerBusinessLogicGeneratorContract = codeHammerBusinessLogicGeneratorContract;
            this.codeHammerDataAccessLayerGeneratorContract = codeHammerDataAccessLayerGeneratorContract;

            this.codeHammerDTOGeneratorContract = codeHammerDTOGeneratorContract;
            this.codeHammerSqlGeneratorContract = codeHammerSqlGeneratorContract;
            this.fluentNHibernateGeneratorContract = fluentNHibernateGeneratorContract;
            this.logFuncContract = logFuncContract;
            this.ioManagerContract = ioManagerContract;
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.dbDataSupportAdapterContract = dbDataSupportAdapterContract;

            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
            dbDataSupportAdapterContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.DBDATASUPPORTADAPTER);
            fluentNHibernateGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.FLUENTNHIBERNATEGENERATOR);
            codeHammerSqlGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT);
            codeHammerDTOGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDTOGENERATORCONTRACT);

            this.codeHammerLoggingGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERLOGGINGGENERATORCONTRACT);
            this.codeHammerServiceLibraryGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT);
            this.codeHammerBusinessLogicGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT);
            this.codeHammerDataAccessLayerGeneratorContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT);
        }

        /// <summary>
        /// Generates the SQL and C# code for the specified database.
        /// </summary>
        /// <param name="onlyDependencyDTO">if set to <c>true</c> [only dependency dto].</param>
        /// <param name="onlyDataContract">if set to <c>true</c> [only data contract].</param>
        /// <param name="onlyDTO">if set to <c>true</c> [only dto].</param>
        /// <param name="container">The container.</param>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tablesAndColumnDic">The tables and column dic.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="outputDirectory">The directory where the C# and SQL code should be created.</param>
        /// <param name="connectionString">The connection string to be used to connect the to the database.</param>
        /// <param name="configConnection">The configuration connection.</param>
        /// <param name="createMultipleFiles">A flag indicating if the generated stored procedures should be created in one file or
        /// separate files.</param>
        /// <param name="targetNamespaceDAL">The target namespace DAL.</param>
        /// <param name="targetNamespaceBL">The target namespace BL.</param>
        /// <param name="targetNamespaceDTO">The target namespace DTO.</param>
        /// <param name="usercontrolInheritsTextBox">The usercontrol inherits text box.</param>
        /// <param name="executeStoredprocedureCheckBox">if set to <c>true</c> [execute storedprocedure CheckBox].</param>
        /// <param name="instanceCall">The instance call.</param>
        /// <param name="throttling">if set to <c>true</c> [throttling].</param>
        /// <param name="security">if set to <c>true</c> [security].</param>
        /// <param name="security">if set to <c>true</c> [security].</param>
        /// <exception cref="System.Exception">Table name is empty. Not allowed!</exception>
        public void CodeHammerGenerate(bool onlyDependencyDTO, bool onlyDataContract, bool onlyDTO, string container, bool selectTables, Dictionary<string, List<string>> tablesAndColumnDic, bool crudYesNo, List<string> resultDataOptions, string outputDirectory, string connectionString, string configConnection, bool createMultipleFiles, string targetNamespaceDAL, string targetNamespaceBL, string targetNamespaceDTO, string usercontrolInheritsTextBox, bool executeStoredprocedureCheckBox, string instanceCall, bool throttling, int wcfSecurity)
        {
            if (!crudYesNo)
            {
                codeHammerDataUtilContract.MakeHelpPage.AppendLine("CodeHammer Api help");
            }

            List<CodeHammerTableDto> tableList = new List<CodeHammerTableDto>();

            string sqlPath = string.Empty;
            string csPath = string.Empty;
            string csSPPath = string.Empty;
            string aspnetPath = string.Empty;
            string servicePath = string.Empty;
            string dataContractPath = string.Empty;
            string databaseManagement = string.Empty;

            Dictionary<string, Dictionary<string, List<string>>> spContentDic = new Dictionary<string, Dictionary<string, List<string>>>();
            List<string> columnList = new List<string>();
            ////Contains key:SP, a list of dic -> Value:Key(Tablename), Value: List of Data
            Dictionary<string, List<Dictionary<string, string>>> tableDic = new Dictionary<string, List<Dictionary<string, string>>>();
            codeHammerDataUtilContract.connectionStringDB = connectionString;

            if (!onlyDTO && !onlyDataContract)
            {
                ////This is intended for CRUD
                if (selectTables == true)
                {
                    sqlPath = Path.Combine(outputDirectory, "SQL");
                    csPath = SuffixDto.Instance().CodeHammerPath;
                }

                aspnetPath = Path.Combine(csPath, "Design");
                databaseManagement = Path.Combine(csPath, "Infrastructure");

                try
                {
                    string[] allFileNames = System.IO.Directory.GetFiles(outputDirectory, "*.*", System.IO.SearchOption.AllDirectories);
                    foreach (string filename in allFileNames)
                    {
                        FileAttributes attr = File.GetAttributes(filename);
                        File.SetAttributes(filename, attr & ~FileAttributes.ReadOnly);
                    }
                }
                catch
                {
                }
            }

            // Get a list of the entities in the database

            DataSet ds = null;

            ////then Data tables are represented
            if (selectTables == true)
            {
                if (!dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.DBProvider, codeHammerDataUtilContract.connectionStringDB, codeHammerDataUtilContract.CodeHammerGetTableQuery(ioManagerContract.DatabaseName), out ds))
                {
                    logFuncContract.Logger("Could not retrieve data! ");
                    return;
                }
            }

            tableList.Clear();

            // Process each table
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                CodeHammerTableDto table = new CodeHammerTableDto();

                ////then Data tables are represented------------bug add more foreign key to list
                if (selectTables == true)
                {
                    table.CodeHammerName = (string)dataRow["TABLE_NAME"];
                    table.CodeHammerSchemaName = (string)dataRow["TABLE_SCHEMA"];
                    foreach (KeyValuePair<string, List<string>> tableItem in tablesAndColumnDic)
                    {
                        if (tableItem.Key.Equals(dataRow["TABLE_NAME"]))
                        {
                            foreach (string colItem in tableItem.Value)
                            {
                                CodeHammerQueryTable(codeHammerDataUtilContract.connectionStringDB, table, colItem);
                            }

                            CodeHammerTableDto tbpk = new CodeHammerTableDto()
                            {
                                CodeHammerColumns = table.CodeHammerColumns,
                                CodeHammerName = table.CodeHammerName.Replace(" ", string.Empty).Trim(),
                                CodeHammerNullableFields = table.CodeHammerNullableFields,
                                CodeHammerPrimaryKeys = table.CodeHammerPrimaryKeys.Distinct().ToList(),
                                CodeHammerSchemaName = table.CodeHammerSchemaName
                            };

                            tbpk.CodeHammerPrimaryKeys = table.CodeHammerPrimaryKeys.Distinct().ToList();

                            if (tableList.Count == 0)
                            {
                                tableList.Add(tbpk);
                            }
                            else if (!tableList.Any(x => x.CodeHammerName.Equals(table.CodeHammerName)))
                            {
                                tableList.Add(tbpk);
                            }
                        }
                    }
                }
            }
            //}

            // Generate the necessary SQL and C# code for each table
            int count = 0;
            if (tableList.Count > 0 || selectTables == false)
            {
                //// Create the CRUD stored procedures and data access code for each table
                ////This is intended for datatables

                if (selectTables == true)
                {
                    string tablename = string.Empty;
                    foreach (CodeHammerTableDto table in tableList)
                    {
                        DataSet dt1 = new DataSet();
                        List<string> foreignKeys = new List<string>();
                        foreignKeys.Clear();

                        if (!dbDataSupportAdapterContract.GetPrimaryKeysFromDBSql(ioManagerContract.GetSqlClient, codeHammerDataUtilContract.connectionStringDB, table.CodeHammerName, out dt1))
                        {
                            logFuncContract.Logger("Could not retrieve Primary keys!");
                        }

                        if (dt1.Tables[1].Rows.Count > 0)
                        {
                            try
                            {
                                DataTable foreignKeyTable = dt1.Tables[1];
                                foreach (DataRow item in foreignKeyTable.Rows)
                                {
                                    string name = item["SchemaName"].ToString() + "." + item["TableName"].ToString();
                                    string codeHammerCodeHammerColumnName = item["ColName"].ToString();

                                    foreach (CodeHammerColumn codeHammerCodeHammerColumn in table.CodeHammerColumns)
                                    {
                                        if (codeHammerCodeHammerColumn.CodeHammerName == codeHammerCodeHammerColumnName)
                                        {
                                            if (!foreignKeys.Any(x => x.Equals(codeHammerCodeHammerColumnName)))
                                            {
                                                foreignKeys.Add(codeHammerCodeHammerColumnName);
                                            }
                                        }
                                    }

                                    if (foreignKeys.Count > 0)
                                    {
                                        if (!table.CodeHammerForeignKeys.ContainsKey(name))
                                        {
                                            table.CodeHammerForeignKeys.Add(name, foreignKeys);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }

                        List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos = new List<CodeHammerPropAndValueDto>();
                        List<CodeHammerPkClass> codeHammerPkClassList;
                        ioManagerContract.DbConnection = connectionString;
                        List<Tuple<dynamic, dynamic, string>> dbTypesAndNamesList = dbDataSupportAdapterContract.GetDBTypesAndNames(ioManagerContract.GetSqlClient, codeHammerDataUtilContract.connectionStringDB, table.CodeHammerName, out tablename, out codeHammerPkClassList).Distinct().ToList();
                        if (string.IsNullOrEmpty(table.CodeHammerName))
                        {
                            throw new Exception("Table name is empty. Not allowed!");
                        }

                        ioManagerContract.PkKeys = codeHammerPkClassList;
                        //fluentNHibernateGeneratorContract.SetupFluentNHibernate("CodeHammerRepository", tablename, string.Empty, ioManagerContract.PkKeys);

                        foreach (Tuple<dynamic, dynamic, string> item in dbTypesAndNamesList)
                        {
                            foreach (CodeHammerPkClass pkClass in ioManagerContract.PkKeys)
                            {
                                CodeHammerPropAndValueDto pd = new CodeHammerPropAndValueDto()
                                {
                                    PropName = item.Item1,
                                    PropValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                                    VariableName = codeHammerDataUtilContract.FirstLetterToLowerCase(item.Item1),
                                    VariableValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                                };

                                if (!CodeHammerPropAndValueDtos.Any(x => x.PropName.Equals(pd.PropName)))
                                {
                                    CodeHammerPropAndValueDtos.Add(pd);
                                }
                            }
                        }

                        if (!onlyDTO && !onlyDataContract)
                        {
                            if (crudYesNo)
                            {
                                if (ioManagerContract.UseOrm)
                                {
                                    #region FluentNHibernate

                                    #region Generate FluentNHibernate classes

                                    //FluentNhibernate class
                                    //codeHammerDataUtilContract.CreateDataFolder(csPath + ioManagerContract.DtoDir);
                                    //CodeGeneratorClassLibrary codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary(SuffixDto.Instance().DtoTextBox, tablename + SuffixDto.Instance().DtoTextBox, "fluentNHibernate", ioManagerContract.PkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.AddPropertiesFLUENTNHIBERNATE(false, CodeHammerPropAndValueDtos, tablename, ioManagerContract.PkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.DtoFolder + tablename + SuffixDto.Instance().DtoTextBox + ".cs");
                                    //codeGeneratorClassFLuentNhibernateLibrary.CodeCheckVirtualDeclarationOperators(csPath + ioManagerContract.DtoDir + tablename + SuffixDto.Instance().DtoTextBox + ".cs");

                                    //if (ioManagerContract.PkKeysHoldRelations.Count > 0)
                                    //{
                                    //    foreach (PkClass pkClass in ioManagerContract.PkKeysHoldRelations)
                                    //    {
                                    //        if (!string.IsNullOrEmpty(pkClass.ForeignKeyRef))
                                    //        {
                                    //            // ioManagerContract.PkKeys.Clear();
                                    //            tablename = pkClass.ForeignKeyRef;
                                    //            dbDataSupportAdapterContract.InitNHibernateDTOGneration(tablename, ioManagerContract.dbConnection, out CodeHammerPropAndValueDtos, true);
                                    //            codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary("Domain", tablename + SuffixDto.Instance().DtoTextBox, "fluentNHibernate", ioManagerContract.PkKeys);
                                    //            codeGeneratorClassFLuentNhibernateLibrary.AddPropertiesFLUENTNHIBERNATE(false, CodeHammerPropAndValueDtos, tablename, ioManagerContract.PkKeys);
                                    //            codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.DtoFolder + tablename + SuffixDto.Instance().DtoTextBox + ".cs");
                                    //            codeGeneratorClassFLuentNhibernateLibrary.CodeCheckVirtualDeclarationOperators(csPath + ioManagerContract.DtoDir + tablename + SuffixDto.Instance().DtoTextBox + ".cs");
                                    //        }
                                    //    }
                                    //}

                                    ////codeHammerDataUtilContract.CreateDataFolder(csPath + ioManagerContract.EntitiesDir);
                                    ////CodeGeneratorClassLibrary codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary("Entities", tablename, "fluentNHibernate", ioManagerContract.PkKeys);
                                    ////codeGeneratorClassFLuentNhibernateLibrary.AddPropertiesFLUENTNHIBERNATE(CodeHammerPropAndValueDtos, tablename, ioManagerContract.PkKeys);
                                    ////codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.EntitiesDir + tablename + ".cs");

                                    ////Check in the folder Entities
                                    //codeGeneratorClassFLuentNhibernateLibrary.CodeCheckVirtualDeclarationOperators(csPath + ioManagerContract.EntitiesDir + tablename + ".cs");

                                    //FluentNhibernate Map class
                                    //codeHammerDataUtilContract.CreateDataFolder(csPath + ioManagerContract.CodeHammerRepositoryMappingFolder);
                                    //codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary("CodeHammerRepository.Mapping", table.CodeHammerName, "FluentNHibernateMapping", ioManagerContract.PkKeysHoldRelations);
                                    //codeGeneratorClassFLuentNhibernateLibrary.AddFluentNhibernateMapping(CodeHammerPropAndValueDtos, table.CodeHammerName, ioManagerContract.PkKeysHoldRelations);
                                    //codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.CodeHammerRepositoryMappingFolder + table.CodeHammerName + "Map.cs");
                                    //codeGeneratorClassFLuentNhibernateLibrary.CodeCheckMappingDeclarationOperators(csPath + ioManagerContract.CodeHammerRepositoryMappingFolder + table.CodeHammerName + "Map.cs");

                                    ////FluentNhibernate Helper class
                                    //codeHammerDataUtilContract.CreateDataFolder(csPath + ioManagerContract.CodeHammerRepositoryHelperFolder);
                                    //codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary("Helper", tablename, "FluentNHibernateHelper", ioManagerContract.PkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.AddFluentNhibernateHelper(CodeHammerPropAndValueDtos, tablename, ioManagerContract.PkKeys, csPath + ioManagerContract.CodeHammerRepositoryHelperFolder + "CodeHammerFluentNHibernateHelper.cs");
                                    //codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.CodeHammerRepositoryHelperFolder + "FluentNHibernateHelper.cs");

                                    ////FluentNhibernateRepository class
                                    //codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary("CodeHammerRepository", tablename, "ServerDataRepository", pkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.AddConstructorFluentNhibernateRepository(CodeHammerPropAndValueDtos, tablename, pkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(IOManager.output + "ServerDataRepository.cs");

                                    ////FluentNhibernateIRepository class
                                    //codeGeneratorClassFLuentNhibernateLibrary = new CodeGeneratorClassLibrary(tablename, "IServerDataRepository", pkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.AddConstructorFluentNhibernateIRepository(CodeHammerPropAndValueDtos, tablename, pkKeys);
                                    //codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(IOManager.output + "IServerDataRepository.cs");

                                    #endregion Generate FluentNHibernate classes

                                    #endregion FluentNHibernate
                                }
                                else
                                {
                                    if (!ioManagerContract.UseOrm)
                                    {
                                        if (!ioManagerContract.EmptyDataLayerCheckBox)
                                        {
                                            codeHammerSqlGeneratorContract.CodeHammerCreateInsertStoredProcedure(ioManagerContract.DatabaseName, table, csPath, createMultipleFiles);
                                            codeHammerSqlGeneratorContract.CodeHammerCreateUpdateStoredProcedure(ioManagerContract.DatabaseName, table, csPath, createMultipleFiles);
                                            codeHammerSqlGeneratorContract.CodeHammerCreateDeleteStoredProcedure(ioManagerContract.DatabaseName, table, csPath, createMultipleFiles);
                                            codeHammerSqlGeneratorContract.CodeHammerCreateSelectStoredProcedure(ioManagerContract.DatabaseName, table, csPath, createMultipleFiles);
                                            codeHammerSqlGeneratorContract.CodeHammerCreateSelectAllStoredProcedure("10", ioManagerContract.DatabaseName, table, csPath, createMultipleFiles);
                                        }
                                    }
                                }
                            }
                        }

                        if (crudYesNo)
                        {
                            if (!onlyDTO && !onlyDataContract)
                            {
                                //if (onlyDependencyDTO)
                                //{
                                //    //FluentNhibernate class
                                //    codeHammerDataUtilContract.CreateDataFolder(csPath + ioManagerContract.DtoDir);
                                //    CodeHammer.Framework.CodeGeneratorClassLibrary codeGeneratorClassFLuentNhibernateLibrary = new CodeHammer.Framework.CodeGeneratorClassLibrary("Domain", codeHammerDataUtilContract.CodeHammerFormatPascal(tablename) + SuffixDto.Instance().DtoTextBox, "fluentNHibernate", ioManagerContract.PkKeys);
                                //    codeGeneratorClassFLuentNhibernateLibrary.AddPropertiesFLUENTNHIBERNATE(onlyDependencyDTO, CodeHammerPropAndValueDtos, codeHammerDataUtilContract.CodeHammerFormatPascal(tablename), ioManagerContract.PkKeys);
                                //    codeGeneratorClassFLuentNhibernateLibrary.GenerateCSharpCode(csPath + ioManagerContract.DtoFolder + codeHammerDataUtilContract.CodeHammerFormatPascal(tablename) + SuffixDto.Instance().DtoTextBox + ".cs");
                                //    ////Check in the folder Entities
                                //    codeGeneratorClassFLuentNhibernateLibrary.CodeCheckVirtualDeclarationOperators(csPath + ioManagerContract.DtoDir + codeHammerDataUtilContract.CodeHammerFormatPascal(tablename) + SuffixDto.Instance().DtoTextBox + ".cs");
                                //}
                                //else
                                //{
                                //CodeHammerDTOGenerator.Instance.CodeHammerCreateDataTransferClass(onlyDTO, selectTables, tableDic, table, csPath, aspnetPath, resultDataOptions, usercontrolInheritsTextBox, connectionString, configConnection, outputDirectory, databaseManagement, csPath, throttling);
                                //}
                            }
                        }

                        codeHammerDTOGeneratorContract.CodeHammerCreateDataTransferClass(onlyDataContract, onlyDTO, selectTables, tableDic, table, csPath, aspnetPath, resultDataOptions, usercontrolInheritsTextBox, connectionString, configConnection, outputDirectory, databaseManagement, csPath, throttling, wcfSecurity);

                        if (!onlyDTO && !onlyDataContract)
                        {
                            if (crudYesNo)
                            {
                                if (!ioManagerContract.UseOrm)
                                {
                                    codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataAccessClass(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceDAL, csPath);
                                }

                                if (ioManagerContract.UseIoC && !ioManagerContract.UseOrm)
                                {
                                    codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataAccessInterface(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceDAL, csPath);
                                }

                                codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataManagementInterface(csPath, ioManagerContract.UseIoC);

                                codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBusinessLogicClass(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceBL, csPath);

                                if (ioManagerContract.UseIoC)
                                {
                                    codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBusinessLogicInterface(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceBL, csPath);
                                }

                                if (ioManagerContract.Log4Net)
                                {
                                    codeHammerLoggingGeneratorContract.CodeHammerLogging(csPath);
                                    codeHammerLoggingGeneratorContract.CodeHammerLog4NetSqlScript(csPath);
                                }

                                codeHammerBusinessLogicGeneratorContract.CodeHammerCreateBusinessLogicUnitTest(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceBL, csPath);
                                codeHammerDataAccessLayerGeneratorContract.CodeHammerCreateDataAccessClassUnitTest(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, targetNamespaceDAL, csPath);
                                codeHammerServiceLibraryGeneratorContract.CodeHammerServiceLibraryUnitTest(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, csPath);
                            }

                            codeHammerServiceLibraryGeneratorContract.ServiceLibrary(tableList, container, selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, csPath, instanceCall);

                            if (crudYesNo)
                            {
                                codeHammerServiceLibraryGeneratorContract.CodeHammerServiceContractLibrary(selectTables, tableDic, ioManagerContract.DatabaseName, table, crudYesNo, resultDataOptions, csPath);
                            }
                        }
                        count++;
                    }

                    if (!onlyDTO && !onlyDataContract)
                    {
                        if (crudYesNo)
                        {
                            if (executeStoredprocedureCheckBox)
                            {
                                if (!ioManagerContract.UseOrm)
                                {
                                    string systemMessage = string.Empty;
                                    string pathToProject = csPath + ioManagerContract.CodeHammerStoredProcedureFolder;
                                    if (!dbDataSupportAdapterContract.ExecuteStoredProcedureInToDatabase(pathToProject, out systemMessage))
                                    {
                                        logFuncContract.Logger(systemMessage);
                                        Console.WriteLine(systemMessage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Codes the hammer get between.
        /// </summary>
        /// <param name="strSource">The STR source.</param>
        /// <param name="strStart">The STR start.</param>
        /// <param name="strEnd">The STR end.</param>
        /// <returns>
        /// return true if success
        /// </returns>
        public string CodeHammerGetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Retrieves the codeHammerCodeHammerColumn, primary key, and foreign key information for
        /// the specified table.
        /// </summary>
        /// <param name="connecDB">The connec database.</param>
        /// <param name="table">The table instance that information should be retrieved for.</param>
        /// <param name="codeHammerColumnName">Name of the code hammer column.</param>
        public void CodeHammerQueryTable(string connecDB, CodeHammerTableDto table, string codeHammerColumnName)
        {
            // Get a list of the entities in the database
            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();
            DataTable dataTablePK = new DataTable();

            if (!dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.DBProvider, connecDB, codeHammerDataUtilContract.CodeHammerGetCodeHammerColumnQuery(table.CodeHammerName), out ds))
            {
                logFuncContract.Logger("Could not retrieve data. See Log");
                return;
            }

            //SqlDataAdapter dataAdapter = new SqlDataAdapter(codeHammerDataUtilContract.CodeHammerGetCodeHammerColumnQuery(table.CodeHammerName), connection);
            //dataAdapter.Fill(ds);

            dataTable = ds.Tables[0];
            dataTablePK = ds.Tables[1];

            foreach (DataRow codeHammerCodeHammerColumnRow in dataTable.Rows)
            {
                CodeHammerColumn codeHammerCodeHammerColumn = new CodeHammerColumn();
                codeHammerCodeHammerColumn.CodeHammerName = codeHammerCodeHammerColumnRow["COLUMN_NAME"].ToString();
                codeHammerCodeHammerColumn.CodeHammerType = codeHammerCodeHammerColumnRow["DATA_TYPE"].ToString();
                codeHammerCodeHammerColumn.CodeHammerPrecision = codeHammerCodeHammerColumnRow["NUMERIC_PRECISION"].ToString();
                codeHammerCodeHammerColumn.CodeHammerScale = codeHammerCodeHammerColumnRow["NUMERIC_SCALE"].ToString();

                // Determine the codeHammerCodeHammerColumn's length
                if (codeHammerCodeHammerColumnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                {
                    codeHammerCodeHammerColumn.CodeHammerLength = codeHammerCodeHammerColumnRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
                }
                else
                {
                    codeHammerCodeHammerColumn.CodeHammerLength = codeHammerCodeHammerColumnRow["COLUMN_LENGTH"].ToString();
                }

                // Is the codeHammerCodeHammerColumn a RowGuidCol codeHammerCodeHammerColumn?
                if (codeHammerCodeHammerColumnRow["IS_ROWGUIDCOL"].ToString() == "1")
                {
                    codeHammerCodeHammerColumn.CodeHammerIsRowGuidCol = true;
                }

                // Is the codeHammerCodeHammerColumn an Identity codeHammerCodeHammerColumn?
                if (codeHammerCodeHammerColumnRow["IS_IDENTITY"].ToString() == "1")
                {
                    codeHammerCodeHammerColumn.CodeHammerIsIdentity = true;
                }

                // Is codeHammerCodeHammerColumnRow codeHammerCodeHammerColumn a computed codeHammerCodeHammerColumn?
                if (codeHammerCodeHammerColumnRow["IS_COMPUTED"].ToString() == "1")
                {
                    codeHammerCodeHammerColumn.CodeHammerIsComputed = true;
                }

                // Is codeHammerCodeHammerColumnRow codeHammerCodeHammerColumn a computed codeHammerCodeHammerColumn?
                if (codeHammerCodeHammerColumnRow["IS_NULLABLE"].ToString() == "YES")
                {
                    codeHammerCodeHammerColumn.CodeHammerIsNullable = true;
                }

                if (codeHammerColumnName.Equals(codeHammerCodeHammerColumn.CodeHammerName))
                {
                    table.CodeHammerColumns.Add(codeHammerCodeHammerColumn);
                }
            }

            // Get the list of primary keys

            DataSet dt1 = null;
            if (!dbDataSupportAdapterContract.GetPrimaryKeysFromDBSql(ioManagerContract.DBProvider, codeHammerDataUtilContract.connectionStringDB, table.CodeHammerName, out dt1))
            {
                logFuncContract.Logger("Could not retrieve Primary keys!");
            }

            foreach (DataRow item in dt1.Tables[0].Rows)
            {
                foreach (CodeHammerColumn itemCol in table.CodeHammerColumns)
                {
                    itemCol.CodeHammerTableName = item.ItemArray[0].ToString();

                    if (item.ItemArray[2] != null)
                    {
                        if (itemCol.CodeHammerName.Equals(item.ItemArray[1]))
                            itemCol.CodeHammerIsIdentity = Convert.ToBoolean(item.ItemArray[2]);
                    }

                    if (item.ItemArray[1].ToString() == itemCol.CodeHammerName)
                    {
                        if (!table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(itemCol.CodeHammerName)))
                        {
                            table.CodeHammerPrimaryKeys.Add(itemCol);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the data tables.
        /// </summary>
        /// <returns>
        /// return true if success
        /// </returns>
        public DataSet CodeHammerRetrieveDataTables()
        {
            DataSet tableList = new DataSet();

            if (!dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.DBProvider, codeHammerDataUtilContract.connectionStringDB, codeHammerDataUtilContract.CodeHammerGetTableQuery(ioManagerContract.DatabaseName), out tableList))
            {
                logFuncContract.Logger("Could not retrieve data!");
                return null;
            }

            return tableList;
        }

        /// <summary>
        /// Codes the hammer retrieve primary keys.
        /// </summary>
        /// <returns>
        /// return true if success
        /// </returns>
        public DataSet CodeHammerRetrievePrimaryKeys()
        {
            DataSet tableList = new DataSet();

            if (!dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.DBProvider, codeHammerDataUtilContract.connectionStringDB, codeHammerDataUtilContract.CodeHammerGetPrimaryKeys(ioManagerContract.DatabaseName), out tableList))
            {
                logFuncContract.Logger("Could not retrieve data!");
                return null;
            }

            return tableList;
        }

        /// <summary>
        /// Retrieves the stored procedure from database.
        /// </summary>
        /// <returns>
        /// return true if success
        /// </returns>
        public DataSet CodeHammerRetrieveStoredProcedureFromDatabase()
        {
            DataSet tableList = new DataSet();
            if (!dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.DBProvider, codeHammerDataUtilContract.connectionStringDB, codeHammerDataUtilContract.CodeHammerGetPrimaryKeys(ioManagerContract.DatabaseName), out tableList))
            {
                logFuncContract.Logger("Could not retrieve data!");
                return null;
            }

            return tableList;
        }

        /// <summary>
        /// Initializes the n hibernate dto gneration.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="codeHammerPropAndValueDtos">The property and value dtos.</param>
        /// <param name="exclude">if set to <c>true</c> [exclude].</param>
        /// <exception cref="System.Exception">Table name is empty. Not allowed!</exception>
        public void InitNHibernateDTOGeneration(string tableName, string connectionString, out List<CodeHammerPropAndValueDto> codeHammerPropAndValueDtos, bool exclude)
        {
            ioManagerContract.PkKeys.Clear();
            codeHammerPropAndValueDtos = new List<CodeHammerPropAndValueDto>();
            ioManagerContract.DbConnection = connectionString;
            List<CodeHammerPkClass> pkKeys;

            List<Tuple<dynamic, dynamic, string>> dbTypesAndNamesList = dbDataSupportAdapterContract.GetDBTypesAndNames(ioManagerContract.GetSqlClient, connectionString, tableName, out tableName, out pkKeys).Distinct().ToList();
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Table name is empty. Not allowed!");
            }

            fluentNHibernateGeneratorContract.SetupFluentNHibernate("CodeHammerRepository", tableName, string.Empty, ioManagerContract.PkKeys);

            foreach (Tuple<dynamic, dynamic, string> item in dbTypesAndNamesList)
            {
                if (exclude)
                {
                    foreach (CodeHammerPkClass pkClass in ioManagerContract.PkKeysHoldRelations)
                    {
                        if (!pkClass.ForeignKeyRefID.Equals(item.Item1))
                        {
                            CodeHammerPropAndValueDto pd = new CodeHammerPropAndValueDto()
                            {
                                PropName = item.Item1,
                                PropValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                                VariableName = codeHammerDataUtilContract.FirstLetterToLowerCase(item.Item1),
                                VariableValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                            };

                            if (!codeHammerPropAndValueDtos.Any(x => x.PropName.Equals(pd.PropName)))
                            {
                                codeHammerPropAndValueDtos.Add(pd);
                            }
                        }
                    }
                }
                else
                {
                    CodeHammerPropAndValueDto pd = new CodeHammerPropAndValueDto()
                    {
                        PropName = item.Item1,
                        PropValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                        VariableName = codeHammerDataUtilContract.FirstLetterToLowerCase(item.Item1),
                        VariableValue = codeHammerDataUtilContract.GetClrType(item.Item2).FullName + item.Item3,
                    };

                    if (!codeHammerPropAndValueDtos.Any(x => x.PropName.Equals(pd.PropName)))
                    {
                        codeHammerPropAndValueDtos.Add(pd);
                    }
                }
            }
        }
    }
}