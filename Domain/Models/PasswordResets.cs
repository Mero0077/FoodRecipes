using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PasswordReset:BaseModel
    {
        public Guid UserId { get; set; }
        public string OtpCode { get; set; }
        public DateTime Expiration { get; set; }

        public User User { get; set; }
    }

}
