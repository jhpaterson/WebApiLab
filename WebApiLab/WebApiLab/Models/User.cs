using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiLab.Models
{
    public class User
    {
        public List<Role> Roles { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address HomeAddress { get; set; }
        public bool IsApproved { get; set; }
    }
}