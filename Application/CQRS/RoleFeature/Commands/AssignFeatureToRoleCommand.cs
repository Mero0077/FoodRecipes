using Application.CQRS.Feature.Queries;
using Application.CQRS.Role.Queries;
using Application.CQRS.RoleFeature.Queries;
using Application.DTOs.RoleFeature;
using Application.Enums.ErrorCodes;
using Application.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RoleFeature.Commands
{
    public record AssignFeatureToRoleCommand(AssignFeatureToRoleDTO AssignRoleToFeatureDTO) : IRequest<bool>;
    class AssignFeatureToRoleCommandHandler : IRequestHandler<AssignFeatureToRoleCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.RoleFeature> _roleFeatureRepository;

        public AssignFeatureToRoleCommandHandler(IMapper mapper,IMediator mediator,IGeneralRepository<Domain.Models.RoleFeature> roleFeatureRepository)
        {
            this._mapper = mapper;
            this._mediator = mediator;
            this._roleFeatureRepository = roleFeatureRepository;
        }
        public async Task<bool> Handle(AssignFeatureToRoleCommand request, CancellationToken cancellationToken)
        {
            var resultRoleIsExisted = await _mediator.Send(new IsRoleExistedQuery(request.AssignRoleToFeatureDTO.RoleId));
            if (!resultRoleIsExisted)
                throw new ValidationException("Role isn't Existed", ErrorCodes.NotFound);
            var resultFeatureIsExisted = await _mediator.Send(new IsFeatureExistsByIdQuery(request.AssignRoleToFeatureDTO.FeatureId));
            if (!resultFeatureIsExisted)
                throw new ValidationException("Feature isn't Existed", ErrorCodes.NotFound);
            var resultRoleFeatureIsExisted = await _mediator.Send(new CheckRoleFeatureQuery(request.AssignRoleToFeatureDTO.FeatureId,request.AssignRoleToFeatureDTO.RoleId));
            if(resultRoleFeatureIsExisted)
                throw new ValidationException("Role Is Assigned Already", ErrorCodes.AlreadyExist);

            var result = _mapper.Map<Domain.Models.RoleFeature>(request.AssignRoleToFeatureDTO);
            await _roleFeatureRepository.AddAsync(result);
            await _roleFeatureRepository.SaveChangesAsync();
            return true;
        }
    }
}
