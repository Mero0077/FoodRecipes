using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RoleFeature : BaseModel
    {
        public virtual Role Role { get; set; }
        public Guid RoleID { get; set; }

        public virtual Feature Feature { get; set; }

        public Guid FeatureID { get; set; }
    }
}
