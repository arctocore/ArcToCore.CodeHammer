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
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Generates C# data access, data transfer classes, bl access and usercontrol template.
    /// </summary>

    public class CodeHammerDTOGenerator : CodeHammerDTOGeneratorContract
    {
        #region Variables

        /// <summary>
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerDTOGenerator"/> class.
        /// </summary>
        /// <param name="codeHammerDataUtilContract">The code hammer data utility contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        public CodeHammerDTOGenerator(CodeHammerDataUtilContract codeHammerDataUtilContract,
            IOManagerContract ioManagerContract,
             LogFuncContract logFuncContract,
            FuncTypeFactoryContract funcTypeFactoryContract)
        {
            this.logFuncContract = logFuncContract;
            this.ioManagerContract = ioManagerContract;
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;

            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
        }

        /// <summary>
        /// Creates a C# class for all of the table's
        /// stored procedures.
        /// </summary>
        /// <param name="onlyDataContract">if set to <c>true</c> [only data contract].</param>
        /// <param name="onlyDTO">if set to <c>true</c> [only dto].</param>
        /// <param name="selectTables">if set to <c>true</c> [select tables].</param>
        /// <param name="tableDic">The table dic.</param>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
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
        /// <exception cref="System.Exception">logging is missing
        /// or</exception>
        public bool CodeHammerCreateDataTransferClass(bool onlyDataContract, bool onlyDTO, bool selectTables, Dictionary<string, List<Dictionary<string, string>>> tableDic, CodeHammerTableDto table, string path, string aspnetPath, List<string> resultDataOptions, string usercontrolInheritsTextBox, string connectionString, string configConnection, string outputDirectory, string databaseManagementPath, string csPath, bool throttling, int wcfSecurity)
        {
            StringBuilder strASCXGridView = new StringBuilder();
            StringBuilder strASCXGridViewDesignerEdit = new StringBuilder();
            StringBuilder strASCXGridViewDesignerAdd = new StringBuilder();
            StringBuilder strASCXGridViewTableDetail = new StringBuilder();
            StringBuilder strASCXGridViewTableEdit = new StringBuilder();
            StringBuilder strASCXGridViewTableEditSave = new StringBuilder();
            StringBuilder strASCXGridViewTableAddSave = new StringBuilder();
            StringBuilder strASCXGridViewTableEditSaveDto = new StringBuilder();
            StringBuilder strASCXGridViewTableAddSaveDto = new StringBuilder();
            StringBuilder strASCXGridViewTableAddDeleteDto = new StringBuilder();

            StringBuilder strASCXGridViewTableIDEdit = new StringBuilder();
            StringBuilder strASCXGridViewTableAdd = new StringBuilder();
            StringBuilder strASCXGridViewTableDelete = new StringBuilder();

            StringBuilder strHtmlDecodeRowsCommand = new StringBuilder();
            StringBuilder strCodeHammerJS = new StringBuilder();
            StringBuilder strCS = new StringBuilder();

            string blClassName = string.Empty;
            string className = string.Empty;
            string classNameDTO = string.Empty;
            string quates = @"""";
            string generateTag = "<!--<auto-generated>-->";
            string dataKeyNames = string.Empty;

            try
            {
                dataKeyNames = table.CodeHammerPrimaryKeys[0].CodeHammerName;
            }
            catch
            {
                return false;
            }

            string dataKeyNamesDataType = codeHammerDataUtilContract.CodeHammerGetCsType(table.CodeHammerPrimaryKeys[0]);

            string replaceTempASCX = string.Empty;
            string replaceTempASCXDesigner = string.Empty;

            ////DataTable
            if (selectTables == true)
            {
                blClassName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;
                className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim());
                classNameDTO = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim());// +"DTO";
            }
            else
            {
                resultDataOptions.Clear();
            }

            string classNameDTOTemp = classNameDTO;
            string pathToProject = csPath + ioManagerContract.DtoFolder + classNameDTO + SuffixDto.Instance().DtoTextBox + ".cs";

            path = pathToProject;

            string DataAccessManagerCS = string.Empty;
            string DatabaseManagerCS = string.Empty;
            string databaseManagerName = string.Empty;
            string databaseAccessManagerName = string.Empty;
            string databaseConnectionStringName = string.Empty;
            string usercontrolCSName = string.Empty;
            string usercontrolASCXName = string.Empty;
            string usercontrolASCXDesignerName = string.Empty;
            string CodeHammerScript = string.Empty;

            string AppConfig = codeHammerDataUtilContract.CodeHammerWebConfig(out databaseConnectionStringName);

            string contentDataAccessManagerCS = string.Empty;
            string contentDatabaseManagerCS = string.Empty;
            string contentAppConfig = string.Empty;

            contentAppConfig = AppConfig;
            if (!onlyDTO && !onlyDataContract)
            {
                using (StreamWriter writer = new StreamWriter(csPath + ioManagerContract.VisualstudioServiceHostProjectName + "Web.config"))
                {
                    string replaceAppConfig = contentAppConfig;

                    if (ioManagerContract.Log4Net)
                    {
                        string log4NetString = string.Empty;
                        string logging = codeHammerDataUtilContract.CodeHammerGetLog4NetConfig(out log4NetString);

                        if (string.IsNullOrEmpty(logging))
                        {
                            throw new Exception("logging is missing");
                        }

                        replaceAppConfig = replaceAppConfig.Replace("<LOG4NET>", logging.Replace("<LOGDB>", "<connectionString value=" + quates + connectionString + quates + "/>"));
                    }
                    else
                    {
                        replaceAppConfig = replaceAppConfig.Replace("<LOG4NET>", string.Empty);
                    }

                    //first SecurityBinding

                    if (wcfSecurity == 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Length = 0;
                        sb.Capacity = 0;

                        sb.AppendLine("  <bindings>");
			            sb.AppendLine("   <webHttpBinding>");
				        sb.AppendLine("    <binding>");
					    sb.AppendLine("      <security mode=" + quates + "Transport" + quates + ">");
						sb.AppendLine("       <transport clientCredentialType=" + quates + "Certificate" + quates + " proxyCredentialType=" + quates + "Basic" + quates + " realm=" + quates + quates + " />");
					    sb.AppendLine("      </security>");
				        sb.AppendLine("    </binding>");
			            sb.AppendLine("   </webHttpBinding>");
                        sb.AppendLine(" </bindings>");

                        replaceAppConfig = replaceAppConfig.Replace("<SECURITYBINDING>", sb.ToString());

                        StringBuilder sc = new StringBuilder();
                        sc.Length = 0;
                        sc.Capacity = 0;

                        sc.AppendLine("  <serviceCredentials>");
                        sc.AppendLine("             <serviceCertificate storeLocation=" + quates + CertInformation.Instance().StoreLocation + quates +  " findValue=" + quates + CertInformation.Instance().FindValue + quates + " storeName=" + quates + CertInformation.Instance().StoreName + quates + " x509FindType=" + quates + "FindBySubjectName" + quates  +" />");
                        sc.AppendLine("            </serviceCredentials>");


                        replaceAppConfig = replaceAppConfig.Replace("<SERVICECREDENTIALS>", sc.ToString());

                    }
                    else
                    {
                        replaceAppConfig = replaceAppConfig.Replace("<SECURITYBINDING>", string.Empty);
                        replaceAppConfig = replaceAppConfig.Replace("<SERVICECREDENTIALS>", string.Empty);
                    }


                    //Second ServiceCredentials

                    if (throttling)
                    {
                        replaceAppConfig = replaceAppConfig.Replace("<dbsetup>", connectionString).Replace("<THROTTLING>", "<serviceThrottling maxConcurrentCalls=" + quates + "200" + quates + " maxConcurrentInstances =" + quates + "200" + quates + " maxConcurrentSessions =" + quates + "200" + quates + "/>");
                    }
                    else
                    {
                        replaceAppConfig = replaceAppConfig.Replace("<dbsetup>", connectionString).Replace("<THROTTLING>", string.Empty);
                    }
                    writer.Write(replaceAppConfig);
                }
            }

            if (selectTables == true)
            {
                if (onlyDTO && !onlyDataContract)
                {
                    #region OnlyDto

                    ioManagerContract.CodeEditorBuilder.Length = 0;
                    ioManagerContract.CodeEditorBuilder.Capacity = 0;

                    ioManagerContract.CodeEditorBuilder.AppendLine("// <auto-generated>");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     This code was generated by a CodeHammer");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     the code is regenerated");
                    ioManagerContract.CodeEditorBuilder.AppendLine("// </auto-generated>");
                    ioManagerContract.CodeEditorBuilder.AppendLine();

                    ioManagerContract.CodeEditorBuilder.AppendLine("namespace Domain");
                    ioManagerContract.CodeEditorBuilder.AppendLine("{");

                    //// Create the header for the class
                    ioManagerContract.CodeEditorBuilder.AppendLine("    using System;");
                    ioManagerContract.CodeEditorBuilder.AppendLine();

                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// <summary>");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// This class " + classNameDTO + SuffixDto.Instance().DtoTextBox);
                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// </summary>");

                    ioManagerContract.CodeEditorBuilder.AppendLine("    public class " + classNameDTO + SuffixDto.Instance().DtoTextBox);
                    ioManagerContract.CodeEditorBuilder.AppendLine("    {");

                    string type = string.Empty;
                    string name = string.Empty;
                    bool nullableDataType;

                    for (int i = 0; i < table.CodeHammerColumns.Count; i++)
                    {
                        CodeHammerColumn codeHammerColumn = table.CodeHammerColumns[i];
                        string parameter = codeHammerDataUtilContract.CodeHammerCreateMethodParameter(codeHammerColumn);
                        name = parameter.Split(' ')[1];
                        nullableDataType = codeHammerColumn.CodeHammerIsNullable;

                        if (nullableDataType == false)
                        {
                            type = parameter.Split(' ')[0];
                        }

                        if (nullableDataType == true)
                        {
                            if (codeHammerColumn.CodeHammerType.Equals("varchar") || codeHammerColumn.CodeHammerType.Equals("ntext") || codeHammerColumn.CodeHammerType.Equals("nvarchar") || codeHammerColumn.CodeHammerType.Equals("nchar") || codeHammerColumn.CodeHammerType.Equals("xml"))
                            {
                                type = parameter.Split(' ')[0];
                            }
                            else if (codeHammerColumn.CodeHammerType.Equals("varbinary") || codeHammerColumn.CodeHammerType.Equals("binary") || codeHammerColumn.CodeHammerType.Equals("image"))
                            {
                                type = "byte[]";//// parameter.Split(' ')[0] ;
                            }
                            else
                            {
                                type = parameter.Split(' ')[0] + "?";
                            }
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <summary>");

                        if (type == "bool")
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets a value indicating whether this instance " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets the " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " value.");
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </summary>");

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <value>");
                        if (type == "bool")
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        ///   <c>true</c> if this instance is demo; otherwise, <c>false</c>.");
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// The " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </value>");

                        ioManagerContract.CodeEditorBuilder.AppendLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");

                        if (i < (table.CodeHammerColumns.Count - 1))
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine();
                        }
                    }

                    if (!onlyDTO)
                    {
                        ioManagerContract.CodeEditorBuilder.AppendLine();
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <summary>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets the size of the page.");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </summary>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <value>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// The size of the page.");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </value>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        public int PageSize { get; set; }");
                    }
                    //// Close out the class and namespace
                    ioManagerContract.CodeEditorBuilder.AppendLine("    }");
                    ioManagerContract.CodeEditorBuilder.AppendLine("}");

                    #endregion OnlyDto
                }

                if (onlyDataContract && onlyDTO)
                {
                    #region OnlyDataContract

                    ioManagerContract.CodeEditorBuilder.Length = 0;
                    ioManagerContract.CodeEditorBuilder.Capacity = 0;

                    string dataContractDir = csPath + ioManagerContract.VisualstudioProjectDataContractFolder + className + SuffixDto.Instance().DataContractTextBox + ".cs";

                    //// Create the header for the class
                    ioManagerContract.CodeEditorBuilder.AppendLine("// <auto-generated>");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     This code was generated by a CodeHammer");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    ioManagerContract.CodeEditorBuilder.AppendLine("//     the code is regenerated");
                    ioManagerContract.CodeEditorBuilder.AppendLine("// </auto-generated>");
                    ioManagerContract.CodeEditorBuilder.AppendLine();

                    //// Create the header for the class
                    ioManagerContract.CodeEditorBuilder.AppendLine("namespace ServiceLibrary");
                    ioManagerContract.CodeEditorBuilder.AppendLine("{");
                    ioManagerContract.CodeEditorBuilder.AppendLine();

                    ioManagerContract.CodeEditorBuilder.AppendLine("    using System;");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    using System.Data;");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    using System.Runtime.Serialization;");
                    ioManagerContract.CodeEditorBuilder.AppendLine();

                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// <summary>");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// This class " + classNameDTO + SuffixDto.Instance().DataContractTextBox);
                    ioManagerContract.CodeEditorBuilder.AppendLine("    /// </summary>");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    [DataContract]");
                    ioManagerContract.CodeEditorBuilder.AppendLine("    public class " + classNameDTO + SuffixDto.Instance().DataContractTextBox);
                    ioManagerContract.CodeEditorBuilder.AppendLine("    {");

                    string type = string.Empty;
                    string name = string.Empty;
                    bool nullableDataType;

                    for (int i = 0; i < table.CodeHammerColumns.Count; i++)
                    {
                        CodeHammerColumn codeHammerColumn = table.CodeHammerColumns[i];
                        string parameter = codeHammerDataUtilContract.CodeHammerCreateMethodParameter(codeHammerColumn);
                        name = parameter.Split(' ')[1];
                        nullableDataType = codeHammerColumn.CodeHammerIsNullable;

                        if (nullableDataType == false)
                        {
                            type = parameter.Split(' ')[0];
                        }

                        if (nullableDataType == true)
                        {
                            if (codeHammerColumn.CodeHammerType.Equals("varchar") || codeHammerColumn.CodeHammerType.Equals("ntext") || codeHammerColumn.CodeHammerType.Equals("nvarchar") || codeHammerColumn.CodeHammerType.Equals("nchar") || codeHammerColumn.CodeHammerType.Equals("xml"))
                            {
                                type = parameter.Split(' ')[0];
                            }
                            else if (codeHammerColumn.CodeHammerType.Equals("varbinary") || codeHammerColumn.CodeHammerType.Equals("binary") || codeHammerColumn.CodeHammerType.Equals("image"))
                            {
                                type = "byte[]";//// parameter.Split(' ')[0] ;
                            }
                            else
                            {
                                type = parameter.Split(' ')[0] + "?";
                            }
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <summary>");

                        if (type == "bool")
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets a value indicating whether this instance " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets the " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " value.");
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </summary>");

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <value>");
                        if (type == "bool")
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        ///   <c>true</c> if this instance is demo; otherwise, <c>false</c>.");
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        /// The " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                        }

                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </value>");

                        if (nullableDataType == true)
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        [DataMember(Name = " + quates + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + quates + ", EmitDefaultValue = true, IsRequired=false)]");
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        [DataMember(Name = " + quates + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + quates + ", EmitDefaultValue = true, IsRequired=true)]");
                        }

                        if (string.IsNullOrEmpty(codeHammerColumn.CodeHammerLength))
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");
                        }
                        else
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");

                            //streamDataContractWriter.WriteLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name) + " { get { return " + codeHammerDataUtilContract.CodeHammerFormatPascal(name) + "; } set { if (value.ToString().Length > " + codeHammerColumn.CodeHammerLength + ") throw new Exception(" + quates + "Invalid length of property." + quates + "); } }");
                        }

                        if (i < (table.CodeHammerColumns.Count - 1))
                        {
                            ioManagerContract.CodeEditorBuilder.AppendLine();
                        }
                    }

                    if (!onlyDTO && !onlyDataContract)
                    {
                        ioManagerContract.CodeEditorBuilder.AppendLine();
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <summary>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// Gets or sets the size of the page.");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </summary>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// <value>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// The size of the page.");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        /// </value>");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        [DataMember(Name = " + quates + "PageSize" + quates + ", EmitDefaultValue = true, IsRequired=true)]");
                        ioManagerContract.CodeEditorBuilder.AppendLine("        public int PageSize { get; set; }");
                    }

                    //// Close out the class and namespace
                    ioManagerContract.CodeEditorBuilder.AppendLine("    }");
                    ioManagerContract.CodeEditorBuilder.AppendLine("}");

                    #endregion OnlyDataContract
                }

                #region DTO

                try
                {
                    if (!onlyDTO && !onlyDataContract)
                    {
                        if (!ioManagerContract.UseOrm)
                        {
                            using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                            {
                                //// Create the header for the class
                                streamWriter.WriteLine("// <auto-generated>");
                                streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                                streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                                streamWriter.WriteLine("//     the code is regenerated");
                                streamWriter.WriteLine("// </auto-generated>");
                                streamWriter.WriteLine();

                                streamWriter.WriteLine("namespace Domain");
                                streamWriter.WriteLine("{");

                                //// Create the header for the class
                                streamWriter.WriteLine("    using System;");

                                streamWriter.WriteLine("    /// <summary>");
                                streamWriter.WriteLine("    /// This class " + classNameDTO + SuffixDto.Instance().DtoTextBox);
                                streamWriter.WriteLine("    /// </summary>");

                                streamWriter.WriteLine("    public class " + classNameDTO + SuffixDto.Instance().DtoTextBox);
                                streamWriter.WriteLine("    {");

                                string type = string.Empty;
                                string name = string.Empty;
                                bool nullableDataType;

                                for (int i = 0; i < table.CodeHammerColumns.Count; i++)
                                {
                                    CodeHammerColumn codeHammerColumn = table.CodeHammerColumns[i];
                                    string parameter = codeHammerDataUtilContract.CodeHammerCreateMethodParameter(codeHammerColumn);
                                    name = parameter.Split(' ')[1];
                                    nullableDataType = codeHammerColumn.CodeHammerIsNullable;

                                    if (nullableDataType == false)
                                    {
                                        type = parameter.Split(' ')[0];
                                    }

                                    if (nullableDataType == true)
                                    {
                                        if (codeHammerColumn.CodeHammerType.Equals("varchar") || codeHammerColumn.CodeHammerType.Equals("ntext") || codeHammerColumn.CodeHammerType.Equals("nvarchar") || codeHammerColumn.CodeHammerType.Equals("nchar") || codeHammerColumn.CodeHammerType.Equals("xml"))
                                        {
                                            type = parameter.Split(' ')[0];
                                        }
                                        else if (codeHammerColumn.CodeHammerType.Equals("varbinary") || codeHammerColumn.CodeHammerType.Equals("binary") || codeHammerColumn.CodeHammerType.Equals("image"))
                                        {
                                            type = "byte[]";//// parameter.Split(' ')[0] ;
                                        }
                                        else
                                        {
                                            type = parameter.Split(' ')[0] + "?";
                                        }
                                    }

                                    streamWriter.WriteLine("        /// <summary>");

                                    if (type == "bool")
                                    {
                                        streamWriter.WriteLine("        /// Gets or sets a value indicating whether this instance " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                                    }
                                    else
                                    {
                                        streamWriter.WriteLine("        /// Gets or sets the " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " value.");
                                    }

                                    streamWriter.WriteLine("        /// </summary>");

                                    streamWriter.WriteLine("        /// <value>");
                                    if (type == "bool")
                                    {
                                        streamWriter.WriteLine("        ///   <c>true</c> if this instance is demo; otherwise, <c>false</c>.");
                                    }
                                    else
                                    {
                                        streamWriter.WriteLine("        /// The " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                                    }

                                    streamWriter.WriteLine("        /// </value>");

                                    streamWriter.WriteLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");

                                    if (i < (table.CodeHammerColumns.Count - 1))
                                    {
                                        streamWriter.WriteLine();
                                    }
                                }

                                streamWriter.WriteLine();
                                streamWriter.WriteLine("        /// <summary>");
                                streamWriter.WriteLine("        /// Gets or sets the size of the page.");
                                streamWriter.WriteLine("        /// </summary>");
                                streamWriter.WriteLine("        /// <value>");
                                streamWriter.WriteLine("        /// The size of the page.");
                                streamWriter.WriteLine("        /// </value>");
                                streamWriter.WriteLine("        public int PageSize { get; set; }");

                                //// Close out the class and namespace
                                streamWriter.WriteLine("    }");
                                streamWriter.WriteLine("}");
                            }
                        }
                    }

                #endregion DTO

                    if (!onlyDTO && !onlyDataContract)
                    {
                        #region DataContract

                        string dataContractDir = csPath + ioManagerContract.VisualstudioProjectDataContractFolder + className + SuffixDto.Instance().DataContractTextBox + ".cs";
                        using (StreamWriter streamDataContractWriter = new StreamWriter(dataContractDir))
                        {
                            //// Create the header for the class
                            streamDataContractWriter.WriteLine("// <auto-generated>");
                            streamDataContractWriter.WriteLine("//     This code was generated by a CodeHammer");
                            streamDataContractWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                            streamDataContractWriter.WriteLine("//     the code is regenerated");
                            streamDataContractWriter.WriteLine("// </auto-generated>");
                            streamDataContractWriter.WriteLine();

                            //// Create the header for the class
                            streamDataContractWriter.WriteLine("namespace ServiceLibrary");
                            streamDataContractWriter.WriteLine("{");
                            streamDataContractWriter.WriteLine();

                            streamDataContractWriter.WriteLine("    using System;");
                            streamDataContractWriter.WriteLine("    using System.Data;");
                            streamDataContractWriter.WriteLine("    using System.Runtime.Serialization;");
                            streamDataContractWriter.WriteLine();

                            streamDataContractWriter.WriteLine("    /// <summary>");
                            streamDataContractWriter.WriteLine("    /// This class " + classNameDTO + SuffixDto.Instance().DataContractTextBox);
                            streamDataContractWriter.WriteLine("    /// </summary>");
                            streamDataContractWriter.WriteLine("    [DataContract]");
                            streamDataContractWriter.WriteLine("    public class " + classNameDTO + SuffixDto.Instance().DataContractTextBox);
                            streamDataContractWriter.WriteLine("    {");

                            string type = string.Empty;
                            string name = string.Empty;
                            bool nullableDataType;

                            for (int i = 0; i < table.CodeHammerColumns.Count; i++)
                            {
                                CodeHammerColumn codeHammerColumn = table.CodeHammerColumns[i];
                                string parameter = codeHammerDataUtilContract.CodeHammerCreateMethodParameter(codeHammerColumn);
                                name = parameter.Split(' ')[1];
                                nullableDataType = codeHammerColumn.CodeHammerIsNullable;

                                if (nullableDataType == false)
                                {
                                    type = parameter.Split(' ')[0];
                                }

                                if (nullableDataType == true)
                                {
                                    if (codeHammerColumn.CodeHammerType.Equals("varchar") || codeHammerColumn.CodeHammerType.Equals("ntext") || codeHammerColumn.CodeHammerType.Equals("nvarchar") || codeHammerColumn.CodeHammerType.Equals("nchar") || codeHammerColumn.CodeHammerType.Equals("xml"))
                                    {
                                        type = parameter.Split(' ')[0];
                                    }
                                    else if (codeHammerColumn.CodeHammerType.Equals("varbinary") || codeHammerColumn.CodeHammerType.Equals("binary") || codeHammerColumn.CodeHammerType.Equals("image"))
                                    {
                                        type = "byte[]";//// parameter.Split(' ')[0] ;
                                    }
                                    else
                                    {
                                        type = parameter.Split(' ')[0] + "?";
                                    }
                                }

                                streamDataContractWriter.WriteLine("        /// <summary>");

                                if (type == "bool")
                                {
                                    streamDataContractWriter.WriteLine("        /// Gets or sets a value indicating whether this instance " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                                }
                                else
                                {
                                    streamDataContractWriter.WriteLine("        /// Gets or sets the " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " value.");
                                }

                                streamDataContractWriter.WriteLine("        /// </summary>");

                                streamDataContractWriter.WriteLine("        /// <value>");
                                if (type == "bool")
                                {
                                    streamDataContractWriter.WriteLine("        ///   <c>true</c> if this instance is demo; otherwise, <c>false</c>.");
                                }
                                else
                                {
                                    streamDataContractWriter.WriteLine("        /// The " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", ""));
                                }

                                streamDataContractWriter.WriteLine("        /// </value>");

                                if (nullableDataType == true)
                                {
                                    streamDataContractWriter.WriteLine("        [DataMember(Name = " + quates + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + quates + ", EmitDefaultValue = true, IsRequired=false)]");
                                }
                                else
                                {
                                    streamDataContractWriter.WriteLine("        [DataMember(Name = " + quates + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + quates + ", EmitDefaultValue = true, IsRequired=true)]");
                                }

                                if (string.IsNullOrEmpty(codeHammerColumn.CodeHammerLength))
                                {
                                    streamDataContractWriter.WriteLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");
                                }
                                else
                                {
                                    streamDataContractWriter.WriteLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name).Replace("_", "") + " { get; set; }");

                                    //streamDataContractWriter.WriteLine("        public " + type + " " + codeHammerDataUtilContract.CodeHammerFormatPascal(name) + " { get { return " + codeHammerDataUtilContract.CodeHammerFormatPascal(name) + "; } set { if (value.ToString().Length > " + codeHammerColumn.CodeHammerLength + ") throw new Exception(" + quates + "Invalid length of property." + quates + "); } }");
                                }

                                if (i < (table.CodeHammerColumns.Count - 1))
                                {
                                    streamDataContractWriter.WriteLine();
                                }
                            }

                            streamDataContractWriter.WriteLine();
                            streamDataContractWriter.WriteLine("        /// <summary>");
                            streamDataContractWriter.WriteLine("        /// Gets or sets the size of the page.");
                            streamDataContractWriter.WriteLine("        /// </summary>");
                            streamDataContractWriter.WriteLine("        /// <value>");
                            streamDataContractWriter.WriteLine("        /// The size of the page.");
                            streamDataContractWriter.WriteLine("        /// </value>");
                            streamDataContractWriter.WriteLine("        [DataMember(Name = " + quates + "PageSize" + quates + ", EmitDefaultValue = true, IsRequired=true)]");
                            streamDataContractWriter.WriteLine("        public int PageSize { get; set; }");

                            //// Close out the class and namespace
                            streamDataContractWriter.WriteLine("    }");
                            streamDataContractWriter.WriteLine("}");
                        }

                        #endregion DataContract
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw new Exception(ex.ToString());
                }
            }
            return true;
        }
    }
}