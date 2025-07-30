using Application.CQRS.WishList.Commands;
using Application.CQRS.WishList;
using Application.DTOs.Recipes;
using Application.Enums.ErrorCodes;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.ErrorVM;
using Presentation.ViewModels.RecipeWishList;
using System.Security.Claims;
using Presentation.ViewModels.WishList;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Presentation.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeswishListController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public RecipeswishListController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.AddRecipeToWishlist })]

        public async Task<ResponseVM<AddRecipewishListVM>> AddRecipeTowishList([FromBody]AddRecipewishListVM addRecipewishListVM)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId)) return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.UnAuthorized);
            var res = await _mediator.Send(new AddRecipeToWishlistCommand(userId,_mapper.Map<WishListRecipeDTO>(addRecipewishListVM)));
            if (res != null)
            {
                BackgroundJob.Enqueue<HangfireJobs>(job => job.Log());
                var mapped = _mapper.Map<AddRecipewishListVM>(res);
                return new SuccessResponseVM<AddRecipewishListVM>(mapped, "Reciped added to wislist");
            }
            else return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.BusinessRuleViolation, "Recipe could not be added!");
        }

        [HttpDelete]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.RemoveRecipeFromWishlist })]
        public async Task<ResponseVM<AddRecipewishListVM>> RemoveRecipeFromWishlist([FromBody] AddRecipewishListVM addRecipewishListVM)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId)) return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.UnAuthorized);
            var res=  await _mediator.Send(new RemoveRecipeFromWislistCommand(userId,_mapper.Map<WishListRecipeDTO>(addRecipewishListVM)));
            if(res != null)
            {
                var mapped = _mapper.Map<AddRecipewishListVM>(res);
                return new SuccessResponseVM<AddRecipewishListVM>(mapped, "Reciped removed from wislist");
            }
            else return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.BusinessRuleViolation, "Recipe could not be remmoved!");

        }
    }
}
