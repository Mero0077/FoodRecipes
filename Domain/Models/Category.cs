using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Category : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation property for related recipes
        public virtual ICollection<Recipe> Recipes { get; set; }
    }

}
