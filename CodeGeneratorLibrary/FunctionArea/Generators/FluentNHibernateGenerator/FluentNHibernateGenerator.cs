/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGenerator
{
    using CodeHammer.Entities;
    using CodeHammer.Framework.FunctionArea.DataUtil;
    using CodeHammer.Framework.FunctionArea.Log;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// this class FluentNHibernateGenerator
    /// </summary>

    public class FluentNHibernateGenerator : CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGeneratorPENDING.FluentNHibernateGeneratorContract
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
        /// The code hammer data utility contract
        /// </summary>
        private CodeHammerDataUtilContract codeHammerDataUtilContract = null;

        /// <summary>
        /// The only class in the compile unit. This class contains 2 fields,
        /// 3 properties, a constructor, an entry point, and 1 simple method.
        /// </summary>
        private CodeTypeDeclaration targetClass = new CodeTypeDeclaration();

        /// <summary>
        /// Define the compile unit to use for code generation.
        /// </summary>
        private CodeCompileUnit targetUnit;

        #endregion Variable

        public FluentNHibernateGenerator(LogFuncContract logFuncContract,
            FuncTypeFactoryContract funcTypeFactoryContract,
            CodeHammerDataUtilContract codeHammerDataUtilContract)
        {
            this.logFuncContract = logFuncContract;
            this.funcTypeFactoryContract = funcTypeFactoryContract;
            this.codeHammerDataUtilContract = codeHammerDataUtilContract;

            logFuncContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.LOGFUNCCONTRACT);
            codeHammerDataUtilContract = funcTypeFactoryContract.GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum.CODEHAMMERDATAUTILCONTRACT);
        }

        #region Class

        /// <summary>
        /// Setups the fluent n hibernate.
        /// </summary>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="typeclass">The typeclass.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>
        /// if sucess then return true
        /// </returns>
        /// <exception cref="System.Exception">Please setup primary keys.
        /// or
        /// Please setup primary keys.</exception>
        public bool SetupFluentNHibernate(string namespaceName, string tablename, string typeclass, List<CodeHammerPkClass> pkKeys)
        {
            string quates = @"""";
            targetUnit = new CodeCompileUnit();
            CodeNamespace codeNamespace = new CodeNamespace(namespaceName);

            try
            {
                if (typeclass == "fluentNHibernate")
                {
                    #region fluentNHibernate Class

                    targetClass = new CodeTypeDeclaration(codeHammerDataUtilContract.FirstLetterUppercaseFirst(tablename));
                    targetClass.IsClass = true;
                    targetClass.TypeAttributes = TypeAttributes.Public;
                    codeNamespace.Types.Add(targetClass);
                    targetUnit.Namespaces.Add(codeNamespace);

                    #endregion fluentNHibernate Class
                }
                else if (typeclass == "FluentNHibernateMapping")
                {
                    #region fluentNHibernate MapClass

                    codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Text"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("FluentNHibernate.Mapping"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("Domain"));

                    targetClass = new CodeTypeDeclaration("CodeHammer" + codeHammerDataUtilContract.FirstLetterUppercaseFirst(tablename) + "Map");
                    targetClass.IsClass = true;
                    targetClass.TypeAttributes = TypeAttributes.Public;
                    targetClass.BaseTypes.Add("ClassMap<" + "CodeHammer" + codeHammerDataUtilContract.FirstLetterUppercaseFirst(tablename) + "DTO>");
                    codeNamespace.Types.Add(targetClass);
                    targetUnit.Namespaces.Add(codeNamespace);

                    #endregion fluentNHibernate MapClass
                }
                else if (typeclass == "FluentNHibernateHelper")
                {
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("DataAccessManagerArea"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("DatabaseManagerArea"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("FluentNHibernate.Cfg"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("FluentNHibernate.Cfg.Db"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("NHibernate.Tool.hbm2ddl"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("NHibernate"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
                    ////codeNamespace.Imports.Add(new CodeNamespaceImport("System.Text"));

                    ////targetClass = new CodeTypeDeclaration(codeHammerDataUtilContract.FirstLetterUppercaseFirst("FluentNHibernateHelper"));
                    ////targetClass.IsClass = true;
                    ////targetClass.TypeAttributes = TypeAttributes.Public;
                    ////codeNamespace.Types.Add(targetClass);
                    ////targetUnit.Namespaces.Add(codeNamespace);
                }
                else if (typeclass == "ServerDataRepository")
                {
                    codeNamespace.Imports.Add(new CodeNamespaceImport("NHibernate.Linq"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Text"));

                    targetClass = new CodeTypeDeclaration(codeHammerDataUtilContract.FirstLetterUppercaseFirst("ServerDataRepository"));
                    targetClass.BaseTypes.Add("IServerDataRepository");
                    targetClass.IsClass = true;
                    targetClass.TypeAttributes = TypeAttributes.Public;
                    codeNamespace.Types.Add(targetClass);
                    targetUnit.Namespaces.Add(codeNamespace);
                }
                else if (typeclass == "IServerDataRepository")
                {
                    codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
                    targetClass = new CodeTypeDeclaration(codeHammerDataUtilContract.FirstLetterUppercaseFirst("IServerDataRepository"));
                    targetClass.BaseTypes.Add("IServerDataRepository");
                    targetClass.IsClass = true;
                    targetClass.TypeAttributes = TypeAttributes.Interface;
                    codeNamespace.Types.Add(targetClass);
                    targetUnit.Namespaces.Add(codeNamespace);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion Class

        #region Constructor

        /// <summary>
        /// Add a constructor to the class.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Property List cannot be null!
        /// or
        /// Property list cannot be empty!</exception>
        public bool AddConstructor(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList)
        {
            try
            {
                if (CodeHammerPropAndValueDtoList == null)
                {
                    throw new Exception("Property List cannot be null!");
                }

                if (CodeHammerPropAndValueDtoList.Count == 0)
                {
                    throw new Exception("Property list cannot be empty!");
                }

                // Declare the constructor
                CodeConstructor constructor = new CodeConstructor();
                constructor.Attributes = MemberAttributes.Public;

                foreach (CodeHammerPropAndValueDto CodeHammerPropAndValueDto in CodeHammerPropAndValueDtoList)
                {
                    constructor.Parameters.Add(new CodeParameterDeclarationExpression(CodeHammerPropAndValueDto.VariableValue, CodeHammerPropAndValueDto.VariableName));
                    // Add field initialization logic
                    CodeFieldReferenceExpression widthReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), CodeHammerPropAndValueDto.VariableName);
                    constructor.Statements.Add(new CodeAssignStatement(widthReference, new CodeArgumentReferenceExpression(CodeHammerPropAndValueDto.VariableName)));
                }
                targetClass.Members.Add(constructor);

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion Constructor

        #region Language

        /// <summary>
        /// Generate CSharp source code from the compile unit.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>if sucess then return true</returns>
        public bool GenerateCSharpCode(string fileName)
        {
            try
            {
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                // Creates a new CodeGeneratorOptions.
                CodeGeneratorOptions genOptions = new CodeGeneratorOptions();

                // Sets a value indicating that the code generator should insert blank lines between type members.
                genOptions.BlankLinesBetweenMembers = true;

                // Sets the style of bracing format to use: either "Block" to start a
                // bracing block on the same line as the declaration of its container, or
                // "C" to start the bracing for the block on the following line.
                genOptions.BracingStyle = "C";

                // Sets a value indicating that the code generator should not append an else,
                // catch or finally block, including brackets, at the closing line of a preceeding if or try block.
                genOptions.ElseOnClosing = true;

                // Sets the string to indent each line with.
                genOptions.IndentString = "    ";

                genOptions.VerbatimOrder = true;

                // Uses the CodeGeneratorOptions indexer property to set an
                // example object to the type's string-keyed ListDictionary.
                // Custom ICodeGenerator implementations can use objects
                // in this dictionary to customize process behavior.
                genOptions["CustomGeneratorOptionStringExampleID"] = "BuildFlags: /A /B /C /D /E";
                using (StreamWriter sourceWriter = new StreamWriter(fileName))
                {
                    provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, genOptions);
                }

                CodeCheckOperators(fileName);
                CodeCheckAttDeclarationOperators(fileName);
                RemoveGeneratedComment(provider, genOptions, fileName);
                CodeCheckOperatorsEntities(fileName);
                CodeCheckOperatorsFluentNHibernateConstructor(fileName);
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Generates the vb code.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>if sucess then return true</returns>
        public bool GenerateVBCode(string fileName)
        {
            try
            {
                CodeDomProvider provider = CodeDomProvider.CreateProvider("VisualBasic");
                // Creates a new CodeGeneratorOptions.
                CodeGeneratorOptions genOptions = new CodeGeneratorOptions();

                // Sets a value indicating that the code generator should insert blank lines between type members.
                genOptions.BlankLinesBetweenMembers = true;

                // Sets the style of bracing format to use: either "Block" to start a
                // bracing block on the same line as the declaration of its container, or
                // "C" to start the bracing for the block on the following line.
                genOptions.BracingStyle = "C";

                // Sets a value indicating that the code generator should not append an else,
                // catch or finally block, including brackets, at the closing line of a preceeding if or try block.
                genOptions.ElseOnClosing = false;

                // Sets the string to indent each line with.
                genOptions.IndentString = "    ";

                // Uses the CodeGeneratorOptions indexer property to set an
                // example object to the type's string-keyed ListDictionary.
                // Custom ICodeGenerator implementations can use objects
                // in this dictionary to customize process behavior.
                genOptions["CustomGeneratorOptionStringExampleID"] = "BuildFlags: /A /B /C /D /E";
                using (StreamWriter sourceWriter = new StreamWriter(fileName))
                {
                    provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, genOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion Language

        #region Fields And Properties

        /// <summary>
        /// Adds the fields.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtos">The property and value dtos.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Exception</exception>
        public bool AddFields(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos)
        {
            try
            {
                foreach (CodeHammerPropAndValueDto CodeHammerPropAndValueDto in CodeHammerPropAndValueDtos)
                {
                    CodeMemberField widthValueField;
                    string formattedObj = char.ToLower(CodeHammerPropAndValueDto.VariableName[0]) + CodeHammerPropAndValueDto.VariableName.Substring(1);
                    try
                    {
                        widthValueField = new CodeMemberField(CodeHammerPropAndValueDto.VariableValue, CodeHammerPropAndValueDto.VariableName);
                        widthValueField.Attributes = MemberAttributes.Private;
                        widthValueField.Name = formattedObj;
                        widthValueField.Comments.Add(new CodeCommentStatement("<summary>", true));
                        widthValueField.Comments.Add(new CodeCommentStatement("The " + CodeHammerPropAndValueDto.VariableName + " of the object.", true));
                        widthValueField.Comments.Add(new CodeCommentStatement("</summary>", true));
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                    targetClass.Members.Add(widthValueField);
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Add three properties to the class.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtos">The property and value dtos.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception"></exception>
        public bool AddProperties(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos)
        {
            try
            {
                foreach (CodeHammerPropAndValueDto CodeHammerPropAndValueDto in CodeHammerPropAndValueDtos)
                {
                    //// Declare the read-only Width property.
                    CodeMemberProperty property;
                    string formattedObj = char.ToUpper(CodeHammerPropAndValueDto.PropName[0]) + CodeHammerPropAndValueDto.PropName.Substring(1);

                    try
                    {
                        property = new CodeMemberProperty();
                        property.Attributes =
                        MemberAttributes.Public | MemberAttributes.Final;
                        property.Name = formattedObj;
                        property.HasGet = true;
                        property.Type = new CodeTypeReference(CodeHammerPropAndValueDto.PropValue);

                        property.Comments.Add(new CodeCommentStatement("<summary>", true));
                        property.Comments.Add(new CodeCommentStatement("The " + CodeHammerPropAndValueDto.VariableName + " property of the object.", true));
                        property.Comments.Add(new CodeCommentStatement("</summary>", true));
                    }
                    catch (Exception ex)
                    {
                        logFuncContract.Logger(ex.Message);
                        return false;
                    }

                    formattedObj = char.ToLower(CodeHammerPropAndValueDto.VariableName[0]) + CodeHammerPropAndValueDto.VariableName.Substring(1);

                    property.GetStatements.Add(new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), formattedObj)));

                    property.SetStatements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), formattedObj), new CodePropertySetValueReferenceExpression()));

                    targetClass.Members.Add(property);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion Fields And Properties

        #region Properties FLUENTNHIBERNATE

        /// <summary>
        /// Adds the properties fluentnhibernate.
        /// </summary>
        /// <param name="onlyDependencyDTO">if set to <c>true</c> [only dependency dto].</param>
        /// <param name="CodeHammerPropAndValueDtos">The property and value dtos.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkClass">The pk class.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Exception</exception>
        public bool AddPropertiesFLUENTNHIBERNATE(bool onlyDependencyDTO, List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos, string tablename, List<CodeHammerPkClass> pkClass)
        {
            try
            {
                CodeMemberField valueField = null;

                foreach (CodeHammerPropAndValueDto CodeHammerPropAndValueDto in CodeHammerPropAndValueDtos)
                {
                    ////check if the foreignkey is not the same as the variable. Due the mapping of foreignkeys.
                    if (!pkClass.Any(x => !string.IsNullOrEmpty(x.ForeignKeyRefID) && x.ForeignKeyRefID.Equals(CodeHammerPropAndValueDto.PropName)))
                    {
                        valueField = new CodeMemberField
                        {
                            Attributes = MemberAttributes.Public | MemberAttributes.Override,
                            Name = codeHammerDataUtilContract.FirstLetterUppercaseFirst(CodeHammerPropAndValueDto.PropName),
                            Type = new CodeTypeReference(CodeHammerPropAndValueDto.PropValue),
                        };

                        valueField.Name += " { get; set; }";

                        valueField.Comments.Add(new CodeCommentStatement("<summary>", true));
                        valueField.Comments.Add(new CodeCommentStatement("Gets or sets the " + codeHammerDataUtilContract.FirstLetterUppercaseFirst(CodeHammerPropAndValueDto.PropName), true));
                        valueField.Comments.Add(new CodeCommentStatement("</summary>", true));
                        valueField.Comments.Add(new CodeCommentStatement("<value>", true));
                        valueField.Comments.Add(new CodeCommentStatement("The " + codeHammerDataUtilContract.FirstLetterUppercaseFirst(CodeHammerPropAndValueDto.PropName), true));
                        valueField.Comments.Add(new CodeCommentStatement("</value>", true));
                        targetClass.Members.Add(valueField);
                    }
                }

                valueField = new CodeMemberField
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Override,
                    Name = "PageSize",
                    Type = new CodeTypeReference(typeof(int)),
                };

                valueField.Name += " { get; set; }";

                valueField.Comments.Add(new CodeCommentStatement("<summary>", true));
                valueField.Comments.Add(new CodeCommentStatement("The PageSize of the object.", true));
                valueField.Comments.Add(new CodeCommentStatement("</summary>", true));
                targetClass.Members.Add(valueField);

                #region Speciel case for refferenced foreign keys

                List<string> codeSetupPkCheck = new List<string>();
                codeSetupPkCheck.Clear();

                CodeMemberField valueFieldFk = null;
                var listTemp = pkClass;//.Distinct();
                foreach (CodeHammerPkClass pkClassItem in listTemp)
                {
                    if (!string.IsNullOrEmpty(pkClassItem.ForeignKeyRef))
                    {
                        if (pkClassItem.ForeignKeyRef != tablename)
                        {
                            if (!codeSetupPkCheck.Any(x => x.Equals(pkClassItem.ForeignKeyRef)))
                            {
                                codeSetupPkCheck.Add(pkClassItem.ForeignKeyRef);
                                valueFieldFk = new CodeMemberField
                                {
                                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                                    Name = pkClassItem.ForeignKeyRef,
                                    Type = new CodeTypeReference("CodeHammer" + pkClassItem.ForeignKeyRef + SuffixDto.Instance().DtoTextBox),
                                };

                                valueFieldFk.Name += " { get; set; }";

                                valueFieldFk.Comments.Add(new CodeCommentStatement("<summary>", true));
                                valueFieldFk.Comments.Add(new CodeCommentStatement("The " + pkClassItem.ForeignKeyRef + " of the object.", true));
                                valueFieldFk.Comments.Add(new CodeCommentStatement("</summary>", true));
                                targetClass.Members.Add(valueFieldFk);
                            }
                        }
                    }
                }

                #endregion Speciel case for refferenced foreign keys

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion Properties FLUENTNHIBERNATE

        #region FluentNhibernateMapping

        /// <summary>
        /// Adds the constructor fluent nhibernate mapping.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Property List cannot be null!
        /// or
        /// Property list cannot be empty!</exception>
        public bool AddFluentNhibernateMapping(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, List<CodeHammerPkClass> pkKeys)
        {
            try
            {
                string quates = @"""";

                if (CodeHammerPropAndValueDtoList == null)
                {
                    throw new Exception("Property List cannot be null!");
                }

                if (CodeHammerPropAndValueDtoList.Count == 0)
                {
                    throw new Exception("Property list cannot be empty!");
                }

                // Declare the constructor
                //Create Class Constructor
                CodeConstructor ccon = new CodeConstructor();
                ccon.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                CodeFieldReferenceExpression reference = new CodeFieldReferenceExpression();
                ccon.Parameters.Add(new CodeParameterDeclarationExpression());

                // Declaring a return statement for method ToString.

                StringBuilder str = new StringBuilder();
                str.Length = 0;
                str.Capacity = 0;
                //missing schema include
                //str.AppendLine();
                str.AppendLine("               Table(" + quates + tablename + "Domain" + quates + ");");
                str.AppendLine("               LazyLoad();");

                List<string> codeSetupPkCheck = new List<string>();
                codeSetupPkCheck.Clear();

                List<string> codeSetupFkCheck = new List<string>();
                codeSetupFkCheck.Clear();

                List<string> codeSetupPropertiesCheck = new List<string>();
                codeSetupPropertiesCheck.Clear();

                foreach (CodeHammerPropAndValueDto CodeHammerPropAndValueDto in CodeHammerPropAndValueDtoList)
                {
                    foreach (CodeHammerPkClass pkClass in pkKeys)
                    {
                        if (!string.IsNullOrEmpty(pkClass.PrimaryKeyRef))
                        {
                            if (!codeSetupPkCheck.Any(x => !string.IsNullOrEmpty(pkClass.PrimaryKeyRef) && x.Equals(pkClass.PrimaryKeyRef)))
                            {
                                codeSetupPkCheck.Add(pkClass.PrimaryKeyRef);
                                str.AppendLine("               Id(x => x." + codeHammerDataUtilContract.FirstLetterUppercaseFirst(pkClass.PrimaryKeyRef) + ").GeneratedBy.Identity[].Column(" + quates + codeHammerDataUtilContract.FirstLetterUppercaseFirst(pkClass.PrimaryKeyRef) + quates + ");");
                            }
                        }

                        if (!string.IsNullOrEmpty(pkClass.ForeignKeyRef))
                        {
                            if (!codeSetupFkCheck.Any(x => !string.IsNullOrEmpty(pkClass.ForeignKeyRefID) && x.Equals(pkClass.ForeignKeyRef)))
                            {
                                codeSetupFkCheck.Add(pkClass.ForeignKeyRef);
                                str.AppendLine("               References(x => x." + pkClass.ForeignKeyRef + ").Column(" + quates + pkClass.ForeignKeyRefID + quates + ");");
                            }
                        }

                        if (!codeSetupPropertiesCheck.Any(x => x.Equals(CodeHammerPropAndValueDto.PropName)))
                        {
                            if (!codeSetupPkCheck.Any(x => x.Equals(CodeHammerPropAndValueDto.PropName)))
                            {
                                codeSetupPropertiesCheck.Add(CodeHammerPropAndValueDto.PropName);
                                if (!pkKeys.Any(x => !string.IsNullOrEmpty(x.ForeignKeyRefID) && x.ForeignKeyRefID.Equals(CodeHammerPropAndValueDto.PropName)))
                                {
                                    str.AppendLine("               Map(x => x." + codeHammerDataUtilContract.FirstLetterUppercaseFirst(CodeHammerPropAndValueDto.PropName) + ").Column(" + quates + codeHammerDataUtilContract.FirstLetterUppercaseFirst(CodeHammerPropAndValueDto.PropName) + quates + ");");
                                }
                            }
                        }
                    }
                }

                ccon.Statements.Add(new CodeSnippetStatement(str.ToString()));
                targetClass.Members.Add(ccon);
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion FluentNhibernateMapping

        #region FluentNhibernateHelper

        /// <summary>
        /// Adds the constructor fluent nhibernate helper.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        public bool AddFluentNhibernateHelper(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, List<CodeHammerPkClass> pkKeys, string path)
        {
            try
            {
                string quates = @"""";

                StringBuilder str = new StringBuilder();
                str.Length = 0;
                str.Capacity = 0;

                str.AppendLine("    public class CodeHammerFluentNHibernateHelper");
                str.AppendLine("    {");

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// The session factory");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        private static ISessionFactory sessionFactory;");

                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// Gets the session factory.");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        /// <value>");
                str.AppendLine("        /// The session factory.");
                str.AppendLine("        /// </value>");
                str.AppendLine("        private static ISessionFactory SessionFactory");
                str.AppendLine("        {");
                str.AppendLine("            get");
                str.AppendLine("            {");
                str.AppendLine("                if (sessionFactory == null)");
                str.AppendLine("                    InitializeSessionFactory();");
                str.AppendLine("                    return sessionFactory;");
                str.AppendLine("            }");
                str.AppendLine("        }");
                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// Initializes the session factory.");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        private static void InitializeSessionFactory()");
                str.AppendLine("        {");

                str.AppendLine("            string systemMessage = string.Empty;");
                str.AppendLine("            string nestedSystemMessage;");
                str.AppendLine("            SqlConnection sqlConnection;");
                str.AppendLine();
                str.AppendLine("            if (!CodeHammerDatabaseManager.Instance.InitDatabaseConnection(out sqlConnection, out nestedSystemMessage))");
                str.AppendLine("            {");
                str.AppendLine("                 systemMessage = " + quates + "{" + Guid.NewGuid().ToString().ToUpper() + "}" + quates + "+" + quates + "   Error: CodeHammerDatabaseManager.Instance.InitDatabaseConnection " + quates + " + nestedSystemMessage;");
                str.AppendLine("                 return;");
                str.AppendLine("            }");
                str.AppendLine();

                str.AppendLine("             sessionFactory = Fluently.Configure()");
                str.AppendLine("            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(sqlConnection.ConnectionString))");
                str.AppendLine("            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))");
                str.AppendLine("            .BuildSessionFactory();");
                str.AppendLine("       }");
                str.AppendLine();

                str.AppendLine("       /// <summary>");
                str.AppendLine("       /// Opens the session.");
                str.AppendLine("       /// </summary>");
                str.AppendLine("       /// <returns>return true if success</returns>");
                str.AppendLine("       public static ISession OpenSession()");
                str.AppendLine("       {");
                str.AppendLine("           return SessionFactory.OpenSession();");
                str.AppendLine("       }");
                str.AppendLine("   }");

                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace CodeHammerRepository.Helper");
                    streamWriter.WriteLine("{");

                    streamWriter.WriteLine("    using System.Data.SqlClient;");
                    streamWriter.WriteLine("    using System.Reflection;");
                    streamWriter.WriteLine("    using CodeHammerRepository.Infrastructure;");
                    streamWriter.WriteLine("    using FluentNHibernate.Cfg;");
                    streamWriter.WriteLine("    using FluentNHibernate.Cfg.Db;");
                    streamWriter.WriteLine("    using NHibernate;");

                    streamWriter.WriteLine();

                    streamWriter.WriteLine("    /// <summary>");
                    streamWriter.WriteLine("    /// This class " + tablename);
                    streamWriter.WriteLine("    /// </summary>");
                    streamWriter.WriteLine(str.ToString());
                    streamWriter.WriteLine("}");
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion FluentNhibernateHelper

        #region FluentNhibernateUnitOfWork

        /// <summary>
        /// Adds the constructor fluent nhibernate helper.
        /// </summary>
        /// <param name="tablename">The tablename.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        public bool AddFluentNhibernateUnitOfWork(string tablename, string path)
        {
            try
            {
                string quates = @"""";

                StringBuilder str = new StringBuilder();
                str.Length = 0;
                str.Capacity = 0;

                str.AppendLine("    public class CodeHammerUnitOfWork");
                str.AppendLine("    {");

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// The session factory");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        private static ISessionFactory sessionFactory;");
                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// The _transaction");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        private static ITransaction transaction;");

                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        ///Initializes a new instance of the <see cref=" + quates + "UnitOfWork" + quates + "/> class.");
                str.AppendLine("        ///</summary>");
                str.AppendLine("        ///<param name=" + quates + "sessionFactory" + quates + ">The session factory.</param>");
                str.AppendLine("        ///");
                str.AppendLine("        public UnitOfWork(ISessionFactory sessionFactory)");
                str.AppendLine("        {");
                str.AppendLine("            sessionFactory = sessionFactory;");
                str.AppendLine("            Session = sessionFactory.OpenSession();");
                str.AppendLine("            Session.FlushMode = FlushMode.Auto;");
                str.AppendLine("            transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);");
                str.AppendLine("        }");

                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        public void Dispose()");
                str.AppendLine("        {");
                str.AppendLine("           if(Session.IsOpen)");
                str.AppendLine("           {");
                str.AppendLine("        	  Session.Close();");
                str.AppendLine("           }");
                str.AppendLine("        }");

                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// Commits this instance.");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        /// <exception cref=" + quates + "System.InvalidOperationException" + quates + ">No active transation</exception>");
                str.AppendLine("        public void Commit()");
                str.AppendLine("        {");
                str.AppendLine("        	if(!_transaction.IsActive)");
                str.AppendLine("        	{");
                str.AppendLine("        		throw new InvalidOperationException(" + quates + "No active transation" + quates + ");");
                str.AppendLine("        	}");
                str.AppendLine("        	_transaction.Commit();");
                str.AppendLine("        }");
                str.AppendLine();

                str.AppendLine("        /// <summary>");
                str.AppendLine("        /// Rollbacks this instance.");
                str.AppendLine("        /// </summary>");
                str.AppendLine("        public void Rollback()");
                str.AppendLine("        {");
                str.AppendLine("        	if(_transaction.IsActive)");
                str.AppendLine("        	{");
                str.AppendLine("        		transaction.Rollback();");
                str.AppendLine("        	}");
                str.AppendLine("        }");

                str.AppendLine("   }");

                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace CodeHammerRepository.Helper");
                    streamWriter.WriteLine("{");

                    streamWriter.WriteLine("    using System;");
                    streamWriter.WriteLine("    using System.Data;");
                    streamWriter.WriteLine("    using NHibernate;");

                    streamWriter.WriteLine();

                    streamWriter.WriteLine("    /// <summary>");
                    streamWriter.WriteLine("    /// This class CodeHammerUnitOfWork");
                    streamWriter.WriteLine("    /// </summary>");
                    streamWriter.WriteLine(str.ToString());
                    streamWriter.WriteLine("}");
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion FluentNhibernateUnitOfWork

        #region FluentNhibernateRepository

        /// <summary>
        /// Adds the constructor fluent nhibernate repository.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        public bool AddConstructorFluentNhibernateRepository(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, List<CodeHammerPkClass> pkKeys)
        {
            try
            {
                //    string quates = @"""";

                //    StringBuilder str = new StringBuilder();
                //    str.Length = 0;
                //    str.Capacity = 0;

                /*
                public ServerDataRepository()
            {
            }

            public ServerData Get(int id)
            {
                using (var session = NHibernateHelper.OpenSession())
                    return session.Get<ServerData>(id);
            }

            public IEnumerable<ServerData> GetAll()
            {
                using (var session = NHibernateHelper.OpenSession())
                    return session.Query<ServerData>().ToList();
            }

            public ServerData Add(ServerData serverData)
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(serverData);
                        transaction.Commit();
                    }
                    return serverData;
                }
            }

            public void Delete(int id)
            {
                var serverData = Get(id);

                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(serverData);
                        transaction.Commit();
                    }
                }
            }

            public bool Update(ServerData serverData)
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(serverData);
                        try
                        {
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    return true;
                }
            }

                 */
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion FluentNhibernateRepository

        #region FluentNhibernateIRepository

        /// <summary>
        /// Adds the constructor fluent nhibernate i repository.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        public bool AddConstructorFluentNhibernateIRepository(List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, List<CodeHammerPkClass> pkKeys)
        {
            try
            {
                //    string quates = @"""";

                //    StringBuilder str = new StringBuilder();
                //    str.Length = 0;
                //    str.Capacity = 0;

                /*
              ServerData Get(int id);
            IEnumerable<ServerData> GetAll();
            ServerData Add(ServerData serverData);
            void Delete(int id);
            bool Update(ServerData serverData);
                 */
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        #endregion FluentNhibernateIRepository

        #region Methods

        /// <summary>
        /// Adds a method to the class. This method multiplies values stored
        /// in both fields.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="type">The type. ex. typeof(DateTime)</param>
        /// <param name="CodeHammerPropAndValueDtoList">The property and value dto list.</param>
        /// <param name="formattedOutput">The formatted output.</param>
        /// <returns>if sucess then return true</returns>
        /// <exception cref="System.Exception">Property List cannot be null!
        /// or
        /// Property list cannot be empty!</exception>
        public bool AddMethod(string methodName, Type type, List<CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string formattedOutput)
        {
            try
            {
                CodeMemberMethod method = new CodeMemberMethod();
                method.Attributes =
                    MemberAttributes.Public | MemberAttributes.Override;
                method.Name = methodName;
                method.ReturnType =
                    new CodeTypeReference(type);

                if (CodeHammerPropAndValueDtoList == null)
                {
                    throw new Exception("Property List cannot be null!");
                }

                if (CodeHammerPropAndValueDtoList.Count == 0)
                {
                    throw new Exception("Property list cannot be empty!");
                }

                CodeFieldReferenceExpression reference;

                // Declaring a return statement for method ToString.
                CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement();
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < CodeHammerPropAndValueDtoList.Count; i++)
                {
                    reference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), ((CodeHammerPropAndValueDto)CodeHammerPropAndValueDtoList[i]).PropName);
                    returnStatement.Expression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(type), methodName,
                   new CodePrimitiveExpression(formattedOutput),
                   reference);
                    method.Statements.Add(returnStatement);
                }

                targetClass.Members.Add(method);
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Codes the check.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returnsif sucess then return true></returns>
        private bool CodeCheckOperators(string filename)
        {
            try
            {
                string contents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("};", "}");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }

                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Codes the check operators entities.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private bool CodeCheckOperatorsEntities(string filename)
        {
            try
            {
                string contents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("{ get; set; };", "{ get; set; }");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Codes the check operators fluent n hibernate constructor.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        private bool CodeCheckOperatorsFluentNHibernateConstructor(string filename)
        {
            try
            {
                string contents;
                string tempContents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("(void )", "()");
                    tempContents = contents.Replace("LazyLoad;", "LazyLoad();");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(tempContents);
                }
                return true;
            }
            catch (Exception ex)
            {
                logFuncContract.Logger(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Removes the generated comment.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="genOptions">The gen options.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        private bool RemoveGeneratedComment(CodeDomProvider provider, CodeGeneratorOptions genOptions, string filename)
        {
            try
            {
                using (var stringWriter = new StringWriter())
                using (var streamWriter = new StreamWriter(filename))
                {
                    provider.GenerateCodeFromCompileUnit(targetUnit, stringWriter, genOptions);
                    StringBuilder sb = stringWriter.GetStringBuilder();
                    /* Remove the header comment (444 is for C#, use 435 for VB) */
                    sb.Remove(0, 405);
                    streamWriter.WriteLine("// <auto-generated>");
                    streamWriter.WriteLine("//     This code was generated by a CodeHammer");
                    streamWriter.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    streamWriter.WriteLine("//     the code is regenerated");
                    streamWriter.WriteLine("// </auto-generated>");

                    streamWriter.Write(sb);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Codes the check att declaration operators.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        private bool CodeCheckAttDeclarationOperators(string filename)
        {
            try
            {
                string contents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("()", string.Empty);
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Codes the check virtual declaration operators.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeCheckVirtualDeclarationOperators(string filename)
        {
            try
            {
                string contents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("public", "public virtual");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }

                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("public virtual class", "public class");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Codes the check mapping declaration operators.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        public bool CodeCheckMappingDeclarationOperators(string filename)
        {
            try
            {
                string contents;
                using (StreamReader reader = new StreamReader(filename))
                {
                    contents = reader.ReadToEnd();

                    contents = contents.Replace("Identity[]", "Identity()");
                }

                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(contents);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Methods
    }
}