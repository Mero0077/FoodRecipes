using Domain.IRepositories;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HotRecipeRepository : IHotRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public HotRecipeRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<HotRecipe>> GetTopRecipesAsync(int limit)
        {
           return await _context.HotRecipes.OrderByDescending(e => e.ViewCount*0.2 + e.WishlistCount*0.8).Take(limit).ToListAsync();
        }

        public async Task IncrementViewCountAsync(Guid recipeId)
        {
            var Hotrecipe = await _context.HotRecipes.FindAsync(recipeId);
            if (Hotrecipe == null)
            {
                Hotrecipe = new HotRecipe
                {
                    Id = recipeId,
                    ViewCount = 1,
                    WishlistCount = 0,
                    LastViewedAt = DateTime.UtcNow
                };
                _context.HotRecipes.Add(Hotrecipe);
            }
            else
            {
                Hotrecipe.ViewCount += 1;
                Hotrecipe.LastViewedAt = DateTime.UtcNow;
                _context.HotRecipes.Update(Hotrecipe);
            }

            await _context.SaveChangesAsync();
        }

        public async Task IncrementWishlistCountAsync(Guid recipeId)
        {
            var recipe = await _context.HotRecipes.FindAsync(recipeId);

            if (recipe == null)
            {
                recipe = new HotRecipe
                {
                    Id = recipeId,
                    ViewCount = 0,
                    WishlistCount = 1,
                    LastViewedAt = DateTime.UtcNow
                };
                _context.HotRecipes.Add(recipe);
            }
            else
            {
                recipe.WishlistCount += 1;
                recipe.LastViewedAt = DateTime.UtcNow;
                _context.HotRecipes.Update(recipe);
            }

            await _context.SaveChangesAsync();
        }

    }
}
