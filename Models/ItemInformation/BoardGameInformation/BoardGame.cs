using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Models.ItemInformation.BoardGameInformation
{
    public class BoardGame : ItemGeneric
    {
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required]
        [Display(Name = "Max Players")]
        [Range(1, 20)]
        public int MaxPlayerNumber { get; set; }

        [Required]
        [Display(Name = "Min Players")]
        [Range(1, 20)]
        public int MinPlayerNumber { get; set; }

        [Required]
        public Guid BoardGameBrandID { get; set; }

        [Display(Name = "Brand")]
        public BoardGameBrand BoardGameBrand { get; set; }

        public ICollection<InventoryItem> InventoryItem { get; set; }
    }
}
