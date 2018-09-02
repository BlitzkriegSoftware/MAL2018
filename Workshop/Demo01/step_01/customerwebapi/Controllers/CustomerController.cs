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
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_customerRepository.GetById(id));
        }

        /// <summary>
        /// Add/Update
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Customer</returns>
        [HttpPost]
        [Route("AddUpdate")]
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
        public ActionResult Delete(int id)
        {
            return Ok(_customerRepository.Delete(id));
        }

    }
}