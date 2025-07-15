using Application.DTOs.User;
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
    public record CheckIfUserExistsQuery(string username):IRequest<bool>;
    public class CheckIfUserExistsQueryHandler : IRequestHandler<CheckIfUserExistsQuery, bool>
    {
        private IGeneralRepository<User> _generalRepository;
        public CheckIfUserExistsQueryHandler(IGeneralRepository<User> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<bool> Handle(CheckIfUserExistsQuery request, CancellationToken cancellationToken)
        {
            return await _generalRepository.AnyAsync(u => u.UserName == request.username);
        }
    }
}
