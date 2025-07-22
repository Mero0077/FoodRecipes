using Application.CQRS.WishList.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.ErrorVM;
using Presentation.ViewModels.WishList;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public WishListController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<ResponseVM<AddWishListVM>> Create()
        {
            var res=  await _mediator.Send(new EnsureWishListExistsCommand());
            var mapped= _mapper.Map<AddWishListVM>(res);
            return new SuccessResponseVM<AddWishListVM>(mapped,"Wislist added!");
        }
    }
}
