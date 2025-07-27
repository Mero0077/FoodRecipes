using Application.CQRS.HotRecipe.Queries;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.HotRecipe.Commands
{
    public record IncrementViewCountAsync(Guid recipeId):IRequest;
    public class IncrementViewCountAsyncHandler : IRequestHandler<IncrementViewCountAsync>
    {
        private readonly IGeneralRepository<Domain.Models.HotRecipe> generalRepository;

        public IncrementViewCountAsyncHandler(IGeneralRepository<Domain.Models.HotRecipe> generalRepository, IMediator mediator)
        {
            this.generalRepository = generalRepository;
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        public async Task Handle(IncrementViewCountAsync request, CancellationToken cancellationToken)
        {
            var GetHotRecipe = await Mediator.Send(new GetHotRecipe(request.recipeId));
            if (GetHotRecipe == null)
            {
                var HotRecipe = new Domain.Models.HotRecipe()
                {
                    RecipeId = request.recipeId,
                    ViewCount = 1,
                    LastViewedAt = DateTime.UtcNow,
                    WishlistCount = 0

                };
            }
            else
            {
                GetHotRecipe.ViewCount += 1;
                GetHotRecipe.LastViewedAt=DateTime.UtcNow;
               await generalRepository.UpdateIncludeAsync(GetHotRecipe, nameof(GetHotRecipe.ViewCount),nameof(GetHotRecipe.LastViewedAt));
               
            }

            await generalRepository.SaveChangesAsync();

        }
    }
}
