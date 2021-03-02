using MvcWebApplication.Models.ItemInformation.BoardGameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.ViewModels.BoardGameCrud
{
    public class BoardGameCrudViewModel
    {
        public BoardGame BoardGame;
        public IEnumerable<BoardGameBrand> BoardGameBrands;
    }
}
