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
    using System.Collections.Generic;

    /// <summary>
    /// Class that stores information for tables in a database.
    /// </summary>

    public class CodeHammerTableDto
    {
        /// <summary>
        /// Default constructor; initializes all collections.
        /// </summary>
        public CodeHammerTableDto()
        {
            this.CodeHammerColumns = new List<CodeHammerColumn>();
            this.CodeHammerPrimaryKeys = new List<CodeHammerColumn>();
            this.CodeHammerForeignKeys = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Contains the list of CodeHammerColumn instances that define the table.
        /// </summary>
        /// <value>
        /// The code hammer columns.
        /// </value>
        public List<CodeHammerColumn> CodeHammerColumns { get; set; }

        /// <summary>
        /// Contains the list of CodeHammerColumn instances that define the table.  The Dictionary returned
        /// is keyed on the foreign key name, and the value associated with the key is an
        /// List of CodeHammerColumn instances that compose the foreign key.
        /// </summary>
        /// <value>
        /// The code hammer foreign keys.
        /// </value>
        public Dictionary<string, List<string>> CodeHammerForeignKeys { get; set; }

        /// <summary>
        /// Gets the code hammer nullable fields.
        /// </summary>
        /// <value>
        /// The code hammer nullable fields.
        /// </value>
        public Dictionary<string, string> CodeHammerNullableFields { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        /// <value>
        /// The name of the code hammer.
        /// </value>
        public string CodeHammerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the code hammer schema.
        /// </summary>
        /// <value>
        /// The name of the code hammer schema.
        /// </value>
        public string CodeHammerSchemaName { get; set; }

        /// <summary>
        /// Contains the list of primary key CodeHammerColumn instances that define the table.
        /// </summary>
        /// <value>
        /// The code hammer primary keys.
        /// </value>
        public List<CodeHammerColumn> CodeHammerPrimaryKeys { get; set; }
    }
}