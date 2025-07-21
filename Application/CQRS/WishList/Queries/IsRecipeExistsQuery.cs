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
    public record IsRecipeExistsQuery(Guid RecipeId):IRequest<bool>;

    public class IsRecipeExistsQueryHandler : IRequestHandler<IsRecipeExistsQuery, bool>
    {
        private IGeneralRepository<Recipe> _Reciperepository;

        public IsRecipeExistsQueryHandler(IGeneralRepository<Recipe> Reciperepository)
        {
            _Reciperepository = Reciperepository;
        }
        public async Task<bool> Handle(IsRecipeExistsQuery request, CancellationToken cancellationToken)
        {
            bool exists= _Reciperepository.IsExists(request.RecipeId);

            if (!exists)
                throw new NotFoundException("Recipe not found!", ErrorCodes.NotFound);

            return true;
        }
    }
}
