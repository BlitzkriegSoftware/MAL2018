﻿using CustomerData.Models;
using System;
using System.Collections.Generic;

namespace CustomerData
{
    /// <summary>
    /// Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// PK of Person
        /// <para>Mongo DB is super fussy about this name</para>
        /// </summary>
        public int _id { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string NameLast { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        public string NameFirst { get; set; }
        /// <summary>
        /// E-Mail
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime Birthday { get; set; }

        private List<Address> _addresses = null;

        /// <summary>
        /// Address List
        /// </summary>
        public List<Address> Addresses
        {
            get
            {
                if (_addresses == null) _addresses = new List<Address>();
                return _addresses;
            }
        }

        private Dictionary<string, string> _preference = null;
        /// <summary>
        /// Preferences
        /// </summary>
        public Dictionary<string, string> Preference
        {
            get
            {
                if (_preference == null) _preference = new Dictionary<string, string>();
                return _preference;
            }
        }

        /// <summary>
        ///  Debug string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("id {0}|{1}|{2}|{3:yyyy/MM/dd}|{4}|{5}|p {6}|a {7}", this._id, this.NameFirst, this.NameLast, this.Birthday, this.Company, this.EMail, this.Preference.Count, this.Addresses.Count);
        }

    }
}
