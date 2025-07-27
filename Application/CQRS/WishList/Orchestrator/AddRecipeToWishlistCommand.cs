
using Application.CQRS.WishList.Commands;
using Application.CQRS.WishList.Queries;
using Application.DTOs.Recipes;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using Hangfire;
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
            if(userId==null) throw new UnauthorizedAccessException("User ID not found in token");

            var wishList = await mediator.Send(new IsWishListExistsQuery());
            if(wishList==null) throw new BusinessLogicException("WishList not exists!", ErrorCodes.BusinessRuleViolation);

            if ( await mediator.Send(new IsRecipeExistsQuery(request.wishListRecipeDTO.RecipeId)))
            {
                throw new NotFoundException("Recipe not found!", ErrorCodes.NotFound);

            }

            var dto = new IsRecipeAddedByUserDTO
            {
                RecipeId = request.wishListRecipeDTO.RecipeId,
                UserId = userId
            };
           
            if(await mediator.Send(new IsRecipeAlreadyAddedByUserQuery(dto)))
            {
                throw new BusinessLogicException("Recipe already added!", ErrorCodes.AlreadyExist);
            }

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
