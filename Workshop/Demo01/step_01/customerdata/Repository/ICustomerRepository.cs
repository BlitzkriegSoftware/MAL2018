using System.Collections.Generic;

namespace CustomerData.Repository
{
    public interface ICustomerRepository
    {
        /// <summary>
        ///  Gets a customer by ID
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>Customer</returns>
        Customer GetById(int id);

        /// <summary>
        /// Search for customers (search all string fields)
        /// </summary>
        /// <param name="text">Search text</param>
        /// <returns>Customer matches</returns>
        IEnumerable<Customer> Search(string text);

        /// <summary>
        /// Search by Address
        /// </summary>
        /// <param name="text">Search text</param>
        /// <returns>Customer matches</returns>
        IEnumerable<Customer> SearchByAddress(string text);

        /// <summary>
        /// Add/Update 
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Customer</returns>
        Customer AddUpdate(Customer c);
        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>True is so</returns>
        bool Delete(int id);

    }
}
