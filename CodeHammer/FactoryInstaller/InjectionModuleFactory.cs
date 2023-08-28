/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace FactoryInstaller
{
    using CodeGen.Framework.FunctionArea.Dgml;
    using CodeHammer.Framework;
    using CodeHammer.Framework.FunctionArea.DataTypeManager;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Generators;
    using CodeHammer.Framework.FunctionArea.Generators.BusinessGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DataGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.DtoGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGeneratorPENDING;
    using CodeHammer.Framework.FunctionArea.Generators.LoggingGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.ServiceLibraryGenerator;
    using CodeHammer.Framework.FunctionArea.Generators.SqlGenerator;
    using CodeHammer.Framework.FunctionArea.IndentTextWriter;
    using CodeHammer.Framework.FunctionArea.Log;
    using CodeHammer.Framework.FunctionArea.ProjectManager;
    using Ninject.Modules;

    /// <summary>
    /// this class InjectionModuleFactory
    /// </summary>

    public class InjectionModuleFactory : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            /// Layer Framework bindings
            Bind<FuncTypeFactoryContract>().To(typeof(FuncTypeFactory)).InSingletonScope();
            Bind<CodeHammerDataUtilContract>().To(typeof(CodeHammerDataUtil)).InSingletonScope();
            Bind<DbDataSupportAdapterContract>().To(typeof(DbDataSupportAdapter)).InSingletonScope();
            Bind<HelpManagerContract>().To(typeof(HelpManager)).InSingletonScope();
            Bind<IOManagerContract>().To(typeof(IOManager)).InSingletonScope();
            Bind<CodeHammerBusinessLogicGeneratorContract>().To(typeof(CodeHammerBusinessLogicGenerator)).InSingletonScope();
            Bind<CodeHammerDataAccessLayerGeneratorContract>().To(typeof(CodeHammerDataAccessLayerGenerator)).InSingletonScope();
            Bind<CodeHammerDTOGeneratorContract>().To(typeof(CodeHammerDTOGenerator)).InSingletonScope();
            Bind<FluentNHibernateGeneratorContract>().To(typeof(FluentNHibernateGenerator)).InSingletonScope();
            Bind<CodeHammerLoggingGeneratorContract>().To(typeof(CodeHammerLoggingGenerator)).InSingletonScope();
            Bind<CodeHammerServiceLibraryGeneratorContract>().To(typeof(CodeHammerServiceLibraryGenerator)).InSingletonScope();
            Bind<CodeHammerGeneratorContract>().To(typeof(CodeHammerGenerator)).InSingletonScope();
            Bind<LogFuncContract>().To(typeof(LogFunc)).InSingletonScope();
            Bind<ProjectManagerContract>().To(typeof(ProjectManager)).InSingletonScope();
            Bind<CodeHammerSqlGeneratorContract>().To(typeof(CodeHammerSqlGenerator)).InSingletonScope();
            Bind<DgmlFuncContract>().To(typeof(DgmlFunc)).InSingletonScope();
            Bind<IndentTextWriterFuncContract>().To(typeof(IndentTextWriterFunc)).InSingletonScope();
        }
    }
}