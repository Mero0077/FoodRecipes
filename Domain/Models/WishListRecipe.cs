using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WishListRecipe:BaseModel
    {
        public Guid  RecipeId { get; set; }
        public Guid WishListId { get; set; }

        public virtual WishList WishList { get; set; }
        public virtual Recipe recipe { get; set; }
    }
}
