using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Models.ItemInformation
{
    public class ItemGeneric
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "UPC")]
        [StringLength(12, MinimumLength = 12)]
        public string UPC { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(2000)]
        public string Description { get; set; }
    }
}
