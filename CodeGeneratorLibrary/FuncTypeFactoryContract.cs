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
    /// <summary>
    /// this interface FuncTypeFactoryContract
    /// </summary>

    public interface FuncTypeFactoryContract
    {
        /// <summary>
        /// Gets the function from type enum.
        /// </summary>
        /// <param name="funcTypeEnum">The function type enum.</param>
        /// <returns></returns>
        dynamic GetFuncFromTypeEnum(FuncTypeFactory.FuncTypeEnum funcTypeEnum);
    }
}