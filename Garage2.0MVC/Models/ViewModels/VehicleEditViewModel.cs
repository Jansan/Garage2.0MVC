using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehicleEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Color { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string Model { get; set; }
        [DisplayName("Owner")]
        public int MemberId { get; set; }



    }
}
