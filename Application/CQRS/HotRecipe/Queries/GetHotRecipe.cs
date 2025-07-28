using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.HotRecipe.Queries
{
    public record GetHotRecipe(Guid RecipeId):IRequest<Domain.Models.HotRecipe>;
    public class GetHotRecipeHandler : IRequestHandler<GetHotRecipe, Domain.Models.HotRecipe>
    {
        private readonly IGeneralRepository<Domain.Models.HotRecipe> generalRepository;

        public GetHotRecipeHandler(IGeneralRepository<Domain.Models.HotRecipe> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public async Task<Domain.Models.HotRecipe> Handle(GetHotRecipe request, CancellationToken cancellationToken)
        {
            return await generalRepository.GetOneByIdAsync(request.RecipeId);
        }
    }
}
