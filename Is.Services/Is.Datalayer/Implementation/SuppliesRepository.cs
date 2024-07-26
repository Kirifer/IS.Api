using Is.Datalayer.Entities;
using Is.Datalayer.Interface;

namespace Is.Datalayer.Implementation
{
    public class SuppliesRepository : BaseRepository<Supplies>, ISuppliesRepository
    {
        public SuppliesRepository(AtsDbContext context) : base(context)
        {

        }
    }
}
