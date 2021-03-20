using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Entities
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber1 { get; set; }
        [Range(0, long.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber2 { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string ImageName { get; set; }
    }
}
