using Application.DTOs.User;
using BCrypt.Net;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account.Queries
{
    public record CheckIfUserNameAndPasswordMatchesQuery(LoginDTO LoginDTO):IRequest<bool>;

    public class CheckIfUserNameAndPasswordMatchesQueryHandler : IRequestHandler<CheckIfUserNameAndPasswordMatchesQuery, bool>
    {
        private IMediator _mediator;
        private readonly IGeneralRepository<User> _generalRepository;

        public CheckIfUserNameAndPasswordMatchesQueryHandler(IMediator mediator, IGeneralRepository<User> generalRepository)
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
        }
        public async Task<bool> Handle(CheckIfUserNameAndPasswordMatchesQuery request, CancellationToken cancellationToken)
        {

            var user = await _generalRepository.GetOneWithTrackingAsync(e => e.UserName == request.LoginDTO.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDTO.Password,user.Password))
                throw new UnauthorizedAccessException("Invalid Username or Pass!");

            return false;
            
        }
    }
}
