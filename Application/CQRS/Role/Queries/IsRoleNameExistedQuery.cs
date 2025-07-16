using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Role.Queries
{

   public record IsRoleNameExistedQuery(string name) : IRequest<bool>;

    public class IsRoleNameExistedHandler : IRequestHandler<IsRoleNameExistedQuery, bool>
    {
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public IsRoleNameExistedHandler(IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._roleRepository = roleRepository;
        }
        public async Task<bool> Handle(IsRoleNameExistedQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Get(e=>e.Name.ToLower() == request.name.ToLower()).AnyAsync();
            return role;
        }
    }

}
