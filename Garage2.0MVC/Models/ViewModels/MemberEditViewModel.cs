using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class MemberEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "CheckUniqueEmail", controller: "Members")]
        [DisplayName("User")]
        public string Email { get; set; }
    }
}
