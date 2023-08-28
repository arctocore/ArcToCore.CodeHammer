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
    using System;
    using System.Data;
    using System.Text;

    /// <summary>
    /// this interface CodeHammerDataUtilContract
    /// </summary>

    public interface CodeHammerDataUtilContract
    {
        /// <summary>
        /// Clears the folder.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        void ClearFolder(string FolderName);

        /// <summary>
        /// Clears the folder and creates a new.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>

        void ClearFolderAndCreateNew(string FolderName);

        /// <summary>
        /// Codes the hammer get route configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        string CodeHammerGetRouteConfig(out string name);

        /// <summary>
        /// Codes the hammer create method parameter.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerCreateMethodParameter(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer create method sp parameter.
        /// </summary>
        /// <param name="codeHammerReversedEngineedSPType">Type of the code hammer reversed engineed sp.</param>
        /// <param name="codeHammerReversedEngineedSPName">Name of the code hammer reversed engineed sp.</param>
        /// <returns>return if success</returns>
        string CodeHammerCreateMethodSPParameter(string codeHammerReversedEngineedSPType, string codeHammerReversedEngineedSPName);

        /// <summary>
        /// Codes the hammer create parameter string.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <param name="checkForOutputParameter">if set to <c>true</c> [check for output parameter].</param>
        /// <returns>return if success</returns>
        string CodeHammerCreateParameterString(CodeHammer.Entities.CodeHammerColumn codeHammerColumn, bool checkForOutputParameter);

        /// <summary>
        /// Codes the hammer create sub directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="deleteIfExists">if set to <c>true</c> [delete if exists].</param>
        void CodeHammerCreateSubDirectory(string name, bool deleteIfExists);

        /// <summary>
        /// Codes the hammer format camel.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>return if success</returns>
        string CodeHammerFormatCamel(string original);

        /// <summary>
        /// Codes the hammer format camel dto.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>return if success</returns>
        string CodeHammerFormatCamelDTO(string original);

        /// <summary>
        /// Codes the name of the hammer format class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>return if success</returns>
        string CodeHammerFormatClassName(string tableName);

        /// <summary>
        /// Codes the hammer format pascal.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>return if success</returns>
        string CodeHammerFormatPascal(string original);

        /// <summary>
        /// Codes the name of the hammer format variable.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>return if success</returns>
        string CodeHammerFormatVariableName(string tableName);

        /// <summary>
        /// Codes the hammer get code hammer column query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetCodeHammerColumnQuery(string tableName);

        /// <summary>
        /// Codes the type of the hammer get cs.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetCsType(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer get cs type reader get.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetCsTypeReaderGet(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer get cs type reader get default value.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetCsTypeReaderGetDefaultValue(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer get database access manager.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetDatabaseAccessManager(out string name);

        /// <summary>
        /// Codes the hammer get database connection.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetDatabaseConnection(out string name);

        /// <summary>
        /// Codes the hammer get database manager.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetDatabaseManager(out string name);

        /// <summary>
        /// Codes the hammer get global castle.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetGlobalCastle(out string name);

        /// <summary>
        /// Codes the hammer get log4 class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetLog4Class(out string name);

        /// <summary>
        /// Codes the hammer get log4 net configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetLog4NetConfig(out string name);

        /// <summary>
        /// Codes the hammer get nullable types resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="newtableName">Name of the newtable.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetNullableTypesResource(string name, string tableName, string newtableName);

        /// <summary>
        /// Codes the length of the hammer get parameter.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetParameterLength(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer get primary keys.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetPrimaryKeys(string databaseName);

        /// <summary>
        /// Codes the hammer get resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetResource(string name);

        /// <summary>
        /// Codes the hammer get resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetResource(string name, string oldValue, string newValue);

        /// <summary>
        /// Codes the hammer get resource as stream.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        System.IO.Stream CodeHammerGetResourceAsStream(string name);

        /// <summary>
        /// Codes the type of the hammer get reversed stored procedure cs.
        /// </summary>
        /// <param name="codeHammerReversedSP">The code hammer reversed sp.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetReversedStoredProcedureCsType(string codeHammerReversedSP);

        /// <summary>
        /// Codes the hammer get stored procedure parameters.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetStoredProcedureParams(string spName);

        /// <summary>
        /// Codes the hammer get stored procedure query.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetStoredProcedureQuery(string databaseName);

        /// <summary>
        /// Codes the hammer get table query.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>return if success</returns>
        string CodeHammerGetTableQuery(string databaseName);

        /// <summary>
        /// Codes the hammer to cs type convert to.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerToCsTypeConvertTo(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the type of the hammer to cs type convert to.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns>return if success</returns>
        string CodeHammerToCsTypeConvertToType(CodeHammer.Entities.CodeHammerColumn codeHammerColumn);

        /// <summary>
        /// Codes the hammer web configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return if success</returns>
        string CodeHammerWebConfig(out string name);

        /// <summary>
        /// Copies the code hammer get code hammer project.
        /// </summary>
        /// <param name="vsVersion">The vs version.</param>
        /// <param name="file">The file.</param>
        void CopyCodeHammerGetCodeHammerProject(string vsVersion, string file);

        /// <summary>
        /// Creates the data folder.
        /// </summary>
        /// <param name="path">The path.</param>
        void CreateDataFolder(string path);

        /// <summary>
        /// Deletes the lines.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="linesToRemove">The lines to remove.</param>
        /// <returns>return if success</returns>
        string DeleteLines(string s, int linesToRemove);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        void DeleteProject(string FolderName);

        /// <summary>
        /// Finds the solution.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="sln">The SLN.</param>
        void FindSolution(string path, out string sln);

        /// <summary>
        /// Firsts the letter to lower case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>return if success</returns>
        System.Collections.Generic.IEnumerable<char> FirstLetterToLowerCase(string value);

        /// <summary>
        /// Firsts the letter uppercase first.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>return if success</returns>
        string FirstLetterUppercaseFirst(string s);

        /// <summary>
        /// Gets the type of the color.
        /// </summary>
        /// <param name="sqlType">Type of the SQL.</param>
        /// <returns>return if success</returns>
        Type GetClrType(System.Data.SqlDbType sqlType);

        /// <summary>
        /// Javascriptsystems the messagetring.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>return if success</returns>
        string JavascriptsystemMessagetring(string message);

        /// <summary>
        /// Normalizes the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>return if success</returns>
        string NormalizeString(string source);

        /// <summary>
        /// Zips the specified zip path.
        /// </summary>
        /// <param name="zipPath">The zip path.</param>
        /// <param name="extractPath">The extract path.</param>
        void Zip(string zipPath, string extractPath);

        /// <summary>
        /// Gets or sets the make help page.
        /// </summary>
        /// <value>
        /// The make help page.
        /// </value>
        StringBuilder MakeHelpPage { get; set; }

        /// <summary>
        /// Gets or sets the connection string database.
        /// </summary>
        /// <value>
        /// The connection string database.
        /// </value>
        string connectionStringDB { get; set; }

        /// <summary>
        /// Gets or sets the data table names.
        /// </summary>
        /// <value>
        /// The data table names.
        /// </value>
        DataSet DataTableNames { get; set; }
    }
}