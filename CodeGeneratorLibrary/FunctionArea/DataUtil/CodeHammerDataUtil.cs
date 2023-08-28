/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.DataTypeManager
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;

    /// <summary>
    /// this class CodeHammerDataUtil
    /// </summary>

    public class CodeHammerDataUtil : CodeHammer.Framework.FunctionArea.DataUtil.CodeHammerDataUtilContract
    {
        #region Variables

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The automatic execute sp to database
        /// </summary>
        public bool autoExecuteSpToDb = false;

        ////private  readonly char[] whitespace = new char[] { ' ', '\n', '    ', '\r', '\f', '\v' };
        /// <summary>
        /// The connection string database
        /// </summary>
        public string connectionStringDB
        {
            get;
            set;
        }

        /// <summary>
        /// The make help page
        /// </summary>
        public StringBuilder MakeHelpPage
        {
            get;
            set;
        }

        /// <summary>
        /// The progress value
        /// </summary>
        public int progressValue = 0;

        /// <summary>
        /// The whitespace
        /// </summary>
        private readonly char[] whitespace = new char[] { '\t' };

        public string JavascriptsystemMessagetring(string message)
        {
            string quates = @"""";
            return "<script type=" + quates + "text/javascript" + quates + ">alert('" + message + "');</script>";
        }

        /// <summary>
        /// Normalizes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>return true if success</returns>
        public string NormalizeString(string source)
        {
            return String.Join(" ", source.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
        }

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerDataUtil"/> class.
        /// </summary>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        public CodeHammerDataUtil(IOManagerContract ioManagerContract,
            FuncTypeFactoryContract funcTypeFactoryContract,
            LogFuncContract logFuncContract)
        {
            this.logFuncContract = logFuncContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.ioManagerContract = ioManagerContract;

            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
        }

        #region Reserved for CodeDom

        /// <summary>
        /// Firsts the letter to lower case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>if sucess then return true</returns>
        public IEnumerable<char> FirstLetterToLowerCase(string value)
        {
            var firstChar = (byte)value.First();
            return string.Format("{0}{1}", (char)(firstChar + 32), value.Substring(1));
        }

        /// <summary>
        /// Firsts the letter uppercase first.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>if sucess then return true</returns>
        public string FirstLetterUppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Gets the type of the color.
        /// </summary>
        /// <param name="sqlType">Type of the SQL.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">sqlType</exception>
        public Type GetClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal);

                case SqlDbType.Float:
                    return typeof(double);

                case SqlDbType.Int:
                    return typeof(int);

                case SqlDbType.Real:
                    return typeof(float);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);

                case SqlDbType.SmallInt:
                    return typeof(short);

                case SqlDbType.TinyInt:
                    return typeof(byte);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }

        #endregion Reserved for CodeDom

        #region Directory Section

        /// <summary>
        /// Clears the folder.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        public void ClearFolder(string FolderName)
        {
            try
            {
                Directory.Delete(FolderName, true);
            }
            catch (Exception ex)
            {
            }
        }

        public void ClearFolderAndCreateNew(string FolderName)
        {
            try
            {
                Directory.Delete(FolderName, true);
            }
            catch (Exception ex)
            {
            }

            try
            {
                Thread.Sleep(2000);
                Directory.CreateDirectory(FolderName);
            }
            catch (Exception ex)
            {
            }


        }

        

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        public void DeleteProject(string FolderName)
        {
            try
            {
                Directory.Delete(FolderName, true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Creates the specified sub-directory, if it doesn't exist.
        /// </summary>
        /// <param name="name">The name of the sub-directory to be created.</param>
        /// <param name="deleteIfExists">Indicates if the directory should be deleted if it exists.</param>
        public void CodeHammerCreateSubDirectory(string name, bool deleteIfExists)
        {
            try
            {
                Directory.Delete(name, true);
            }
            catch (Exception ex)
            {
            }

            try
            {
                Thread.Sleep(2000);
                Directory.CreateDirectory(name);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Creates the data folder.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateDataFolder(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Finds the solution.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="sln">The SLN.</param>
        public void FindSolution(string path, out string sln)
        {
            sln = string.Empty;
            try
            {
                sln = string.Empty;
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (var fi in di.GetFiles())
                {
                    sln = fi.Name;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Zips the specified zip path.
        /// </summary>
        /// <param name="zipPath">The zip path.</param>
        /// <param name="extractPath">The extract path.</param>
        public void Zip(string zipPath, string extractPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion Directory Section

        #region Get StreamRessource Section

        /// <summary>
        /// Codes the hammer get nullable types resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="newtableName">Name of the newtable.</param>
        /// <returns>
        /// The value of the specified manifest resource, with all instances of oldValue replaced with newValue
        /// </returns>
        public string CodeHammerGetNullableTypesResource(string name, string tableName, string newtableName)
        {
            try
            {
                string returnValue = CodeHammerGetResource(name);
                return returnValue.Replace(tableName, newtableName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <returns>
        /// The value of the specified manifest resource.
        /// </returns>
        public string CodeHammerGetResource(string name)
        {
            //name = string.Empty;
            try
            {
                using (StreamReader streamReader = new StreamReader(CodeHammerGetResourceAsStream(name)))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <param name="oldValue">A string to be replaced.</param>
        /// <param name="newValue">A string to replace all occurrences of oldValue.</param>
        /// <returns>
        /// The value of the specified manifest resource, with all instances of oldValue replaced with newValue.
        /// </returns>
        public string CodeHammerGetResource(string name, string oldValue, string newValue)
        {
            try
            {
                string returnValue = CodeHammerGetResource(name);
                return returnValue.Replace(oldValue, newValue);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <returns>
        /// A stream that contains the resource.
        /// </returns>
        public Stream CodeHammerGetResourceAsStream(string name)
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            try
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Get StreamRessource Section

        #region Database Resource

        /// <summary>
        /// Gets or sets the data table names.
        /// </summary>
        /// <value>
        /// The data table names.
        /// </value>
        public DataSet DataTableNames
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of codeHammerColumns for the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>
        /// The query that should be used for retrieving the list of codeHammerColumns for the specified table.
        /// </returns>
        public string CodeHammerGetCodeHammerColumnQuery(string tableName)
        {
            try
            {
                return CodeHammerGetResource("CodeHammer.Framework.Resources.SqlProcess.CodeHammerColumnQuery.sql", "#TableName#", tableName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get database access manager.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetDatabaseAccessManager(out string name)
        {
            name = string.Empty;
            try
            {
                name = "CodeHammerDataAccessManager.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DatabaseManager.CodeHammerDataAccessManager.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get database connection.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetDatabaseConnection(out string name)
        {
            name = string.Empty;
            try
            {
                name = "DatabaseConnectionString.txt";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DatabaseManager.DatabaseConnectionStringSql.txt");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer web configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerWebConfig(out string name)
        {
            name = string.Empty;
            try
            {
                name = "Web.config";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.WcfService.WebConfig.txt");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get database manager.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetDatabaseManager(out string name)
        {
            name = string.Empty;
            try
            {
                name = "CodeHammerDatabaseManager.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DatabaseManager.CodeHammerDatabaseManager.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get global castle.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetGlobalCastle(out string name)
        {
            name = string.Empty;
            try
            {
                name = "Global.asax.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.WcfService.CastleGlobal.asax.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get route configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>>return true if success</returns>
        public string CodeHammerGetRouteConfig(out string name)
        {
            name = string.Empty;
            try
            {
                name = "RouteConfig.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.WcfService.RouteConfig.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get log4 net configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetLog4NetConfig(out string name)
        {
            name = string.Empty;
            try
            {
                name = "log4Net.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.Logging.Logging.txt");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get log4 class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetLog4Class(out string name)
        {
            name = string.Empty;
            try
            {
                name = "Logger.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.Logging.Logger.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get primary keys.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetPrimaryKeys(string databaseName)
        {
            //System.Reflection.Assembly thisExe;
            //thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            //string[] resources = thisExe.GetManifestResourceNames();
            //string list = "";

            //// Build the string of resources.
            //foreach (string resource in resources)
            //    list += resource + "\r\n";

            try
            {
                return CodeHammerGetResource("CodeHammer.Framework.Resources.SqlProcess.CodeHammerGetPrimaryKeys.sql", "#DatabaseName#", databaseName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the stored procedure params.
        /// Tablename, Paramname and datatype
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetStoredProcedureParams(string spName)
        {
            try
            {
                return CodeHammerGetResource("CodeHammer.Framework.Resources.SqlProcess.CodeHammerGetContentsFromSPQuery.sql", "#SpName#", spName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the stored procedure query.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetStoredProcedureQuery(string databaseName)
        {
            try
            {
                return CodeHammerGetResource("CodeHammer.Framework.Resources.SqlProcess.CodeHammerSPQuery.sql", "#DatabaseName#", databaseName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of tables for the specified database.
        /// </summary>
        /// <param name="databaseName">The database to be queried for.</param>
        /// <returns>
        /// The query that should be used for retrieving the list of tables for the specified database.
        /// </returns>
        public string CodeHammerGetTableQuery(string databaseName)
        {
            try
            {
                return CodeHammerGetResource("CodeHammer.Framework.Resources.SqlProcess.CodeHammerTableQuery.sql", "#DatabaseName#", databaseName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer javascript.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerJavascript(out string name)
        {
            name = string.Empty;
            try
            {
                name = "CodeHammerScript.js";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DotNet.CodeHammerScript.CodeHammerScript.js");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Database Resource

        #region CodeHammerProject

        /// <summary>
        /// Codes the hammer get code hammer project.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>

        public void CopyCodeHammerGetCodeHammerProject(string vsVersion, string file)
        {
            try
            {
                if (vsVersion.Contains("VS2012"))
                {
                    using (Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(ioManagerContract.CodeHammerProject2012))
                    {
                        if (resource == null)
                        {
                            throw new ArgumentException("No such resource", "resourceName");
                        }
                        using (Stream output = File.OpenWrite(file))
                        {
                            resource.CopyTo(output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #endregion CodeHammerProject

        #region Asp.net UserControl

        /// <summary>
        /// Codes the hammer get user control grid view ascx.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetUserControlGridViewASCX(out string name)
        {
            name = string.Empty;
            try
            {
                name = "UserControlGridView.aspx";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DotNet.UserControl.UserControlGridViewTemplate.aspx");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get user control grid view cs.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetUserControlGridViewCS(out string name)
        {
            name = string.Empty;
            try
            {
                name = "UserControlGridView.aspx.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DotNet.UserControl.UserControlGridViewTemplate.aspx.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get user control grid view designer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerGetUserControlGridViewDesigner(out string name)
        {
            name = string.Empty;
            try
            {
                name = "UserControlGridView.aspx.designer.cs";
                return CodeHammerGetResource("CodeHammer.Framework.Resources.DotNet.UserControl.UserControlGridViewTemplate.aspx.designer.cs");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Asp.net UserControl

        #region Sql Generation

        /// <summary>
        /// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
        /// </summary>
        /// <param name="codeHammerColumn">Object that stores the information for the codeHammerColumn the parameter represents.</param>
        /// <param name="checkForOutputParameter">Indicates if the created parameter should be checked to see if it should be created as an output parameter.</param>
        /// <returns>
        /// String containing parameter information of the specified codeHammerColumn for a stored procedure.
        /// </returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.CodeHammerType</exception>
        public string CodeHammerCreateParameterString(CodeHammerColumn codeHammerColumn, bool checkForOutputParameter)
        {
            try
            {
                string parameter;

                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    case "binary":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "bigint":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "bit":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "Flag:bit":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    //case "geometry":
                    //    parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                    //    break;

                    //case "hierarchyid":
                    //    parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                    //    break;

                    //case "geography":
                    //    parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                    //    break;

                    case "datetime2":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "char":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "datetime":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "date":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    //case "DATETIMEOFFSET":
                    //    parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                    //    break;

                    case "decimal":
                        if (codeHammerColumn.CodeHammerScale.Length == 0)
                            parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + codeHammerColumn.CodeHammerPrecision + ")";
                        else
                            parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + codeHammerColumn.CodeHammerPrecision + ", " + codeHammerColumn.CodeHammerScale + ")";
                        break;

                    case "float":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + codeHammerColumn.CodeHammerPrecision + ")";
                        break;

                    case "image":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "int":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "money":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "nchar":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "ntext":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "nvarchar":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "numeric":
                        if (codeHammerColumn.CodeHammerScale.Length == 0)
                            parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + codeHammerColumn.CodeHammerPrecision + ")";
                        else
                            parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + codeHammerColumn.CodeHammerPrecision + ", " + codeHammerColumn.CodeHammerScale + ")";
                        break;

                    case "real":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "smalldatetime":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "smallint":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "smallmoney":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "sql_variant":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "sysname":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "text":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "timestamp":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    //case "time":
                    //    parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                    //    break;

                    case "tinyint":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "varbinary":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "varchar":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType + "(" + CodeHammerGetParameterLength(codeHammerColumn) + ")";
                        break;

                    case "uniqueidentifier":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    case "xml":
                        parameter = "@" + codeHammerColumn.CodeHammerName + " " + codeHammerColumn.CodeHammerType;
                        break;

                    default:  //// Unknow data type
                        Console.WriteLine("SQL Server data type : " + codeHammerColumn.CodeHammerType + " not supported");
                        throw (new Exception("SQL Server data type : " + codeHammerColumn.CodeHammerType + " not supported"));
                }

                //// Return the new parameter string
                if (checkForOutputParameter && (codeHammerColumn.CodeHammerIsIdentity || codeHammerColumn.CodeHammerIsRowGuidCol))
                {
                    return parameter + " OUTPUT";
                }
                else
                {
                    return parameter;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the foreign key information for the specified table.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="tableName">Name of the table that foreign keys should be checked for.</param>
        /// <returns>
        /// DataReader containing the foreign key information for the specified table.
        /// </returns>
        ////public DataTable CodeHammerGetForeignKeyList(SqlConnection connection, string tableName)
        ////{
        ////    SqlParameter parameter;

        ////    using (SqlCommand command = new SqlCommand("sp_fkeys", connection))
        ////    {
        ////        command.CommandType = CommandType.StoredProcedure;

        ////        parameter = new SqlParameter("@pktable_name", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_name", DataRowVersion.Current, DBNull.Value);
        ////        command.Parameters.Add(parameter);
        ////        parameter = new SqlParameter("@pktable_owner", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_owner", DataRowVersion.Current, DBNull.Value);
        ////        command.Parameters.Add(parameter);
        ////        parameter = new SqlParameter("@pktable_qualifier", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_qualifier", DataRowVersion.Current, DBNull.Value);
        ////        command.Parameters.Add(parameter);
        ////        parameter = new SqlParameter("@fktable_name", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_name", DataRowVersion.Current, tableName);
        ////        command.Parameters.Add(parameter);
        ////        parameter = new SqlParameter("@fktable_owner", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_owner", DataRowVersion.Current, DBNull.Value);
        ////        command.Parameters.Add(parameter);
        ////        parameter = new SqlParameter("@fktable_qualifier", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_qualifier", DataRowVersion.Current, DBNull.Value);
        ////        command.Parameters.Add(parameter);

        ////        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        ////        DataTable dataTable = new DataTable();
        ////        dataAdapter.Fill(dataTable);

        ////        return dataTable;
        ////    }
        ////}

        /// <summary>
        /// Creates the length portion of the specified codeHammerColumn.
        /// </summary>
        /// <param name="codeHammerColumn">Object that stores the information for the codeHammerColumn the parameter represents.</param>
        /// <returns>
        /// String containing length information for the specific codeHammerColumn.
        /// </returns>
        public string CodeHammerGetParameterLength(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                if (codeHammerColumn.CodeHammerLength == "-1")
                {
                    return "max";
                }

                return codeHammerColumn.CodeHammerLength;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Sql Generation

        #region DateType Section

        /// <summary>
        /// Creates a string for a method parameter representing the specified codeHammerColumn.
        /// </summary>
        /// <param name="codeHammerColumn">Object that stores the information for the codeHammerColumn the parameter represents.</param>
        /// <returns>
        /// String containing parameter information of the specified codeHammerColumn for a method call.
        /// </returns>
        public string CodeHammerCreateMethodParameter(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                return CodeHammerGetCsType(codeHammerColumn) + " " + CodeHammerFormatCamel(codeHammerColumn.CodeHammerName.Replace(" ", string.Empty).Trim());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the method SP parameter.
        /// </summary>
        /// <param name="codeHammerReversedEngineedSPType">Type of the code hammer reversed engineed SP.</param>
        /// <param name="codeHammerReversedEngineedSPName">Name of the code hammer reversed engineed SP.</param>
        /// <returns>
        /// String containing parameter information of the specified codeHammerString for a method call.
        /// </returns>
        public string CodeHammerCreateMethodSPParameter(string codeHammerReversedEngineedSPType, string codeHammerReversedEngineedSPName)
        {
            try
            {
                return CodeHammerGetReversedStoredProcedureCsType(codeHammerReversedEngineedSPType) + " " + CodeHammerFormatCamelDTO(codeHammerReversedEngineedSPName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the name of the method to call on a SqlDataReader for the specified codeHammerColumn.
        /// </summary>
        /// <param name="codeHammerColumn">The codeHammerColumn to retrieve data for.</param>
        /// <returns>
        /// The name of the method to call on a SqlDataReader for the specified codeHammerColumn.
        /// </returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.CodeHammerType</exception>
        public string CodeHammerGetCsType(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    //case "time":
                    //    return "TimeSpan";

                    case "binary":
                        return "byte[]";

                    case "bigint":
                        return "long";

                    case "bit":
                        return "bool";

                    case "Flag:bit":
                        return "bool";

                    case "char":
                        return "string";

                    case "datetime":
                        return "DateTime";

                    case "decimal":
                        return "decimal";

                    case "float":
                        return "double";

                    case "image":
                        return "byte[]";

                    case "int":
                        return "int";

                    case "money":
                        return "decimal";

                    case "nchar":
                        return "string";

                    case "ntext":
                        return "string";

                    case "nvarchar":
                        return "string";

                    case "numeric":
                        return "decimal";

                    case "real":
                        return "float";

                    case "smalldatetime":
                        return "DateTime";

                    case "smallint":
                        return "short";

                    case "smallmoney":
                        return "float";

                    case "sql_variant":
                        return "byte[]";

                    case "sysname":
                        return "string";

                    case "text":
                        return "string";

                    case "timestamp":
                        return "DateTime";

                    case "tinyint":
                        return "byte";

                    case "varbinary":
                        return "byte[]";

                    case "varchar":
                        return "string";

                    case "uniqueidentifier":
                        return "Guid";

                    case "xml":
                        return "string";

                    //case "geography":
                    //    return "string";

                    //case "hierarchyid":
                    //    return "int";

                    case "datetime2":
                        return "DateTime";

                    case "date":
                        return "DateTime";

                    //case "geometry":
                    //    return "byte[]";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid SQL Server data type specified: " + codeHammerColumn.CodeHammerType + " in column: " + codeHammerColumn.CodeHammerName));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the cs type reader get.
        /// </summary>
        /// <param name="codeHammerColumn">The codeHammerColumn.</param>
        /// <returns>
        /// return cs type
        /// </returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.Type</exception>
        public string CodeHammerGetCsTypeReaderGet(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    //case "time":
                    //    return "TimeSpan";

                    case "binary":
                        return "byte[]";

                    case "bigint":
                        return "GetInt64";

                    case "bit":
                        return "GetBoolean";

                    case "Flag:bit":
                        return "GetBoolean";

                    case "char":
                        return "GetString";

                    case "datetime":
                        return "GetDateTime";

                    case "date":
                        return "GetDateTime";

                    case "decimal":
                        return "GetDecimal";

                    case "float":
                        return "GetDouble";

                    case "image":
                        return "byte[]";

                    case "int":
                        return "GetInt32";

                    case "money":
                        return "GetDecimal";

                    case "nchar":
                        return "GetString";

                    case "ntext":
                        return "GetString";

                    case "nvarchar":
                        return "GetString";

                    case "numeric":
                        return "GetDecimal";

                    case "real":
                        return "GetFloat";

                    case "smalldatetime":
                        return "GetDateTime";

                    case "smallint":
                        return "GetInt16";

                    case "smallmoney":
                        return "GetFloat";

                    case "sql_variant":
                        return "byte[]";

                    case "sysname":
                        return "GetString";

                    case "text":
                        return "GetString";

                    case "timestamp":
                        return "DateTime";

                    case "tinyint":
                        return "byte";

                    case "varbinary":
                        return "byte[]";

                    case "varchar":
                        return "GetString";

                    case "uniqueidentifier":
                        return "GetGuid";

                    case "xml":
                        return "GetString";

                    //case "geography":
                    //    return "GetString";

                    //case "hierarchyid":
                    //    return "GetInt32";

                    case "datetime2":
                        return "DateTime";

                    //case "geometry":
                    //    return "byte[]";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid SQL Server data type specified: " + codeHammerColumn.CodeHammerType));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer get cs type reader get default value.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.CodeHammerType</exception>
        public string CodeHammerGetCsTypeReaderGetDefaultValue(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    case "binary":
                    case "image":
                    case "varbinary":

                        return "new byte[0]";

                    case "bigint":
                        return "0";

                    case "bit":
                        return "false";

                    case "Flag:bit":
                        return "false";

                    case "char":
                        return "string.Empty";

                    case "datetime":
                        return "new DateTime()";

                    case "date":
                        return "new DateTime()";

                    case "decimal":
                        return "0";

                    case "float":
                        return "0";

                    case "int":
                        return "0";

                    case "money":
                        return "0";

                    case "nchar":
                        return "string.Empty";

                    case "ntext":
                        return "string.Empty";

                    case "nvarchar":
                        return "string.Empty";

                    case "numeric":
                        return "0";

                    case "real":
                        return "0";

                    case "smalldatetime":
                        return "new DateTime()";

                    case "smallint":
                        return "0";

                    case "smallmoney":
                        return "0";

                    case "sysname":
                        return "string.Empty";

                    case "text":
                        return "string.Empty";

                    case "timestamp":
                        return "new DateTime()";

                    case "varchar":
                        return "string.Empty";

                    case "uniqueidentifier":
                        return "new Guid()";

                    case "xml":
                        return "string.Empty";

                    case "datetime2":
                        return "new DateTime()";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid SQL Server data type specified: " + codeHammerColumn.CodeHammerType));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Convert to c# type from param type
        /// Gets the type of the reversed stored procedure cs.
        /// </summary>
        /// <param name="codeHammerReversedSP">The code hammer reversed SP.</param>
        /// <returns>
        /// if all goes well then return true
        /// </returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.Type</exception>
        public string CodeHammerGetReversedStoredProcedureCsType(string codeHammerReversedSP)
        {
            try
            {
                switch (codeHammerReversedSP.ToLower())
                {
                    //case "time":
                    //    return "TimeSpan";

                    case "binary":
                        return "byte[]";

                    case "bigint":
                        return "long";

                    case "bit":
                        return "bool";

                    case "Flag:bit":
                        return "bool";

                    case "char":
                        return "string";

                    case "datetime":
                        return "DateTime";

                    case "decimal":
                        return "decimal";

                    case "float":
                        return "double";

                    case "image":
                        return "byte[]";

                    case "int":
                        return "int";

                    case "money":
                        return "decimal";

                    case "nchar":
                        return "string";

                    case "ntext":
                        return "string";

                    case "nvarchar":
                        return "string";

                    case "numeric":
                        return "decimal";

                    case "real":
                        return "float";

                    case "smalldatetime":
                        return "DateTime";

                    case "smallint":
                        return "short";

                    case "smallmoney":
                        return "float";

                    case "sql_variant":
                        return "byte[]";

                    case "sysname":
                        return "string";

                    case "text":
                        return "string";

                    case "timestamp":
                        return "DateTime";

                    case "tinyint":
                        return "byte";

                    case "varbinary":
                        return "byte[]";

                    case "varchar":
                        return "string";

                    case "uniqueidentifier":
                        return "Guid";

                    case "xml":
                        return "string";

                    //case "geography":
                    //    return "string";

                    //case "hierarchyid":
                    //    return "int";

                    case "datetime2":
                        return "DateTime";

                    case "date":
                        return "DateTime";

                    //case "geometry":
                    //    return "byte[]";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid data type specified: " + codeHammerReversedSP));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the hammer to cs type convert to.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.CodeHammerType</exception>
        public string CodeHammerToCsTypeConvertTo(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    //case "time":
                    //    return "ToString";

                    case "binary":
                        return "Encoding.UTF8.GetBytes";

                    case "bigint":
                        return "int";

                    case "bit":
                        return "bool";

                    case "Flag:bit":
                        return "bool";

                    case "char":
                        return "string";

                    case "datetime":
                        return "DateTime";

                    case "date":
                        return "DateTime";

                    case "decimal":
                        return "decimal";

                    case "float":
                        return "double";

                    case "image":
                        return "Encoding.UTF8.GetBytes";

                    case "int":
                        return "int";

                    case "money":
                        return "decimal";

                    case "nchar":
                        return "string";

                    case "ntext":
                        return "string";

                    case "nvarchar":
                        return "string";

                    case "numeric":
                        return "decimal";

                    case "real":
                        return "float";

                    case "smalldatetime":
                        return "DateTime";

                    case "smallint":
                        return "short";

                    case "smallmoney":
                        return "float";

                    case "sql_variant":
                        return "Encoding.UTF8.GetBytes";

                    case "sysname":
                        return "string";

                    case "text":
                        return "string";

                    case "timestamp":
                        return "DateTime";

                    case "tinyint":
                        return "byte";

                    case "varbinary":
                        return "Encoding.UTF8.GetBytes";

                    case "varchar":
                        return "string";

                    case "uniqueidentifier":
                        return "Guid";

                    case "xml":
                        return "string";

                    //case "geography":
                    //    return "ToString";

                    //case "hierarchyid":
                    //    return "ToInt32";

                    case "datetime2":
                        return "DateTime";

                    //case "geometry":
                    //    return "byte[]";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid SQL Server data type specified: " + codeHammerColumn.CodeHammerType));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Codes the type of the hammer to cs type convert to.
        /// </summary>
        /// <param name="codeHammerColumn">The code hammer column.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid SQL Server data type specified:  + codeHammerColumn.CodeHammerType</exception>
        public string CodeHammerToCsTypeConvertToType(CodeHammerColumn codeHammerColumn)
        {
            try
            {
                switch (codeHammerColumn.CodeHammerType.ToLower())
                {
                    //case "time":
                    //    return "ToString";

                    case "binary":
                        return "Encoding.UTF8.GetBytes";

                    case "bigint":
                        return "ToInt32";

                    case "bit":
                        return "ToBoolean";

                    case "Flag:bit":
                        return "ToBoolean";

                    case "char":
                        return "ToString";

                    case "datetime":
                        return "ToDateTime";

                    case "date":
                        return "ToDateTime";

                    case "decimal":
                        return "ToDecimal";

                    case "float":
                        return "ToDouble";

                    case "image":
                        return "Encoding.UTF8.GetBytes";

                    case "int":
                        return "ToInt32";

                    case "money":
                        return "ToDecimal";

                    case "nchar":
                        return "ToString";

                    case "ntext":
                        return "ToString";

                    case "nvarchar":
                        return "ToString";

                    case "numeric":
                        return "ToDecimal";

                    case "real":
                        return "ToSingle";

                    case "smalldatetime":
                        return "ToDateTime";

                    case "smallint":
                        return "ToInt16";

                    case "smallmoney":
                        return "ToSingle";

                    case "sql_variant":
                        return "Encoding.UTF8.GetBytes";

                    case "sysname":
                        return "ToString";

                    case "text":
                        return "ToString";

                    case "timestamp":
                        return "ToDateTime";

                    case "tinyint":
                        return "ToByte";

                    case "varbinary":
                        return "Encoding.UTF8.GetBytes";

                    case "varchar":
                        return "ToString";

                    case "uniqueidentifier":
                        return "new Guid";

                    case "xml":
                        return "ToString";

                    //case "geography":
                    //    return "ToString";

                    //case "hierarchyid":
                    //    return "ToInt32";

                    case "datetime2":
                        return "ToDateTime";

                    //case "geometry":
                    //    return "byte[]";

                    default:  //// Unknow data type
                        throw (new Exception("Invalid SQL Server data type specified: " + codeHammerColumn.CodeHammerType));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion DateType Section

        #region Class name Modification

        /// <summary>
        /// Codes the hammer format camel DTO.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>return true if success</returns>
        public string CodeHammerFormatCamel(string original)
        {
            try
            {
                if (original.Length > 0)
                {
                    return Char.ToLower(original[0]) + original.Substring(1);
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Formats a string in Camel case (the first letter is in lower case).
        /// </summary>
        /// <param name="original">A string to be formatted.</param>
        /// <returns>
        /// A string in Camel case.
        /// </returns>
        public string CodeHammerFormatCamelDTO(string original)
        {
            try
            {
                string originalTemp = original.Replace("_", string.Empty);
                if (original.Length > 0)
                {
                    if (original.Length > 2 && char.IsUpper(originalTemp[0]) && char.IsUpper(originalTemp[1]) && char.IsUpper(originalTemp[2]))
                    {
                        char s1 = Char.ToLower(originalTemp[0]);
                        char s2 = Char.ToLower(originalTemp[1]);
                        char s3 = Char.ToLower(originalTemp[2]);
                        return (s1.ToString() + s2.ToString() + s3.ToString()) + original.Substring(3);
                    }
                    if (original.Length == 2 && char.IsUpper(originalTemp[0]) && char.IsUpper(originalTemp[1]))
                    {
                        char s1 = Char.ToLower(originalTemp[0]);
                        char s2 = Char.ToLower(originalTemp[1]);
                        return (s1.ToString() + s2.ToString() + original.Substring(2));
                    }
                    else
                    {
                        return Char.ToLower(original[0]) + original.Substring(1);
                    }
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                ioManagerContract.LogBuilder.AppendLine(ex.StackTrace);
                ioManagerContract.LogBuilder.AppendLine();
                throw new Exception(ex.StackTrace);
            }
        }

        /// <summary>
        /// Formats the table name for use as a data transfer object.
        /// </summary>
        /// <param name="tableName">The name of the table to format.</param>
        /// <returns>
        /// The table name, formatted for use as a data transfer object.
        /// </returns>
        public string CodeHammerFormatClassName(string tableName)
        {
            try
            {
                string className;

                if (Char.IsUpper(tableName[0]))
                {
                    className = tableName;
                }
                else
                {
                    className = CodeHammerFormatPascal(tableName);
                }

                //// Attept to removing a trailing 's' or 'S', unless, the last two characters are both 's' or 'S'.
                if (className[className.Length - 1] == 'S' && className[className.Length - 2] != 'S')
                {
                    className = className.Substring(0, className.Length - 1);
                }
                else if (className[className.Length - 1] == 's' && className[className.Length - 2] != 's')
                {
                    className = className.Substring(0, className.Length - 1);
                }

                return className;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Formats a string in Pascal case (the first letter is in upper case).
        /// </summary>
        /// <param name="original">A string to be formatted.</param>
        /// <returns>
        /// A string in Pascal case.
        /// </returns>
        public string CodeHammerFormatPascal(string original)
        {
            try
            {
                if (original.Length > 0)
                {
                    return Char.ToUpper(original[0]) + original.Substring(1);
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Formats the table name for use as a variable.
        /// </summary>
        /// <param name="tableName">The name of the table to format.</param>
        /// <returns>
        /// The table name, formatted for use as a variable.
        /// </returns>
        public string CodeHammerFormatVariableName(string tableName)
        {
            try
            {
                string variableName;

                if (Char.IsLower(tableName[0]))
                {
                    variableName = tableName;
                }
                else
                {
                    variableName = CodeHammerFormatCamelDTO(tableName);
                }

                //// Attept to removing a trailing 's' or 'S', unless, the last two characters are both 's' or 'S'.
                if (variableName[variableName.Length - 1] == 'S' && variableName[variableName.Length - 2] != 'S')
                {
                    variableName = variableName.Substring(0, variableName.Length - 1);
                }
                else if (variableName[variableName.Length - 1] == 's' && variableName[variableName.Length - 2] != 's')
                {
                    variableName = variableName.Substring(0, variableName.Length - 1);
                }

                if (variableName == "bool"
                    || variableName == "byte"
                    || variableName == "char"
                    || variableName == "decimal"
                    || variableName == "double"
                    || variableName == "float"
                    || variableName == "long"
                    || variableName == "object"
                    || variableName == "sbyte"
                    || variableName == "short"
                    || variableName == "string"
                    || variableName == "uint"
                    || variableName == "ulong"
                    || variableName == "ushort")
                {
                    variableName += "Value";
                }

                return variableName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Class name Modification

        #region Populate Database

        /// <summary>
        /// Populates the server.
        /// </summary>
        /// <param name="ServerNames">The server names.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        /// if all goes well then return true
        /// </returns>
        ////public bool PopulateSqlServer(out List<string> ServerNames, out string error)
        ////{
        ////    error = string.Empty;
        ////    ServerNames = new List<string>();
        ////    SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
        ////    DataTable serversTable = servers.GetDataSources();

        ////    ServerNames.Add("Select server");
        ////    foreach (DataRow row in serversTable.Rows)
        ////    {
        ////        string serverName = row[0].ToString();
        ////        try
        ////        {
        ////            if (row[1].ToString() != "")
        ////            {
        ////                serverName += "\\" + row[1].ToString();
        ////            }
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            error = "Problem populating server instance: " + ex.StackTrace;
        ////            IOManager.Instance.LogBuilder.AppendLine(error);
        ////            IOManager.Instance.LogBuilder.AppendLine();
        ////            throw new Exception(ex.StackTrace);
        ////            //return false;
        ////        }

        ////        ServerNames.Add(serverName);
        ////    }

        ////    return true;
        ////}

        #endregion Populate Database

        #region StringManupulation

        /// <summary>
        /// Deletes the lines.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="linesToRemove">The lines to remove.</param>
        /// <returns></returns>
        public string DeleteLines(string s, int linesToRemove)
        {
            try
            {
                return s.Split(Environment.NewLine.ToCharArray(),
                               linesToRemove + 1,
                               StringSplitOptions.RemoveEmptyEntries
                    ).Skip(linesToRemove)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion StringManupulation
    }
}