/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.FileIO
{
    using CodeHammer.Entities;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// this interface IOManagerContract
    /// </summary>

    public interface IOManagerContract
    {
        /// <summary>
        /// Gets or sets the bl prefix.
        /// </summary>
        /// <value>
        /// The bl prefix.
        /// </value>
        string BLPrefix { get; set; }

        /// <summary>
        /// Gets or sets the EmptyDataLayerCheckBox.
        /// </summary>
        /// <value>
        /// The EmptyDataLayerCheckBox.
        /// </value>
        bool EmptyDataLayerCheckBox { get; set; }

        /// <summary>
        /// Gets the route configuration template.
        /// </summary>
        /// <value>
        /// The route configuration template.
        /// </value>
        string RouteConfigTemplate { get; }

        /// <summary>
        /// Gets the visualstudio service host application start.
        /// </summary>
        /// <value>
        /// The visualstudio service host application start.
        /// </value>
        string VisualstudioServiceHostAppStart { get; }

        /// <summary>
        /// Gets or sets the host folder.
        /// </summary>
        /// <value>
        /// The host folder.
        /// </value>
        string HostFolder { get; set; }

        /// <summary>
        /// Gets or sets the oracle tables.
        /// </summary>
        /// <value>
        /// The oracle tables.
        /// </value>
        List<string> OracleTables { get; set; }

        /// <summary>
        /// Gets or sets the database get schema.
        /// </summary>
        /// <value>
        /// The database get schema.
        /// </value>
        string DbGetSchema { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use orm].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use orm]; otherwise, <c>false</c>.
        /// </value>
        bool UseOrm { get; set; }

        /// <summary>
        /// Gets or sets the unit test builder.
        /// </summary>
        /// <value>
        /// The unit test builder.
        /// </value>
        StringBuilder UnitTestBuilder { get; set; }

        /// <summary>
        /// Gets or sets the vs unit test builder.
        /// </summary>
        /// <value>
        /// The vs unit test builder.
        /// </value>
        StringBuilder VSUnitTestBuilder { get; set; }

        /// <summary>
        /// Gets or sets the database provider.
        /// </summary>
        /// <value>
        /// The database provider.
        /// </value>
        string DBProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use io c].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use io c]; otherwise, <c>false</c>.
        /// </value>
        bool UseIoC { get; set; }

        /// <summary>
        /// Gets the business.
        /// </summary>
        /// <value>
        /// The business.
        /// </value>
        string Business { get; }

        /// <summary>
        /// Gets or sets the log builder.
        /// </summary>
        /// <value>
        /// The log builder.
        /// </value>
        StringBuilder LogBuilder { get; set; }

        /// <summary>
        /// Gets or sets the code editor builder.
        /// </summary>
        /// <value>
        /// The code editor builder.
        /// </value>
        StringBuilder CodeEditorBuilder { get; set; }

        /// <summary>
        /// Gets or sets the dependency table builder.
        /// </summary>
        /// <value>
        /// The dependency table builder.
        /// </value>
        StringBuilder DependencyTableBuilder { get; set; }

        /// <summary>
        /// Gets the log path.
        /// </summary>
        /// <value>
        /// The log path.
        /// </value>
        string LogPath { get; }

        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool CheckIfFileExists(string path);

        /// <summary>
        /// Gets the code hammer business logic.
        /// </summary>
        /// <value>
        /// The code hammer business logic.
        /// </value>
        string CodeHammerBusinessLogic { get; }

        /// <summary>
        /// Gets the code hammer business logic folder.
        /// </summary>
        /// <value>
        /// The code hammer business logic folder.
        /// </value>
        string CodeHammerBusinessLogicFolder { get; }

        /// <summary>
        /// Gets the code hammer logging.
        /// </summary>
        /// <value>
        /// The code hammer logging.
        /// </value>
        string CodeHammerLogging { get; }

        /// <summary>
        /// Gets the code hammer logging log4 net.
        /// </summary>
        /// <value>
        /// The code hammer logging log4 net.
        /// </value>
        string CodeHammerLoggingLog4Net { get; }

        /// <summary>
        /// Gets the code hammer project2012.
        /// </summary>
        /// <value>
        /// The code hammer project2012.
        /// </value>
        string CodeHammerProject2012 { get; }

        /// <summary>
        /// Gets the name of the code hammer project.
        /// </summary>
        /// <value>
        /// The name of the code hammer project.
        /// </value>
        string CodeHammerProjectName { get; }

        /// <summary>
        /// Gets the code hammer project name zip.
        /// </summary>
        /// <value>
        /// The code hammer project name zip.
        /// </value>
        string CodeHammerProjectNameZip { get; }

        /// <summary>
        /// Gets the code hammer repository.
        /// </summary>
        /// <value>
        /// The code hammer repository.
        /// </value>
        string CodeHammerRepository { get; }

        /// <summary>
        /// Gets the code hammer repository database management folder.
        /// </summary>
        /// <value>
        /// The code hammer repository database management folder.
        /// </value>
        string CodeHammerRepositoryDatabaseManagementFolder { get; }

        /// <summary>
        /// Gets the code hammer repository folder.
        /// </summary>
        /// <value>
        /// The code hammer repository folder.
        /// </value>
        string CodeHammerRepositoryFolder { get; }

        /// <summary>
        /// Gets the code hammer repository helper folder.
        /// </summary>
        /// <value>
        /// The code hammer repository helper folder.
        /// </value>
        string CodeHammerRepositoryHelperFolder { get; }

        /// <summary>
        /// Gets the code hammer repository mapping folder.
        /// </summary>
        /// <value>
        /// The code hammer repository mapping folder.
        /// </value>
        string CodeHammerRepositoryMappingFolder { get; }

        /// <summary>
        /// Gets the code hammer solution test.
        /// </summary>
        /// <value>
        /// The code hammer solution test.
        /// </value>
        string CodeHammerSolutionTest { get; }

        /// <summary>
        /// Gets the code hammer stored procedure folder.
        /// </summary>
        /// <value>
        /// The code hammer stored procedure folder.
        /// </value>
        string CodeHammerStoredProcedureFolder { get; }

        /// <summary>
        /// Creates the log4 net SQL.
        /// </summary>
        /// <returns></returns>
        string CreateLog4NetSql();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IOManagerContract"/> is crud.
        /// </summary>
        /// <value>
        ///   <c>true</c> if crud; otherwise, <c>false</c>.
        /// </value>
        bool Crud { get; set; }

        /// <summary>
        /// Gets or sets the dal prefix.
        /// </summary>
        /// <value>
        /// The dal prefix.
        /// </value>
        string DalPrefix { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        string Data { get; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        string DatabaseName { get; set; }

        /// <summary>
        /// Gets the data contract folder.
        /// </summary>
        /// <value>
        /// The data contract folder.
        /// </value>
        string DataContractFolder { get; }

        /// <summary>
        /// Gets the data contract template.
        /// </summary>
        /// <value>
        /// The data contract template.
        /// </value>
        string DataContractTemplate { get; }

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        string DbConnection { get; set; }

        /// <summary>
        /// Gets the dto.
        /// </summary>
        /// <value>
        /// The dto.
        /// </value>
        string Domain { get; }

        /// <summary>
        /// Gets the dto dir.
        /// </summary>
        /// <value>
        /// The dto dir.
        /// </value>
        string DtoDir { get; }

        /// <summary>
        /// Gets the dto folder.
        /// </summary>
        /// <value>
        /// The dto folder.
        /// </value>
        string DtoFolder { get; }

        /// <summary>
        /// Gets the find fk from pk.
        /// </summary>
        /// <value>
        /// The find fk from pk.
        /// </value>
        string FindFKFromPK { get; }

        /// <summary>
        /// Gets the name of the find foreign keys from table.
        /// </summary>
        /// <value>
        /// The name of the find foreign keys from table.
        /// </value>
        string FindForeignKeysFromTableName { get; }

        /// <summary>
        /// Gets or sets the get process identifier.
        /// </summary>
        /// <value>
        /// The get process identifier.
        /// </value>
        int? GetProcessID { get; set; }

        /// <summary>
        /// Gets the i code hammer business logic folder.
        /// </summary>
        /// <value>
        /// The i code hammer business logic folder.
        /// </value>
        string ICodeHammerBusinessLogicFolder { get; }

        /// <summary>
        /// Gets the i code hammer repository folder.
        /// </summary>
        /// <value>
        /// The i code hammer repository folder.
        /// </value>
        string ICodeHammerRepositoryFolder { get; }

        /// <summary>
        /// Gets or sets the instance call.
        /// </summary>
        /// <value>
        /// The instance call.
        /// </value>
        string InstanceCall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [log4 net].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [log4 net]; otherwise, <c>false</c>.
        /// </value>
        bool Log4Net { get; set; }

        /// <summary>
        /// Gets the name space data contract.
        /// </summary>
        /// <value>
        /// The name space data contract.
        /// </value>
        string NameSpaceDataContract { get; }

        /// <summary>
        /// Gets the namespace data service.
        /// </summary>
        /// <value>
        /// The namespace data service.
        /// </value>
        string NamespaceDataService { get; }

        /// <summary>
        /// Gets or sets the name space prefix.
        /// </summary>
        /// <value>
        /// The name space prefix.
        /// </value>
        string NameSpacePrefix { get; set; }

        /// <summary>
        /// Gets the namespace service contract.
        /// </summary>
        /// <value>
        /// The namespace service contract.
        /// </value>
        string NamespaceServiceContract { get; }

        /// <summary>
        /// Gets or sets the name of the next table.
        /// </summary>
        /// <value>
        /// The name of the next table.
        /// </value>
        string NextTableName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no dependy on table].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no dependy on table]; otherwise, <c>false</c>.
        /// </value>
        bool NoDependyOnTable { get; set; }

        /// <summary>
        /// Gets the output error.
        /// </summary>
        /// <value>
        /// The output error.
        /// </value>
        string OutputError { get; }

        /// <summary>
        /// Gets or sets the name of the select table.
        /// </summary>
        /// <value>
        /// The name of the select table.
        /// </value>
        string SelectTableName { get; set; }

        /// <summary>
        /// Gets the service contract folder.
        /// </summary>
        /// <value>
        /// The service contract folder.
        /// </value>
        string ServiceContractFolder { get; }

        /// <summary>
        /// Gets the service contract template.
        /// </summary>
        /// <value>
        /// The service contract template.
        /// </value>
        string ServiceContractTemplate { get; }

        /// <summary>
        /// Gets the service folder.
        /// </summary>
        /// <value>
        /// The service folder.
        /// </value>
        string ServiceFolder { get; }

        /// <summary>
        /// Gets the service template.
        /// </summary>
        /// <value>
        /// The service template.
        /// </value>
        string ServiceTemplate { get; }

        /// <summary>
        /// Gets the SQL pk.
        /// </summary>
        /// <value>
        /// The SQL pk.
        /// </value>
        string SqlPK { get; }

        /// <summary>
        /// Gets the SQL PKFK.
        /// </summary>
        /// <value>
        /// The SQL PKFK.
        /// </value>
        string SqlPKFK { get; }

        /// <summary>
        /// Gets the SQL pk library.
        /// </summary>
        /// <value>
        /// The SQL pk library.
        /// </value>
        string SqlPKLib { get; }

        /// <summary>
        /// Gets or sets the SQL prefix.
        /// </summary>
        /// <value>
        /// The SQL prefix.
        /// </value>
        string SqlPrefix { get; set; }

        /// <summary>
        /// Gets the SQL table.
        /// </summary>
        /// <value>
        /// The SQL table.
        /// </value>
        string SqlTable { get; }

        /// <summary>
        /// Gets or sets the stream format type enum.
        /// </summary>
        /// <value>
        /// The stream format type enum.
        /// </value>
        IOManager.StreamFormat StreamFormatTypeEnum { get; set; }

        /// <summary>
        /// Gets the SVC template.
        /// </summary>
        /// <value>
        /// The SVC template.
        /// </value>
        string SvcTemplate { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [unit test].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [unit test]; otherwise, <c>false</c>.
        /// </value>
        bool UnitTest { get; set; }

        /// <summary>
        /// Gets or sets the unit test type enum.
        /// </summary>
        /// <value>
        /// The unit test type enum.
        /// </value>
        IOManager.UnitTestEnum UnitTestTypeEnum { get; set; }

        /// <summary>
        /// Gets the visualstudio logging project folder.
        /// </summary>
        /// <value>
        /// The visualstudio logging project folder.
        /// </value>
        string VisualstudioLoggingProjectFolder { get; }

        /// <summary>
        /// Gets the visualstudio project data contract folder.
        /// </summary>
        /// <value>
        /// The visualstudio project data contract folder.
        /// </value>
        string VisualstudioProjectDataContractFolder { get; }

        /// <summary>
        /// Gets the visualstudio service host services.
        /// </summary>
        /// <value>
        /// The visualstudio service host services.
        /// </value>
        string VisualstudioServiceHostServices { get; }

        /// <summary>
        /// Gets the visualstudio service host project.
        /// </summary>
        /// <value>
        /// The visualstudio service host project.
        /// </value>
        string VisualstudioServiceHostProject { get; }

        /// <summary>
        /// Gets the name of the visualstudio service host project.
        /// </summary>
        /// <value>
        /// The name of the visualstudio service host project.
        /// </value>
        string VisualstudioServiceHostProjectName { get; }

        /// <summary>
        /// Gets the visualstudio service library project.
        /// </summary>
        /// <value>
        /// The visualstudio service library project.
        /// </value>
        string VisualstudioServiceLibraryProject { get; }

        /// <summary>
        /// Gets the visualstudio service library project folder.
        /// </summary>
        /// <value>
        /// The visualstudio service library project folder.
        /// </value>
        string VisualstudioServiceLibraryProjectFolder { get; }

        /// <summary>
        /// Gets the visualstudio solution business test project folder.
        /// </summary>
        /// <value>
        /// The visualstudio solution business test project folder.
        /// </value>
        string VisualstudioSolutionBusinessTestProjectFolder { get; }

        /// <summary>
        /// Gets the visualstudio solution data repository test project folder.
        /// </summary>
        /// <value>
        /// The visualstudio solution data repository test project folder.
        /// </value>
        string VisualstudioSolutionDataRepositoryTestProjectFolder { get; }

        /// <summary>
        /// Gets the visualstudio solution service library test project folder.
        /// </summary>
        /// <value>
        /// The visualstudio solution service library test project folder.
        /// </value>
        string VisualstudioSolutionServiceLibraryTestProjectFolder { get; }

        /// <summary>
        /// Gets the visualstudio solution test project folder.
        /// </summary>
        /// <value>
        /// The visualstudio solution test project folder.
        /// </value>
        string VisualstudioSolutionTestProjectFolder { get; }

        /// <summary>
        /// Gets the vs project data contract folder.
        /// </summary>
        /// <value>
        /// The vs project data contract folder.
        /// </value>
        string VsProjectDataContractFolder { get; }

        /// <summary>
        /// Gets the vs project data service folder.
        /// </summary>
        /// <value>
        /// The vs project data service folder.
        /// </value>
        string VsProjectDataServiceFolder { get; }

        /// <summary>
        /// Gets the vs project service contract folder.
        /// </summary>
        /// <value>
        /// The vs project service contract folder.
        /// </value>
        string VsProjectServiceContractFolder { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [WCF performance].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [WCF performance]; otherwise, <c>false</c>.
        /// </value>
        bool WcfPerformance { get; set; }

        /// <summary>
        /// Gets or sets the WCF prefix.
        /// </summary>
        /// <value>
        /// The WCF prefix.
        /// </value>
        string WcfPrefix { get; set; }

        /// <summary>
        /// Gets or sets the WCF security enum.
        /// </summary>
        /// <value>
        /// The WCF security enum.
        /// </value>
        IOManager.WcfSecurity WcfSecurityEnum { get; set; }

        /// <summary>
        /// Gets or sets the pk keys.
        /// </summary>
        /// <value>
        /// The pk keys.
        /// </value>
        List<CodeHammerPkClass> PkKeys { get; set; }

        /// <summary>
        /// Gets or sets the pk keys hold relations.
        /// </summary>
        /// <value>
        /// The pk keys hold relations.
        /// </value>
        List<CodeHammerPkClass> PkKeysHoldRelations { get; set; }

        /// <summary>
        /// Gets the web configuration template.
        /// </summary>
        /// <value>
        /// The web configuration template.
        /// </value>
        string WebConfigTemplate { get; }

        /// <summary>
        /// Gets the get SQL client.
        /// </summary>
        /// <value>
        /// The get SQL client.
        /// </value>
        string GetSqlClient { get; }

        /// <summary>
        /// Gets the get oracle client.
        /// </summary>
        /// <value>
        /// The get oracle client.
        /// </value>
        string GetOracleClient { get; }

        /// <summary>
        /// Gets the get ODBC client.
        /// </summary>
        /// <value>
        /// The get ODBC client.
        /// </value>
        string GetOdbcClient { get; }

        /// <summary>
        /// Gets the get OLE database client.
        /// </summary>
        /// <value>
        /// The get OLE database client.
        /// </value>
        string GetOleDbClient { get; }
    }
}