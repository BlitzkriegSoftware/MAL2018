using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CustomerData;

namespace customerwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerData.Repository.ICustomerRepository _customerRepository;

        public CustomerController(CustomerData.Repository.ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_customerRepository.GetById(id));
        }

        [HttpPost]
        [Route("AddUpdate")]
        public ActionResult AddUpdate(Customer c)
        {
            return Ok(_customerRepository.AddUpdate(c));
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_customerRepository.Delete(id));
        }



    }
}