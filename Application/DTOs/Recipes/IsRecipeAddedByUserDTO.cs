using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Recipes
{
    public class IsRecipeAddedByUserDTO
    {
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}
