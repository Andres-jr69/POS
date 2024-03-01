using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infractructure.Persistences.Context;
using POS.Infractructure.Persistences.Interfaces;

namespace POS.Infractructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepositorio
    {
        private readonly PosContext _context;
        public UserRepository(PosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AccountByUserName(string userName)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName!.Equals(userName));

            return account!;
        }
    }
}
