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
