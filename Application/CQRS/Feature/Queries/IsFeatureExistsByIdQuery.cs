using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Feature.Queries
{

    public record IsFeatureExistsByIdQuery(Guid id) : IRequest<bool>;
    class IsFeatureExistsByIdQueryHandler : IRequestHandler<IsFeatureExistsByIdQuery, bool>
    {
        private readonly IGeneralRepository<Domain.Models.Feature> _featureRepository;

        public IsFeatureExistsByIdQueryHandler(IGeneralRepository<Domain.Models.Feature> featureRepository)
        {
            this._featureRepository = featureRepository;
        }
        public async Task<bool> Handle(IsFeatureExistsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _featureRepository.AnyAsync(e => e.Id == request.id);
        }
    }
}
