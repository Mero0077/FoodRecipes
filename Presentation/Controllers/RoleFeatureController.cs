using Application.CQRS.RoleFeature.Commands;
using Application.DTOs.RoleFeature;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using Presentation.ViewModels.RoleFeature;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFeatureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoleFeatureController(IMapper mapper ,IMediator mediator) 
        {
            this._mapper = mapper;
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task <ResponseViewModel<bool>> AssignFeatureToRole([FromBody]AssignFeatureToRoleVM assignFeatureToRoleVM)
        {
            var mapped = _mapper.Map<AssignFeatureToRoleDTO>(assignFeatureToRoleVM);
            var result = await _mediator.Send(new AssignFeatureToRoleCommand(mapped));
            return  ResponseViewModel<bool>.Success(result);
        }

    }
}
