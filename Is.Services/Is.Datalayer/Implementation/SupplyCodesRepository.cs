using Is.Datalayer.Entities;
using Is.Datalayer.Interface;

namespace Is.Datalayer.Implementation
{
    public class SupplyCodesRepository : BaseRepository<SupplyCodes>, ISupplyCodesRepository
    {
        public SupplyCodesRepository(AtsDbContext context) : base(context)
        {

        }
    }
}
