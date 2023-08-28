/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.SqlGenerator
{
    /// <summary>
    /// this interface CodeHammerSqlGeneratorContract
    /// </summary>

    public interface CodeHammerSqlGeneratorContract
    {
        /// <summary>
        /// Codes the hammer create delete stored procedure.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDeleteStoredProcedure(string databaseName, CodeHammer.Entities.CodeHammerTableDto table, string path, bool createMultipleFiles);

        /// <summary>
        /// Codes the hammer create insert stored procedure.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateInsertStoredProcedure(string databaseName, CodeHammer.Entities.CodeHammerTableDto table, string path, bool createMultipleFiles);

        /// <summary>
        /// Codes the hammer create select all stored procedure.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectAllStoredProcedure(string pageSize, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, string path, bool createMultipleFiles);

        /// <summary>
        /// Codes the hammer create select stored procedure.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectStoredProcedure(string databaseName, CodeHammer.Entities.CodeHammerTableDto table, string path, bool createMultipleFiles);

        /// <summary>
        /// Codes the hammer create update stored procedure.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="path">The path.</param>
        /// <param name="createMultipleFiles">if set to <c>true</c> [create multiple files].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateUpdateStoredProcedure(string databaseName, CodeHammer.Entities.CodeHammerTableDto table, string path, bool createMultipleFiles);
    }
}