using Application.CQRS.HotRecipe.Queries;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.HotRecipe.Commands
{
    public record IncrementWishlistCountAsync(Guid recipeId) : IRequest;
    public class IncrementWishlistCountAsyncHandler : IRequestHandler<IncrementWishlistCountAsync>
    {
        private readonly IGeneralRepository<Domain.Models.HotRecipe> generalRepository;

        public IncrementWishlistCountAsyncHandler(IGeneralRepository<Domain.Models.HotRecipe> generalRepository, IMediator mediator)
        {
            this.generalRepository = generalRepository;
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        public async Task Handle(IncrementWishlistCountAsync request, CancellationToken cancellationToken)
        {
            var GetHotRecipe = await Mediator.Send(new GetHotRecipe(request.recipeId));

            if (GetHotRecipe == null)
            {
                var HotRecipe = new Domain.Models.HotRecipe()
                {
                    RecipeId = request.recipeId,
                    ViewCount = 0,
                    LastViewedAt = DateTime.UtcNow,
                    WishlistCount = 1

                };
            }
            else
            {
                GetHotRecipe.WishlistCount += 1;
                GetHotRecipe.LastViewedAt = DateTime.UtcNow;
                await generalRepository.UpdateIncludeAsync(GetHotRecipe, nameof(GetHotRecipe.WishlistCount), nameof(GetHotRecipe.LastViewedAt));

            }

            await generalRepository.SaveChangesAsync();

        }
    }
}
