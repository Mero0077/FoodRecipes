using Domain.IRepositories;
using MediatR;
using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Role.Queries
{
    public record IsRoleExistedQuery(Guid Id) : IRequest<bool>;

    public class IsRoleExistedQueryHandler : IRequestHandler<IsRoleExistedQuery, bool>
    {
        private readonly IGeneralRepository<Domain.Models.Role> _roleRepository;

        public IsRoleExistedQueryHandler(IGeneralRepository<Domain.Models.Role> roleRepository) 
        {
            this._roleRepository = roleRepository;
        }
        public async Task<bool> Handle(IsRoleExistedQuery request, CancellationToken cancellationToken)
        {
            if (await _roleRepository.Get(e => e.Id == request.Id).AnyAsync())
                return true;
            return false;
        }
    }
}
