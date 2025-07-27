using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HotRecipe:BaseModel
    {
        public Guid RecipeId { get; set; } 

        public virtual Recipe Recipe { get; set; }
        public int ViewCount { get; set; } = 0;

        public int WishlistCount { get; set; } = 0;

        public DateTime LastViewedAt { get; set; } = DateTime.UtcNow;

        public double Score;
    }
}
