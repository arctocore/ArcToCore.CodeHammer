/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.DtoGenerator
{
    /// <summary>
    /// this interface CodeHammerDTOGeneratorContract
    /// </summary>

    public interface CodeHammerDTOGeneratorContract
    {
        /// <summary>
        /// Codes the hammer create data transfer class.
        /// </summary>
        /// <param name="onlyDataContract">if set to <c>true</c> [only data contract].</param>
        /// <param name="onlyDTO">if set to <c>true</c> [only dto].</param>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="aspnetPath">The aspnet path.</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="usercontrolInheritsTextBox">The usercontrol inherits text box.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="configConnection">The configuration connection.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="databaseManagementPath">The database management path.</param>
        /// <param name="csPath">The cs path.</param>
        /// <param name="throttling">if set to <c>true</c> [throttling].</param>
        /// <param name="security">if set to <c>true</c> [security].</param>
        /// <returns>
        /// if sucess then return true
        /// </returns>
        bool CodeHammerCreateDataTransferClass(bool onlyDataContract, bool onlyDTO, bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, CodeHammer.Entities.CodeHammerTableDto table, string path, string aspnetPath, System.Collections.Generic.List<string> resultDataOptions, string usercontrolInheritsTextBox, string connectionString, string configConnection, string outputDirectory, string databaseManagementPath, string csPath, bool throttling, int wcfSecurity);
    }
}