/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator
{
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.IO;

    /// <summary>
    /// this class CodeHammerLoggingGenerator
    /// </summary>

    public class CodeHammerLoggingGenerator : CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator.CodeHammerLoggingGeneratorContract
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
        /// Initializes a new instance of the <see cref="CodeHammerLoggingGenerator"/> class.
        /// </summary>
        /// <param name="codeHammerDataUtilContract">The code hammer data utility contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        public CodeHammerLoggingGenerator(CodeHammerDataUtilContract codeHammerDataUtilContract,
            IOManagerContract ioManagerContract,
             LogFuncContract logFuncContract,
            FuncTypeFactoryContract funcTypeFactoryContract)
        {
            this.logFuncContract = logFuncContract;
            this.ioManagerContract = ioManagerContract;
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;

            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
        }

        /// <summary>
        /// Generates the interface for class.
        /// </summary>
        /// <param name="csPath">The cs path.</param>
        /// <exception cref="Exception">logging class is missing</exception>
        private void GenerateInterfaceForClass(string csPath)
        {
            using (StreamWriter writer = new StreamWriter(csPath + ioManagerContract.CodeHammerLoggingLog4Net + "\\Logger.cs"))
            {
                string replaceAppConfig = string.Empty;

                string log4NetString = string.Empty;
                string logging = codeHammerDataUtilContract.CodeHammerGetLog4Class(out log4NetString);

                if (ioManagerContract.Log4Net && ioManagerContract.UseIoC)
                {
                    log4NetString = logging.Replace("<INTERFACE>", ": ILogger");
                }

                if (ioManagerContract.Log4Net && !ioManagerContract.UseIoC)
                {
                    log4NetString = logging.Replace("<INTERFACE>", "");
                }

                if (string.IsNullOrEmpty(logging))
                {
                    throw new Exception("logging class is missing");
                }

                writer.Write(log4NetString);
            }
        }

        /// <summary>
        /// Codes the hammer log4 net SQL script.
        /// </summary>
        /// <param name="csPath">The cs path.</param>
        /// <returns>if success then return true</returns>
        public bool CodeHammerLog4NetSqlScript(string csPath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(csPath + ioManagerContract.CodeHammerLoggingLog4Net + "\\Log4NetSqlScript.sql"))
                {
                    if (ioManagerContract.Log4Net)
                    {
                        writer.WriteLine("USE " + ioManagerContract.DatabaseName);
                        writer.WriteLine("CREATE TABLE [dbo].[<Tablename>](");
                        writer.WriteLine("[Id] [int] IDENTITY(1,1) NOT NULL,");
                        writer.WriteLine("[Date] [datetime] NOT NULL,");
                        writer.WriteLine("[Thread] [nvarchar](255) NOT NULL,");
                        writer.WriteLine("[Level] [nvarchar](50) NOT NULL,");
                        writer.WriteLine("[Logger] [nvarchar](255) NOT NULL,");
                        writer.WriteLine("[User] [nvarchar](50) NULL,");
                        writer.WriteLine("[Message] [nvarchar](4000) NOT NULL,");
                        writer.WriteLine("[Exception] [nvarchar](2000) NULL");
                        writer.WriteLine(") ON [PRIMARY]");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log4NetSqlScript.sql was not generated ", ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Codes the hammer service contract library.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Exception</exception>
        public bool CodeHammerLogging(string path)
        {
            try
            {
                GenerateInterfaceForClass(path);
                string quates = @"""";

                string pathToProject = path + ioManagerContract.VisualstudioLoggingProjectFolder + "ILogger.cs";

                using (StreamWriter streamWriter = new StreamWriter(pathToProject))
                {
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace Logging.Log4Net");
                    streamWriter.WriteLine("{");
                    //// Create the header for the class
                    streamWriter.WriteLine("    using System;");
                    streamWriter.WriteLine();

                    streamWriter.WriteLine("    /// <summary>");
                    streamWriter.WriteLine("    /// This interface ILogger");
                    streamWriter.WriteLine("    /// </summary>");
                    streamWriter.WriteLine("    public interface ILogger");
                    streamWriter.WriteLine("    {");

                    streamWriter.WriteLine("        /// <summary>");
                    streamWriter.WriteLine("        /// Logs the error);");
                    streamWriter.WriteLine("        /// </summary>");
                    streamWriter.WriteLine("        /// <param name=" + quates + "type" + quates + ">The type.</param>");
                    streamWriter.WriteLine("        /// <param name=" + quates + "info" + quates + ">The information.</param>");
                    streamWriter.WriteLine("        /// <param name=" + quates + "ex" + quates + ">The ex.</param>");
                    streamWriter.WriteLine("        void LogError(Type type, string info, Exception ex);");
                    //// Close out the class and namespace
                    streamWriter.WriteLine("    }");
                    streamWriter.WriteLine("}");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.ToString());
            }
        }
    }
}