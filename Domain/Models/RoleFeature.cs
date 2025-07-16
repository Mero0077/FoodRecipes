using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RoleFeature : BaseModel
    {
        public Role Role { get; set; }
        public Guid RoleID { get; set; }

        public Feature Feature { get; set; }

        public Guid FeatureID { get; set; }
    }
}
