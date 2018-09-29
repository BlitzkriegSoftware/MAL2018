using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CustomerData;

namespace customerwebapi.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerData.Repository.ICustomerRepository _customerRepository;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="customerRepository">ICustomerRepository</param>
        public CustomerController(CustomerData.Repository.ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <response code="200">Customer</response>
        /// <response code="404">ID Not Found</response>
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(CustomerData.Customer), 200)]
        public ActionResult GetById(int id)
        {
            var c = _customerRepository.GetById(id);
            if (c != null)
                return Ok(c);
            else
                return NotFound(id);
        }

        /// <summary>
        /// Search First Name, Last Name, E-Mail, Company
        /// </summary>
        /// <param name="text">search text (invariant)</param>
        /// <returns>List of matching customers</returns>
        [HttpGet]
        [Route("Search/{text}")]
        [ProducesResponseType(typeof(IEnumerable<CustomerData.Customer>), 200)]
        public ActionResult Search(string text)
        {
            var r = _customerRepository.Search(text);
            return Ok(r);
        }


        /// <summary>
        /// Search Address1, Address2, City
        /// </summary>
        /// <param name="text">search text (invariant)</param>
        /// <returns>List of matching customers</returns>
        [HttpGet]
        [Route("SearchByAddress/{text}")]
        [ProducesResponseType(typeof(IEnumerable<CustomerData.Customer>), 200)]
        public ActionResult SearchByAddress(string text)
        {
            var r = _customerRepository.SearchByAddress(text);
            return Ok(r);
        }

        /// <summary>
        /// Add/Update
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Customer</returns>
        [HttpPost]
        [Route("AddUpdate")]
        [ProducesResponseType(typeof(CustomerData.Customer), 200)]
        public ActionResult AddUpdate(Customer c)
        {
            return Ok(_customerRepository.AddUpdate(c));
        }

        /// <summary>
        /// Delete by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>True if ok</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public ActionResult Delete(int id)
        {
            return Ok(_customerRepository.Delete(id));
        }

    }
}