using Application.CQRS.Account.Queries;
using Application.DTOs.User;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
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
        private readonly IGeneralRepository<User> _generalRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginCommandHandler(IMediator mediator,IGeneralRepository<User> generalRepository,IJwtTokenGenerator jwtTokenGenerator)
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var res= await _mediator.Send(new CheckIfUserNameAndPasswordMatchesQuery(request.LoginDTO));

           
           return _jwtTokenGenerator.Generate(2,"omar", Domain.Enums.Role.Admin);
            

        }
    }
}
