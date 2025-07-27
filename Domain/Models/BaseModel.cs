using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
