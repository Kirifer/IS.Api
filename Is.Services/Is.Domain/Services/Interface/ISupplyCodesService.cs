using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Models.Entities.SupplyCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Is.Domain.Services.Interface
{
    public interface ISupplyCodesService : IEntityService
    {
        Task<Response<List<IsSupplyCodesDto>>> GetSupplyCodesAsync();
        Task<Response<IsSupplyCodesDto>> GetSupplyCodeAsync(Guid id);
        Task<Response<IsSupplyCodesDto>> CreateSupplyCodesAsync(IsSupplyCodesCreateDto payload);
        Task<Response<IsSupplyCodesDto>> UpdateSupplyCodesAsync(Guid id, IsSupplyCodesUpdateDto payload);
        Task<Response<IsSupplyCodesDto>> DeleteSupplyCodesAsync(Guid id);
    }
}
