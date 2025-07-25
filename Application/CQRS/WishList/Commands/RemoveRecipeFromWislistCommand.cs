﻿using Application.CQRS.WishList.Queries;
using Application.DTOs.Recipes;
using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Commands
{
    public record RemoveRecipeFromWislistCommand(WishListRecipeDTO wishListRecipeDTO):IRequest<WishListRecipeDTO>;
    public class RemoveRecipeFromWislistCommandHandler : IRequestHandler<RemoveRecipeFromWislistCommand, WishListRecipeDTO>
    {
        private IGeneralRepository<WishListRecipe> GeneralRepository { get; }
        private IMediator mediator;
        private IMapper _mapper;
        public RemoveRecipeFromWislistCommandHandler(IGeneralRepository<WishListRecipe> generalRepository, IMediator mediator,IMapper mapper )
        {
            GeneralRepository = generalRepository;
            this.mediator = mediator;
            this._mapper = mapper;
        }
        public async Task<WishListRecipeDTO> Handle(RemoveRecipeFromWislistCommand request, CancellationToken cancellationToken)
        {
            var wishList = await mediator.Send(new IsWishListExistsQuery());

            await mediator.Send(new IsRecipeExistsQuery(request.wishListRecipeDTO.RecipeId));

            var ToBeDeletedId= await GeneralRepository.Get(e=>e.WishListId==request.wishListRecipeDTO.WishListId && e.RecipeId==request.wishListRecipeDTO.RecipeId)
                .Select(e=>e.Id).FirstOrDefaultAsync();
           var res=  await GeneralRepository.DeleteAsync(ToBeDeletedId);
           await GeneralRepository.SaveChangesAsync();

            return _mapper.Map<WishListRecipeDTO>(res);
        }
    }
}
