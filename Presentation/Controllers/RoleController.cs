using Application.CQRS.Role.Commands;
using Application.CQRS.Role.Queries;
using Application.DTOs.Roles;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;
using Presentation.ViewModels;
using Presentation.ViewModels.Roles;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleController(IMediator mediator,IMapper mapper) 
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.GetAllRoles })]

        public async Task<ResponseViewModel<IEnumerable<GetAllRolesVM>>> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            var resultMapped =  _mapper.Map<IEnumerable<GetAllRolesVM>>(result);
            return ResponseViewModel<IEnumerable<GetAllRolesVM>>.Success(resultMapped);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.CreateRole })]

        public async Task<ResponseViewModel<Guid>> CreateRole(CreateRoleVM createRoleVM)
        {
            var mapped = _mapper.Map<CreateRoleDTO>(createRoleVM);
            var result = await _mediator.Send(new CreateRoleCommand(mapped));
            return  ResponseViewModel<Guid>.Success(result);
        }

        [HttpDelete]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.DeleteRole })]

        public async Task<ResponseViewModel<bool>> DeleteRole([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            return ResponseViewModel<bool>.Success(result);
        }

        [HttpPut]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.UpdateRole })]

        public async Task<ResponseViewModel<bool>> UpdateRole([FromQuery] Guid id, [FromBody] UpdateRoleVM updateRoleVM)
        {
            var result = await _mediator.Send(new UpdateRoleCommand(id, updateRoleVM.Name));
            return ResponseViewModel<bool>.Success(result);
        }
    }
}
