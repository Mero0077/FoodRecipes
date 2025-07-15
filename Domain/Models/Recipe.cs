using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Recipe : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999.99)]
        public decimal Price { get; set; }

        [MaxLength(30)]
        public string Tag { get; set; }

        // Foreign key for Category
        [Required]
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; } = null!;

        [MaxLength(250)]
        [Url]
        public string? ImageUrl { get; set; }
    }
    
}
