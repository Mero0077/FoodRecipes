using Application.CQRS.HotRecipe.Commands;
using Application.CQRS.Recipe.Events;
using Application.CQRS.WishList.Events;
using Domain.IRepositories;
using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Event_Handlers
{
    public class AddHotRecipeToWishListEventHandler : INotificationHandler<AddHotRecipeToWishListEvent>
    {
        private readonly IHotRecipeRepository _repo;
        private readonly IMediator _mediator;
        public AddHotRecipeToWishListEventHandler(IHotRecipeRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        public async Task Handle(AddHotRecipeToWishListEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new IncrementWishlistCountAsync(notification.RecipeId));

        }
    }



}
