using Domain.Models;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;Database=FoodRecipesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DatabaseSeeders.SeedFeatureData(modelBuilder);
            DatabaseSeeders.SeedNoneRole(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<RoleFeature> RoleFeatures { get; set; }

        public DbSet<WishList> wishLists { get; set; }
        public DbSet<WishListRecipe> WishListsRecipes { get;set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<HotRecipe> HotRecipes { get; set; }
        public DbSet<WishListRecipe> WishListsRecipes { get; set; }

    }
}
