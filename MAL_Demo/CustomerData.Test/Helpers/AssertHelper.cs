using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerData.Test.Helpers
{
    /// <summary>
    /// Helpers to do asserts on objects in bulk
    /// </summary>
    public class AssertHelper
    {
        /// <summary>
        /// XUnit Tests that a model serializes correctly
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="thing1">Instance of T to test</param>
        /// <param name="output">XUnit Test Output</param>
        public static void AssertSerialization<T>(T thing1, TestContext output)
        {
            var json = JsonConvert.SerializeObject(thing1);

            output.WriteLine("{0} -> {1}", thing1.GetType().Name, json);

            T thing2 = JsonConvert.DeserializeObject<T>(json);

            Dictionary<String, Object> t1 = thing1.GetType()
                .GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(thing1, null));

            Dictionary<String, Object> t2 = thing2.GetType()
                .GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(thing2, null));

            foreach (var key in t1.Keys)
            {
                var type = thing1.GetType();
                var mem = type.GetMember(key).First();
                if (!Attribute.IsDefined(mem, typeof(JsonIgnoreAttribute)))
                {
                    if (t1[key] != null)
                    {
                        Assert.AreEqual(t1[key].ToString(), t2[key].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Compare two dates down to the second, but not past that
        /// </summary>
        /// <param name="d1">date-1</param>
        /// <param name="d2">date-2</param>
        public static void AssertDatesEqual(DateTime d1, DateTime d2)
        {
            Assert.AreEqual(d1.Year, d2.Year);
            Assert.AreEqual(d1.Month, d2.Month);
            Assert.AreEqual(d1.Day, d2.Day);
            Assert.AreEqual(d1.Hour, d2.Hour);
            Assert.AreEqual(d1.Minute, d2.Minute);
            Assert.AreEqual(d1.Second, d2.Second);
        }

    }
}