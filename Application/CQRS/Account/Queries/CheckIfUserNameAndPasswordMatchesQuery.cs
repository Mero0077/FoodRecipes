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
    public record CheckIfUserNameAndPasswordMatchesQuery(LoginDTO LoginDTO):IRequest<User>;

    public class CheckIfUserNameAndPasswordMatchesQueryHandler : IRequestHandler<CheckIfUserNameAndPasswordMatchesQuery, User>
    {
        private IMediator _mediator;
        private readonly IGeneralRepository<User> _generalRepository;

        public CheckIfUserNameAndPasswordMatchesQueryHandler(IMediator mediator, IGeneralRepository<User> generalRepository)
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
        }
        public async Task<User> Handle(CheckIfUserNameAndPasswordMatchesQuery request, CancellationToken cancellationToken)
        {

            var user = await _generalRepository.GetOneWithTrackingAsync(e => e.UserName == request.LoginDTO.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDTO.Password, user.Password))
                return null;

            return user;
            
        }
    }
}
