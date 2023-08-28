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
    /// this class SuffixDto
    /// </summary>

    public class SuffixDto
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        private static SuffixDto instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixDto"/> class.
        /// </summary>
        protected SuffixDto()
        {
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static SuffixDto Instance()
        {
            if (instance == null)
            {
                instance = new SuffixDto();
            }

            return instance;
        }

        #endregion Singleton

        /// <summary>
        /// Gets or sets the code hammer path.
        /// </summary>
        /// <value>
        /// The code hammer path.
        /// </value>
        public string CodeHammerPath { get; set; }

        /// <summary>
        /// Gets or sets the bl suffix text box.
        /// </summary>
        /// <value>
        /// The bl suffix text box.
        /// </value>
        public string BlSuffixTextBox { get; set; }

        /// <summary>
        /// Gets or sets the dal text box.
        /// </summary>
        /// <value>
        /// The dal text box.
        /// </value>
        public string DalTextBox { get; set; }

        /// <summary>
        /// Gets or sets the dto text box.
        /// </summary>
        /// <value>
        /// The dto text box.
        /// </value>
        public string DtoTextBox { get; set; }

        /// <summary>
        /// Gets or sets the data contract text box.
        /// </summary>
        /// <value>
        /// The data contract text box.
        /// </value>
        public string DataContractTextBox { get; set; }

        /// <summary>
        /// Gets or sets the service contract text box.
        /// </summary>
        /// <value>
        /// The service contract text box.
        /// </value>
        public string ServiceContractTextBox { get; set; }

        /// <summary>
        /// Gets or sets the service text box.
        /// </summary>
        /// <value>
        /// The service text box.
        /// </value>
        public string ServiceTextBox { get; set; }

        /// <summary>
        /// Gets or sets the solution text box.
        /// </summary>
        /// <value>
        /// The solution text box.
        /// </value>
        public string SolutionTextBox { get; set; }

        /// <summary>
        /// Gets or sets the test text box.
        /// </summary>
        /// <value>
        /// The test text box.
        /// </value>
        public string TestTextBox { get; set; }

        /// <summary>
        /// Gets or sets the name space text box.
        /// </summary>
        /// <value>
        /// The name space text box.
        /// </value>
        public string NameSpaceTextBox { get; set; }

        /// <summary>
        /// Gets or sets the SQL prefix text box.
        /// </summary>
        /// <value>
        /// The SQL prefix text box.
        /// </value>
        public string SqlPrefixTextBox { get; set; }
    }
}