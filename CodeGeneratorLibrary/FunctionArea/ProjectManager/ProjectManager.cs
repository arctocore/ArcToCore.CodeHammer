/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.ProjectManager
{
    using System;
    using CodeHammer.Framework.FunctionArea.FileIO;
    using CodeHammer.Framework.FunctionArea.Log;

    /// <summary>
    /// this class ProjectManager
    /// </summary>

    public class ProjectManager : CodeHammer.Framework.FunctionArea.ProjectManager.ProjectManagerContract
    {
        #region Variable

        /// <summary>
        /// The log function contract
        /// </summary>
        private LogFuncContract logFuncContract = null;

        /// <summary>
        /// The function type factory contract
        /// </summary>
        private FuncTypeFactoryContract funcTypeFactoryContract = null;

        /// <summary>
        /// The io manager contract
        /// </summary>
        private IOManagerContract ioManagerContract = null;

        #endregion Variable

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="logFuncContract">The log function contract.</param>
        /// <param name="funcTypeFactoryContract">The function type factory contract.</param>
        /// <param name="ioManagerContract">The io manager contract.</param>
        public ProjectManager(LogFuncContract logFuncContract,
            FuncTypeFactoryContract funcTypeFactoryContract,
            IOManagerContract ioManagerContract)
        {
            this.logFuncContract = logFuncContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.ioManagerContract = ioManagerContract;

            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            ioManagerContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.IOMANAGERCONTRACT);
        }

        #region Add to Project

        ///// <summary>
        ///// Adds the file to code hammer test project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerTestProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerSolutionTest;
        //        Project visualstudioSolutionTest = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.VisualstudioSolutionTestProjectFolder);
        //        DirectoryInfo diBusiness = new DirectoryInfo(folder + ioManagerContract.VisualstudioSolutionBusinessTestProjectFolder);
        //        DirectoryInfo diData = new DirectoryInfo(folder + ioManagerContract.VisualstudioSolutionDataRepositoryTestProjectFolder);
        //        DirectoryInfo diServiceLibrary = new DirectoryInfo(folder + ioManagerContract.VisualstudioSolutionServiceLibraryTestProjectFolder);

        //        #region CleanUp

        //        if (ioManagerContract.Crud)
        //        {
        //            if (ioManagerContract.UnitTest)
        //            {
        //                foreach (var fi in diBusiness.GetFiles())
        //                {
        //                    visualstudioSolutionTest.AddItem("Compile", @"" + "BusinessTests\\" + fi.Name);
        //                    visualstudioSolutionTest.Save();
        //                }

        //                foreach (var fi in diData.GetFiles())
        //                {
        //                    visualstudioSolutionTest.AddItem("Compile", @"" + "DataTests\\" + fi.Name);
        //                    visualstudioSolutionTest.Save();
        //                }

        //                foreach (var fi in diServiceLibrary.GetFiles())
        //                {
        //                    visualstudioSolutionTest.AddItem("Compile", @"" + "ServiceLibraryTests\\" + fi.Name);
        //                    visualstudioSolutionTest.Save();
        //                }
        //            }
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            try
        //            {
        //                diBusiness.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }

        //            try
        //            {
        //                diData.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }

        //            try
        //            {
        //                diServiceLibrary.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }

        //        if (!ioManagerContract.UnitTest)
        //        {
        //            try
        //            {
        //                diBusiness.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }

        //            try
        //            {
        //                diData.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }

        //            try
        //            {
        //                diServiceLibrary.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }

        //        #endregion CleanUp

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer logging project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>
        ///// if sucess then return true
        ///// </returns>
        //public bool AddFileToCodeHammerLoggingProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerLogging;
        //        Project visualstudioLogging = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.VisualstudioLoggingProjectFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioLogging.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Replace("Log4Net\\", string.Empty).Trim().Equals(fi.Name))
        //                    {
        //                        visualstudioLogging.RemoveItem(pi);
        //                        visualstudioLogging.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            if (fi.Name.Equals("Log4NetSqlScript.sql"))
        //            {
        //                visualstudioLogging.AddItem("Content", @"" + "Log4Net\\" + fi.Name);
        //                visualstudioLogging.Save();
        //            }
        //            else
        //            {
        //                visualstudioLogging.AddItem("Compile", @"" + "Log4Net\\" + fi.Name);
        //                visualstudioLogging.Save();
        //            }
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in visualstudioLogging.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Log4Net\\"))
        //                {
        //                    visualstudioLogging.RemoveItem(pi);
        //                    visualstudioLogging.Save();
        //                }
        //            }

        //            try
        //            {
        //                di.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer business logic project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerBusinessLogicProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerBusinessLogic;
        //        Project codeHammerBusinessLogic = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.CodeHammerBusinessLogicFolder);
        //        DirectoryInfo idi = new DirectoryInfo(folder + ioManagerContract.ICodeHammerBusinessLogicFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerBusinessLogic.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    codeHammerBusinessLogic.RemoveItem(pi);
        //                    codeHammerBusinessLogic.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in idi.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerBusinessLogic.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    codeHammerBusinessLogic.RemoveItem(pi);
        //                    codeHammerBusinessLogic.Save();
        //                }
        //            }
        //        }

        //        if (ioManagerContract.Crud)
        //        {
        //            foreach (var fi in di.GetFiles())
        //            {
        //                codeHammerBusinessLogic.AddItem("Compile", @"" + "Business\\" + fi.Name);
        //                codeHammerBusinessLogic.Save();
        //            }

        //            foreach (var fi in idi.GetFiles())
        //            {
        //                codeHammerBusinessLogic.AddItem("Compile", @"" + "IBusiness\\" + fi.Name);
        //                codeHammerBusinessLogic.Save();
        //            }
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in codeHammerBusinessLogic.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Business\\"))
        //                {
        //                    codeHammerBusinessLogic.RemoveItem(pi);
        //                    codeHammerBusinessLogic.Save();
        //                }

        //                try
        //                {
        //                    idi.Delete(true);
        //                }
        //                catch (Exception ex)
        //                {
        //                    logFuncContract.Logger(ex.Message);
        //                }
        //            }

        //            try
        //            {
        //                di.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer data access project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerDataAccessProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerRepository;
        //        Project codeHammerRepository = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.CodeHammerRepositoryFolder);

        //        DirectoryInfo diMngt = new DirectoryInfo(folder + ioManagerContract.CodeHammerRepositoryDatabaseManagementFolder);

        //        DirectoryInfo idi = new DirectoryInfo(folder + ioManagerContract.ICodeHammerRepositoryFolder);

        //        DirectoryInfo diSp = new DirectoryInfo(folder + ioManagerContract.CodeHammerStoredProcedureFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerRepository.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    codeHammerRepository.RemoveItem(pi);
        //                    codeHammerRepository.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in idi.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerRepository.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    codeHammerRepository.RemoveItem(pi);
        //                    codeHammerRepository.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in diSp.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerRepository.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".sql") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Replace("StoredProcedures\\", string.Empty).Trim().Equals(fi.Name))
        //                    {
        //                        codeHammerRepository.RemoveItem(pi);
        //                        codeHammerRepository.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in diSp.GetFiles())
        //        {
        //            codeHammerRepository.AddItem("Content", @"" + "StoredProcedures\\" + fi.Name);
        //            codeHammerRepository.Save();
        //        }

        //        foreach (var fi in idi.GetFiles())
        //        {
        //            codeHammerRepository.AddItem("Compile", @"" + "IData\\" + fi.Name);
        //            codeHammerRepository.Save();
        //        }

        //        foreach (var fi in diMngt.GetFiles())
        //        {
        //            codeHammerRepository.AddItem("Compile", @"" + "Infrastructure\\" + fi.Name);
        //            codeHammerRepository.Save();
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            codeHammerRepository.AddItem("Compile", @"" + "Data\\" + fi.Name);
        //            codeHammerRepository.Save();
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in codeHammerRepository.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Data\\"))
        //                {
        //                    codeHammerRepository.RemoveItem(pi);
        //                    codeHammerRepository.Save();
        //                    try
        //                    {
        //                        di.Delete(true);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }

        //                if (pi.UnevaluatedInclude.Contains("StoredProcedures\\"))
        //                {
        //                    codeHammerRepository.RemoveItem(pi);
        //                    codeHammerRepository.Save();
        //                    try
        //                    {
        //                        diSp.Delete(true);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }

        //                if (pi.UnevaluatedInclude.Contains("Infrastructure\\"))
        //                {
        //                    codeHammerRepository.RemoveItem(pi);
        //                    codeHammerRepository.Save();
        //                    try
        //                    {
        //                        diMngt.Delete(true);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }

        //                try
        //                {
        //                    idi.Delete(true);
        //                }
        //                catch (Exception ex)
        //                {
        //                    logFuncContract.Logger(ex.Message);
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer repository mapping project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerRepositoryMappingProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerRepository;
        //        Project codeHammerRepositoryMapping = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.CodeHammerRepositoryMappingFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in codeHammerRepositoryMapping.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    codeHammerRepositoryMapping.RemoveItem(pi);
        //                    codeHammerRepositoryMapping.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            codeHammerRepositoryMapping.AddItem("Compile", @"" + "Mapping\\" + fi.Name);
        //            codeHammerRepositoryMapping.Save();
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in codeHammerRepositoryMapping.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Mapping\\"))
        //                {
        //                    codeHammerRepositoryMapping.RemoveItem(pi);
        //                    codeHammerRepositoryMapping.Save();
        //                    try
        //                    {
        //                        di.Delete(true);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer repository helper project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerRepositoryHelperProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.CodeHammerRepository;
        //        Project repositoryHelper = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.CodeHammerRepositoryHelperFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in repositoryHelper.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    repositoryHelper.RemoveItem(pi);
        //                    repositoryHelper.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            repositoryHelper.AddItem("Compile", @"" + "Helper\\" + fi.Name);
        //            repositoryHelper.Save();
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in repositoryHelper.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Helper\\"))
        //                {
        //                    repositoryHelper.RemoveItem(pi);
        //                    repositoryHelper.Save();
        //                    try
        //                    {
        //                        di.Delete(true);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer dto project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerDtoProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.Domain;
        //        Project dto = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.DtoFolder);

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in dto.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    dto.RemoveItem(pi);
        //                    dto.Save();
        //                }
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            dto.AddItem("Compile", @"" + "Domain\\" + fi.Name);
        //            dto.Save();
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in dto.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("Domain\\"))
        //                {
        //                    dto.RemoveItem(pi);
        //                    dto.Save();
        //                }
        //            }

        //            try
        //            {
        //                di.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return true;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer service library host.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>
        ///// if sucess then return true
        ///// </returns>
        //public bool AddFileToCodeHammerServiceLibraryHost(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.VisualstudioServiceHostProject;
        //        string pathFolder = @"" + folder + ioManagerContract.VisualstudioServiceHostProjectName;

        //        string servicePath = @"" + folder + ioManagerContract.VisualstudioServiceHostServices;

        //        Project visualstudioServiceLibraryHost = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceHostProjectName);

        //        DirectoryInfo diServices = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceHostServices);

        //        DirectoryInfo diRouteConfig = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceHostAppStart);

        //        foreach (var fi in diRouteConfig.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryHost.Items.ToList())
        //            {
        //                //if (fi.Name.Contains("RouteConfig.cs"))
        //                //{
        //                //    if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains("RouteConfig.cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                //    {
        //                //        visualstudioServiceLibraryHost.RemoveItem(pi);
        //                //        visualstudioServiceLibraryHost.Save();
        //                //    }
        //                //}
        //            }
        //        }

        //        foreach (var fi in diServices.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryHost.Items.ToList())
        //            {
        //                if (fi.Name.Contains(".svc"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".svc") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                    {
        //                        visualstudioServiceLibraryHost.RemoveItem(pi);
        //                        visualstudioServiceLibraryHost.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryHost.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Equals("Web.Debug.config"))
        //                {
        //                    visualstudioServiceLibraryHost.RemoveItem(pi);
        //                    visualstudioServiceLibraryHost.Save();
        //                    try
        //                    {
        //                        File.Delete(pathFolder + "Web.Debug.config");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }

        //                if (pi.UnevaluatedInclude.Equals("Web.Release.config"))
        //                {
        //                    visualstudioServiceLibraryHost.RemoveItem(pi);
        //                    visualstudioServiceLibraryHost.Save();
        //                    try
        //                    {
        //                        File.Delete(pathFolder + "Web.Release.config");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }
        //            }
        //        }

        //        //foreach (var fi in diRouteConfig.GetFiles())
        //        //{
        //        //    if (fi.Name.Contains("RouteConfig.cs"))
        //        //    {
        //        //        visualstudioServiceLibraryHost.AddItem("Compile", @"" + "App_Start\\" + fi.Name);
        //        //        visualstudioServiceLibraryHost.Save();
        //        //        visualstudioServiceLibraryHost.Build();
        //        //    }
        //        //}

        //        foreach (var fi in diServices.GetFiles())
        //        {
        //            if (fi.Name.Contains(".svc"))
        //            {
        //                visualstudioServiceLibraryHost.AddItem("Content", @"" + "Services\\" + fi.Name);
        //                visualstudioServiceLibraryHost.Save();
        //                visualstudioServiceLibraryHost.Build();
        //            }
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            //if (fi.Name.Equals("RouteConfig.cs"))
        //            //{
        //            //    if (!visualstudioServiceLibraryHost.AllEvaluatedItems.Any(x => x.UnevaluatedInclude.Equals("RouteConfig.cs")))
        //            //    {
        //            //        visualstudioServiceLibraryHost.AddItem("Compile", @"" + "App_Start\\" + fi.Name);
        //            //        visualstudioServiceLibraryHost.Save();
        //            //        visualstudioServiceLibraryHost.Build();
        //            //    }
        //            //}

        //            if (fi.Name.Equals("Web.config"))
        //            {
        //                if (!visualstudioServiceLibraryHost.AllEvaluatedItems.Any(x => x.UnevaluatedInclude.Equals("Web.config")))
        //                {
        //                    visualstudioServiceLibraryHost.AddItem("Content", @"" + fi.Name);
        //                    visualstudioServiceLibraryHost.Save();
        //                    visualstudioServiceLibraryHost.Build();
        //                }
        //            }

        //            if (fi.Name.Equals("Global.asax"))
        //            {
        //                if (!visualstudioServiceLibraryHost.AllEvaluatedItems.Any(x => x.UnevaluatedInclude.Equals("Global.asax")))
        //                {
        //                    visualstudioServiceLibraryHost.AddItem("Content", @"" + fi.Name);
        //                    visualstudioServiceLibraryHost.Save();
        //                    visualstudioServiceLibraryHost.Build();
        //                }
        //            }
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            //foreach (var fi in diRouteConfig.GetFiles())
        //            //{
        //            //    if (fi.Name.Contains("RouteConfig.cs"))
        //            //    {
        //            //        try
        //            //        {
        //            //            File.Delete(fi.FullName);
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            logFuncContract.Logger(ex.Message);
        //            //        }
        //            //    }
        //            //}

        //            //foreach (ProjectItem pi in visualstudioServiceLibraryHost.Items.ToList())
        //            //{
        //            //    if (pi.UnevaluatedInclude.Contains("RouteConfig.cs"))
        //            //    {
        //            //        visualstudioServiceLibraryHost.RemoveItem(pi);
        //            //        visualstudioServiceLibraryHost.Save();
        //            //    }
        //            //}
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (var fi in diServices.GetFiles())
        //            {
        //                if (fi.Name.Contains(".svc"))
        //                {
        //                    try
        //                    {
        //                        File.Delete(fi.FullName);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logFuncContract.Logger(ex.Message);
        //                    }
        //                }
        //            }

        //            foreach (ProjectItem pi in visualstudioServiceLibraryHost.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains(".svc"))
        //                {
        //                    visualstudioServiceLibraryHost.RemoveItem(pi);
        //                    visualstudioServiceLibraryHost.Save();
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Adds the file to code hammer service library project.
        ///// </summary>
        ///// <param name="folder">The folder.</param>
        ///// <returns>if sucess then return true</returns>
        //public bool AddFileToCodeHammerServiceLibraryProject(string folder)
        //{
        //    try
        //    {
        //        string path = @"" + folder + ioManagerContract.VisualstudioServiceLibraryProject;
        //        Project visualstudioServiceLibraryProject = ProjectCollection.GlobalProjectCollection.LoadProject(path);
        //        DirectoryInfo diWcf = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceLibraryProjectFolder);

        //        DirectoryInfo di = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceLibraryProjectFolder + "\\Service\\");
        //        DirectoryInfo diServiceContract = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceLibraryProjectFolder + "\\ServiceContract\\");
        //        DirectoryInfo diDataContract = new DirectoryInfo(folder + ioManagerContract.VisualstudioServiceLibraryProjectFolder + "\\DataContract\\");

        //        foreach (var fi in di.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryProject.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Replace("ServiceLib\\Service\\", string.Empty).Trim().Equals(fi.Name))
        //                    {
        //                        visualstudioServiceLibraryProject.RemoveItem(pi);
        //                        visualstudioServiceLibraryProject.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in diServiceContract.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryProject.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Replace("ServiceLib\\ServiceContract\\", string.Empty).Trim().Equals(fi.Name))
        //                    {
        //                        visualstudioServiceLibraryProject.RemoveItem(pi);
        //                        visualstudioServiceLibraryProject.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in diDataContract.GetFiles())
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryProject.Items.ToList())
        //            {
        //                if (!pi.UnevaluatedInclude.Contains(".csproj") && pi.UnevaluatedInclude.Contains(".cs") && !pi.UnevaluatedInclude.Contains("Properties\\AssemblyInfo.cs"))
        //                {
        //                    if (!pi.UnevaluatedInclude.Replace("ServiceLib\\DataContract\\", string.Empty).Trim().Equals(fi.Name))
        //                    {
        //                        visualstudioServiceLibraryProject.RemoveItem(pi);
        //                        visualstudioServiceLibraryProject.Save();
        //                    }
        //                }
        //            }
        //        }

        //        foreach (var fi in diDataContract.GetFiles())
        //        {
        //            visualstudioServiceLibraryProject.AddItem("Compile", @"" + "ServiceLib\\DataContract\\" + fi.Name);
        //            visualstudioServiceLibraryProject.Save();
        //        }

        //        foreach (var fi in di.GetFiles())
        //        {
        //            visualstudioServiceLibraryProject.AddItem("Compile", @"" + "ServiceLib\\Service\\" + fi.Name);
        //            visualstudioServiceLibraryProject.Save();
        //        }

        //        foreach (var fi in diServiceContract.GetFiles())
        //        {
        //            visualstudioServiceLibraryProject.AddItem("Compile", @"" + "ServiceLib\\ServiceContract\\" + fi.Name);
        //            visualstudioServiceLibraryProject.Save();
        //        }

        //        if (!ioManagerContract.Crud)
        //        {
        //            foreach (ProjectItem pi in visualstudioServiceLibraryProject.Items.ToList())
        //            {
        //                if (pi.UnevaluatedInclude.Contains("ServiceLib\\"))
        //                {
        //                    visualstudioServiceLibraryProject.RemoveItem(pi);
        //                    visualstudioServiceLibraryProject.Save();
        //                }
        //            }

        //            try
        //            {
        //                diWcf.Delete(true);
        //            }
        //            catch (Exception ex)
        //            {
        //                logFuncContract.Logger(ex.Message);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //    }
        //}

        #endregion Add to Project

        #region BuildProjects NOT IN USE

        /// <summary>
        /// Builds the project.
        /// </summary>
        /// <param name="csproject">The csproject.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Could not build the project</exception>
        /// <exception cref="Exception">Could not build the project</exception>
        //public bool BuildProject(string csproject)
        //{
        //    try
        //    {
        //        List<ILogger> loggers = new List<ILogger>();
        //        loggers.Add(new Microsoft.Build.BuildEngine.ConsoleLogger());
        //        var projectCollection = new ProjectCollection();
        //        projectCollection.RegisterLoggers(loggers);
        //        var project = projectCollection.LoadProject(csproject); // Needs a reference to System.Xml
        //        try
        //        {
        //            project.Build();
        //            Thread.Sleep(2000);
        //        }
        //        finally
        //        {
        //            projectCollection.UnregisterAllLoggers();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logFuncContract.Logger(ex.Message);
        //        return false;
        //        throw new Exception("Could not build the project", ex);
        //    }
        //}

        #endregion BuildProjects NOT IN USE
    }
}