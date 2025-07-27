using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipe.Queries
{
    public record GetRecipeQuery(Guid RecipeId) : IRequest<Domain.Models.Recipe>;

    public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, Domain.Models.Recipe>
    {
        private readonly IGeneralRepository<Domain.Models.Recipe> _Reciperepository;

        public GetRecipeQueryHandler(IGeneralRepository<Domain.Models.Recipe> Reciperepository)
        {
            _Reciperepository = Reciperepository;
        }

        public async Task<Domain.Models.Recipe> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _Reciperepository.GetOneByIdAsync(request.RecipeId);
            return recipe;
        }
    }

}