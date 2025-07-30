using Application.CQRS.HotRecipe.Queries;
using Application.CQRS.Recipe.Commands;
using Application.CQRS.Recipe.Events;
using Application.CQRS.Recipe.Queries;
using Application.DTOs.Recipes;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Application.Views;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.ErrorVM;
using Presentation.ViewModels.Recipe;
using Presentation.ViewModels.WishList;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper mapper;

        public RecipesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponseVM<RecipeVM>> GetRecipe([FromQuery] Guid id)
        {
            var res= await _mediator.Send(new GetRecipeQuery(id));
            await _mediator.Publish(new RecipeViewedEvent(id));

            return res == null ?
            new FailureResponseVM<RecipeVM>(ErrorCodes.NotFound, "Failed to fetch recipe.") :
            new SuccessResponseVM<RecipeVM>(mapper.Map<RecipeVM>(res), "Reciped retrieved!");

        }

        [HttpGet]
        public async Task<ResponseVM<List<RecipeVM>>> GetHotRecipes([FromQuery] int limit)
        {

            var hotRecipes = await _mediator.Send(new GetHotRecipes(limit));
            var mapped = mapper.Map<List<RecipeVM>>(hotRecipes);

            return new SuccessResponseVM<List<RecipeVM>>(mapped);

        }

        [HttpPost]
        public async Task<ResponseVM<RecipeVM>> AddRecipe([FromBody]RecipeVM recipeVM)
        {
          var recipe= await _mediator.Send(new AddRecipeCommand(mapper.Map<AddRecipeDTO>(recipeVM)));
            
            return recipe!=null?
            new SuccessResponseVM<RecipeVM>( mapper.Map<RecipeVM>(recipe),"Reciped added!") :
            new FailureResponseVM<RecipeVM>(ErrorCodes.BusinessRuleViolation, "Failed to add recipe.");
        }
    }
}
