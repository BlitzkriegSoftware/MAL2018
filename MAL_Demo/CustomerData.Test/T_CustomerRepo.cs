using CustomerData.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerData.Test
{
    [TestClass]
    public class T_CustomerRepo
    {
        public TestContext TestContext { get; set; }

       [TestMethod]
        public void T_RoundTrip_1()
        {
            var repo = new CustomerRepository();

            var c = repo.GetById(1);

            Assert.IsNotNull(c);
            TestContext.WriteLine("ById: {0}", c.ToString());

            var c2 = Common.DeepCopier.DeepCopy<Customer>(c);

            c2._id = 0;
            c2.Birthday = new System.DateTime(1983, 3, 21);

            TestContext.WriteLine("New: {0}", c2.ToString());

            var c3 = repo.AddUpdate(c2);
            TestContext.WriteLine("Post New: {0}", c3.ToString());

            var deleted = repo.Delete(c3._id);
            Assert.IsTrue(deleted);

            var c4 = repo.GetById(c3._id);
            Assert.IsNull(c4);

            c2 = Common.DeepCopier.DeepCopy<Customer>(c);
            var c5 = repo.AddUpdate(c2);
            TestContext.WriteLine("Update: {0}", c5.ToString());

        }
    }
}
