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
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// this class IOManager
    /// </summary>

    public class IOManager : CodeHammer.Framework.FunctionArea.FileIO.IOManagerContract
    {
        /// <summary>
        /// The log4 net SQL
        /// </summary>
        private StringBuilder log4NetSql = new StringBuilder();

        /// <summary>
        /// Creates the log4 net SQL.
        /// </summary>
        /// <returns></returns>
        public string CreateLog4NetSql()
        {
            string quates = @"""";
            log4NetSql.Length = 0;
            log4NetSql.Capacity = 0;

            log4NetSql.AppendLine("To install Log4Net");
            log4NetSql.AppendLine("1. Right click on the projects: Logging.");
            log4NetSql.AppendLine("2. Choose Manage NuGet packages.");
            log4NetSql.AppendLine("3. Paste Log4Net inside the search textfield.");
            log4NetSql.AppendLine("4. Press install.");
            log4NetSql.AppendLine("5. Press Close.");

            log4NetSql.AppendLine();
            log4NetSql.AppendLine("--------------------------------------------------------------");
            log4NetSql.AppendLine("Create table Log. Remember to modify tablename in Web.config and in the Log4NetSqlScript.sql");
            log4NetSql.AppendLine("commandText value=" + quates + "INSERT INTO <Tablename>" + quates);
            log4NetSql.AppendLine("Create table Log. Remember to modify tablename in Web.config");
            log4NetSql.AppendLine("Execute the " + @"file://Log4NetSqlScript.sql to create the log4Net Table");
            log4NetSql.AppendLine("--------------------------------------------------------------");
            return log4NetSql.ToString();
        }

        /// <summary>
        /// Gets the SQL PKFK.
        /// </summary>
        /// <value>
        /// The SQL PKFK.
        /// </value>
        public string SqlPKFK
        {
            get
            {
                return "SELECT OBJECT_NAME(f.parent_object_id) AS tablename ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS foreignkey ,OBJECT_NAME (f.referenced_object_id) AS RefTable FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id WHERE f.parent_object_id = OBJECT_ID('TABLENAME');";
            }
        }

        /// <summary>
        /// Gets the find fk from pk.
        /// </summary>
        /// <value>
        /// The find fk from pk.
        /// </value>
        public string FindFKFromPK
        {
            get
            {
                return "SELECT OBJECT_SCHEMA_NAME(f.OBJECT_ID) SchemaName, OBJECT_NAME(f.parent_object_id) TableName, COL_NAME(fc.parent_object_id,fc.parent_column_id) ColName FROM    sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id INNER JOIN sys.tables t ON t.OBJECT_ID = fc.referenced_object_id WHERE OBJECT_NAME (f.referenced_object_id) = 'TABLENAME'";
            }
        }

        /// <summary>
        /// Gets the name of the find foreign keys from table.
        /// </summary>
        /// <value>
        /// The name of the find foreign keys from table.
        /// </value>
        public string FindForeignKeysFromTableName
        {
            get
            {
                return "SELECT SCHEMA_NAME(f.SCHEMA_ID) as SchemaName, OBJECT_NAME(f.parent_object_id) AS TableName, COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnName, SCHEMA_NAME(o.SCHEMA_ID) as ReferenceSchemaName, OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName, COL_NAME(fc.referenced_object_id,fc.referenced_column_id) AS ReferenceColumnName FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id INNER JOIN sys.objects AS o ON o.OBJECT_ID = fc.referenced_object_id WHERE OBJECT_NAME(f.parent_object_id) = 'TABLENAME'";
            }
        }

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public string DbConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [no dependy on table].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no dependy on table]; otherwise, <c>false</c>.
        /// </value>
        public bool NoDependyOnTable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IOManager"/> is crud.
        /// </summary>
        /// <value>
        ///   <c>true</c> if crud; otherwise, <c>false</c>.
        /// </value>
        public bool Crud
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the select table.
        /// </summary>
        /// <value>
        /// The name of the select table.
        /// </value>
        public string SelectTableName
        {
            get;
            set;
        }

        /// <summary>
        /// get and set EmptyDataLayerCheckBox
        /// </summary>
        public bool EmptyDataLayerCheckBox 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the SQL prefix.
        /// </summary>
        /// <value>
        /// The SQL prefix.
        /// </value>
        public string SqlPrefix { get; set; }

        /// <summary>
        /// Gets or sets the bl prefix.
        /// </summary>
        /// <value>
        /// The bl prefix.
        /// </value>
        public string BLPrefix { get; set; }

        /// <summary>
        /// Gets or sets the dal prefix.
        /// </summary>
        /// <value>
        /// The dal prefix.
        /// </value>
        public string DalPrefix { get; set; }

        /// <summary>
        /// Gets or sets the WCF prefix.
        /// </summary>
        /// <value>
        /// The WCF prefix.
        /// </value>
        public string WcfPrefix { get; set; }

        /// <summary>
        /// Gets or sets the name space prefix.
        /// </summary>
        /// <value>
        /// The name space prefix.
        /// </value>
        public string NameSpacePrefix { get; set; }

        /// <summary>
        /// Gets or sets the code editor builder.
        /// </summary>
        /// <value>
        /// The code editor builder.
        /// </value>
        public StringBuilder CodeEditorBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// The dependency table builder
        /// </summary>
        /// <value>
        /// The dependency table builder.
        /// </value>
        public StringBuilder DependencyTableBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the instance call.
        /// </summary>
        /// <value>
        /// The instance call.
        /// </value>
        public string InstanceCall { get; set; }

        /// <summary>
        /// Gets the SQL table.
        /// </summary>
        /// <value>
        /// The SQL table.
        /// </value>
        public string SqlTable
        {
            get
            {
                return "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TABLENAME'";
            }
        }

        /// <summary>
        /// Gets the SQL pk.
        /// </summary>
        /// <value>
        /// The SQL pk.
        /// </value>
        public string SqlPK
        {
            get
            {
                return "SELECT ta.name,col.name ,col.is_identity  ,col.is_nullable from sys.tables ta inner join sys.indexes ind on ind.object_id = ta.object_id inner join sys.index_columns indcol on indcol.object_id = ta.object_id and indcol.index_id = ind.index_id inner join sys.columns col on col.object_id = ta.object_id and col.column_id = indcol.column_id where ind.is_primary_key = 1 and ta.name ='TABLENAME' order by ta.name,indcol.key_ordinal";
            }
        }

        /// <summary>
        /// Gets the SQL pk library.
        /// </summary>
        /// <value>
        /// The SQL pk library.
        /// </value>
        public string SqlPKLib
        {
            get
            {
                return "SELECT ta.name,col.name as pk ,col.is_identity  ,col.is_nullable from sys.tables ta inner join sys.indexes ind on ind.object_id = ta.object_id inner join sys.index_columns indcol on indcol.object_id = ta.object_id and indcol.index_id = ind.index_id inner join sys.columns col on col.object_id = ta.object_id and col.column_id = indcol.column_id where ind.is_primary_key = 1 and ta.name ='TABLENAME' order by ta.name,indcol.key_ordinal";
            }
        }

        /// <summary>
        /// Gets the i code hammer business logic folder.
        /// </summary>
        /// <value>
        /// The i code hammer business logic folder.
        /// </value>
        public string ICodeHammerBusinessLogicFolder
        {
            get
            {
                return @"\Business\IBusiness\";
            }
        }

        /// <summary>
        /// The code hammer stored procedure folder
        /// </summary>
        public string CodeHammerStoredProcedureFolder
        {
            get
            {
                return @"\Data\StoredProcedures\";
            }
        }

        /// <summary>
        /// The i code hammer repository folder
        /// </summary>
        public string ICodeHammerRepositoryFolder
        {
            get
            {
                return @"\Data\IData\";
            }
        }

        /// <summary>
        /// The visualstudio project data contract folder
        /// </summary>
        public string VisualstudioProjectDataContractFolder
        {
            get
            {
                return @"\ServiceLibrary\ServiceLib\DataContract\";
            }
        }

        /// <summary>
        /// The business
        /// </summary>
        public string Business
        {
            get
            {
                return @"Business\";
            }
        }

        /// <summary>
        /// The data
        /// </summary>
        public string Data
        {
            get
            {
                return @"Data\";
            }
        }

        /// <summary>
        /// The dto
        /// </summary>
        public string DtoDir
        {
            get
            {
                return @"\Domain\Domain\";
            }
        }

        /// <summary>
        /// The output error
        /// </summary>
        public string OutputError
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The visualstudio service library project folder
        /// </summary>
        public string VisualstudioServiceLibraryProjectFolder
        {
            get
            {
                return @"\ServiceLibrary\ServiceLib\";
            }
        }

        /// <summary>
        /// The visualstudio logging project folder
        /// </summary>
        public string VisualstudioLoggingProjectFolder
        {
            get
            {
                return @"\Logging\Log4Net\";
            }
        }

        /// <summary>
        /// The visualstudio solution test project folder
        /// </summary>
        public string VisualstudioSolutionTestProjectFolder
        {
            get
            {
                return @"\SolutionTest\";
            }
        }

        /// <summary>
        /// The visualstudio solution business test project folder
        /// </summary>
        public string VisualstudioSolutionBusinessTestProjectFolder
        {
            get
            {
                return @"\SolutionTest\BusinessTests\";
            }
        }

        /// <summary>
        /// The visualstudio solution data repository test project folder
        /// </summary>
        public string VisualstudioSolutionDataRepositoryTestProjectFolder
        {
            get
            {
                return @"\SolutionTest\DataTests\";
            }
        }

        /// <summary>
        /// The visualstudio solution service library test project folder
        /// </summary>
        public string VisualstudioSolutionServiceLibraryTestProjectFolder
        {
            get
            {
                return @"\SolutionTest\ServiceLibraryTests\";
            }
        }

        /// <summary>
        /// The code hammer business logic folder
        /// </summary>
        public string CodeHammerBusinessLogicFolder
        {
            get
            {
                return @"\Business\Business\";
            }
        }

        /// <summary>
        /// The code hammer repository folder
        /// </summary>
        public string CodeHammerRepositoryFolder
        {
            get
            {
                return @"\Data\Data\";
            }
        }

        /// <summary>
        /// The code hammer repository database management folder
        /// </summary>
        public string CodeHammerRepositoryDatabaseManagementFolder
        {
            get
            {
                return @"\Data\Infrastructure\";
            }
        }

        /// <summary>
        /// The code hammer repository database management interface folder
        /// </summary>
        public string CodeHammerRepositoryDatabaseManagementInterfaceFolder
        {
            get
            {
                return @"\Data\IDatabaseManagement\";
            }
        }

         

        /// <summary>
        /// The dto folder
        /// </summary>
        public string DtoFolder
        {
            get
            {
                return @"\Domain\Domain\";
            }
        }

        /// <summary>
        /// The code hammer project name
        /// </summary>
        public string CodeHammerProjectNameZip
        {
            get
            {
                return "CodeHammerServiceProject.zip";
            }
        }

        /// <summary>
        /// The code hammer project name
        /// </summary>
        public string CodeHammerProjectName
        {
            get
            {
                return @"\";
            }
        }

        /// <summary>
        /// The code hammer project
        /// </summary>
        public string CodeHammerProject2012
        {
            get
            {
                return "CodeHammer.Framework.Resources.CodeHammerServiceWcfProject.CodeHammerServiceProject.zip";
            }
        }

        /// <summary>
        /// The code hammer logging log4 net
        /// </summary>
        public string CodeHammerLoggingLog4Net
        {
            get
            {
                return @"\Logging\Log4Net";
            }
        }

        /// <summary>
        /// The codeHammer logging
        /// </summary>
        public string CodeHammerLogging
        {
            get
            {
                return @"\Logging\Logging.csproj";
            }
        }

        /// <summary>
        /// The code hammer solution test
        /// </summary>
        public string CodeHammerSolutionTest
        {
            get
            {
                return @"\SolutionTest\SolutionTest.csproj";
            }
        }

        /// <summary>
        /// The code hammer business logic
        /// </summary>
        public string CodeHammerBusinessLogic
        {
            get
            {
                return @"\Business\Business.csproj";
            }
        }

        /// <summary>
        /// The code hammer repository
        /// </summary>
        public string CodeHammerRepository
        {
            get
            {
                return @"\Data\Data.csproj";
            }
        }

        /// <summary>
        /// The dto
        /// </summary>
        public string Domain
        {
            get
            {
                return @"\Domain\Domain.csproj";
            }
        }

        /// <summary>
        /// The code hammer repository mapping folder
        /// </summary>
        public string CodeHammerRepositoryMappingFolder
        {
            get
            {
                return @"\Data\Mapping\";
            }
        }

        /// <summary>
        /// The code hammer repository helper folder
        /// </summary>
        public string CodeHammerRepositoryHelperFolder
        {
            get
            {
                return @"\Data\Helper\";
            }
        }

        /// <summary>
        /// The SVC template
        /// </summary>
        public string SvcTemplate
        {
            get
            {
                return "CodeHammer.Resources.WcfService.SvcTemplate.svc";
            }
        }

        /// <summary>
        /// The web configuration template
        /// </summary>
        public string WebConfigTemplate
        {
            get
            {
                return "CodeHammer.Resources.WcfService.WebConfig.config";
            }
        }

        /// <summary>
        /// The visualstudio service host project
        /// </summary>
        public string VisualstudioServiceHostProject
        {
            get
            {
                return @"\ServiceHost\ServiceHost.csproj";
            }
        }

        /// <summary>
        /// The visualstudio service host project name
        /// </summary>
        public string VisualstudioServiceHostProjectName
        {
            get
            {
                return @"\ServiceHost\";
            }
        }

        /// <summary>
        /// Gets the visualstudio service host services.
        /// </summary>
        /// <value>
        /// The visualstudio service host services.
        /// </value>
        public string VisualstudioServiceHostServices
        {
            get
            {
                return @"\ServiceHost\Services\";
            }
        }

        /// <summary>
        /// Gets the visualstudio service host application start.
        /// </summary>
        /// <value>
        /// The visualstudio service host application start.
        /// </value>
        public string VisualstudioServiceHostAppStart
        {
            get
            {
                return @"\ServiceHost\App_Start\";
            }
        }

        /// <summary>
        /// The data contract template
        /// </summary>
        public string DataContractTemplate
        {
            get
            {
                return "CodeHammer.Resources.WcfService.DataContract.cs";
            }
        }

        /// <summary>
        /// The service contract template
        /// </summary>
        public string ServiceContractTemplate
        {
            get
            {
                return "CodeHammer.Resources.WcfService.ICodeHammerService.cs";
            }
        }

        /// <summary>
        /// Gets the route configuration template.
        /// </summary>
        /// <value>
        /// The route configuration template.
        /// </value>
        public string RouteConfigTemplate
        {
            get
            {
                return "CodeHammer.Resources.WcfService.RouteConfig.cs";
            }
        }

        /// <summary>
        /// The service template
        /// </summary>
        public string ServiceTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceTemplate"];
            }
        }

        /// <summary>
        /// The name space data contract
        /// </summary>
        public string NameSpaceDataContract
        {
            get
            {
                return "ServiceLibrary.DataContract";
            }
        }

        /// <summary>
        /// The namespace data service
        /// </summary>
        public string NamespaceDataService
        {
            get
            {
                return "ServiceLibrary.Service";
            }
        }

        /// <summary>
        /// The namespace service contract
        /// </summary>
        public string NamespaceServiceContract
        {
            get
            {
                return "ServiceLibrary.ServiceContract";
            }
        }

        /// <summary>
        /// The data contract folder
        /// </summary>
        public string DataContractFolder
        {
            get
            {
                return @"DataContract\";
            }
        }

        /// <summary>
        /// The service folder
        /// </summary>
        public string ServiceFolder
        {
            get
            {
                return @"Service\";
            }
        }

        /// <summary>
        /// The service contract folder
        /// </summary>
        public string ServiceContractFolder
        {
            get
            {
                return @"ServiceContract\";
            }
        }

        /// <summary>
        /// Gets the log path.
        /// </summary>
        /// <value>
        /// The log path.
        /// </value>
        public string LogPath
        {
            get
            {
                return Application.StartupPath + "\\CodeHammerLog.txt";
            }
        }

        /// <summary>
        /// The visualstudio project
        /// </summary>
        public string VisualstudioServiceLibraryProject
        {
            get
            {
                return @"\ServiceLibrary\ServiceLibrary.csproj";
            }
        }

        /// <summary>
        /// The vs project data contract folder
        /// </summary>
        public string VsProjectDataContractFolder
        {
            get
            {
                return @"\ServiceLibrary\ServiceLib\DataContract\";
            }
        }

        /// <summary>
        /// Gets or sets the database get schema.
        /// </summary>
        /// <value>
        /// The database get schema.
        /// </value>
        public string DbGetSchema
        {
            get;
            set;
        }

        /// <summary>
        /// The vs project data service folder
        /// </summary>
        public string VsProjectDataServiceFolder
        {
            get
            {
                return @"\ServiceLib\Service\";
            }
        }

        /// <summary>
        /// The vs project service contract folder
        /// </summary>
        public string VsProjectServiceContractFolder
        {
            get
            {
                return @"\ServiceLib\ServiceContract\";
            }
        }

        /// <summary>
        /// The use io c
        /// </summary>
        public bool UseIoC
        {
            get;
            set;
        }

        /// <summary>
        /// The use orm
        /// </summary>
        public bool UseOrm
        {
            get;
            set;
        }

        /// <summary>
        /// The log builder
        /// </summary>
        public StringBuilder LogBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the host folder.
        /// </summary>
        /// <value>
        /// The host folder.
        /// </value>
        public string HostFolder
        {
            get;
            set;
        }

        /// <summary>
        /// The unit test builder
        /// </summary>
        public StringBuilder UnitTestBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// The vs unit test builder
        /// </summary>
        public StringBuilder VSUnitTestBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the pk keys.
        /// </summary>
        /// <value>
        /// The pk keys.
        /// </value>
        public List<CodeHammerPkClass> PkKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the oracle tables.
        /// </summary>
        /// <value>
        /// The oracle tables.
        /// </value>
        public List<string> OracleTables
        {
            get;
            set;
        }

        /// <summary>
        /// The pk keys relations
        /// </summary>
        public List<CodeHammerPkClass> PkKeysRelations = new List<CodeHammerPkClass>();

        /// <summary>
        /// The pk keys hold relations
        /// </summary>
        /// <value>
        /// The pk keys hold relations.
        /// </value>
        public List<CodeHammerPkClass> PkKeysHoldRelations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the next table.
        /// </summary>
        /// <value>
        /// The name of the next table.
        /// </value>
        public string NextTableName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the get process identifier.
        /// </summary>
        /// <value>
        /// The get process identifier.
        /// </value>
        public int? GetProcessID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log4 net].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [log4 net]; otherwise, <c>false</c>.
        /// </value>
        public bool Log4Net
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [WCF performance].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [WCF performance]; otherwise, <c>false</c>.
        /// </value>
        public bool WcfPerformance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [unit test].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [unit test]; otherwise, <c>false</c>.
        /// </value>
        public bool UnitTest
        {
            get;
            set;
        }

        /// <summary>
        /// this enum StreamFormat
        /// </summary>
        public enum StreamFormat
        {
            None,
            Json,
            Xml
        }

        /// <summary>
        /// this enum WcfSecurity
        /// </summary>
        public enum WcfSecurity
        {
            None,
            CertificateAuthentication
        }

        /// <summary>
        /// Gets or sets the WCF security enum.
        /// </summary>
        /// <value>
        /// The WCF security enum.
        /// </value>
        public WcfSecurity WcfSecurityEnum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the stream format type enum.
        /// </summary>
        /// <value>
        /// The stream format type enum.
        /// </value>
        public StreamFormat StreamFormatTypeEnum
        {
            get;
            set;
        }

        /// <summary>
        /// this enum UnitTestEnum
        /// </summary>
        public enum UnitTestEnum
        {
            NUnit,
            VSUnitTest
        }

        /// <summary>
        /// Gets or sets the unit test type enum.
        /// </summary>
        /// <value>
        /// The unit test type enum.
        /// </value>
        public UnitTestEnum UnitTestTypeEnum
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>return true if success</returns>
        public bool CheckIfFileExists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the get SQL client.
        /// </summary>
        /// <value>
        /// The get SQL client.
        /// </value>
        public string GetSqlClient
        {
            get
            {
                return "System.Data.SqlClient";
            }
        }

        /// <summary>
        /// Gets or sets the database provider.
        /// </summary>
        /// <value>
        /// The database provider.
        /// </value>
        public string DBProvider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the get oracle client.
        /// </summary>
        /// <value>
        /// The get oracle client.
        /// </value>
        public string GetOracleClient
        {
            get
            {
                return "Oracle.ManagedDataAccess.Client";
            }
        }

        /// <summary>
        /// Gets the get ODBC client.
        /// </summary>
        /// <value>
        /// The get ODBC client.
        /// </value>
        public string GetOdbcClient
        {
            get
            {
                return "System.Data.Odbc";
            }
        }

        /// <summary>
        /// Gets the get OLE database client.
        /// </summary>
        /// <value>
        /// The get OLE database client.
        /// </value>
        public string GetOleDbClient
        {
            get
            {
                return "System.Data.OleDb";
            }
        }
    }
}