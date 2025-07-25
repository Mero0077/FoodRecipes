﻿using Application.DTOs.Recipes;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Queries
{
    public record IsRecipeAlreadyAddedByUserQuery(IsRecipeAddedByUserDTO IsRecipeAddedByUserDTO):IRequest;
    public class IsRecipeAlreadyAddedByUserQueryHandler : IRequestHandler<IsRecipeAlreadyAddedByUserQuery>
    {
        private readonly IGeneralRepository<WishListRecipe> generalRepository;

        public IsRecipeAlreadyAddedByUserQueryHandler(IGeneralRepository<WishListRecipe> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public async Task Handle(IsRecipeAlreadyAddedByUserQuery request, CancellationToken cancellationToken)
        {
            var isrecipeadded = await generalRepository.AnyAsync(e => e.RecipeId == request.IsRecipeAddedByUserDTO.RecipeId && e.WishList.UserId == request.IsRecipeAddedByUserDTO.UserId);
            if(isrecipeadded) throw new BusinessLogicException("Recipe already added!", ErrorCodes.AlreadyExist);
        }
    }

}
