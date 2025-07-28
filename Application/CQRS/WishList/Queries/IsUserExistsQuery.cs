using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Queries
{
    public class IsUserExistsQuery:IRequest<Guid>;
    public class IsUserExistsHandler : IRequestHandler<IsUserExistsQuery, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsUserExistsHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<Guid> Handle(IsUserExistsQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("ID");
            try
            {
                if (userIdClaim == null)
                    return null;
                var userId = Guid.Parse(userIdClaim.Value);

                return Task.FromResult(userId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
