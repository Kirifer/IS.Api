using Is.Datalayer.Entities;
using Is.Datalayer.Interface;

namespace Is.Datalayer.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AtsDbContext context) : base(context)
        {

        }
    }
}
