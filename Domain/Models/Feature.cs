﻿//using Domain.Enums;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Feature : BaseModel
    {
        public string FeatureName { get; set; }
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; } = new List<RoleFeature>();

    }
}
