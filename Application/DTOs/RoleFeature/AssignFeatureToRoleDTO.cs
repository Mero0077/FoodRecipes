using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RoleFeature
{
    public class AssignFeatureToRoleDTO
    {
        public Guid RoleId { get; set; }
        public Guid FeatureId { get; set; }
    }
}
