using Application.CQRS.Role.Commands;
using Application.CQRS.Role.Queries;
using Application.DTOs.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ResponseViewModel<IEnumerable<GetAllRolesVM>>> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            var resultMapped =  _mapper.Map<IEnumerable<GetAllRolesVM>>(result);
            return ResponseViewModel<IEnumerable<GetAllRolesVM>>.Success(resultMapped);
        }

        [HttpPost]
        public async Task<ResponseViewModel<Guid>> CreateRole(CreateRoleVM createRoleVM)
        {
            var mapped = _mapper.Map<CreateRoleDTO>(createRoleVM);
            var result = await _mediator.Send(new CreateRoleCommand(mapped));
            return  ResponseViewModel<Guid>.Success(result);
        }

        [HttpDelete]
        public async Task<ResponseViewModel<bool>> DeleteRole([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            return ResponseViewModel<bool>.Success(result);
        }
    }
}
