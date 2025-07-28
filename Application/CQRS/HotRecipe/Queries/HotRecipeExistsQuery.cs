using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.HotRecipe.Queries
{
    public record HotRecipeExistsQuery(Guid RecipeId):IRequest<bool>;
    public class HotRecipeExistsQueryHandler : IRequestHandler<HotRecipeExistsQuery, bool>
    {
        private readonly IGeneralRepository<Domain.Models.HotRecipe> generalRepository;

        public HotRecipeExistsQueryHandler(IGeneralRepository<Domain.Models.HotRecipe> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public async Task<bool> Handle(HotRecipeExistsQuery request, CancellationToken cancellationToken)
        {
            var RecipeExists = await generalRepository.IsExists(request.RecipeId);
            return RecipeExists;
        }
    }
}
