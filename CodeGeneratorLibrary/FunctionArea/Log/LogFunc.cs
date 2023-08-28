/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Log
{
    using CodeHammer.Framework.FunctionArea.FileIO;
    using System;
    using System.IO;

    /// <summary>
    /// this class LogFunc
    /// </summary>

    public class LogFunc : CodeHammer.Framework.FunctionArea.Log.LogFuncContract
    {
        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFunc" /> class.
        /// </summary>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        public LogFunc(FuncTypeFactoryContract funcTypeFactoryContract,
            IOManagerContract ioManagerContract)
        {
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.ioManagerContract = ioManagerContract;
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
        }

        /// <summary>
        /// Logs the specified log message.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        /// <param name="path">The path.</param>
        /// <returns>if success return true</returns>
        public bool Logger(string logMessage)
        {
            try
            {
                if (!File.Exists(ioManagerContract.LogPath))
                {
                    using (StreamWriter w = File.CreateText(ioManagerContract.LogPath))
                    {
                        w.Write("\r\nLog Entry : ");
                        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                            DateTime.Now.ToLongDateString());
                        w.WriteLine("  :");
                        w.WriteLine("  :{0}", logMessage);
                        w.WriteLine("-------------------------------");
                    }
                }
                else
                {
                    using (StreamWriter w = File.AppendText(ioManagerContract.LogPath))
                    {
                        w.Write("\r\nLog Entry : ");
                        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                            DateTime.Now.ToLongDateString());
                        w.WriteLine("  :");
                        w.WriteLine("  :{0}", logMessage);
                        w.WriteLine("-------------------------------");
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Dumps the log.
        /// </summary>
        /// <param name="r">The r.</param>
        public void DumpLog(System.IO.StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}