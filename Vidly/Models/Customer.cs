﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        public String Name { get; set; }

        public DateTime Birthdate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
   
        public MembershipType MembershipType { get; set; }
        
        public byte MembershipTypeId { get; set; }
    }
}