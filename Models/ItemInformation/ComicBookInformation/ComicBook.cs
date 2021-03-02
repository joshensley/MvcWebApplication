using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Models.ItemInformation.ComicBookInformation
{
    public class ComicBook : ItemGeneric
    {
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required]
        [Display(Name = "Pages")]
        [Range(1, 3000)]
        public int Pages { get; set; }

        [Required]
        public Guid ComicBookBrandId { get; set; }
        public ComicBookBrand ComicBookBrand { get; set; }

    }
}
