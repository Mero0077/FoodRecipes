using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RoleFeature.Queries
{
    public record CheckRoleFeatureQuery(Guid featureId, Guid roleId) : IRequest<bool>;
    public class CheckRoleFeatureQueryHandler : IRequestHandler<CheckRoleFeatureQuery, bool>
    {
        private readonly IGeneralRepository<Domain.Models.RoleFeature> _roleFeatureRepository;

        public CheckRoleFeatureQueryHandler(IGeneralRepository<Domain.Models.RoleFeature> roleFeatureRepository)
        {
            this._roleFeatureRepository = roleFeatureRepository;
        }
        public async Task<bool> Handle(CheckRoleFeatureQuery request, CancellationToken cancellationToken)
        {
            return await _roleFeatureRepository.GetAll().AnyAsync(e => e.RoleID == request.roleId && e.FeatureID == request.featureId);
        }
    }
}
