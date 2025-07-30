using Application.CQRS.WishList.Commands;
using Application.Enums.ErrorCodes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.ErrorVM;
using Presentation.ViewModels.WishList;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public WishListController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ResponseVM<AddWishListVM>> Create()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId)) return new FailureResponseVM<AddWishListVM>(ErrorCodes.UnAuthorized);
            var res = await _mediator.Send(new EnsureWishListExistsCommand(userId));
            var mapped = _mapper.Map<AddWishListVM>(res);
            return new SuccessResponseVM<AddWishListVM>(mapped, "Wislist added!");
        }

    }
}
