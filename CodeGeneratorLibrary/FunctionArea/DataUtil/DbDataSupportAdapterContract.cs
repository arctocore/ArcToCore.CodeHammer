using System;

namespace CodeHammer.Framework.FunctionArea.DataUtil
{
    /// <summary>
    /// this interface DbDataSupportAdapterContract
    /// </summary>

    public interface DbDataSupportAdapterContract
    {
        /// <summary>
        /// Creates the support data adapter.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="sqlText">The SQL text.</param>
        /// <param name="dataTable">The data table.</param>
        /// <returns>if success return true</returns>
        bool CreateSupportDataAdapter(string providerName, string connectionString, string sqlText, out System.Data.DataSet dataSet);

        /// <summary>
        /// Populates the data base SQL server.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="selectedServer">The selected server.</param>
        /// <param name="dbList">The database list.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <returns></returns>
        bool PopulateDataBaseSqlServer(string userId, string password, string selectedServer, out System.Collections.Generic.List<string> dbList, out string systemMessage);

        /// <summary>
        /// Executes the stored procedure in to database.
        /// </summary>
        /// <param name="sqlPath">The SQL path.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <returns>if success return true</returns>
        bool ExecuteStoredProcedureInToDatabase(string sqlPath, out string systemMessage);

        /// <summary>
        /// Codes the hammer split SQL commands.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>if success return true</returns>
        System.Collections.Generic.IEnumerable<string> CodeHammerSplitSqlCommands(string commandText);

        /// <summary>
        /// Gets the database types and names.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="table">The table.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if success return true</returns>
        System.Collections.Generic.List<Tuple<dynamic, dynamic, string>> GetDBTypesAndNames(string providerName, string connectionString, string tablename, out string table, out System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys);

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
        bool GetPKFromDBDtoSql(string providerName, string connectionString, string table, out System.Data.DataSet dataSet);

        /// <summary>
        /// Gets the primary keys from database SQL.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns></returns>
        bool GetPrimaryKeysFromDBSql(string providerName, string connectionString, string tablename, out System.Data.DataSet dataSet);
    }
}