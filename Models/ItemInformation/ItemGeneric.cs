﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageFilePath { get; set; }

        [Display(Name = "Image Form File")]
        [NotMapped]
        public IFormFile FormFileUpload { get; set; }
    }
}
