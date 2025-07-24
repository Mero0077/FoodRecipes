using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IJwtTokenGenerator
    {
        string Generate(Guid userId, string name, Domain.Models.Role role);
    }
}
