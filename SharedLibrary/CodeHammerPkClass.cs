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
    /// this class CodeHammerPkClass
    /// Primary key class
    /// </summary>

    public class CodeHammerPkClass
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        private static CodeHammerPkClass instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerPkClass"/> class.
        /// </summary>
        public CodeHammerPkClass()
        {
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static CodeHammerPkClass Instance()
        {
            if (instance == null)
            {
                instance = new CodeHammerPkClass();
            }

            return instance;
        }

        #endregion Singleton

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the col.
        /// </summary>
        /// <value>
        /// The name of the col.
        /// </value>
        public string ColName { get; set; }

        /// <summary>
        /// Gets or sets the is identity.
        /// </summary>
        /// <value>
        /// The is identity.
        /// </value>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Gets or sets the is nullable.
        /// </summary>
        /// <value>
        /// The is nullable.
        /// </value>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference.
        /// </summary>
        /// <value>
        /// The foreign key reference.
        /// </value>
        public string ForeignKeyRef { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference identifier.
        /// </summary>
        /// <value>
        /// The foreign key reference identifier.
        /// </value>
        public string ForeignKeyRefID { get; set; }

        /// <summary>
        /// Gets or sets the primary key reference.
        /// </summary>
        /// <value>
        /// The primary key reference.
        /// </value>
        public string PrimaryKeyRef { get; set; }
    }
}