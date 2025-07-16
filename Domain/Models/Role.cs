using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; } = new List<RoleFeature>();
    }
}
