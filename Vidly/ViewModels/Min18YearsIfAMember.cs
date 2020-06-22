using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.ViewModels
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (CustomerFormViewModel)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == 1)
                return ValidationResult.Success;

            if (customer.Birthdate == null || customer.Birthdate.Year == 0001)
                return new ValidationResult("Date of Birth is required");

            var age = DateTime.Today.Year - customer.Birthdate.Year;

            return (age >= 18) ?
                ValidationResult.Success :
                new ValidationResult("Customer should be at least 18 years old to go on a membership");
        }
    }
}