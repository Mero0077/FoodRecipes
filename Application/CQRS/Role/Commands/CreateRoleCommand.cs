using Application.CQRS.Role.Queries;
using Application.DTOs.Roles;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using AutoMapper;
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
    public record CreateRoleCommand(CreateRoleDTO CreateRoleDTO) : IRequest<Guid>;
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public CreateRoleCommandHandler(IMediator mediator,IMapper mapper,IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._mediator = mediator;
            this._mapper = mapper;
            this._roleRepository = roleRepository;
        }
        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            if (request.CreateRoleDTO == null)
                throw new ValidationException("The Request Shouldnot be null", StatusCodes.Status400BadRequest,ErrorCodes.BadRequest);

            if(string.IsNullOrEmpty(request.CreateRoleDTO.Name) || string.IsNullOrWhiteSpace(request.CreateRoleDTO.Name))
                throw new ValidationException("Nams is NULL", StatusCodes.Status400BadRequest, ErrorCodes.BadRequest);

            request.CreateRoleDTO.Name = request.CreateRoleDTO.Name!.Trim();


            if (request.CreateRoleDTO.Name.Length > 30)
                throw new ValidationException("Role Name Shouldnot be Long",StatusCodes.Status400BadRequest, ErrorCodes.BadRequest);

            if (await _mediator.Send(new IsRoleExistedQuery(request.CreateRoleDTO.Name)))
                throw new ValidationException("Role is Already Exists", StatusCodes.Status400BadRequest, ErrorCodes.BadRequest);
            var role = _mapper.Map<Domain.Models.Role>(request.CreateRoleDTO);

            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync(cancellationToken);
            return role.Id;
        }
    }
}
