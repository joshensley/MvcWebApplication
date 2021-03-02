using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvcWebApplication.Models.ItemInformation.BoardGameInformation;
using MvcWebApplication.Models.ItemInformation.ComicBookInformation;
using MvcWebApplication.Models.ItemInformation;

namespace MvcWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<BoardGameBrand> BoardGameBrand { get; set; }

        public DbSet<BoardGame> BoardGame { get; set; }

        public DbSet<ComicBookBrand> ComicBookBrand { get; set; }

        public DbSet<ComicBook> ComicBook { get; set; }

        public DbSet<InventoryItem> InventoryItem { get; set; }
    }
}
