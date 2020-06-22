using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please entre the customer's name")]
        public String Name { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime Birthdate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }

        [Display(Name = "Membership Type")]
        public String MembershipType { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public string GetTitle()
        {
            return (Id > 0) ? "Edit Customer" : "New Customer";
        }

        public CustomerFormViewModel()
        {
            this.Id = 0;
        }

        public CustomerFormViewModel(Customer customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.MembershipTypeId = customer.MembershipTypeId;
            this.Birthdate = customer.Birthdate;
            this.MembershipType = "";
            this.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
        }
    }
}