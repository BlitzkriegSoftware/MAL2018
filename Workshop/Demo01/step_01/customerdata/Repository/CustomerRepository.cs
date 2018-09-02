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
            return new Customer();
        }

        /// <summary>
        /// Add/Update 
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Customer</returns>
        public Customer AddUpdate(Customer c)
        {
            return c;
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>True is so</returns>
        public bool Delete(int id)
        {
            return true;
        }

    }
}
