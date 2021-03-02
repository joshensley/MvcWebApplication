using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.ViewModels.Home
{
    public class UnionItems
    {

        public Guid ID { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Display(Name = "UPC")]
        [StringLength(12, MinimumLength = 12)]
        public string UPC { get; set; }

        
        [Display(Name = "Title")]
        [StringLength(256)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageFilePath { get; set; }
    }
}
