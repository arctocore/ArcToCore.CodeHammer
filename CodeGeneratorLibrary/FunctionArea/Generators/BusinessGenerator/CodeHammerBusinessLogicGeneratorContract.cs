/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.BusinessGenerator
{
    /// <summary>
    /// this interface CodeHammerBusinessLogicGeneratorContract
    /// </summary>

    public interface CodeHammerBusinessLogicGeneratorContract
    {
        /// <summary>
        /// Codes the hammer create bl delete method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLDeleteMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl delete method test.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLDeleteMethodTest(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl insert method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLInsertMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl insert method test.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLInsertMethodTest(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl select all method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLSelectAllMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl select all method test.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLSelectAllMethodTest(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl select method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLSelectMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl select method test.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLSelectMethodTest(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl update method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLUpdateMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create bl update method test.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateBLUpdateMethodTest(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create business logic class.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceBL">The target namespace bl.</param>
        /// <param name="path">The path.</param>
        void CodeHammerCreateBusinessLogicClass(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceBL, string path);

        /// <summary>
        /// Codes the hammer create business logic interface.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceBL">The target namespace bl.</param>
        /// <param name="path">The path.</param>
        void CodeHammerCreateBusinessLogicInterface(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceBL, string path);

        /// <summary>
        /// Codes the hammer create business logic unit test.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="targetNamespaceBL">The target namespace bl.</param>
        /// <param name="path">The path.</param>
        void CodeHammerCreateBusinessLogicUnitTest(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string targetNamespaceBL, string path);

        /// <summary>
        /// Codes the hammer create ibl delete method.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        void CodeHammerCreateIBLDeleteMethod(CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create ibl delete method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateIBLDeleteMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create ibl insert method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateIBLInsertMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create ibl select all method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateIBLSelectAllMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create ibl select method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateIBLSelectMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer create ibl update method.
        /// </summary>
        /// <param name="codeHammerDALClassName">Name of the code hammer dal class.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerCreateIBLUpdateMethod(string codeHammerDALClassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);
    }
}