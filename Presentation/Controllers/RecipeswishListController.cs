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

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("wishlist/recipes")]
        public async Task<ResponseVM<AddRecipewishListVM>> AddRecipeTowishList([FromBody]AddRecipewishListVM addRecipewishListVM)
        {
            var res = await _mediator.Send(new AddRecipeToWishlistCommand(_mapper.Map<WishListRecipeDTO>(addRecipewishListVM)));
            if (res != null)
            {
                BackgroundJob.Enqueue<HangfireJobs>(job => job.Log());
                var mapped = _mapper.Map<AddRecipewishListVM>(res);
                return new SuccessResponseVM<AddRecipewishListVM>(mapped, "Reciped added to wislist");
            }
            else return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.BusinessRuleViolation, "Recipe could not be added!");
        }

        [HttpDelete("wishlist/recipes/{recipeId}")]
        public async Task<ResponseVM<AddRecipewishListVM>> RemoveRecipeFromWishlist([FromBody] AddRecipewishListVM addRecipewishListVM)
        {
            var res=  await _mediator.Send(new RemoveRecipeFromWislistCommand(_mapper.Map<WishListRecipeDTO>(addRecipewishListVM)));
            if(res != null)
            {
                var mapped = _mapper.Map<AddRecipewishListVM>(res);
                return new SuccessResponseVM<AddRecipewishListVM>(mapped, "Reciped removed from wislist");
            }
            else return new FailureResponseVM<AddRecipewishListVM>(ErrorCodes.BusinessRuleViolation, "Recipe could not be remmoved!");

        }
    }
}
