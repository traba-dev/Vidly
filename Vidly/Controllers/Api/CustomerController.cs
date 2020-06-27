using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomerController : ApiController
    {
        public VidlyContext _Context;

        public CustomerController()
        {
            _Context = new VidlyContext();
        }
        //GET Api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            IEnumerable<CustomerDto> customers = _Context.Customers.Include(c => c.MembershipType).ToList().Select(Mapper.Map<Customer,CustomerDto>);

            return customers;
        }
        //GET Api/customers/id
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }
        //POST Api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _Context.Customers.Add(Mapper.Map<CustomerDto,Customer>(customerDto));
            _Context.SaveChanges();

            customerDto.Id = Mapper.Map<CustomerDto, Customer>(customerDto).Id;

            return Created(new Uri(Request.RequestUri+"/"+ Mapper.Map<CustomerDto, Customer>(customerDto).Id), customerDto);
        }

        //PUT Api/customers
        [HttpPut]
        public void UpdateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer _Customer = _Context.Customers.SingleOrDefault(c => c.Id == customerDto.Id);

            if (_Customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, _Customer);
          
            _Context.Entry(_Customer).State = EntityState.Modified;
            _Context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer _Customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

            if (_Customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _Context.Customers.Remove(_Customer);
            _Context.SaveChanges();
        }
    }
}
