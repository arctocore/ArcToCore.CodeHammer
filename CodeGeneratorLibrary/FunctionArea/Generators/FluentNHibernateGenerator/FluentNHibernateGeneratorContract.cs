/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Framework.FunctionArea.Generators.FluentNHibernateGeneratorPENDING
{
    using System;

    /// <summary>
    /// this interface FluentNHibernateGeneratorContract
    /// </summary>

    public interface FluentNHibernateGeneratorContract
    {
        /// <summary>
        /// Adds the constructor.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <returns>if sucess then return true</returns>
        bool AddConstructor(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList);

        /// <summary>
        /// Adds the constructor fluent nhibernate i repository.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        bool AddConstructorFluentNhibernateIRepository(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys);

        /// <summary>
        /// Adds the constructor fluent nhibernate repository.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        bool AddConstructorFluentNhibernateRepository(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys);

        /// <summary>
        /// Adds the fields.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtos">The code hammer property and value dtos.</param>
        /// <returns>if sucess then return true</returns>
        bool AddFields(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos);

        /// <summary>
        /// Adds the fluent nhibernate helper.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool AddFluentNhibernateHelper(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys, string path);

        /// <summary>
        /// Adds the fluent nhibernate mapping.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        bool AddFluentNhibernateMapping(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string tablename, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys);

        /// <summary>
        /// Adds the fluent nhibernate unit of work.
        /// </summary>
        /// <param name="tablename">The tablename.</param>
        /// <param name="path">The path.</param>
        /// <returns>if sucess then return true</returns>
        bool AddFluentNhibernateUnitOfWork(string tablename, string path);

        /// <summary>
        /// Adds the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="type">The type.</param>
        /// <param name="CodeHammerPropAndValueDtoList">The code hammer property and value dto list.</param>
        /// <param name="formattedOutput">The formatted output.</param>
        /// <returns>if sucess then return true</returns>
        bool AddMethod(string methodName, Type type, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtoList, string formattedOutput);

        /// <summary>
        /// Adds the properties.
        /// </summary>
        /// <param name="CodeHammerPropAndValueDtos">The code hammer property and value dtos.</param>
        /// <returns>if sucess then return true</returns>
        bool AddProperties(System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos);

        /// <summary>
        /// Adds the properties fluentnhibernate.
        /// </summary>
        /// <param name="onlyDependencyDTO">if set to <c>true</c> [only dependency dto].</param>
        /// <param name="CodeHammerPropAndValueDtos">The code hammer property and value dtos.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="pkClass">The pk class.</param>
        /// <returns>if sucess then return true</returns>
        bool AddPropertiesFLUENTNHIBERNATE(bool onlyDependencyDTO, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPropAndValueDto> CodeHammerPropAndValueDtos, string tablename, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkClass);

        /// <summary>
        /// Codes the check mapping declaration operators.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeCheckMappingDeclarationOperators(string filename);

        /// <summary>
        /// Codes the check virtual declaration operators.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>if sucess then return true</returns>
        bool CodeCheckVirtualDeclarationOperators(string filename);

        /// <summary>
        /// Generates the c sharp code.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>if sucess then return true</returns>
        bool GenerateCSharpCode(string fileName);

        /// <summary>
        /// Generates the vb code.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>if sucess then return true</returns>
        bool GenerateVBCode(string fileName);

        /// <summary>
        /// Setups the fluent n hibernate.
        /// </summary>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="typeclass">The typeclass.</param>
        /// <param name="pkKeys">The pk keys.</param>
        /// <returns>if sucess then return true</returns>
        bool SetupFluentNHibernate(string namespaceName, string tablename, string typeclass, System.Collections.Generic.List<CodeHammer.Entities.CodeHammerPkClass> pkKeys);
    }
}