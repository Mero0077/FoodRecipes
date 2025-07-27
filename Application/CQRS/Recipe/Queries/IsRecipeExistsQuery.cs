using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipe.Queries
{
    public record IsRecipeExistsQuery(Guid RecipeId):IRequest<bool>;

    public class IsRecipeExistsQueryHandler : IRequestHandler<IsRecipeExistsQuery,bool>
    {
        private IGeneralRepository<Domain.Models.Recipe> _Reciperepository;

        public IsRecipeExistsQueryHandler(IGeneralRepository<Domain.Models.Recipe> Reciperepository)
        {
            _Reciperepository = Reciperepository;
        }
        public async Task<bool> Handle(IsRecipeExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _Reciperepository.GetOneByIdAsync(request.RecipeId);
            return exists == null ? true : false;

        }
    }
 }
