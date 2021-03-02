using MvcWebApplication.Models.ItemInformation.BoardGameInformation;
using MvcWebApplication.Models.ItemInformation.ComicBookInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MvcWebApplication.Models.ItemInformation
{
    public class InventoryItem
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        [Required]
        [Display(Name = "Received #")]
        public int ReceivedNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Received Date")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Order #")]
        public int? OrderNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ordered Date")]
        public DateTime? OrderedDate { get; set; }

        [Display(Name = "Shipping #")]
        public int? ShippingNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }


        // Board Game
        public Guid BoardGameID { get; set; }
        public BoardGame BoardGame { get; set; }

        // Comic Book
        public Guid ComicBookID { get; set; }
        public ComicBook ComicBook { get; set; }


    }
}
