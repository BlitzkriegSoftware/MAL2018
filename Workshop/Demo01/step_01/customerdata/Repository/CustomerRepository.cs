using System.Collections.Generic;
using System.Linq;

namespace CustomerData.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public CustomerRepository() { }


        /// <summary>
        ///  Gets a customer by ID
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>Customer</returns>
        public Customer GetById(int id)
        {
            Customer customer = null;
            if (TestData.DataFactory.CustomerData.ContainsKey(id)) customer= TestData.DataFactory.CustomerData[id];
            return customer;
        }

        /// <summary>
        /// Search for text in Company, E-mail, Name First, Name Last
        /// </summary>
        /// <param name="text">Search text (invarient)</param>
        /// <returns>List of matches</returns>
        public IEnumerable<Customer> Search(string text)
        {
            text = text.ToLowerInvariant();
            var results = TestData.DataFactory.CustomerData.Values.AsQueryable().Where(c => 
               c.Company.ToLowerInvariant().Contains(text) ||
               c.EMail.ToLowerInvariant().Contains(text) ||
               c.NameFirst.ToLowerInvariant().Contains(text) ||
               c.NameLast.ToLowerInvariant().Contains(text) 
           ).Select(c => c).ToList();

            return results;
        }


        /// <summary>
        /// Search for customers by address
        /// </summary>
        /// <param name="text">Search text (invarient)</param>
        /// <returns>List of matches</returns>
        public IEnumerable<Customer> SearchByAddress(string text)
        {
            text = text.ToLowerInvariant();
            var results = TestData.DataFactory.CustomerData.Values.AsQueryable()
                    .Where(c => c.Addresses.Any(a => 
                        a.City.ToLowerInvariant().Contains(text) ||
                        a.Address1.ToLowerInvariant().Contains(text) ||
                        (a.Address2 + "").ToLowerInvariant().Contains(text)
                    )).ToList();
            return results;
        }

        /// <summary>
        /// Add/Update 
        /// </summary>
        /// <param name="c2">Customer</param>
        /// <returns>Customer</returns>
        public Customer AddUpdate(Customer c2)
        {
            if(TestData.DataFactory.CustomerData.ContainsKey(c2._id))
            {
                TestData.DataFactory.CustomerData[c2._id] = c2;
            } else
            {
                TestData.DataFactory.CustomerData.Add(c2._id, c2);
            }
            return c2;
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>True is so</returns>
        public bool Delete(int id)
        {
            bool deleted = false;
            if(TestData.DataFactory.CustomerData.ContainsKey(id)) { TestData.DataFactory.CustomerData.Remove(id); deleted = true;  }
            return deleted;
        }

    }
}
