/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.DataUtil
{
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// this class DbDataSupportAdapter
    /// </summary>

    //[assembly: AssemblyKeyFileAttribute("keyfile.snk")]
    public class DbDataSupportAdapter : CodeHammer.Framework.FunctionArea.DataUtil.DbDataSupportAdapterContract
    {
        #region Variables

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataSupportAdapter" /> class.
        /// </summary>
        /// <param name="iFuncTypeFactory">The i function type factory.</param>
        /// <param name="iLogFunc">The i log function.</param>
        /// <param name="iFileIOFunc">The i file io function.</param>
        public DbDataSupportAdapter(LogFuncContract logFuncContract,
            IOManagerContract ioManagerContract,
            FuncTypeFactoryContract funcTypeFactoryContract,
            CodeHammerDataUtilContract codeHammerDataUtilContract)
        {
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;
            this.logFuncContract = logFuncContract;
            this.ioManagerContract = ioManagerContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
        }

        #region Methods

        /// <summary>
        /// Splits the SQL commands.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        /// return true if success
        /// </returns>
        public IEnumerable<string> CodeHammerSplitSqlCommands(string commandText)
        {
            IEnumerable<string> commands = commandText.Split(new string[] { "\r\nGO\r\n", "\r\nGo\r\n", "\r\ngo\r\n", "\r\ngO\r\n" }, StringSplitOptions.None);

            return commands;
        }

        /// <summary>
        /// Executes all commands.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        /// return true if success
        /// </returns>
        public IEnumerable<DbDataReader> CodeHammerExecuteAllCommands(string providerName, string commandText, string connectionString)
        {
            List<DbDataReader> results = new List<DbDataReader>();

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    // Define the query.
                    string queryString = commandText;

                    // Create the DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = queryString;
                    command.Connection = connection;

                    foreach (string commandStr in CodeHammerSplitSqlCommands(commandText))
                    {
                        command.CommandText = commandStr;
                        results.Add(command.ExecuteReader(CommandBehavior.Default));
                    }

                    return results.AsReadOnly();
                }
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="sqlText">The SQL text.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns>
        /// if success return true
        /// </returns>
        public bool CreateSupportDataAdapter(string providerName, string connectionString, string sqlText, out DataSet dataSet)
        {
            dataSet = new DataSet();
            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    // Define the query.
                    string queryString = sqlText;

                    // Create the DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = queryString;
                    command.Connection = connection;

                    // Create the DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Executes the stored procedure in to database.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="sqlPath">The SQL path.</param>
        /// <param name="systemMessage">The systemMessage.</param>
        /// <returns>return true if success</returns>

        public bool ExecuteStoredProcedureInToDatabase(string sqlPath, out string systemMessage)
        {
            systemMessage = null;
            StreamReader sr = null;
            DbCommand codeHammerExecuteSPCommand = null;

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(ioManagerContract.DBProvider);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = codeHammerDataUtilContract.connectionStringDB;
                connection.Open();
                using (connection)
                {
                    // Create the DbCommand.
                    codeHammerExecuteSPCommand = factory.CreateCommand();

                    codeHammerExecuteSPCommand.Connection = connection;

                    List<string> files = Directory.GetFiles(sqlPath, "*.sql*", SearchOption.AllDirectories).ToList();
                    foreach (string item in files)
                    {
                        sr = new StreamReader(item);
                        codeHammerExecuteSPCommand.CommandText = sr.ReadToEnd();
                        codeHammerExecuteSPCommand.ExecuteNonQuery();
                        sr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                sr.Close();

                systemMessage = "ExecuteStoredProcedureInToDatabase: Failed while executing stored procedures:" +
                    codeHammerExecuteSPCommand.CommandText + "\n"
                    + ex.Message;
                logFuncContract.Logger(systemMessage);
                ioManagerContract.LogBuilder.AppendLine(systemMessage);
                ioManagerContract.LogBuilder.AppendLine();
                Console.WriteLine(ex.Message);
                return true;
            }
            finally
            {
                sr.Close();
            }

            return true;
        }

        /// <summary>
        /// Populates the data base SQL server.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="selectedServer">The selected server.</param>
        /// <param name="dbList">The database list.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <returns></returns>
        public bool PopulateDataBaseSqlServer(string userId, string password, string selectedServer, out List<string> dbList, out string systemMessage)
        {
            systemMessage = string.Empty;
            dbList = new List<string>();

            List<string> dbListTemp = new List<string>();
            List<string> oracleTablesTemp = new List<string>();

            dbListTemp.Clear();
            oracleTablesTemp.Clear();
            DataTable tblDatabases = null;
            DbConnectionStringBuilder builder =
           new DbConnectionStringBuilder();

            if (ioManagerContract.DBProvider.Equals("Oracle.ManagedDataAccess.Client"))
            {
                builder.Add("Data Source", selectedServer);
                // enter credentials if you want
                builder.Add("User Id", userId);
                builder.Add("Password", password);
            }

            if (ioManagerContract.DBProvider.Equals("System.Data.SqlClient"))
            {
                builder.Add("server", selectedServer);
                // enter credentials if you want
                builder.Add("Uid", userId);
                builder.Add("Password", password);
            }

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(ioManagerContract.DBProvider);

                using (DbConnection con = factory.CreateConnection())
                {
                    con.ConnectionString = builder.ConnectionString;
                    con.Open();
                    //get databases
                    if (ioManagerContract.DBProvider.Equals("Oracle.ManagedDataAccess.Client"))
                    {
                        tblDatabases = con.GetSchema("Tables");
                        ioManagerContract.OracleTables = new List<string>();
                        ioManagerContract.OracleTables.Clear();
                        //add to list
                        foreach (DataRow row in tblDatabases.Rows)
                        {
                            dbListTemp.Add(row[0].ToString());
                            oracleTablesTemp.Add(row[1].ToString());
                        }

                        ioManagerContract.OracleTables = oracleTablesTemp.Distinct().ToList();
                    }

                    if (ioManagerContract.DBProvider.Equals("System.Data.SqlClient"))
                    {
                        tblDatabases = con.GetSchema("Databases");

                        //add to list
                        foreach (DataRow row in tblDatabases.Rows)
                        {
                            string strDatabaseName = row["database_name"].ToString();
                            dbListTemp.Add(strDatabaseName);
                        }
                    }
                }

                dbList = dbListTemp.Distinct().ToList();

                return true;
            }
            catch (Exception ex)
            {
                systemMessage = ex.StackTrace;
                //logFuncContract.Logger(systemMessage);
                //ioManagerContract.LogBuilder.AppendLine(systemMessage);
                //ioManagerContract.LogBuilder.AppendLine();
                //logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        ////<summary>
        ////Gets the columns from database.
        ////</summary>
        ////<param name="table">The table.</param>
        ////<param name="dt">The dt.</param>
        /// <summary>
        /// Gets the columns from database.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="table">The table.</param>
        /// <param name="dataTable">The data table.</param>
        /// <returns>
        /// if success return true
        /// </returns>
        private bool GetColumnsFromDB(string providerName, string connectionString, string table, out DataTable dataTable)
        {
            dataTable = new DataTable();
            string sql = ioManagerContract.SqlTable.Replace("TABLENAME", table);

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    // Define the query.
                    string queryString = sql;

                    // Create the DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = queryString;
                    command.Connection = connection;

                    // Create the DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataTable);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the pk from database.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="table">The table.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns>
        /// if success return true
        /// </returns>
        public bool GetPKFromDBDtoSql(string providerName, string connectionString, string table, out DataSet dataSet)
        {
            dataSet = new DataSet();
            DataTable dbTable = null;

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    string[] restrictions = new string[4];
                    restrictions[2] = table;
                    dbTable = connection.GetSchema("Tables", restrictions);
                }

                //using (var con = new SqlConnection(connectionString))
                //{
                //    if (con.State == ConnectionState.Closed)
                //    {
                //        con.Open();
                //    }

                //    string[] restrictions = new string[4];
                //    restrictions[2] = table;
                //    dbTable = con.GetSchema("Tables", restrictions);
                //    con.Close();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            string schemaName = dbTable.Rows[0].ItemArray[1].ToString();
            string tableName = dbTable.Rows[0].ItemArray[2].ToString();

            string sqlPkFk = ioManagerContract.SqlPKFK.Replace("TABLENAME", schemaName + "." + tableName) + "\n";
            string sqlPk = ioManagerContract.SqlPKLib.Replace("TABLENAME", table);

            string joinSP = sqlPkFk + sqlPk;
            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    // Create the DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = joinSP;
                    command.Connection = connection;

                    // Create the DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                }

                return true;

                //using (var con = new SqlConnection(connectionString))

                //using (var cmdDataContractSP = new SqlCommand(joinSP, con))
                //{
                //    using (var da = new SqlDataAdapter(cmdDataContractSP))
                //    {
                //        cmdDataContractSP.CommandType = CommandType.Text;
                //        da.Fill(dataSet);
                //        con.Close();
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the primary keys from database SQL.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns>if success return true</returns>
        /// <exception cref="Exception">the exception</exception>
        public bool GetPrimaryKeysFromDBSql(string providerName, string connectionString, string tablename, out DataSet dataSet)
        {
            dataSet = new DataSet();
            string tablequery = ioManagerContract.SqlPK.Replace("TABLENAME", tablename);
            string tbFK = ioManagerContract.FindFKFromPK.Replace("TABLENAME", tablename);
            string joinSql = tablequery + "\n" + tbFK;

            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                using (connection)
                {
                    // Create the DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = joinSql;
                    command.Connection = connection;

                    // Create the DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                }

                return true;
                //    using (var con = new SqlConnection(connectionString))
                //    using (var cmdDataContractSP = new SqlCommand(joinSql, con))
                //    {
                //        using (var da = new SqlDataAdapter(cmdDataContractSP))
                //        {
                //            cmdDataContractSP.CommandType = CommandType.Text;
                //            da.Fill(dataSet);
                //            con.Close();
                //        }
                //    }

                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the database types and names.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="table">The table.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>
        /// if success return true
        /// </returns>
        public List<Tuple<dynamic, dynamic, string>> GetDBTypesAndNames(string providerName, string connectionString, string tablename, out string table, out List<CodeHammerPkClass> pkKeys)
        {
            table = string.Empty;
            pkKeys = new List<CodeHammerPkClass>();
            DataTable dataTable = null;
            DataSet dataSetPrimaryKey = null;

            try
            {
                if (!this.GetColumnsFromDB(providerName, connectionString, tablename, out dataTable))
                {
                    return null;
                }

                if (!this.GetPKFromDBDtoSql(providerName, connectionString, tablename, out dataSetPrimaryKey))
                {
                    return null;
                }

                List<Tuple<dynamic, dynamic, string>> eoList = new List<Tuple<dynamic, dynamic, string>>();
                foreach (DataRow item in dataTable.Rows)
                {
                    dynamic cname = null;
                    dynamic cType = null;
                    string nullableState = string.Empty;
                    cname = item[3];

                    if (item[6].Equals("YES"))
                    {
                        SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item[7].ToString(), true);
                        cType = type;
                        if (!item[7].ToString().Contains("char"))
                        {
                            nullableState = "?";
                        }
                    }
                    else
                    {
                        SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item[7].ToString(), true);
                        cType = type;
                    }

                    table = item[2].ToString();
                    if (!eoList.Any(x => x.Item1.Equals(cname)))
                    {
                        eoList.Add(new Tuple<dynamic, dynamic, string>(cname, cType, nullableState));
                    }

                    CodeHammerPkClass pk = null;

                    if (dataSetPrimaryKey.Tables[0].Rows.Count == 0)
                    {
                        foreach (DataRow itemPk in dataSetPrimaryKey.Tables[1].Rows)
                        {
                            pk = new CodeHammerPkClass()
                            {
                                Name = itemPk[0].ToString(),
                                ColName = itemPk[1].ToString(),
                                IsIdentity = Convert.ToBoolean(itemPk[2].ToString()),
                                IsNullable = Convert.ToBoolean(itemPk[2].ToString()),
                                PrimaryKeyRef = itemPk["pk"].ToString()
                            };

                            pkKeys.Add(pk);
                        }
                    }
                    else
                    {
                        foreach (DataRow itemPkFk in dataSetPrimaryKey.Tables[0].Rows)
                        {
                            foreach (DataRow itemPk in dataSetPrimaryKey.Tables[1].Rows)
                            {
                                pk = new CodeHammerPkClass()
                                {
                                    Name = itemPk[0].ToString(),
                                    ColName = itemPk[1].ToString(),
                                    IsIdentity = Convert.ToBoolean(itemPk[2].ToString()),
                                    IsNullable = Convert.ToBoolean(itemPk[2].ToString()),
                                    ForeignKeyRef = itemPkFk["RefTable"].ToString(),
                                    ForeignKeyRefID = itemPkFk["foreignkey"].ToString(),
                                    PrimaryKeyRef = itemPk["pk"].ToString()
                                };

                                pkKeys.Add(pk);
                            }
                        }
                    }
                }

                return eoList;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return null;
            }
        }

        #endregion Methods
    }
}