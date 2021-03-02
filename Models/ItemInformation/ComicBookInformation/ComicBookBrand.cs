using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Models.ItemInformation.ComicBookInformation
{
    public class ComicBookBrand
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [StringLength(256)]
        public string BrandName { get; set; }

        public ICollection<ComicBook> ComicBooks { get; set; }

    }
}
