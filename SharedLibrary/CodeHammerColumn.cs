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
    /// Class that stores information for columns in a database table.
    /// </summary>

    public class CodeHammerColumn
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        private static CodeHammerColumn instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerColumn"/> class.
        /// </summary>
        public CodeHammerColumn()
        {
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static CodeHammerColumn Instance()
        {
            if (instance == null)
            {
                instance = new CodeHammerColumn();
            }

            return instance;
        }

        #endregion Singleton

        #region Properties

        /// <summary>
        /// Gets or sets the name of the code hammer table.
        /// </summary>
        /// <value>
        /// The name of the code hammer table.
        /// </value>
        public string CodeHammerTableName { get; set; }

        /// <summary>
        /// Name of the column.
        /// </summary>
        /// <value>
        /// The name of the code hammer.
        /// </value>
        public string CodeHammerName { get; set; }

        /// <summary>
        /// Data type of the column.
        /// </summary>
        /// <value>
        /// The type of the code hammer.
        /// </value>
        public string CodeHammerType { get; set; }

        /// <summary>
        /// Length in bytes of the column.
        /// </summary>
        /// <value>
        /// The length of the code hammer.
        /// </value>
        public string CodeHammerLength { get; set; }

        /// <summary>
        /// Precision of the column.  Applicable to decimal, float, and numeric data types only.
        /// </summary>
        /// <value>
        /// The code hammer precision.
        /// </value>
        public string CodeHammerPrecision { get; set; }

        /// <summary>
        /// Scale of the column.  Applicable to decimal, and numeric data types only.
        /// </summary>
        /// <value>
        /// The code hammer scale.
        /// </value>
        public string CodeHammerScale { get; set; }

        /// <summary>
        /// Flags the column as a uniqueidentifier column.
        /// </summary>
        /// <value>
        /// <c>true</c> if [code hammer is row GUID col]; otherwise, <c>false</c>.
        /// </value>
        public bool CodeHammerIsRowGuidCol { get; set; }

        /// <summary>
        /// Flags the column as an identity column.
        /// </summary>
        /// <value>
        /// <c>true</c> if [code hammer is identity]; otherwise, <c>false</c>.
        /// </value>
        public bool CodeHammerIsIdentity { get; set; }

        /// <summary>
        /// Flags the column as being computed.
        /// </summary>
        /// <value>
        /// <c>true</c> if [code hammer is computed]; otherwise, <c>false</c>.
        /// </value>
        public bool CodeHammerIsComputed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [code hammer is nullable].
        /// </summary>
        /// <value>
        /// <c>true</c> if [code hammer is nullable]; otherwise, <c>false</c>.
        /// </value>
        public bool CodeHammerIsNullable { get; set; }

        #endregion Properties
    }
}