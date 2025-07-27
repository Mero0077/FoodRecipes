using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipe.Events
{
   public record RecipeViewedEvent(Guid Id):INotification;
    
}
