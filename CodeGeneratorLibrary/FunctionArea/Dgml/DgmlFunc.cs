/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeGen.Framework.FunctionArea.Dgml
{
    using CodeHammer.Framework;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    /// <summary>
    /// this class DgmlFunc
    /// </summary>

    public class DgmlFunc : DgmlFuncContract
    {
        #region Variables

        /// <summary>
        /// The data table
        /// </summary>
        private DataSet dataSet;

        /// <summary>
        /// The SQL select builder
        /// </summary>
        private StringBuilder providerSelectBuilder = new StringBuilder();

        /// <summary>
        /// The SQL update builder
        /// </summary>
        private StringBuilder providerUpdateBuilder = new StringBuilder();

        /// <summary>
        /// The DGML builder
        /// </summary>
        private StringBuilder dgmlBuilder = new StringBuilder();

        /// <summary>
        /// The dependency builder
        /// </summary>
        private static StringBuilder dependencyBuilder = new StringBuilder();

        /// <summary>
        /// The quates
        /// </summary>
        private static string quates = @"""";

        /// <summary>
        /// The method call graph
        /// </summary>
        private List<string> MethodCallGraph = new List<string>();

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The database data support adapter contract
        /// </summary>
        private DbDataSupportAdapterContract dbDataSupportAdapterContract = null;

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="DgmlFunc"/> class.
        /// </summary>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="dbDataSupportAdapterContract">The database data support adapter contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        public DgmlFunc(FuncTypeFactoryContract funcTypeFactoryContract,
            IOManagerContract ioManagerContract,
            DbDataSupportAdapterContract dbDataSupportAdapterContract,
            LogFuncContract logFuncContract)
        {
            this.ioManagerContract = ioManagerContract;
            this.dbDataSupportAdapterContract = dbDataSupportAdapterContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.logFuncContract = logFuncContract;

            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
            dbDataSupportAdapterContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.DBDATASUPPORTADAPTER);
            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
        }

        #region Properties

        /// <summary>
        /// Gets the Quates.
        /// </summary>
        /// <value>
        /// The Quates.
        /// </value>
        public string Quates
        {
            get
            {
                return @"""";
            }
        }

        #endregion Properties

        #region Methods

        // your method to pull data from database to datatable
        /// <summary>
        /// Pulls the data.
        /// </summary>
        /// <returns>
        /// if success return true
        /// </returns>
        public bool PullData()
        {
            try
            {
                dgmlBuilder.Clear();
                dependencyBuilder.Clear();

                string query = ioManagerContract.FindForeignKeysFromTableName.Replace("TABLENAME", ioManagerContract.SelectTableName);

                DataSet ds = null;
                var dalFactory = dbDataSupportAdapterContract.CreateSupportDataAdapter(ioManagerContract.GetSqlClient, ioManagerContract.DbConnection, query, out  ds);

                providerSelectBuilder.Length = 0;
                providerSelectBuilder.Capacity = 0;

                providerUpdateBuilder.Length = 0;
                providerUpdateBuilder.Capacity = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        //  0             1               2                   3             4                     5
                        //SchemaName | TableName | ColumnName | ReferenceSchemaName | ReferenceTableName | ReferenceColumnName
                        //Remember to check for table User = [User].
                        dependencyBuilder.Clear();

                        dependencyBuilder.AppendLine("    BaseTable: " + item["TableName"].ToString());// item["TableName"].ToString());
                        dependencyBuilder.AppendLine("                     |                     ");
                        dependencyBuilder.AppendLine("                     |                     ");

                        dgmlBuilder.Clear();
                        dgmlBuilder.AppendLine("<?xml version=" + quates + "1.0" + quates + " encoding=" + quates + "utf-8" + quates + "?>");
                        dgmlBuilder.AppendLine("<DirectedGraph Title=" + quates + "Table dependencies" + quates + " xmlns=" + quates + "http://schemas.microsoft.com/vs/2009/dgml" + quates + ">");

                        SqlSelectJoinMakerRecursive(item, item["TableName"] + "_" + item["TableName"]);
                    }
                }

                ioManagerContract.NoDependyOnTable = true;
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Orders the recursive helper.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="tableName">Name of the table.</param>
        private void SqlSelectJoinMakerRecursive(DataRow dr, string tableName)
        {
            try
            {
                if (!dgmlBuilder.ToString().Contains("<Nodes>"))
                {
                    dgmlBuilder.AppendLine("<Nodes>");
                }

                foreach (DataRow item in dr.Table.Rows)
                {
                    if (tableName.Equals(item["TableName"] + "_" + item["TableName"]))
                    {
                        dgmlBuilder.AppendLine("<Node Id=" + Quates + item["TableName"].ToString() + Quates + " Group=" + quates + "Collapsed" + quates + " Label=" + Quates + item["TableName"].ToString() + Quates + " Category=" + Quates + "C2" + Quates + "/>");

                        dgmlBuilder.AppendLine("<Node Id=" + Quates + item["ReferenceTableName"].ToString() + Quates + " Group=" + quates + "Collapsed" + quates + " Label=" + Quates + item["ReferenceTableName"].ToString() + Quates + " Category=" + Quates + "C1" + Quates + "/>");

                        dgmlBuilder.AppendLine("<Node Id=" + Quates + item["ReferenceColumnName"].ToString() + Quates + " Label=" + Quates + item["ReferenceColumnName"].ToString() + Quates + " Category=" + Quates + "C2" + Quates + "/>");
                    }
                }

                if (!dgmlBuilder.ToString().Contains("</Nodes>"))
                {
                    dgmlBuilder.AppendLine("</Nodes>");
                }

                if (!dgmlBuilder.ToString().Contains("<Links>"))
                {
                    dgmlBuilder.AppendLine("<Links>");
                }

                foreach (DataRow item in dr.Table.Rows)
                {
                    if (tableName.Equals(item["TableName"] + "_" + item["TableName"]))
                    {
                        if (!dependencyBuilder.ToString().Contains("DependencyTable: " + item["ReferenceTableName"].ToString()))
                        {
                            dependencyBuilder.AppendLine("    DependencyTable: " + item["ReferenceTableName"].ToString() + " [" + item["ReferenceColumnName"] + "]");
                        }

                        dgmlBuilder.AppendLine("<Link Source=" + quates + item["TableName"] + quates + " Target=" + quates + item["ReferenceTableName"] + quates + " Background=" + Quates + "Green" + Quates + " Stroke=" + Quates + "#FF0000" + Quates + " Category=" + Quates + "Contains" + Quates + " />");
                        dgmlBuilder.AppendLine("<Link Source=" + quates + item["ReferenceTableName"] + quates + " Target=" + quates + item["ReferenceColumnName"] + quates + " Background=" + Quates + "Green" + Quates + " Stroke=" + Quates + "#FF0000" + Quates + " Category=" + Quates + "Contains" + Quates + " />");
                    }
                }

                dgmlBuilder.AppendLine("</Links>");

                #region Categories

                dgmlBuilder.AppendLine("<Categories>");

                dgmlBuilder.AppendLine("<Category Id=" + Quates + "C1" + Quates + " Label=" + Quates + "C1" + Quates + " BasedOn=" + Quates + "ParentCategory" + Quates + "/>");
                dgmlBuilder.AppendLine("<Category Id=" + Quates + "C2" + Quates + " Label=" + Quates + "C2" + Quates + " BasedOn=" + Quates + "ParentNameSpaceCategory" + Quates + "/>");

                dgmlBuilder.AppendLine("<Category Id=" + Quates + "Contains" + Quates + " Label=" + Quates + "Contains" + Quates + " Description=" + Quates + "Source of the link contains the target object" + Quates + " CanBeDataDriven=" + Quates + "False" + Quates + " CanLinkedNodesBeDataDriven=" + Quates + "True" + Quates + " IncomingActionLabel=" + Quates + "Contained By" + Quates + " IsContainment=" + Quates + "True" + Quates + " OutgoingActionLabel=" + Quates + "Contains" + Quates + " />");
                dgmlBuilder.AppendLine("<Category Id=" + Quates + "ParentCategory" + Quates + " Label=" + Quates + "Parent Category" + Quates + " Background=" + Quates + "#7FFF68" + Quates + "/>");
                dgmlBuilder.AppendLine("<Category Id=" + Quates + "ParentNameSpaceCategory" + Quates + " Label=" + Quates + "Parent Category" + Quates + " Background=" + Quates + "#66CEFF" + Quates + "/>");

                dgmlBuilder.AppendLine("</Categories>");

                #endregion Categories

                dgmlBuilder.AppendLine("</DirectedGraph>");

                ioManagerContract.CodeEditorBuilder = dgmlBuilder;

                ioManagerContract.DependencyTableBuilder = dependencyBuilder;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
            }
        }

        #endregion Methods
    }
}