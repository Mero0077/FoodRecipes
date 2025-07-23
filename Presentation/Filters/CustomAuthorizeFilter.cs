using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class CustomAuthorizeFilter : ActionFilterAttribute
    {
        private readonly FeatureCode featureCode;

        public CustomAuthorizeFilter(FeatureCode featureCode) 
        {
            this.featureCode = featureCode;
        }



    }
}
