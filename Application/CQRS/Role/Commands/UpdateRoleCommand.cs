using Application.CQRS.Role.Queries;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Role.Commands
{
  public record UpdateRoleCommand(Guid id,string name) : IRequest<bool>;

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public UpdateRoleCommandHandler(IMediator mediator,IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._mediator = mediator;
            this._roleRepository = roleRepository;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(request.id));
            if (await _mediator.Send(new IsRoleNameExistedQuery(request.name)))
                throw new BusinessLogicException("Role is Already Existed", ErrorCodes.AlreadyExist);
            role.Name = request.name.Trim();
           await  _roleRepository.SaveChangesAsync();
           return true;
        }
    }
}
