using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customerwebapi.Models
{
    /// <summary>
    /// Error Details
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Debug To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
