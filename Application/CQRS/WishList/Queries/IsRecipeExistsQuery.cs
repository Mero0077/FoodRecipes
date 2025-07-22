using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Queries
{
    public record IsRecipeExistsQuery(Guid RecipeId):IRequest;

    public class IsRecipeExistsQueryHandler : IRequestHandler<IsRecipeExistsQuery>
    {
        private IGeneralRepository<Recipe> _Reciperepository;

        public IsRecipeExistsQueryHandler(IGeneralRepository<Recipe> Reciperepository)
        {
            _Reciperepository = Reciperepository;
        }
        public async Task Handle(IsRecipeExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _Reciperepository.GetOneByIdAsync(request.RecipeId);

            if (exists==null)
                throw new NotFoundException("Recipe not found!", ErrorCodes.NotFound);

        }
    }
 }
