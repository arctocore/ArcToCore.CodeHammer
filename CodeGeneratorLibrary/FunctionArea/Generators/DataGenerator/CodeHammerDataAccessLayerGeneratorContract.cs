/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.DataGenerator
{
    /// <summary>
    /// this interface CodeHammerDataAccessLayerGeneratorContract
    /// </summary>

    public interface CodeHammerDataAccessLayerGeneratorContract
    {
        /// <summary>
        /// Codes the hammer create data access class.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceDAL">The target namespace dal.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDataAccessClass(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceDAL, string path);

        /// <summary>
        /// Codes the hammer create data access class unit test.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceDAL">The target namespace dal.</param>
        /// <param name="path">The path.</param>
        void CodeHammerCreateDataAccessClassUnitTest(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceDAL, string path);

        /// <summary>
        /// Codes the hammer create data access interface.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceDAL">The target namespace dal.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDataAccessInterface(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceDAL, string path);

        /// <summary>
        /// Codes the hammer create data management interface.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="ioc">if set to <c>true</c> [ioc].</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDataManagementInterface(string path, bool ioc);

        /// <summary>
        /// Codes the hammer create delete method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDeleteMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create delete method test.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateDeleteMethodTest(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create insert method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateInsertMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create insert method test.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateInsertMethodTest(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create select all method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectAllMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create select all method test.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectAllMethodTest(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create select method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create select method test.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateSelectMethodTest(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create update method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateUpdateMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create update method test.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateUpdateMethodTest(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// is the code hammer create delete method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool ICodeHammerCreateDeleteMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// is the code hammer create insert method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool ICodeHammerCreateInsertMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// is the code hammer create select all method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool ICodeHammerCreateSelectAllMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// is the code hammer create select method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool ICodeHammerCreateSelectMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// is the code hammer create update method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool ICodeHammerCreateUpdateMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);
    }
}