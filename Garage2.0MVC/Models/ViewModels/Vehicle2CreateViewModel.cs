using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Garage2._0MVC.Models.ViewModels
{
    public class Vehicle2CreateViewModel
    {
        [Required]
        public VehicleTypeEnum Type { get; set; }

        [Required]
        [DisplayName("Registration Number")]
        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [Remote(action: "UniqueRegNum", controller: "VehicleModels")]
        public string RegNum { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Color { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string Model { get; set; }

        [Required]
        [DisplayName("Number of Wheels")]
        [Range(0, 20)]
        public int NumWheels { get; set; }

        [DisplayName("Email")]
        public string MemberEmail { get; set; }

        [DisplayName("User")]
        public int MemberId { get; set; }

        //    [DisplayName("Type")]
        //    public VehicleTypeEnum VehicleType { get; set; }

    }
}
