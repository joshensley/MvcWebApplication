using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Models.ItemInformation.BoardGame
{
    public class BoardGame : ItemGeneric
    {
        [Required]
        [Display(Name = "Players")]
        [Range(1, 20)]
        public int PlayerNumber { get; set; }

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

        [Required]
        public Guid BrandBoardGameID { get; set; }
        public BrandBoardGame BrandBoardGame { get; set; }

        public ICollection<InventoryItemBoardGame> InventoryItemBoardGames { get; set; }
    }
}
