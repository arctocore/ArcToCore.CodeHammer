/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

using CodeHammer.Framework.FunctionArea.FileIO;
namespace CodeHammer.Framework.FunctionArea.Generators
{
    /// <summary>
    /// this interface CodeHammerGeneratorContract
    /// </summary>

    public interface CodeHammerGeneratorContract
    {
        /// <summary>
        /// Codes the hammer generate.
        /// </summary>
        /// <param name="onlyDependencyDTO">if set to <c>true</c> [only dependency dto].</param>
        /// <param name="onlyDataContract">if set to <c>true</c> [only data contract].</param>
        /// <param name="onlyDTO">if set to <c>true</c> [only dto].</param>
        /// <param name="container">The container.</param>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tablesAndColumnDic">The tables and column dic.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="configConnection">The configuration connection.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <param name="targetNamespaceDAL">The target namespace dal.</param>
        /// <param name="targetNamespaceBL">The target namespace bl.</param>
        /// <param name="targetNamespaceDTO">The target namespace dto.</param>
        /// <param name="usercontrolInheritsTextBox">The usercontrol inherits text box.</param>
        /// <param name="executeStoredprocedureCheckBox">if set to <c>true</c> [execute storedprocedure CheckBox].</param>
        /// <param name="instanceCall">The instance call.</param>
        /// <param name="throttling">if set to <c>true</c> [throttling].</param>
        /// <param name="security">if set to <c>true</c> [security].</param>
        void CodeHammerGenerate(bool onlyDependencyDTO, bool onlyDataContract, bool onlyDTO, string container, bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> tablesAndColumnDic, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string outputDirectory, string connectionString, string configConnection, bool createMultipleFiles, string targetNamespaceDAL, string targetNamespaceBL, string targetNamespaceDTO, string usercontrolInheritsTextBox, bool executeStoredprocedureCheckBox, string instanceCall, bool throttling, int wcfSecurity);

        /// <summary>
        /// Codes the hammer get between.
        /// </summary>
        /// <param name="strSource">The string source.</param>
        /// <param name="strStart">The string start.</param>
        /// <param name="strEnd">The string end.</param>
        /// <returns>return string</returns>
        string CodeHammerGetBetween(string strSource, string strStart, string strEnd);

        /// <summary>
        /// Codes the hammer query table.
        /// </summary>
        /// <param name="connecDB">The connec database.</param>
        /// <param name="table">The table.</param>
        /// <param name="codeHammerColumnName">Name of the code hammer column.</param>
        void CodeHammerQueryTable(string connecDB, CodeHammer.Entities.CodeHammerTableDto table, string codeHammerColumnName);

        /// <summary>
        /// Codes the hammer retrieve data tables.
        /// </summary>
        /// <returns>
        /// return dataset
        /// </returns>
        System.Data.DataSet CodeHammerRetrieveDataTables();

        /// <summary>
        /// Codes the hammer retrieve primary keys.
        /// </summary>
        /// <returns>
        /// return dataset
        /// </returns>
        System.Data.DataSet CodeHammerRetrievePrimaryKeys();

        /// <summary>
        /// Codes the hammer retrieve stored procedure from database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>return dataset</returns>
        System.Data.DataSet CodeHammerRetrieveStoredProcedureFromDatabase();

        /// <summary>
        /// Initializes the n hibernate dto generation.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="codeHammerPropAndValueDtos">The code hammer property and value dtos.</param>
        /// <param name="exclude">if set to <c>true</c> [exclude].</param>
        void InitNHibernateDTOGeneration(string tableName, string connectionString, out System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> codeHammerPropAndValueDtos, bool exclude);
    }
}