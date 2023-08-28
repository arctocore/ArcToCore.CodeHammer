/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Crypto
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// this class SelfSignedCertProperties
    /// </summary>
    public class SelfSignedCertProperties
    {
        /// <summary>
        /// Gets or sets the valid from.
        /// </summary>
        /// <value>
        /// The valid from.
        /// </value>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public X500DistinguishedName Name { get; set; }

        public int KeyBitLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private key exportable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private key exportable; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivateKeyExportable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfSignedCertProperties"/> class.
        /// </summary>
        public SelfSignedCertProperties()
        {
            DateTime today = DateTime.Today;
            ValidFrom = today.AddDays(-1);
            ValidTo = today.AddYears(10);
            Name = new X500DistinguishedName("cn=self");
            KeyBitLength = 4096;
        }
    }
}