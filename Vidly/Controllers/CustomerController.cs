using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        private VidlyContext _context;

        public CustomerController()
        {
            _context = new VidlyContext();
        }


        // GET: Customer
        public ActionResult Index()
        {
            //include function is use to load data with eager loading
            //List<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();

            //return View(customers);
            return View();
        }

        public ActionResult Details(int id)
        {

            List<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            Customer customer = new Customer();

            customer = customers.Find(u => u.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }


            return View(customer);
        }

        public ActionResult NewCustomer()
        {
            List<MembershipType> membershipTypes = _context.MembershipTypes.ToList();

            CustomerFormViewModel newCustomer = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("NewCustomer", newCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel pCustomer)
        {
            Customer customer = new Customer
            {
                Id = pCustomer.Id,
                Name = pCustomer.Name,
                IsSubscribedToNewsLetter = pCustomer.IsSubscribedToNewsLetter,
                Birthdate = pCustomer.Birthdate,
                MembershipTypeId = pCustomer.MembershipTypeId
            };
           
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("NewCustomer", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                Customer _Customer = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);
                
                if (_Customer == null)
                    return HttpNotFound();

                _Customer.Name = customer.Name;
                _Customer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                _Customer.MembershipTypeId = customer.MembershipTypeId;
                _Customer.Birthdate = customer.Birthdate;

                _context.Entry(_Customer).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return RedirectToAction("Index","Customer");
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormViewModel viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("NewCustomer",viewModel);
        }
    }
}