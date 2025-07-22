using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WishList:BaseModel
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<WishListRecipe> wishListRecipes { get; set; }
    }
}
