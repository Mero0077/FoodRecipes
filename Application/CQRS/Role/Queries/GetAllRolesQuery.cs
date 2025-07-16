using Application.DTOs.Roles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Role.Queries
{
    public record GetAllRolesQuery : IRequest<IEnumerable<GetAllRolesDTO>>;


    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<GetAllRolesDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;
        
        public GetAllRolesHandler(IMapper mapper,IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._mapper = mapper;
            this._roleRepository = roleRepository;
        }

        public async Task<IEnumerable<GetAllRolesDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var Roles = await _roleRepository.GetAll().ProjectTo<GetAllRolesDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return Roles;
        }
    }
}
