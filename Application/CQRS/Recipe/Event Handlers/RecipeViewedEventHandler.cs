using Application.CQRS.HotRecipe.Commands;
using Application.CQRS.Recipe.Events;
using Domain.IRepositories;
using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipe.Event_Handlers
{
    public class RecipeViewedEventHandler : INotificationHandler<RecipeViewedEvent>
    {
        private readonly IMediator _mediator;

        public RecipeViewedEventHandler( IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(RecipeViewedEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new IncrementViewCountAsync(notification.Id));
         
        }

    }
}
