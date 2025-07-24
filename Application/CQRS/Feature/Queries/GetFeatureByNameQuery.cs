using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Feature.Queries
{
    public record GetFeatureByNameQuery(Domain.Enums.FeatureCode name) : IRequest<Domain.Models.Feature>;
    public class GetFeatureByNameQueryHandler : IRequestHandler<GetFeatureByNameQuery, Domain.Models.Feature>
    {
        private readonly IGeneralRepository<Domain.Models.Feature> _featureRepository;

        public GetFeatureByNameQueryHandler(IGeneralRepository<Domain.Models.Feature> featureRepository)
        {
            this._featureRepository = featureRepository;
        }
        public async Task<Domain.Models.Feature> Handle(GetFeatureByNameQuery request, CancellationToken cancellationToken)
        {
            return await _featureRepository.Get(e => e.FeatureName == request.name.ToString()).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
