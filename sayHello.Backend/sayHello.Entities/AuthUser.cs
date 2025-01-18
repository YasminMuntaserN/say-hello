using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayHello.Entities
{
    public class AuthUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public User User { get; set; }
    }
}
