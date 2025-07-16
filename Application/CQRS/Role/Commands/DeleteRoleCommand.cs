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
    public record DeleteRoleCommand(Guid id) : IRequest<bool>;
     

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public DeleteRoleCommandHandler(IMediator mediator,IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._mediator = mediator;
            this._roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
           var result = await _mediator.Send( new IsRoleExistedQuery(request.id));
            if(!result)
                throw new NotFoundException("Role isn't Found",StatusCodes.Status400BadRequest,ErrorCodes.NotFound);

            await _roleRepository.DeleteAsync(request.id);
            await _roleRepository.SaveChangesAsync();

            return true;
        }
    }
}
