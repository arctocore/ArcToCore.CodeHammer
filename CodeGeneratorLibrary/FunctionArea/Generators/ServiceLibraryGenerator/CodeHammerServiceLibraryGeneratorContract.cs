/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator
{
    /// <summary>
    /// this interface CodeHammerServiceLibraryGeneratorContract
    /// </summary>

    public interface CodeHammerServiceLibraryGeneratorContract
    {
        /// <summary>
        /// Codes the hammer i service create delete method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerIServiceCreateDeleteMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer i service create insert method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerIServiceCreateInsertMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer i service create select all method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerIServiceCreateSelectAllMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer i service create select by identifier.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerIServiceCreateSelectByID(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer i service create update method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerIServiceCreateUpdateMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service contract library.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceContractLibrary(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string path);

        /// <summary>
        /// Codes the hammer service create delete method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateDeleteMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create delete method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateDeleteMethodTest(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create insert method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateInsertMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create insert method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateInsertMethodTest(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create select all method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateSelectAllMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create select all method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateSelectAllMethodTest(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create select by identifier method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateSelectByIDMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create select by identifier test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateSelectByIDTest(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create update method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateUpdateMethod(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service create update method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceCreateUpdateMethodTest(string IblclassName, CodeHammer.Entities.CodeHammerTableDto table, System.IO.StreamWriter streamWriter);

        /// <summary>
        /// Codes the hammer service library unit test.
        /// </summary>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeHammerServiceLibraryUnitTest(bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string path);

        /// <summary>
        /// Services the library.
        /// </summary>
        /// <param name="tableList">The table list.</param>
        /// <param name="container">The container.</param>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="table">The table.</param>
        /// <param name="crudYesNo">if set to <c>true</c> [crud yes no].</param>
        /// <param name="resultDataOptions">The result data options.</param>
        /// <param name="path">The path.</param>
        /// <param name="instanceCall">The instance call.</param>
        /// <returns>if sucess then return true</returns>
        bool ServiceLibrary(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerTableDto> tableList, string container, bool selectTables, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>> tableDic, string databaseName, CodeHammer.Entities.CodeHammerTableDto table, bool crudYesNo, System.Collections.Generic.List<string> resultDataOptions, string path, string instanceCall);
    }
}