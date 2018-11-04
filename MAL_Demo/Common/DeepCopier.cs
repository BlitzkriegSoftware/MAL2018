using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// Deep Copier
    /// </summary>
    public static class DeepCopier
    {
        /// <summary>
        /// Makes a Deep Copy using Json Serialization
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="original">Object Instance To Copy</param>
        /// <returns>Type</returns>
        public static T DeepCopy<T>(T original)
        {
            var json = JsonConvert.SerializeObject(original);
            var o2 = JsonConvert.DeserializeObject<T>(json);
            return o2;
        }
    }
}
