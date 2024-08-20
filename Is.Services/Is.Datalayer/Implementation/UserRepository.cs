using Is.Datalayer.Entities;
using Is.Datalayer.Interface;

using Microsoft.EntityFrameworkCore;

namespace Is.Datalayer.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AtsDbContext context) : base(context)
        {

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await Context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User?> GetByUserNamePasswordAsync(string? username, string? password)
        {
            var user = await Context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

            return user;
        }
    }
}
