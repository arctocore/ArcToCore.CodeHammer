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
    /// this class CertInformation
    /// </summary>

    public class CertInformation
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        private static CertInformation instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertInformation"/> class.
        /// </summary>
        public CertInformation()
        {
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static CertInformation Instance()
        {
            if (instance == null)
            {
                instance = new CertInformation();
            }

            return instance;
        }

        #endregion Singleton

        public string StoreLocation { get; set; }

        public string FindValue { get; set; }

        public string StoreName { get; set; }

        public string X509FindType { get; set; }

    }
}