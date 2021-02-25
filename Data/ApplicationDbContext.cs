using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Models;
using MvcWebApplication.Models.ItemInformation.BoardGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BrandBoardGame> BrandBoardGames { get; set; }
        public DbSet<InventoryItemBoardGame> InventoryItemBoardGame { get; set; }
    }
}
