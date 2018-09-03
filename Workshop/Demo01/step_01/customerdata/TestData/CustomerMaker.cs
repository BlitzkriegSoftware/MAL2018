using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.TestData
{
    public static class CustomerMaker
    {
        private static Random Dice = new Random();

        /// <summary>
        /// Make a Person Full of Data
        /// </summary>
        /// <returns></returns>
        public static Customer PersonMake(int id)
        {
            var c = new Customer()
            {
                _id = id,
                Birthday = Faker.Date.Birthday(),
                EMail = Faker.User.Email(),
                NameLast = Faker.Name.LastName()
            };

            var gender = Faker.Name.Gender();
            if (gender.ToLowerInvariant().StartsWith("f"))
            {
                c.NameFirst = Faker.Name.FemaleFirstName();
            }
            else
            {
                c.NameFirst = Faker.Name.MaleFirstName();
            }

            c.Company = c.EMail.Substring(c.EMail.IndexOf('@') + 1);

            c.EMail = string.Format("{0}.{1}@{2}", c.NameFirst, c.NameLast, c.Company);

            for (int p = 0; p < Dice.Next(2, 6); p++)
            {
                c.Preference.Add(string.Format("{0}-{1}", Faker.Lorem.Word(), p), Faker.Lorem.Sentence());
            }

            c.Addresses.Add(new Models.Address()
            {
                Address1 = string.Format("{0} {1} {2}", Faker.Number.RandomNumber(101, 8888), Faker.Address.StreetName(), Faker.Address.StreetSuffix()),
                City = Faker.Address.USCity(),
                State = Faker.Address.StateAbbreviation(),
                Zip = Faker.Address.USZipCode(),
                Kind = Models.AddressKind.Mailing
            });

            if (Dice.Next(1, 10) > 7)
            {
                c.Addresses.Add(new Models.Address()
                {
                    Address1 = string.Format("{0} {1} {2}", Faker.Number.RandomNumber(101, 8888), Faker.Address.StreetName(), Faker.Address.StreetSuffix()),
                    City = Faker.Address.USCity(),
                    State = Faker.Address.StateAbbreviation(),
                    Zip = Faker.Address.USZipCode(),
                    Kind = Models.AddressKind.Billing
                });
            }

            return c;
        }
    }
}
