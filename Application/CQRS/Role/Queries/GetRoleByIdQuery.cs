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

namespace Application.CQRS.Role.Queries
{
    public record GetRoleByIdQuery(Guid id) : IRequest<Domain.Models.Role>;

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Domain.Models.Role>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public GetRoleByIdQueryHandler(IMapper mapper,IMediator mediator,IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._mapper = mapper;
            this._mediator = mediator;
            this._roleRepository = roleRepository;
        }
        public async Task<Domain.Models.Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(new IsRoleExistedQuery(request.id)))
                throw new NotFoundException("Role isn't Found", ErrorCodes.NotFound);
            var role = await _roleRepository.GetOneByIdAsync(request.id);
            return role;
        }
    }

}
