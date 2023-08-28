/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework
{
    using CodeGen.Framework.FunctionArea.Dgml;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Generators;
    using CodeHammer.Framework.FunctionArea.Generators.BusinessGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DataGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DtoGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.SqlGenerator;
    using CodeHammer.Framework.FunctionArea.IndentTextWriter;
    using CodeHammer.Framework.FunctionArea.Log;
    using CodeHammer.Framework.FunctionArea.ProjectManager;
    using Microsoft.CSharp.RuntimeBinder;
    using Ninject;
    using System;

    /// <summary>
    /// this class FuncTypeFactory
    /// </summary>

    public class FuncTypeFactory : CodeHammer.Framework.FuncTypeFactoryContract
    {
        #region Variables

        /// <summary>
        /// The kernel
        /// </summary>
        private IKernel kernel;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncTypeFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public FuncTypeFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        #region Enum

        /// <summary>
        /// this enum FuncTypeEnum
        /// </summary>
        public enum FuncTypeEnum
        {
            /// <summary>
            /// This enum resolves CodeHammerBusinessLogicGeneratorContract
            /// </summary>
            CODEHAMMERBUSINESSLOGICGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves CodeHammerDataAccessLayerGeneratorContract
            /// </summary>
            CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves  CodeHammerDTOGeneratorContract
            /// </summary>
            CODEHAMMERDTOGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves FluentNHibernateGenerator
            /// </summary>
            FLUENTNHIBERNATEGENERATOR,

            /// <summary>
            /// This enum resolves CodeHammerLoggingGeneratorContract
            /// </summary>
            CODEHAMMERLOGGINGGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves ProjectManagerContract
            /// </summary>
            PROJECTMANAGERCONTRACT,

            /// <summary>
            /// This enum resolves CodeHammerServiceLibraryGeneratorContract
            /// </summary>
            CODEHAMMERSERVICELIBRARYGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves CodeHammerSqlGeneratorContract
            /// </summary>
            CODEHAMMERSQLGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves HelpManagerContract
            /// </summary>
            HELPMANAGERCONTRACT,

            /// <summary>
            /// This enum resolves IOManagerContract
            /// </summary>
            IOMANAGERCONTRACT,

            /// <summary>
            /// This enum resolves logfunccontract
            /// </summary>
            LOGFUNCCONTRACT,

            /// <summary>
            /// The database data support adapter
            /// </summary>
            DBDATASUPPORTADAPTER,

            /// <summary>
            /// This enum resolves CodeHammerDataUtilContract
            /// </summary>
            CODEHAMMERDATAUTILCONTRACT,

            /// <summary>
            /// This enum resolves CodeHammerGeneratorContract
            /// </summary>
            CODEHAMMERGENERATORCONTRACT,

            /// <summary>
            /// This enum resolves DgmlFuncContract
            /// </summary>
            DGMLFUNCCONTRACT,

            /// <summary>
            /// This enum resolves IndentTextWriterFuncContract
            /// </summary>
            INDENTTEXTWRITERFUNCCONTRACT
        }

        /// <summary>
        /// this enum DataProviderType
        /// </summary>
        public enum DataProviderType
        {
            /// <summary>
            /// The access
            /// </summary>
            ACCESS,

            /// <summary>
            /// The ODBC
            /// </summary>
            ODBC,

            /// <summary>
            /// The oledb
            /// </summary>
            OLEDB,

            /// <summary>
            /// The oracle
            /// </summary>
            ORACLE,

            /// <summary>
            /// The SQL
            /// </summary>
            SQL
        }

        #endregion Enum

        #region Methods

        ////Activator.CreateInstance(typeof(DgmlFunc)) as IDgmlFunc;

        /// <summary>
        /// Gets the class instance.
        /// </summary>
        /// <param name="funcTypeEnum">The function type enum.</param>
        /// <returns>if success return true</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Wrong binding</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public dynamic GetFuncFromTypeEnum(FuncTypeEnum funcTypeEnum)
        {
            try
            {
                switch (funcTypeEnum)
                {
                    case FuncTypeEnum.CODEHAMMERBUSINESSLOGICGENERATORCONTRACT:

                        return kernel.Get<CodeHammerBusinessLogicGeneratorContract>();

                    case FuncTypeEnum.CODEHAMMERDATAACCESSLAYERGENERATORCONTRACT:

                        return kernel.Get<CodeHammerDataAccessLayerGeneratorContract>();

                    case FuncTypeEnum.CODEHAMMERDTOGENERATORCONTRACT:

                        return kernel.Get<CodeHammerDTOGeneratorContract>();

                    case FuncTypeEnum.FLUENTNHIBERNATEGENERATOR:

                        return kernel.Get<FluentNHibernateGenerator>();

                    case FuncTypeEnum.CODEHAMMERLOGGINGGENERATORCONTRACT:

                        return kernel.Get<CodeHammerLoggingGeneratorContract>();

                    case FuncTypeEnum.PROJECTMANAGERCONTRACT:

                        return kernel.Get<ProjectManagerContract>();

                    case FuncTypeEnum.CODEHAMMERSERVICELIBRARYGENERATORCONTRACT:

                        return kernel.Get<CodeHammerServiceLibraryGeneratorContract>();

                    case FuncTypeEnum.CODEHAMMERSQLGENERATORCONTRACT:

                        return kernel.Get<CodeHammerSqlGeneratorContract>();

                    case FuncTypeEnum.HELPMANAGERCONTRACT:

                        return kernel.Get<HelpManagerContract>();

                    case FuncTypeEnum.IOMANAGERCONTRACT:

                        return kernel.Get<IOManagerContract>();

                    case FuncTypeEnum.LOGFUNCCONTRACT:

                        return kernel.Get<LogFuncContract>();

                    case FuncTypeEnum.DBDATASUPPORTADAPTER:

                        return kernel.Get<DbDataSupportAdapterContract>();

                    case FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT:

                        return kernel.Get<CodeHammerDataUtilContract>();

                    case FuncTypeEnum.CODEHAMMERGENERATORCONTRACT:

                        return kernel.Get<CodeHammerGeneratorContract>();

                    case FuncTypeEnum.DGMLFUNCCONTRACT:

                        return kernel.Get<DgmlFuncContract>();

                    case FuncTypeEnum.INDENTTEXTWRITERFUNCCONTRACT:

                        return kernel.Get<IndentTextWriterFuncContract>();

                    default:
                        throw new NotSupportedException();
                }
            }
            catch (RuntimeBinderException rbe)
            {
                throw new RuntimeBinderException("Wrong binding", rbe);
            }
        }

        #endregion Methods
    }
}