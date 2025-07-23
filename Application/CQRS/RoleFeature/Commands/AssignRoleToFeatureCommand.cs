using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RoleFeature.Commands
{
    public record AssignRoleToFeatureCommand(Domain.Models.Role role,Domain.Models.Feature feature) : IRequest<bool>;
    //class AssignRoleToFeatureCommandHandler : IRequestHandler<AssignRoleToFeatureCommand, bool>
    //{
    //    //public Task<bool> Handle(AssignRoleToFeatureCommand request, CancellationToken cancellationToken)
    //    //{

    //    //}
    //}
}
