using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Models.Entities.Supply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Is.Domain.Services.Interface
{
    public interface ISuppliesService : IEntityService
    {
        Task<Response<List<IsSuppliesDto>>> GetSuppliesAsync();

        Task<Response<IsSuppliesDto>> GetSupplyAsync(Guid id);

        Task<Response<IsSuppliesDto>> CreateSuppliesAsync(IsSuppliesCreateDto payload);

        Task<Response<IsSuppliesDto>> UpdateSuppliesAsync(Guid id, IsSuppliesUpdateDto payload);

        Task<Response<IsSuppliesDto>> DeleteSuppliesAsync(Guid id);

    }
}
