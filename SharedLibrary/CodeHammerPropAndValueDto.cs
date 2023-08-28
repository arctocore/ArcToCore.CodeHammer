/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Entities
{
    /// <summary>
    /// this class CodeHammerPropAndValueDto
    /// </summary>

    public class CodeHammerPropAndValueDto
    {
        #region Singleton

        private static CodeHammerPropAndValueDto instance;

        public CodeHammerPropAndValueDto()
        {
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static CodeHammerPropAndValueDto Instance()
        {
            if (instance == null)
            {
                instance = new CodeHammerPropAndValueDto();
            }

            return instance;
        }

        #endregion Singleton

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropName { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>
        /// The property value.
        /// </value>
        public dynamic PropValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        /// <value>
        /// The name of the variable.
        /// </value>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets the variable value.
        /// </summary>
        /// <value>
        /// The variable value.
        /// </value>
        public dynamic VariableValue { get; set; }

        /// <summary>
        /// Gets or sets the class reference.
        /// </summary>
        /// <value>
        /// The class reference.
        /// </value>
        public dynamic ClassRef { get; set; }
    }
}