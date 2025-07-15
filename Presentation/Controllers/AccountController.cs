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
            var res= await _mediator.Send(new RegisterUserCommand(_mapper.Map<UserRegisterDTO>(userRegisterVM)));

            return Ok(_mapper.Map<UserRegisterVM>(res));
        }

        [HttpPost("Login")]
        public async Task<string> Login(UserLoginVM userLoginVM)
        {
          return await _mediator.Send(new LoginCommand(_mapper.Map<LoginDTO>(userLoginVM)));
        }
    }
}
