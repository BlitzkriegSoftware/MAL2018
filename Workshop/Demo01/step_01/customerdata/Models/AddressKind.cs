using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.Models
{
    /// <summary>
    /// Kind of Address
    /// </summary>
    public enum AddressKind
    {
        /// <summary>
        /// Mailing
        /// </summary>
        Mailing = 0,
        /// <summary>
        /// Billing
        /// </summary>
        Billing = 1,
        /// <summary>
        /// Other
        /// </summary>    
        Other = 2
    }
}
