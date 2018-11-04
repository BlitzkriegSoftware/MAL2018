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

        private static Dictionary<int, Customer> _customers = null;

        public static Dictionary<int, Customer> CustomerData
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new Dictionary<int, Customer>();
                    for (int i = 0; i < PeopleCount; i++)
                    {
                        var id = i + 1;
                        var p = CustomerMaker.PersonMake(id);
                        _customers.Add(id, p);
                    }
                }

                return _customers;
            }
        }

    }
}
