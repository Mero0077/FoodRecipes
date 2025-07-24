using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Features
{
    public class GetAllFeaturesDTO
    {
        public Guid Id { get; set; }
        public string FeatureName { get; set; }
    }
}
