using Application.CQRS.WishList.Commands;
using Application.CQRS.WishList.Queries;
using Application.DTOs.Recipes;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Orchestrator
{
    public record AddRecipeToWishlistCommand(WishListRecipeDTO wishListRecipeDTO): IRequest<WishListRecipe>;
    public class AddRecipeToWishlistCommandHandler : IRequestHandler<AddRecipeToWishlistCommand, WishListRecipe>
    {
        private IGeneralRepository<WishListRecipe> GeneralRepository { get; }
        private IMediator mediator;
        public AddRecipeToWishlistCommandHandler(IGeneralRepository<WishListRecipe> generalRepository, IMediator mediator)
        {
            GeneralRepository = generalRepository;
            this.mediator = mediator;
        }

        public async Task<WishListRecipe> Handle(AddRecipeToWishlistCommand request, CancellationToken cancellationToken)
        {
           
            var userId = await mediator.Send(new IsUserExistsQuery());

            var wishList = await mediator.Send(new IsWishListExistsQuery());

            await mediator.Send(new IsRecipeExistsQuery(request.wishListRecipeDTO.RecipeId));

            var dto = new IsRecipeAddedByUserDTO
            {
                RecipeId = request.wishListRecipeDTO.RecipeId,
                UserId = userId
            };
            await mediator.Send(new IsRecipeAlreadyAddedByUserQuery(dto));

            var entity = new WishListRecipe
            {
                RecipeId = request.wishListRecipeDTO.RecipeId,
                WishListId = wishList.Id
            };

            await GeneralRepository.AddAsync(entity);
            await GeneralRepository.SaveChangesAsync();
            return entity;
        }
    }
}
