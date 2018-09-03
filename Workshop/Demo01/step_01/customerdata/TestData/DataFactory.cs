using System.Collections.Generic;
using System.Linq;

namespace CustomerData.TestData
{
    /// <summary>
    /// Data Factory
    /// </summary>
    public static class DataFactory
    {
        private const int PeopleCount = 100;

        private static List<Customer> _list = null;

        /// <summary>
        /// Customers
        /// </summary>
        public static IQueryable<Customer> Customers
        {
            get
            {
                return CustomerData.AsQueryable<Customer>();
            }
        }

        public static List<Customer> CustomerData
        {
            get
            {
                if (_list == null)
                {
                    _list = new List<Customer>();
                    for (int i = 0; i < PeopleCount; i++)
                    {
                        var id = i + 1;
                        var p = CustomerMaker.PersonMake(id);
                        _list.Add(p);
                    }
                }

                return _list;
            }
        }

    }
}
