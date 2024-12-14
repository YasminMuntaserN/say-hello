using Microsoft.EntityFrameworkCore;
using sayHello.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayHello.Validation
{

    public class UniqueValidatorService
    {
        private readonly AppDbContext _context;

        public UniqueValidatorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email) == false;
        }

        public async Task<bool> IsUserNameUniqueAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.Username == userName) == false;
        }
    }
}
