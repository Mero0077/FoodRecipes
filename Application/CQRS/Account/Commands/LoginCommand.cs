using Application.CQRS.Account.Queries;
using Application.DTOs.User;
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

namespace Application.CQRS.Account.Commands
{
    public record LoginCommand(LoginDTO LoginDTO):IRequest<string>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<User> _userRepository;
        private readonly IGeneralRepository<Domain.Models.UserRole> _UserRoleRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginCommandHandler(IMediator mediator,IGeneralRepository<User> userRepository,IJwtTokenGenerator jwtTokenGenerator, IGeneralRepository<Domain.Models.UserRole> RoleRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _UserRoleRepository = RoleRepository;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var res= await _mediator.Send(new CheckIfUserNameAndPasswordMatchesQuery(request.LoginDTO));
           var role = await _UserRoleRepository.Get(e => e.UserId == res.Id).Select(e=>e.Role).FirstOrDefaultAsync();                
           return _jwtTokenGenerator.Generate(res.Id,res.FirstName, role);
           if (res != null) throw new UnauthorizedAccessException("Invalid Username or Pass!");

            var roles = await _UserRoleRepository.Get(e => e.UserId == res.Id).Select(e=>e.Role.Name).ToListAsync();
           return _jwtTokenGenerator.Generate(res.Id,res.FirstName, roles);
           

        }
    }
}
