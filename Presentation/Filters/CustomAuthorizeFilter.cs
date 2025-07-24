using Application.CQRS.Feature.Queries;
using Application.CQRS.RoleFeature.Queries;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class CustomAuthorizeFilter : ActionFilterAttribute
    {
        private readonly IMediator _mediator;
        private readonly FeatureCode _featureCode;

        public CustomAuthorizeFilter(IMediator mediator, FeatureCode featureCode)
        {
            this._mediator = mediator;
            this._featureCode = featureCode;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var roleId = context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (roleId == null || string.IsNullOrEmpty(roleId.Value))
                throw new UnAuthorizedException("You are not authorized to access this resource.", ErrorCodes.UnAuthorized);

            var role = Guid.Parse(roleId.Value);

            var feature = await _mediator.Send(new GetFeatureByNameQuery(_featureCode));
            if (feature == null)
                throw new NotFoundException("Feature not found",ErrorCodes.NotFound);

           var result = await _mediator.Send(new CheckRoleFeatureQuery(feature.Id,role));
            if (!result)
                throw new UnAuthorizedException("You don't have permission to access this feature", ErrorCodes.UnAuthorized);
        }
    }
}
