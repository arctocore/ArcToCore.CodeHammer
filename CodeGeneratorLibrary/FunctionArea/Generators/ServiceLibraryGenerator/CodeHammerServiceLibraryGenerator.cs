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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;

    /// <summary>
    /// this class CodeHammerServiceLibraryGenerator
    /// </summary>

    public class CodeHammerServiceLibraryGenerator : CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator.CodeHammerServiceLibraryGeneratorContract
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
        /// Initializes a new instance of the <see cref="CodeHammerServiceLibraryGenerator"/> class.
        /// </summary>
        /// <param name="codeHammerDataUtilContract">The code hammer data utility contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        public CodeHammerServiceLibraryGenerator(CodeHammerDataUtilContract codeHammerDataUtilContract,
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
        /// <exception cref="System.Exception"></exception>
        public bool CodeHammerServiceContractLibrary(bool selectTables, Dictionary<string, List<Dictionary<string, string>>> tableDic, string databaseName, CodeHammerTableDto table, bool crudYesNo, List<string> resultDataOptions, string path)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().ServiceContractTextBox;

                string blclassName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;
                string IblclassName = "bl" + codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;

                string quates = @"""";

                //path = Path.Combine(path, "CodeHammerServiceLib", "Service");

                string pathToProject = path + ioManagerContract.VisualstudioServiceLibraryProjectFolder + ioManagerContract.ServiceContractFolder + className + ".cs";

                using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                {
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace ServiceLibrary");
                    streamWriter.WriteLine("{");
                    //// Create the header for the class
                    streamWriter.WriteLine("    using System;");
                    streamWriter.WriteLine("    using System.IO;");
                    streamWriter.WriteLine("    using System.Collections.Generic;");
                    streamWriter.WriteLine("    using System.Data;");
                    streamWriter.WriteLine("    using System.ServiceModel;");
                    streamWriter.WriteLine("    using System.ServiceModel.Web;");
                    streamWriter.WriteLine("    using System.Linq;");
                    streamWriter.WriteLine("    using System.Text;");
                    streamWriter.WriteLine("    using System.ComponentModel;");

                    streamWriter.WriteLine();

                    streamWriter.WriteLine("    /// <summary>");
                    streamWriter.WriteLine("    /// This interface " + className);
                    streamWriter.WriteLine("    /// </summary>");
                    streamWriter.WriteLine("    [ServiceContract]");
                    streamWriter.WriteLine("    public interface " + className);
                    streamWriter.WriteLine("    {");

                    if (crudYesNo)
                    {
                        CodeHammerIServiceCreateInsertMethod(IblclassName, table, streamWriter);
                        CodeHammerIServiceCreateUpdateMethod(IblclassName, table, streamWriter);
                        CodeHammerIServiceCreateDeleteMethod(IblclassName, table, streamWriter);
                        CodeHammerIServiceCreateSelectAllMethod(IblclassName, table, streamWriter);
                        CodeHammerIServiceCreateSelectByID(IblclassName, table, streamWriter);
                    }
                    //// Close out the class and namespace
                    streamWriter.WriteLine("    }");
                    streamWriter.WriteLine("}");
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw new Exception(ex.ToString());
            }
        }

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
        /// <exception cref="System.Exception"></exception>
        public bool CodeHammerServiceLibraryUnitTest(bool selectTables, Dictionary<string, List<Dictionary<string, string>>> tableDic, string databaseName, CodeHammerTableDto table, bool crudYesNo, List<string> resultDataOptions, string path)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().ServiceTextBox;

                string blclassName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;
                string IblclassName = "bl" + codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;
                string pathToProject = path + ioManagerContract.VisualstudioSolutionServiceLibraryTestProjectFolder + className + SuffixDto.Instance().TestTextBox + ".cs";

                #region DataRepository Layer UnitTest

                if (ioManagerContract.UnitTest)
                {
                    using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                    {
                        streamWriter.WriteLine("// <auto-generated>");
                        streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                        streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                        streamWriter.WriteLine("//     the code is regenerated");
                        streamWriter.WriteLine("// </auto-generated>");
                        streamWriter.WriteLine();
                        streamWriter.WriteLine("namespace SolutionTest.ServiceLibraryTests");
                        streamWriter.WriteLine("{");
                        //// Create the header for the class
                        streamWriter.WriteLine("    using ServiceLibrary;");
                        streamWriter.WriteLine("    using Domain;");
                        if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                        {
                            streamWriter.WriteLine("    using NUnit.Framework;");
                        }
                        if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                        {
                            streamWriter.WriteLine("    using Microsoft.VisualStudio.TestTools.UnitTesting;");
                        }
                        streamWriter.WriteLine();

                        streamWriter.WriteLine("    /// <summary>");
                        streamWriter.WriteLine("    /// This class " + className + SuffixDto.Instance().TestTextBox);
                        streamWriter.WriteLine("    /// </summary>");
                        if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                        {
                            streamWriter.WriteLine("    [TestFixture]");
                        }
                        if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                        {
                            streamWriter.WriteLine("    [TestClass]");
                        }
                        streamWriter.WriteLine("    public class " + className + SuffixDto.Instance().TestTextBox);
                        streamWriter.WriteLine("    {");

                        if (crudYesNo)
                        {
                            CodeHammerServiceCreateInsertMethodTest(IblclassName, table, streamWriter);
                            CodeHammerServiceCreateUpdateMethodTest(IblclassName, table, streamWriter);
                            CodeHammerServiceCreateDeleteMethodTest(IblclassName, table, streamWriter);
                            CodeHammerServiceCreateSelectAllMethodTest(IblclassName, table, streamWriter);
                            CodeHammerServiceCreateSelectByIDTest(IblclassName, table, streamWriter);
                        }
                        //// Close out the class and namespace
                        streamWriter.WriteLine("    }");
                        streamWriter.WriteLine("}");
                    }
                    return true;
                }

                #endregion DataRepository Layer UnitTest
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw new Exception(ex.ToString());
            }

            return true;
        }

        /// <summary>
        /// Codes the hammer service library.
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
        public bool ServiceLibrary(List<CodeHammerTableDto> tableList, string container, bool selectTables, Dictionary<string, List<Dictionary<string, string>>> tableDic, string databaseName, CodeHammerTableDto table, bool crudYesNo, List<string> resultDataOptions, string path, string instanceCall)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().ServiceTextBox;
                string classNameSvc = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim());
                string classNameInterface = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().ServiceContractTextBox;
                string blclassName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;
                string IblclassName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().BlSuffixTextBox;

                string quates = @"""";

                string pathToProject = path + ioManagerContract.VisualstudioServiceLibraryProjectFolder + ioManagerContract.ServiceFolder + className + ".cs";

                using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                {
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace ServiceLibrary");
                    streamWriter.WriteLine("{");
                    //// Create the header for the class
                    streamWriter.WriteLine("    using Business;");
                    streamWriter.WriteLine("    using Domain;");
                    if (ioManagerContract.Log4Net)
                    {
                        streamWriter.WriteLine("    using Logging.Log4Net;");
                    }
                    streamWriter.WriteLine("    using System;");
                    streamWriter.WriteLine("    using System.Collections.Generic;");
                    streamWriter.WriteLine("    using System.Data;");
                    streamWriter.WriteLine("    using System.IO;");
                    streamWriter.WriteLine("    using System.Text;");
                    streamWriter.WriteLine("    using System.Linq;");
                    streamWriter.WriteLine("    using System.ServiceModel;");
                    streamWriter.WriteLine("    using System.ServiceModel.Activation;");
                    streamWriter.WriteLine("    using System.Web;");
                    streamWriter.WriteLine("    using System.Web.Script.Serialization;");
                    streamWriter.WriteLine("    using System.ServiceModel.Web;");
                    streamWriter.WriteLine("    using System.Net;");

                    streamWriter.WriteLine();

                    streamWriter.WriteLine("    /// <summary>");
                    streamWriter.WriteLine("    /// This class " + className);
                    streamWriter.WriteLine("    /// </summary>");
                    streamWriter.WriteLine("    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]");
                    streamWriter.WriteLine("    [ServiceBehavior(InstanceContextMode = " + ioManagerContract.InstanceCall + ")]");

                    if (ioManagerContract.Log4Net && ioManagerContract.UseIoC)
                    {
                        streamWriter.WriteLine("    public class " + className + ": " + classNameInterface);

                        streamWriter.WriteLine("    {");
                        streamWriter.WriteLine();
                        streamWriter.WriteLine("        /// <summary>");
                        streamWriter.WriteLine("        /// The log");
                        streamWriter.WriteLine("        /// </summary>");
                        streamWriter.WriteLine("        private readonly ILogger log;");
                    }

                    if (ioManagerContract.Log4Net && !ioManagerContract.UseIoC)
                    {
                        streamWriter.WriteLine("    public class " + className + ": " + classNameInterface);
                        streamWriter.WriteLine("    {");
                        streamWriter.WriteLine();
                        streamWriter.WriteLine("        /// <summary>");
                        streamWriter.WriteLine("        /// The log");
                        streamWriter.WriteLine("        /// </summary>");
                        streamWriter.WriteLine("        private readonly Logger log = new Logger();");
                    }

                    if (!ioManagerContract.Log4Net && ioManagerContract.UseIoC)
                    {
                        streamWriter.WriteLine("    public class " + className + ": " + classNameInterface);
                        streamWriter.WriteLine("    {");
                    }

                    if (!ioManagerContract.Log4Net && !ioManagerContract.UseIoC)
                    {
                        streamWriter.WriteLine("    public class " + className + ": " + classNameInterface);
                        streamWriter.WriteLine("    {");
                    }

                    var interfaceX = codeHammerDataUtilContract.CodeHammerFormatCamelDTO(blclassName);

                    streamWriter.WriteLine("        /// <summary>");
                    streamWriter.WriteLine("        /// This " + blclassName);
                    streamWriter.WriteLine("        /// </summary>");

                    if (ioManagerContract.UseIoC)
                    {
                        streamWriter.WriteLine("        private I" + blclassName + " " + IblclassName + ";");
                    }
                    else
                    {
                        streamWriter.WriteLine("        private " + blclassName + " " + IblclassName + " = new " + blclassName + "();");
                    }

                    streamWriter.WriteLine();
                    if (ioManagerContract.UseIoC && !ioManagerContract.Log4Net)
                    {
                        streamWriter.WriteLine("        /// <summary>");
                        streamWriter.WriteLine("        /// Initializes a new instance of the <see cref=" + quates + className + quates + "/> class.");
                        streamWriter.WriteLine("        /// </summary>");
                        streamWriter.WriteLine("        /// <param name=" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(className) + ">The " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(className) + ".</param>");
                        streamWriter.WriteLine("        public " + className + "(I" + blclassName + " " + IblclassName + ")");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("           this." + IblclassName + " = " + IblclassName + ";");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine();
                    }

                    if (ioManagerContract.UseIoC && ioManagerContract.Log4Net)
                    {
                        streamWriter.WriteLine("        /// <summary>");
                        streamWriter.WriteLine("        /// Initializes a new instance of the <see cref=" + quates + className + quates + "/> class.");
                        streamWriter.WriteLine("        /// </summary>");
                        streamWriter.WriteLine("        /// <param name=" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(className) + ">The " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(className) + ".</param>");
                        streamWriter.WriteLine("        /// <param name=" + quates + "log" + quates + ">The log.</param>");
                        streamWriter.WriteLine("        public " + className + "(I" + blclassName + " " + IblclassName + ", ILogger log)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("           this.log = log;");
                        streamWriter.WriteLine("           this." + IblclassName + " = " + IblclassName + ";");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine();
                    }

                    if (ioManagerContract.InstanceCall.Equals("InstanceContextMode.Single"))
                    {
                        if (!ioManagerContract.Log4Net)
                        {
                            streamWriter.WriteLine("        /// <summary>");
                            streamWriter.WriteLine("        /// Initializes a new instance of the <see cref=" + quates + className + quates + "/> class.");
                            streamWriter.WriteLine("        /// </summary>");
                            streamWriter.WriteLine("        public " + className + "()");
                            streamWriter.WriteLine("        {");
                            streamWriter.WriteLine("        }");
                        }
                    }

                    if (crudYesNo)
                    {
                        CodeHammerServiceCreateInsertMethod(IblclassName, table, streamWriter);
                        CodeHammerServiceCreateUpdateMethod(IblclassName, table, streamWriter);
                        CodeHammerServiceCreateDeleteMethod(IblclassName, table, streamWriter);
                        CodeHammerServiceCreateSelectByIDMethod(IblclassName, table, streamWriter);
                        CodeHammerServiceCreateSelectAllMethod(IblclassName, table, streamWriter);
                    }
                    //// Close out the class and namespace
                    streamWriter.WriteLine("    }");
                    streamWriter.WriteLine("}");
                }

                ////Generate SVC files
                //pathToProject = path + ioManagerContract.VisualstudioServiceHostServices + classNameSvc + ".svc";
                //using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                //{
                //    streamWriter.WriteLine("<!-- <auto-generated>");
                //    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                //    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                //    streamWriter.WriteLine("//     the code is regenerated");
                //    streamWriter.WriteLine("// </auto-generated>-->");
                //    if (ioManagerContract.UseIoC)
                //    {
                //        streamWriter.WriteLine("<%@ ServiceHost Language=" + quates + "C#" + quates + " Debug=" + quates + "true" + quates + " Service=" + quates + "ServiceLibrary" + quates + " Factory=" + quates + "Castle.Facilities.WcfIntegration.DefaultServiceHostFactory, Castle.Facilities.WcfIntegration" + quates + "%>");
                //    }
                //    else
                //    {
                //        streamWriter.WriteLine("<%@ ServiceHost Language=" + quates + "C#" + quates + " Debug=" + quates + "true" + quates + " Service=" + quates + "ServiceLibrary" + quates + " Factory=" + quates + "System.ServiceModel.Activation.WebScriptServiceHostFactory" + quates + "%>");
                //    }
                //    streamWriter.WriteLine("<%@ Assembly Name=" + quates + "ServiceLibrary" + quates + "%>");
                //}

                ////Read content from RouteConfig file
                string strRouteConfig = string.Empty;
                string routeConfig = codeHammerDataUtilContract.CodeHammerGetRouteConfig(out strRouteConfig);

                string strGlobal = string.Empty;
                string globalAsax = codeHammerDataUtilContract.CodeHammerGetGlobalCastle(out strGlobal);
                StringBuilder strGlobalBuilder = new StringBuilder();
                strGlobalBuilder.Length = 0;
                strGlobalBuilder.Capacity = 0;
                codeHammerDataUtilContract.MakeHelpPage = new StringBuilder();
                codeHammerDataUtilContract.MakeHelpPage.Length = 0;
                codeHammerDataUtilContract.MakeHelpPage.Capacity = 0;

                StringBuilder strGlobalRouteBuilder = new StringBuilder();
                strGlobalRouteBuilder.Length = 0;
                strGlobalRouteBuilder.Capacity = 0;

                pathToProject = path + ioManagerContract.VisualstudioServiceHostProjectName + "Global.asax";
                using (StreamWriter writer = new StreamWriter(pathToProject))
                {
                    writer.Write("<%@ Application Codebehind=" + quates + "Global.asax.cs" + quates + " Inherits=" + quates + "ServiceHost.Global" + quates + " Language=" + quates + "C#" + quates + " %>");
                }

                if (ioManagerContract.UseIoC && container.Equals("castle"))
                {
                    strGlobalBuilder.AppendLine("this.Container = new WindsorContainer();");
                    strGlobalBuilder.AppendLine("            Container.AddFacility<WcfFacility>();");
                }

                int countlines = 0;

                foreach (CodeHammerTableDto col in tableList)
                {
                    string item = codeHammerDataUtilContract.CodeHammerFormatPascal(col.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim().Replace(" ", string.Empty).Trim());
                    if (ioManagerContract.UseIoC && container.Equals("castle"))
                    {
                        if (ioManagerContract.Log4Net)
                        {
                            if (!strGlobalBuilder.ToString().Contains(".ImplementedBy<Logger>()"))
                            {
                                strGlobalBuilder.AppendLine();
                                strGlobalBuilder.AppendLine("            this.Container.Register(");
                                strGlobalBuilder.AppendLine("            Component");
                                strGlobalBuilder.AppendLine("            .For<ILogger>()");
                                strGlobalBuilder.AppendLine("            .ImplementedBy<Logger>()");
                                strGlobalBuilder.AppendLine("            .Named(" + quates + "Logger" + quates + ").LifestylePerWcfOperation());");
                                strGlobalBuilder.AppendLine();
                            }
                        }

                        if (!ioManagerContract.EmptyDataLayerCheckBox)
                        {
                            if (!strGlobalBuilder.ToString().Contains(".ImplementedBy<DatabaseManager>()"))
                            {
                                strGlobalBuilder.AppendLine("            this.Container.Register(");
                                strGlobalBuilder.AppendLine("            Component");
                                strGlobalBuilder.AppendLine("            .For<IDatabaseManager>()");
                                strGlobalBuilder.AppendLine("            .ImplementedBy<DatabaseManager>()");
                                strGlobalBuilder.AppendLine("            .Named(" + quates + "DatabaseManager" + quates + ").LifestylePerWcfOperation());");
                                strGlobalBuilder.AppendLine();
                            }
                        }

                        strGlobalBuilder.AppendLine("            this.Container.Register(");
                        strGlobalBuilder.AppendLine("            Component");
                        strGlobalBuilder.AppendLine("            .For<I" + item + SuffixDto.Instance().DalTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .ImplementedBy<" + item + SuffixDto.Instance().DalTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .Named(" + quates + item + SuffixDto.Instance().DalTextBox + quates + ").LifestylePerWcfOperation());");
                        strGlobalBuilder.AppendLine();

                        strGlobalBuilder.AppendLine("            this.Container.Register(");
                        strGlobalBuilder.AppendLine("            Component");
                        strGlobalBuilder.AppendLine("            .For<I" + item + SuffixDto.Instance().BlSuffixTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .ImplementedBy<" + item + SuffixDto.Instance().BlSuffixTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .Named(" + quates + item + SuffixDto.Instance().BlSuffixTextBox + quates + ").LifestylePerWcfOperation());");
                        strGlobalBuilder.AppendLine();

                        strGlobalBuilder.AppendLine("            this.Container.Register(");
                        strGlobalBuilder.AppendLine("            Component");
                        strGlobalBuilder.AppendLine("            .For<" + item + SuffixDto.Instance().ServiceContractTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .ImplementedBy<" + item + SuffixDto.Instance().ServiceTextBox + ">()");
                        strGlobalBuilder.AppendLine("            .Named(" + quates + "" + item + SuffixDto.Instance().ServiceTextBox + quates + ").LifeStyle.Transient);");
                    }

                    ////Replace content in RouteConfig.cs
                    ////RouteTable.Routes.Add

                    if (countlines == 0)
                    {
                        if (ioManagerContract.Crud)
                        {
                            if (ioManagerContract.UseIoC && container.Equals("castle"))
                            {
                                strGlobalRouteBuilder.AppendLine("RouteTable.Routes.Add(new ServiceRoute(" + quates + "api" + item + "" + quates + ", new WindsorServiceHostFactory<RestServiceModel>(), typeof(ServiceLibrary." + codeHammerDataUtilContract.CodeHammerFormatPascal(item) + SuffixDto.Instance().ServiceContractTextBox + ")));");
                                codeHammerDataUtilContract.MakeHelpPage.AppendLine("http://localhost:8081/" + "api" + item + "/help");
                            }
                            else
                            {
                                strGlobalRouteBuilder.AppendLine("RouteTable.Routes.Add(new ServiceRoute(" + quates + "api" + item + "" + quates + ", new WebServiceHostFactory(), typeof(ServiceLibrary." + codeHammerDataUtilContract.CodeHammerFormatPascal(item) + SuffixDto.Instance().ServiceTextBox + ")));");
                                codeHammerDataUtilContract.MakeHelpPage.AppendLine("http://localhost:8081/" + "api" + item + "/help");
                            }
                        }
                    }
                    else
                    {
                        if (ioManagerContract.Crud)
                        {
                            if (ioManagerContract.UseIoC && container.Equals("castle"))
                            {
                                strGlobalRouteBuilder.AppendLine("            RouteTable.Routes.Add(new ServiceRoute(" + quates + "api" + item + "" + quates + ", new WindsorServiceHostFactory<RestServiceModel>(), typeof(ServiceLibrary." + codeHammerDataUtilContract.CodeHammerFormatPascal(item) + SuffixDto.Instance().ServiceContractTextBox + ")));");
                                codeHammerDataUtilContract.MakeHelpPage.AppendLine("http://localhost:8081/" + "api" + item + "/help");
                            }
                            else
                            {
                                strGlobalRouteBuilder.AppendLine("            RouteTable.Routes.Add(new ServiceRoute(" + quates + "api" + item + "" + quates + ", new WebServiceHostFactory(), typeof(ServiceLibrary." + codeHammerDataUtilContract.CodeHammerFormatPascal(item) + SuffixDto.Instance().ServiceTextBox + ")));");
                                codeHammerDataUtilContract.MakeHelpPage.AppendLine("http://localhost:8081/" + "api" + item + "/help");
                            }
                        }
                    }

                    countlines++;
                }

                countlines = 0;
                pathToProject = path + ioManagerContract.VisualstudioServiceHostProjectName + "Global.asax.cs";

                string usingCastleWcf = "\n    using Castle.Facilities.WcfIntegration;\n" +
                                         "    using Castle.Facilities.WcfIntegration.Rest;\n" +
                                         "    using Castle.MicroKernel.Registration;\n" +
                                         "    using Castle.Windsor;";

                string InitCastleWcf = "/// <summary>\n" +
                                         "         /// Gets or sets the container.\n" +
                                         "         /// </summary>\n" +
                                         "         /// <value>\n" +
                                         "         /// The Container.\n" +
                                         "         /// </value>\n" +
                                         "         public WindsorContainer Container { get; protected set; }\n";

                string newInit = "this.Container = new WindsorContainer();\n" +
                                         "            if (this.Container != null)\n" +
                                         "            {\n" +
                                         "                this.Container.Dispose();\n" +
                                         "            }\n";

                string registerCastle = "RegisterWindsorContainer();";

                string registerCastleFunction = "/// <summary>\n" +
                                                        "         /// Registers the container.\n" +
                                                        "         /// </summary>\n" +
                                                        "         private void RegisterWindsorContainer()\n" +
                                                        "         {\n" +
                                                        "            <CONTAINER.REGISTER>       }\n";

                string globalCastleWcf = string.Empty;
                if (ioManagerContract.UseIoC && container.Equals("castle"))
                {
                    if (!ioManagerContract.Log4Net)
                    {
                        globalCastleWcf = globalAsax.Replace("<USINGCASTLEWCF>",
                            usingCastleWcf).Replace("<INITCASTLEWCF>",
                            InitCastleWcf).Replace("<NEWINITCASTLE>",
                            newInit).Replace("<REGISTERWINDSORCONTAINER>",
                            registerCastle).Replace("<REGISTERWINDSORCONTAINERFUNCTION>",
                            registerCastleFunction);
                    }

                    if (ioManagerContract.Log4Net)
                    {
                        globalCastleWcf = globalAsax.Replace("<USINGCASTLEWCF>",
                            usingCastleWcf).Replace("<LOG4NET>",
                            "using Logging.Log4Net;").Replace("<INITCASTLEWCF>",
                            InitCastleWcf).Replace("<NEWINITCASTLE>",
                            newInit).Replace("<REGISTERWINDSORCONTAINER>",
                            registerCastle).Replace("<REGISTERWINDSORCONTAINERFUNCTION>",
                            registerCastleFunction);
                    }
                    else
                    {
                        globalCastleWcf = globalAsax.Replace("<USINGCASTLEWCF>",
                            usingCastleWcf).Replace("<LOG4NET>",
                            string.Empty).Replace("<INITCASTLEWCF>",
                            InitCastleWcf).Replace("<NEWINITCASTLE>",
                            newInit).Replace("<REGISTERWINDSORCONTAINER>",
                            registerCastle).Replace("<REGISTERWINDSORCONTAINERFUNCTION>",
                            registerCastleFunction);
                    }
                }
                else
                {
                    globalCastleWcf = globalAsax.Replace("<USINGCASTLEWCF>",
                        "\n    using System.Data;").Replace("<LOG4NET>",
                        string.Empty).Replace("<INITCASTLEWCF>",
                        string.Empty).Replace("<NEWINITCASTLE>",
                        string.Empty).Replace("<REGISTERWINDSORCONTAINER>",
                        string.Empty).Replace("<REGISTERWINDSORCONTAINERFUNCTION>",
                        string.Empty);
                }

                string globalAsaxCS = string.Empty;
                string registerConfigCS = string.Empty;
                string globalAsaxReplaceCS = string.Empty;

                if (ioManagerContract.UseIoC && container.Equals("castle"))
                {
                    globalAsaxCS = globalCastleWcf.Replace("<CONTAINER.REGISTER>", strGlobalBuilder.ToString());
                    registerConfigCS = routeConfig.Replace("<CASTLE>",
                                                           "\n    using Castle.Facilities.WcfIntegration;\n    using Castle.Facilities.WcfIntegration.Rest;").Replace("<REGISTERROUTES>",
                                                           strGlobalRouteBuilder.ToString());

                    globalAsaxReplaceCS = globalAsaxCS;
                }
                else
                {
                    globalAsaxCS = globalCastleWcf.Replace("<CONTAINER.REGISTER>", string.Empty);

                    registerConfigCS = routeConfig.Replace("<CASTLE>",
                                       "").Replace("<REGISTERROUTES>",
                                       strGlobalRouteBuilder.ToString());

                    globalAsaxReplaceCS = globalAsaxCS;
                }

                string pathToProjectRouteConfig = path + ioManagerContract.VisualstudioServiceHostProjectName + "App_Start\\RouteConfig.cs";

                using (StreamWriter writer = new StreamWriter(pathToProjectRouteConfig))
                {
                    string noCrud = string.Empty;
                    noCrud = registerConfigCS;
                    writer.Write(noCrud);
                }

                using (StreamWriter writer = new StreamWriter(pathToProject))
                {
                    string noCrud = string.Empty;
                    if (!ioManagerContract.Crud)
                    {
                        noCrud = globalAsaxReplaceCS.Replace("using Business;",
                            string.Empty).Replace("using Data;",
                            string.Empty).Replace("using ServiceLibrary;",
                            string.Empty);
                    }
                    else
                    {
                        if (ioManagerContract.EmptyDataLayerCheckBox)
                        {
                            noCrud = globalAsaxReplaceCS.Replace("using Data.Infrastructure;",
                                "\t");
                        }

                        else
                        {
                            if (!ioManagerContract.UseIoC)
                            {
                                noCrud = globalAsaxReplaceCS.Replace("using Data.Infrastructure;",
                                    "\t");
                            }
                            else
                            {
                                noCrud = globalAsaxReplaceCS;
                            }
                        }
                    }


                    writer.Write(noCrud);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        #region Service Layer

        /// <summary>
        /// Codes the hammer i service create delete method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerIServiceCreateDeleteMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " remove by id.");
                streamWriter.WriteLine("        /// </summary>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                streamWriter.WriteLine("        [OperationContract]");
                streamWriter.WriteLine("        [WebInvoke(Method = " + quates + "DELETE" + quates + ",");

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Json)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Json,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Json,");
                }

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Xml)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Xml,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Xml,");
                }

                streamWriter.WriteLine("        BodyStyle = WebMessageBodyStyle.Bare,");
                string paramId = string.Empty;
                StringBuilder strX = new StringBuilder();

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("{" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "}" + ",");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 1);
                streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + "DataContract/" + paramId + "" + quates + "), Description(" + quates + "Delete data by id" + quates + ")]");

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 2);

                streamWriter.WriteLine("        int " + className + "RemoveByID(" + paramId + ");");
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create delete method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateDeleteMethodTest(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());// +;//// +;

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + className + "RemoveByID test.");
                streamWriter.WriteLine("        /// </summary>");
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                {
                    streamWriter.WriteLine("        [Test]");
                }
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                {
                    streamWriter.WriteLine("        [TestMethod]");
                }
                streamWriter.WriteLine("        public void " + className + "ServiceRemoveByIDTest()");
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            Assert.Fail();");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer i service create insert method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerIServiceCreateInsertMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());

                string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatCamelDTO(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DataContractTextBox;
                var type = codeHammerDataUtilContract.CodeHammerGetCsType(table.CodeHammerPrimaryKeys[0]);
                string quates = @"""";

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName.Replace("_", "") + " save.");
                streamWriter.WriteLine("        /// </summary>");

                streamWriter.WriteLine("        /// <param name=" + quates + "responseDataStream" + quates + ">The responseDataStream.</param>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!item.CodeHammerIsIdentity)
                            streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in saving</exception>");
                streamWriter.WriteLine("        /// <exception cref=" + quates + "FaultException{System.String}" + quates + "></exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");
                streamWriter.WriteLine("        [OperationContract]");
                streamWriter.WriteLine("        [WebInvoke(Method = " + quates + "POST" + quates + ",");
                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Json)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Json,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Json,");
                }

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Xml)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Xml,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Xml,");
                }
                streamWriter.WriteLine("        BodyStyle = WebMessageBodyStyle.Bare,");

                if (!table.CodeHammerColumns.Any(x => x.CodeHammerIsIdentity))
                {
                    StringBuilder strX = new StringBuilder();
                    strX.Length = 0;
                    strX.Capacity = 0;
                    foreach (CodeHammerColumn item in table.CodeHammerColumns)
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            strX.Append("{" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "}" + ",");
                        }
                    }

                    string paramId = strX.ToString().Remove(strX.Length - 1);
                    streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + SuffixDto.Instance().DataContractTextBox + "/" + paramId + "" + quates + "), Description(" + quates + "Save data from stream" + quates + ")]");
                }
                else
                {
                    streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + SuffixDto.Instance().DataContractTextBox + quates + "), Description(" + quates + "Save data from stream" + quates + ")]");
                }

                StringBuilder strXx = new StringBuilder();
                strXx.Length = 0;
                strXx.Capacity = 0;

                if (table.CodeHammerPrimaryKeys.Count > 0)
                {
                    foreach (CodeHammerColumn item in table.CodeHammerColumns)
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            if (!item.CodeHammerIsIdentity)
                                strXx.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                        }
                    }

                    if (strXx.Length != 0)
                    {
                        string paramId = strXx.ToString().Remove(strXx.Length - 2);

                        streamWriter.WriteLine("        int " + className + "Save(Stream responseDataStream, " + paramId + ");");
                    }
                    else
                    {
                        streamWriter.WriteLine("        int " + className + "Save(Stream responseDataStream);");
                    }
                }
                else
                {
                    streamWriter.WriteLine("        int " + className + "Save(Stream responseDataStream);");
                }

                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create insert method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateInsertMethodTest(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());// +;//// +;

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + className + "Save test.");
                streamWriter.WriteLine("        /// </summary>");
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                {
                    streamWriter.WriteLine("        [Test]");
                }
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                {
                    streamWriter.WriteLine("        [TestMethod]");
                }
                streamWriter.WriteLine("        public void " + className + "SaveTest()");
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            Assert.Fail();");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer i service create select all method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerIServiceCreateSelectAllMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " get all.");
                streamWriter.WriteLine("        /// </summary>");
                streamWriter.WriteLine("        /// <param name=" + quates + "PageSize" + quates + ">The PageSize.</param>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                streamWriter.WriteLine("        [OperationContract]");
                streamWriter.WriteLine("        [WebInvoke(Method = " + quates + "GET" + quates + ",");
                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Json)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Json,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Json,");
                }

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Xml)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Xml,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Xml,");
                }
                streamWriter.WriteLine("        BodyStyle = WebMessageBodyStyle.Bare,");
                streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + SuffixDto.Instance().DataContractTextBox + "GetByPageSize/{PageSize}" + quates + "), Description(" + quates + "Get data by pagesize" + quates + ")]");
                streamWriter.WriteLine("        List<" + className + SuffixDto.Instance().DataContractTextBox + "> " + className + "GetAll(string pageSize);");
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create select all method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateSelectAllMethodTest(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());// +;//// +;

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + className + "GetAll test.");
                streamWriter.WriteLine("        /// </summary>");
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                {
                    streamWriter.WriteLine("        [Test]");
                }
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                {
                    streamWriter.WriteLine("        [TestMethod]");
                }
                streamWriter.WriteLine("        public void " + className + "GetAllTest()");
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            Assert.Fail();");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer i service create select by id.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerIServiceCreateSelectByID(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " get by id.");
                streamWriter.WriteLine("        /// </summary>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <returns>return true if success</returns>");
                streamWriter.WriteLine("        [OperationContract]");
                streamWriter.WriteLine("        [WebInvoke(Method = " + quates + "GET" + quates + ",");
                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Json)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Json,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Json,");
                }

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Xml)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Xml,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Xml,");
                }
                streamWriter.WriteLine("        BodyStyle = WebMessageBodyStyle.Bare,");
                StringBuilder strX = new StringBuilder();
                string paramId = string.Empty;

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("{" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "}" + ",");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 1);
                streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + SuffixDto.Instance().DataContractTextBox + "/" + paramId + "" + quates + "), Description(" + quates + "Get the data by id" + quates + ")]");

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 2);

                streamWriter.WriteLine("        " + className + SuffixDto.Instance().DataContractTextBox + " " + className + "GetByID(" + paramId + ");");
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create select by identifier test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateSelectByIDTest(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());// +;//// +;

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + className + "GetByID test.");
                streamWriter.WriteLine("        /// </summary>");
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                {
                    streamWriter.WriteLine("        [Test]");
                }
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                {
                    streamWriter.WriteLine("        [TestMethod]");
                }
                streamWriter.WriteLine("        public void " + className + "GetByIDTest()");
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            Assert.Fail();");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer i service create update method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns></returns>
        public bool CodeHammerIServiceCreateUpdateMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                var type = codeHammerDataUtilContract.CodeHammerGetCsType(table.CodeHammerPrimaryKeys[0]);
                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " set by id.");
                streamWriter.WriteLine("        /// </summary>");
                streamWriter.WriteLine("        /// <param name=" + quates + "responseDataStream" + quates + ">The responseDataStream.</param>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <returns>return true if success</returns>");
                streamWriter.WriteLine("        [OperationContract]");
                streamWriter.WriteLine("        [WebInvoke(Method = " + quates + "PUT" + quates + ",");
                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Json)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Json,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Json,");
                }

                if (ioManagerContract.StreamFormatTypeEnum == IOManager.StreamFormat.Xml)
                {
                    streamWriter.WriteLine("        ResponseFormat = WebMessageFormat.Xml,");
                    streamWriter.WriteLine("        RequestFormat = WebMessageFormat.Xml,");
                }
                streamWriter.WriteLine("        BodyStyle = WebMessageBodyStyle.Bare,");
                StringBuilder strX = new StringBuilder();
                string paramId = string.Empty;

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("{" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "}" + ",");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 1);
                streamWriter.WriteLine("        UriTemplate = " + quates + "/" + className + SuffixDto.Instance().DataContractTextBox + "/" + paramId + "" + quates + "), Description(" + quates + "Update data from stream" + quates + ")]");

                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                paramId = strX.ToString().Remove(strX.Length - 2);

                streamWriter.WriteLine("       int " + className + "SetByID(Stream responseDataStream, " + paramId + ");");

                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create update method test.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>
        /// if sucess then return true
        /// </returns>
        public bool CodeHammerServiceCreateUpdateMethodTest(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());// +;//// +;

                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + className + "SetByID test.");
                streamWriter.WriteLine("        /// </summary>");
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.NUnit)
                {
                    streamWriter.WriteLine("        [Test]");
                }
                if (ioManagerContract.UnitTestTypeEnum == IOManager.UnitTestEnum.VSUnitTest)
                {
                    streamWriter.WriteLine("        [TestMethod]");
                }
                streamWriter.WriteLine("        public void " + className + "SetByIDTest()");
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            Assert.Fail();");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create delete method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateDeleteMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string variableName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;
                string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DataContractTextBox;

                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " remove by id.");
                streamWriter.WriteLine("        /// </summary>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!item.CodeHammerIsIdentity)
                            streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in removing by id</exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                StringBuilder strX = new StringBuilder();
                strX.Length = 0;
                strX.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                string paramId = strX.ToString().Remove(strX.Length - 2);

                streamWriter.WriteLine("        public int " + className + "RemoveByID(" + paramId + ")");

                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            int result = 1;");
                streamWriter.WriteLine("            string systemMessage = string.Empty;");
                streamWriter.WriteLine("            string nestedSystemMessage = string.Empty;");
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            streamWriter.WriteLine("            " + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value;");
                        }
                    }
                }

                streamWriter.WriteLine();
                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");

                StringBuilder strDto = new StringBuilder();
                strDto.Length = 0;
                strDto.Capacity = 0;

                StringBuilder strDTO = new StringBuilder();
                strDTO.Length = 0;
                strDTO.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                        {
                            streamWriter.WriteLine("                if (!" + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + ".TryParse(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value))");
                            streamWriter.WriteLine("                {");
                            streamWriter.WriteLine("                    throw new FaultException(" + quates + "Unable to parse ID: " + quates + "+ " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ");");
                            streamWriter.WriteLine("                }");
                            if (table.CodeHammerColumns.Count > 1)
                            {
                                streamWriter.WriteLine();
                            }
                        }
                    }
                }

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "Value.ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                        }
                        else
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                        }
                    }
                }

                string paramStr = strDto.ToString().Remove(strDto.Length - 3);
                streamWriter.WriteLine("                " + className + SuffixDto.Instance().DtoTextBox + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "= new " + className + SuffixDto.Instance().DtoTextBox + "()");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                };");
                streamWriter.WriteLine();
                streamWriter.WriteLine("                if (!" + IblclassName + "." + className + "RemoveByID(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + ", out nestedSystemMessage))");
                streamWriter.WriteLine("                {");

                StringBuilder strBuildstring1 = new StringBuilder();
                strBuildstring1.Append("    systemMessage = ");

                strBuildstring1.Append("\"Error in method: " + className + "RemoveByID \"");
                strBuildstring1.Append(" + ");
                strBuildstring1.Append(" nestedSystemMessage;");

                streamWriter.WriteLine("                " + strBuildstring1.ToString());
                streamWriter.WriteLine("                    result = 0;");

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                    log.LogError(typeof(" + IblclassName + "), " + quates + "Error in removing by id" + quates + ", new Exception(systemMessage));");
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in removing by id" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in removing by id" + quates + ", new FaultCode(systemMessage));");
                }

                streamWriter.WriteLine("                }");
                streamWriter.WriteLine();

                streamWriter.WriteLine("            }");
                streamWriter.WriteLine("            catch (FaultException fe)");
                streamWriter.WriteLine("            {");
                streamWriter.WriteLine("                result = 0;");
                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + IblclassName + "), " + quates + "Error in removing by id" + quates + ", fe);");
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Message, HttpStatusCode.InternalServerError);");
                }
                else
                {
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Code.Name, HttpStatusCode.InternalServerError);");
                }
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("            return result;");

                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Codes the hammer service create insert method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>
        /// if sucess then return true
        /// </returns>
        public bool CodeHammerServiceCreateInsertMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string variableName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;
                string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DataContractTextBox;
                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " save.");
                streamWriter.WriteLine("        /// </summary>");

                streamWriter.WriteLine("        /// <param name=" + quates + "responseDataStream" + quates + ">The responseDataStream.</param>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!item.CodeHammerIsIdentity)
                            streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in saving</exception>");
                streamWriter.WriteLine("        /// <exception cref=" + quates + "FaultException{System.String}" + quates + "></exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                StringBuilder strX = new StringBuilder();
                strX.Length = 0;
                strX.Capacity = 0;

                if (table.CodeHammerPrimaryKeys.Count > 0)
                {
                    foreach (CodeHammerColumn item in table.CodeHammerColumns)
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            if (!item.CodeHammerIsIdentity)
                                strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + ", ");
                        }
                    }

                    if (strX.Length != 0)
                    {
                        string paramId = strX.ToString().Remove(strX.Length - 2);

                        streamWriter.WriteLine("        public int " + className + "Save(Stream responseDataStream, " + paramId + ")");
                    }
                    else
                    {
                        streamWriter.WriteLine("        public int " + className + "Save(Stream responseDataStream)");
                    }
                }
                else
                {
                    streamWriter.WriteLine("        public int " + className + "Save(Stream responseDataStream)");
                }

                streamWriter.WriteLine("        {");

                streamWriter.WriteLine("            StreamReader reader = new StreamReader(responseDataStream);");
                streamWriter.WriteLine("            string dataStream = reader.ReadToEnd();");
                streamWriter.WriteLine("            int result = 0;");
                streamWriter.WriteLine("            string systemMessage = string.Empty;");
                streamWriter.WriteLine("            string nestedSystemMessage = string.Empty;");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            if (!item.CodeHammerIsIdentity)
                            {
                                streamWriter.WriteLine("            " + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + "Value;");
                            }
                        }
                    }
                }

                streamWriter.WriteLine("            " + codeHammerDataUtilContract.CodeHammerFormatPascal(variableNameDataContract).Replace("_", "") + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract).Replace("_", "") + ";");
                streamWriter.WriteLine();
                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");

                streamWriter.WriteLine("                " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract).Replace("_", "") + " = new JavaScriptSerializer().Deserialize<" + className + SuffixDto.Instance().DataContractTextBox + ">(dataStream);");
                streamWriter.WriteLine("            }");

                streamWriter.WriteLine("            catch (Exception ex)");
                streamWriter.WriteLine("            {");
                streamWriter.WriteLine("                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);");
                streamWriter.WriteLine("            }");

                streamWriter.WriteLine();

                streamWriter.WriteLine("            if (" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract).Replace("_", "") + " == null)");
                streamWriter.WriteLine("            {");

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + className.Replace("_", "") + SuffixDto.Instance().BlSuffixTextBox + "), " + quates + "Error in saving" + quates + ", new Exception(" + quates + "Could not deserialize data stream into " + variableNameDataContract.Replace("_", "") + " object" + quates + "));");
                    streamWriter.WriteLine("                throw new FaultException(" + quates + "Could not deserialize data stream string" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                throw new FaultException(" + quates + "Could not deserialize data stream into " + variableNameDataContract.Replace("_", "") + " object" + quates + ");");
                }

                streamWriter.WriteLine("            }");

                streamWriter.WriteLine();
                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");

                if (table.CodeHammerColumns.Count > 1)
                {
                    foreach (CodeHammerColumn item in table.CodeHammerColumns)
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                            {
                                if (!item.CodeHammerIsIdentity)
                                {
                                    streamWriter.WriteLine("                if (!" + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + ".TryParse(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value))");
                                    streamWriter.WriteLine("                {");
                                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Unable to parse ID: " + quates + "+ " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ");");
                                    streamWriter.WriteLine("                }");
                                    if (table.CodeHammerColumns.Count > 1)
                                    {
                                        streamWriter.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }

                StringBuilder strDto = new StringBuilder();
                strDto.Length = 0;
                strDto.Capacity = 0;

                if (table.CodeHammerColumns.Count > 1)
                {
                    foreach (CodeHammerColumn item in table.CodeHammerColumns)
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            if (!item.CodeHammerIsIdentity)
                            {
                                if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                                {
                                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + "Value.ToString()),");
                                    }
                                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + "Value,");
                                    }
                                    else
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "").Replace("_", "") + "Value,");
                                    }
                                }
                                else
                                {
                                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + ".ToString()),");
                                    }
                                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                                    }
                                    else
                                    {
                                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "").Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                                    }
                                }
                            }
                        }
                        else
                        {
                            strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + "." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ",");
                        }
                    }
                }

                List<string> checkNames = new List<string>();
                checkNames.Clear();
                string paramStr = string.Empty;
                if (strDto.Length == 0)
                {
                    paramStr = "                    //***MISSING VARIABLES***";
                }
                else
                {
                    paramStr = strDto.ToString().Remove(strDto.Length - 3);
                }
                streamWriter.WriteLine("                " + className + SuffixDto.Instance().DtoTextBox + " " + variableName.Replace("_", "") + " = new " + className + SuffixDto.Instance().DtoTextBox + "()");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                };");

                streamWriter.WriteLine();
                streamWriter.WriteLine("                if (!" + IblclassName + "." + className + "Save(" + variableName.Replace("_", "") + ", out nestedSystemMessage))");
                streamWriter.WriteLine("                {");

                StringBuilder strBuildstring1 = new StringBuilder();
                strBuildstring1.Append("    systemMessage = ");
                strBuildstring1.Append("\"Error in method: " + className + "Save \"");
                strBuildstring1.Append(" + ");
                strBuildstring1.Append(" nestedSystemMessage;");

                streamWriter.WriteLine("                " + strBuildstring1.ToString());
                streamWriter.WriteLine("                    result = 0;");

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                    log.LogError(typeof(" + IblclassName + "), " + quates + "Error in saving" + quates + ", new Exception(systemMessage));");
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in saving" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in saving" + quates + ", new FaultCode(systemMessage));");
                }

                streamWriter.WriteLine("                }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("                result = 1;");

                streamWriter.WriteLine("            }");
                streamWriter.WriteLine("            catch (FaultException fe)");
                streamWriter.WriteLine("            {");
                streamWriter.WriteLine("                result = 0;");
                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + IblclassName + "), " + quates + "Error in saving" + quates + ", fe);");
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Message, HttpStatusCode.InternalServerError);");
                }
                else
                {
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Code.Name, HttpStatusCode.InternalServerError);");
                }
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("            return result;");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw;
            }
        }

        /// <summary>
        /// Creates the select all method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Exception</exception>
        public bool CodeHammerServiceCreateSelectAllMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
            string variableName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;
            string quates = @"""";
            string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;

            try
            {
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in selecting by pagesize</exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                streamWriter.WriteLine("        public List<" + className + SuffixDto.Instance().DataContractTextBox + "> " + className + "GetAll(string pageSize)");

                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            string systemMessage = string.Empty;");
                streamWriter.WriteLine("            string nestedSystemMessage = string.Empty;");
                streamWriter.WriteLine();

                streamWriter.WriteLine("            List<" + className + SuffixDto.Instance().DtoTextBox + "> " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List = null;");
                streamWriter.WriteLine("            int pageSizeValue;");
                streamWriter.WriteLine();

                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");

                StringBuilder strDto = new StringBuilder();
                strDto.Length = 0;
                strDto.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strDto.AppendLine("                   " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + variableNameDataContract + "." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ",");
                    }
                }

                streamWriter.WriteLine("                if (!int.TryParse(pageSize, out pageSizeValue))");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine("                    throw new FaultException(" + quates + "Unable to parse value: " + quates + " + pageSize);");
                streamWriter.WriteLine("                }");

                streamWriter.WriteLine();

                string paramStr = strDto.ToString().Remove(strDto.Length - 3);
                streamWriter.WriteLine("                " + className + SuffixDto.Instance().DtoTextBox + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + " = new " + className + SuffixDto.Instance().DtoTextBox + "()");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine("                    PageSize = pageSizeValue");
                streamWriter.WriteLine("                };");
                streamWriter.WriteLine();

                streamWriter.WriteLine("                if (!" + IblclassName + "." + className + "GetAll(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List, out nestedSystemMessage))");
                streamWriter.WriteLine("                {");

                StringBuilder strBuildstring1 = new StringBuilder();
                strBuildstring1.Append("    systemMessage = ");

                strBuildstring1.Append("\"Error in method: " + className + "GetAll \"");
                strBuildstring1.Append(" + ");
                strBuildstring1.Append(" nestedSystemMessage;");

                streamWriter.WriteLine("                " + strBuildstring1.ToString());

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                    log.LogError(typeof(" + IblclassName + "), " + quates + "Error in selecting by pagesize" + quates + ", new Exception(systemMessage));");
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in selecting by pagesize" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in selecting by pagesize" + quates + ", new FaultCode(systemMessage));");
                }

                streamWriter.WriteLine("                }");
                streamWriter.WriteLine();

                strDto.Length = 0;
                strDto.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                    }
                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = new Guid(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                    }

                    if (!strDto.ToString().Contains(codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "")))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ",");
                    }
                }

                paramStr = strDto.ToString().Remove(strDto.Length - 3);

                streamWriter.WriteLine("                    var dtResult = from dr in " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List");

                streamWriter.WriteLine("                        select new " + className + SuffixDto.Instance().DataContractTextBox + "()");
                streamWriter.WriteLine("                        {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                        };");
                streamWriter.WriteLine("                    return dtResult.ToList();");

                streamWriter.WriteLine("            }");
                streamWriter.WriteLine("            catch (FaultException fe)");
                streamWriter.WriteLine("            {");
                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + IblclassName + "), " + quates + "Error in selecting ny pagesize" + quates + ", fe);");
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Message, HttpStatusCode.InternalServerError);");
                }
                else
                {
                    streamWriter.WriteLine("                throw new WebFaultException<string>(fe.Code.Name, HttpStatusCode.InternalServerError);");
                }
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Codes the hammer service create select by id method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Exception</exception>
        public bool CodeHammerServiceCreateSelectByIDMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
            string variableName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;
            string quates = @"""";
            string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;

            //// Append the method header
            streamWriter.WriteLine("        /// <summary>");
            streamWriter.WriteLine("        /// " + table.CodeHammerName + " get by id.");
            streamWriter.WriteLine("        /// </summary>");

            try
            {
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in selecting by id</exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                StringBuilder strX = new StringBuilder();
                strX.Length = 0;
                strX.Capacity = 0;
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                string paramId = strX.ToString().Remove(strX.Length - 2);
                streamWriter.WriteLine("        public " + className + SuffixDto.Instance().DataContractTextBox + " " + className + "GetByID(" + paramId + ")");

                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            string systemMessage = string.Empty;");
                streamWriter.WriteLine("            string nestedSystemMessage = string.Empty;");
                streamWriter.WriteLine();

                streamWriter.WriteLine("            List<" + className + SuffixDto.Instance().DtoTextBox + "> " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List = null;");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("            " + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value;");
                    }
                }
                streamWriter.WriteLine();

                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");

                StringBuilder strDto = new StringBuilder();
                strDto.Length = 0;
                strDto.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            streamWriter.WriteLine("                if (!" + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + ".TryParse(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value))");
                            streamWriter.WriteLine("                {");
                            streamWriter.WriteLine("                    throw new FaultException(" + quates + "Unable to parse ID: " + quates + "+ " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ");");
                            streamWriter.WriteLine("                }");
                            if (table.CodeHammerColumns.Count > 1)
                            {
                                streamWriter.WriteLine();
                            }
                        }
                    }
                }

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "Value.ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                        }
                        else
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                        }
                    }
                }

                string paramStr = strDto.ToString().Remove(strDto.Length - 3);
                streamWriter.WriteLine("                " + className + SuffixDto.Instance().DtoTextBox + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + " = new " + className + SuffixDto.Instance().DtoTextBox + "()");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                };");
                streamWriter.WriteLine();

                streamWriter.WriteLine("                if (!" + IblclassName + "." + className + "GetByID(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List, out nestedSystemMessage))");
                streamWriter.WriteLine("                {");

                StringBuilder strBuildstring1 = new StringBuilder();
                strBuildstring1.Append("    systemMessage = ");

                strBuildstring1.Append("\"Error in method: " + className + "GetByID \"");
                strBuildstring1.Append(" + ");
                strBuildstring1.Append(" nestedSystemMessage;");

                streamWriter.WriteLine("                " + strBuildstring1.ToString());

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                    log.LogError(typeof(" + IblclassName + "), " + quates + "Error in selecting by id" + quates + ", new Exception(systemMessage));");
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in selecting by id" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in selecting by id" + quates + ", new FaultCode(systemMessage));");
                }

                streamWriter.WriteLine("                }");
                streamWriter.WriteLine();

                strDto.Length = 0;
                strDto.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                    }
                    if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = new Guid(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                    }

                    if (!strDto.ToString().Contains(codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "")))
                    {
                        strDto.AppendLine("                            " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.First()." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ",");
                    }
                }

                paramStr = strDto.ToString().Remove(strDto.Length - 3);

                streamWriter.WriteLine("                if (" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableName.Replace("_", "")) + "List.Count == 1)");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine("                    return new " + className + SuffixDto.Instance().DataContractTextBox + "()");
                streamWriter.WriteLine("                    {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                    };");
                streamWriter.WriteLine("                };");
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine("            catch (FaultException fe)");
                streamWriter.WriteLine("            {");
                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + IblclassName + "), " + quates + "Error in selecting by id" + quates + ", fe);");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new WebFaultException<string>(fe.Message, HttpStatusCode.InternalServerError);");
                }
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("            return null;");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Codes the hammer service create update method.
        /// </summary>
        /// <param name="IblclassName">Name of the iblclass.</param>
        /// <param name="table">The table.</param>
        /// <param name="streamWriter">The stream writer.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeHammerServiceCreateUpdateMethod(string IblclassName, CodeHammerTableDto table, StreamWriter streamWriter)
        {
            try
            {
                string className = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim());
                string variableName = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DtoTextBox;
                string variableNameDataContract = codeHammerDataUtilContract.CodeHammerFormatPascal(table.CodeHammerName.Replace("_", "").Replace(" ", string.Empty).Trim()) + SuffixDto.Instance().DataContractTextBox;

                string quates = @"""";
                //// Append the method header
                streamWriter.WriteLine("        /// <summary>");
                streamWriter.WriteLine("        /// " + table.CodeHammerName + " set by id.");
                streamWriter.WriteLine("        /// </summary>");
                streamWriter.WriteLine("        /// <param name=" + quates + "responseDataStream" + quates + ">The responseDataStream.</param>");

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        streamWriter.WriteLine("        /// <param name=" + quates + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + quates + ">The id.</param>");
                    }
                }

                streamWriter.WriteLine("        /// <exception cref=" + quates + "System.ServiceModel.FaultException" + quates + ">Error in updating</exception>");
                streamWriter.WriteLine("        /// <returns>return true if success</returns>");

                StringBuilder strX = new StringBuilder();
                strX.Length = 0;
                strX.Capacity = 0;

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        strX.Append("string " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", ");
                    }
                }

                string paramId = strX.ToString().Remove(strX.Length - 2);

                streamWriter.WriteLine("        public int " + className + "SetByID(Stream responseDataStream, " + paramId + ")");

                streamWriter.WriteLine("        {");

                streamWriter.WriteLine("            StreamReader reader = new StreamReader(responseDataStream);");
                streamWriter.WriteLine("            string dataStream = reader.ReadToEnd();");
                streamWriter.WriteLine("            int result = 0;");
                streamWriter.WriteLine("            string systemMessage = string.Empty;");
                streamWriter.WriteLine("            string nestedSystemMessage = string.Empty;");
                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                    {
                        if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                        {
                            streamWriter.WriteLine("            " + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + " " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value;");
                        }
                    }
                }

                streamWriter.WriteLine("            JavaScriptSerializer jss = new JavaScriptSerializer();");
                streamWriter.WriteLine("            var " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + " = new JavaScriptSerializer().Deserialize<" + className + SuffixDto.Instance().DataContractTextBox + ">(dataStream);");
                streamWriter.WriteLine("            if (" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + " == null)");
                streamWriter.WriteLine("            {");

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + className + SuffixDto.Instance().BlSuffixTextBox + "), " + quates + "Error in updating" + quates + ", new Exception(" + quates + "Could not deserialize data stream string into " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + " object" + quates + "));");
                    streamWriter.WriteLine("                throw new FaultException(" + quates + " Could not deserialize data stream string " + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                throw new FaultException(" + quates + "Could not deserialize data stream string into " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + " object" + quates + ");");
                }

                streamWriter.WriteLine("            }");

                streamWriter.WriteLine();
                streamWriter.WriteLine("            try");
                streamWriter.WriteLine("            {");
                streamWriter.WriteLine();

                StringBuilder strDto = new StringBuilder();
                strDto.Length = 0;
                strDto.Capacity = 0;
                List<string> checkNames = new List<string>();
                checkNames.Clear();

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                        {
                            streamWriter.WriteLine("                if (!" + codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item) + ".TryParse(" + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ", out " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value))");
                            streamWriter.WriteLine("                {");
                            streamWriter.WriteLine("                    throw new FaultException(" + quates + "Unable to parse ID: " + quates + "+ " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ");");
                            streamWriter.WriteLine("                }");
                            if (table.CodeHammerColumns.Count > 1)
                            {
                                streamWriter.WriteLine();
                            }
                        }
                    }
                }

                foreach (CodeHammerColumn item in table.CodeHammerColumns)
                {
                    if (table.CodeHammerPrimaryKeys.Any(x => x.CodeHammerName.Equals(item.CodeHammerName)))
                    {
                        if (!codeHammerDataUtilContract.CodeHammerToCsTypeConvertTo(item).Equals("string"))
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + "Value.ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + "Value,");
                            }
                        }
                        else
                        {
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("Encoding"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = Encoding.UTF8.GetBytes(" + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ".ToString()),");
                            }
                            if (codeHammerDataUtilContract.CodeHammerToCsTypeConvertToType(item).Contains("new Guid"))
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                            else
                            {
                                strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(item.CodeHammerName).Replace("_", "") + ",");
                            }
                        }
                    }
                    else
                    {
                        strDto.AppendLine("                    " + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + " = " + codeHammerDataUtilContract.CodeHammerFormatCamelDTO(variableNameDataContract) + "." + codeHammerDataUtilContract.CodeHammerFormatPascal(item.CodeHammerName).Replace("_", "") + ",");
                    }
                }

                string paramStr = strDto.ToString().Remove(strDto.Length - 3);
                streamWriter.WriteLine("                " + className + SuffixDto.Instance().DtoTextBox + " " + variableName.Replace("_", "") + "= new " + className + SuffixDto.Instance().DtoTextBox + "()");
                streamWriter.WriteLine("                {");
                streamWriter.WriteLine(paramStr);
                streamWriter.WriteLine("                };");

                streamWriter.WriteLine();
                streamWriter.WriteLine("                if (!" + IblclassName + "." + className + "SetByID(" + variableName.Replace("_", "") + ", out nestedSystemMessage))");
                streamWriter.WriteLine("                {");

                StringBuilder strBuildstring1 = new StringBuilder();
                strBuildstring1.Append("    systemMessage = ");
                strBuildstring1.Append("\"Error in method: " + className + "SetByID \"");
                strBuildstring1.Append(" + ");
                strBuildstring1.Append(" nestedSystemMessage;");

                streamWriter.WriteLine("                " + strBuildstring1.ToString());
                streamWriter.WriteLine("                    result = 0;");

                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                    log.LogError(typeof(" + IblclassName + "), " + quates + "Error in setting by id" + quates + ", new Exception(systemMessage));");
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in setting by id" + quates + ");");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new FaultException(" + quates + "Error in setting by id" + quates + ", new FaultCode(systemMessage));");
                }

                streamWriter.WriteLine("                }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("                result = 1;");

                streamWriter.WriteLine("            }");
                streamWriter.WriteLine("            catch (FaultException fe)");
                streamWriter.WriteLine("            {");
                streamWriter.WriteLine("                result = 0;");
                if (ioManagerContract.Log4Net)
                {
                    streamWriter.WriteLine("                log.LogError(typeof(" + IblclassName + "), " + quates + "Error in setting by id" + quates + ", fe);");
                }
                else
                {
                    streamWriter.WriteLine("                    throw new WebFaultException<string>(fe.Message, HttpStatusCode.InternalServerError);");
                }
                streamWriter.WriteLine("            }");
                streamWriter.WriteLine();
                streamWriter.WriteLine("            return result;");
                streamWriter.WriteLine("        }");
                streamWriter.WriteLine();
                checkNames.Clear();
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return true;
                throw;
            }
        }

        #endregion Service Layer
    }
}