using Common;
using Common.Models;
using CustomerData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.Test
{
    [TestClass]
    public class T_Serialization
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void T_ErrorPayload_1()
        {
            var model = new ErrorPayload()
            {
                Data = new Dictionary<string, string>(),
                Message = "message",
                StackTrace = "stack trace",
                StatusCode = 200
            };

            model.Data.Add("k1", "v1");
            model.Data.Add("k2", "v2");

            Helpers.AssertHelper.AssertSerialization<ErrorPayload>(model, this.TestContext);
        }

        [TestMethod]
        public void T_Customer_1()
        {
            var model = CustomerMaker.PersonMake(10031);

            Helpers.AssertHelper.AssertSerialization<Customer>(model, this.TestContext);

            var b = model.Addresses[0].IsEmpty;
            Assert.IsFalse(b);
    
        }

        [TestMethod]
        public void T_ValidationException_1()
        {
            var validationErrors = new List<string>() { "A", "B", "C" };
            var data = new Dictionary<string, string>();
            data.Add("1", "a");
            data.Add("2", "b");

            var model = new BsValidationException("message", "resource", validationErrors)
            {
                Source = "Source",
                HelpLink = "Help Link"
            };

            Helpers.AssertHelper.AssertSerialization<BsValidationException>(model, this.TestContext);

            var text = model.ValidationText(";");
            Assert.IsFalse(string.IsNullOrWhiteSpace(text));
        }

    }
}
