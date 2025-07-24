using Application.DTOs.Features;
using AutoMapper;
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
    public record GetAllFeaturesQuery : IRequest<IEnumerable<GetAllFeaturesDTO>>;
    class GetAllFeaturesQueryHandler : IRequestHandler<GetAllFeaturesQuery, IEnumerable<GetAllFeaturesDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralRepository<Domain.Models.Feature> _featureRepository;

        public GetAllFeaturesQueryHandler(IMapper mapper,IGeneralRepository<Domain.Models.Feature> featureRepository)
        {
            this._mapper = mapper;
            this._featureRepository = featureRepository;
        }
        public async Task<IEnumerable<GetAllFeaturesDTO>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
        {
            var mapped = await _featureRepository.GetAll().ToListAsync();
            var result =_mapper.Map<IEnumerable<GetAllFeaturesDTO>>(mapped);
            return result;
        }
    }
}
