using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account.Shared
{
    public class UserCredentialsChecker
    {
        private readonly IGeneralRepository<User> _generalRepository;

        public UserCredentialsChecker(IGeneralRepository<User> generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<User> GetUserIfCredentialsMatch(string username, string password)
        {
            var user = await _generalRepository.GetOneWithTrackingAsync(e => e.UserName == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password!");

            return user;
        }
    }
}
