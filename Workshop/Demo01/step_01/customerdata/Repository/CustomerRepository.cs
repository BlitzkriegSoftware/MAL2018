using System.Linq;

namespace CustomerData.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public CustomerRepository()
        {

        }

        /// <summary>
        ///  Gets a customer by ID
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>Customer</returns>
        public Customer GetById(int id)
        {
            IQueryable<Customer> customers = TestData.DataFactory.Customers;
            var customer = customers.Where(c => c._id == id).FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Add/Update 
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Customer</returns>
        public Customer AddUpdate(Customer c)
        {
            var customer = TestData.DataFactory.Customers.Where(t => t._id == c._id).FirstOrDefault();
            if((customer == null) || (c._id <= 0))
            {
                int id = TestData.DataFactory.Customers.Max(p => p._id);
                id++;
                c._id = id;
                TestData.DataFactory.CustomerData.Add(c);
            } else
            {
                var index = TestData.DataFactory.CustomerData.IndexOf(c);
                if (index > 0) TestData.DataFactory.CustomerData.RemoveAt(index);
                TestData.DataFactory.CustomerData.Add(c);
            }

            return c;
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>True is so</returns>
        public bool Delete(int id)
        {
            bool deleted = false;
            var customer = TestData.DataFactory.Customers.Where(t => t._id == id).FirstOrDefault();
            var index = TestData.DataFactory.CustomerData.IndexOf(customer);
            if (index > 0) { TestData.DataFactory.CustomerData.RemoveAt(index); deleted = true; }
            return deleted;
        }

    }
}
