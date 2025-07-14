using Application.CQRS.Account.Commands;
using Application.DTOs.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.User;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountController(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            var res= _mediator.Send(new RegisterUserCommand(_mapper.Map<UserRegisterDTO>(userRegisterVM)));

            return Ok(_mapper.Map<UserRegisterVM>(res));
        }
    }
}
