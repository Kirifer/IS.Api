
using System.Net;

using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Datalayer;
using Is.Datalayer.Entities;
using Is.Datalayer.Interface;
using Is.Domain.Services.Interface;
using Is.Models;
using Is.Shared.Enums;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Is.Models.Entities.Supply;

namespace Is.Domain.Services
{
    public class SuppliesService : EntityService, ISuppliesService
    {
        private readonly ISuppliesRepository _suppliesRepository;

        public SuppliesService(
            IMapper mapper,
            ILogger<SuppliesService> logger,

            ISuppliesRepository suppliesRepository)
            : base(mapper, logger)
        {
            _suppliesRepository = suppliesRepository;
        }

        // GET
        public async Task<Response<List<IsSuppliesDto>>> GetSuppliesAsync()
        {
            try
            {
                var result = await _suppliesRepository.GetAllAsync(u => true);

                var suppliesDtos = new List<IsSuppliesDto>();
                foreach (var supplier in result)
                {
                    var suppliesDto = new IsSuppliesDto();
                    suppliesDto.Id = supplier.Id;
                    suppliesDto.Category = supplier.Category;
                    suppliesDto.Item = supplier.Item;
                    suppliesDto.Color = supplier.Color;
                    suppliesDto.Size = supplier.Size;
                    suppliesDto.Quantity = supplier.Quantity;
                    suppliesDto.SuppliesTaken = supplier.SuppliesTaken;
                    suppliesDto.SuppliesLeft = supplier.SuppliesLeft;
                    suppliesDto.CostPerUnit = supplier.CostPerUnit;
                    suppliesDto.Total = supplier.Total;
                    suppliesDtos.Add(suppliesDto);
                }
                return Response<List<IsSuppliesDto>>.Success(suppliesDtos);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching users");
                return Response<List<IsSuppliesDto>>.Exception(ex);
            }
        }

        // GET (FILTER)
        public async Task<Response<IsSuppliesDto>> GetSupplyAsync(Guid id)
        {
            try
            {
                var result = await _suppliesRepository.GetAsync(id);
                var suppliesDto = new IsSuppliesDto
                {
                    Id = result.Id,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    SuppliesTaken = result.SuppliesTaken,
                    SuppliesLeft = result.SuppliesLeft,
                    CostPerUnit = result.CostPerUnit,
                    Total = result.Total,

                };
                return Response<IsSuppliesDto>.Success(suppliesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while fetching supply with the id.");
                return Response<IsSuppliesDto>.Exception(ex);
            }
        }

        // CREATE
        public async Task<Response<IsSuppliesDto>> CreateSuppliesAsync(IsSuppliesCreateDto payload)
        {
            try
            {
                var createRef = new Supplies
                {
                    Id= payload.Id,
                    Category = payload.Category,
                    Item = payload.Item,
                    Color = payload.Color,
                    Size = payload.Size,
                    Quantity = payload.Quantity,
                    SuppliesTaken = payload.SuppliesTaken,
                    SuppliesLeft = payload.SuppliesLeft,
                    CostPerUnit = payload.CostPerUnit,
                    Total = payload.Total,

                };

                var result = await _suppliesRepository.AddAsync(createRef);

                var suppliesDto = new IsSuppliesDto
                {
                    Id = result.Id,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    CostPerUnit = result.CostPerUnit,
                };
                return Response<IsSuppliesDto>.Success(suppliesDto);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while creating supplies.");
                return Response<IsSuppliesDto>.Exception(ex);
            }
        }

        // UPDATE
        public async Task<Response<IsSuppliesDto>> UpdateSuppliesAsync(Guid id, IsSuppliesUpdateDto supplier)
        {
            try
            {
                var updateRef = await _suppliesRepository.GetAsync(id);
                updateRef.Id = supplier.Id;
                updateRef.Category = supplier.Category;
                updateRef.Item = supplier.Item;
                updateRef.Color = supplier.Color;
                updateRef.Size = supplier.Size;
                updateRef.Quantity = supplier.Quantity;
                updateRef.SuppliesTaken = supplier.SuppliesTaken;
                updateRef.SuppliesLeft = supplier.SuppliesLeft;
                updateRef.CostPerUnit = supplier.CostPerUnit;
                updateRef.Total = supplier.Total;

                var result = await _suppliesRepository.UpdateAsync(updateRef);

                var suppliesDto = new IsSuppliesDto
                {
                    Id = result.Id,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    SuppliesTaken = result.SuppliesTaken,
                    SuppliesLeft = result.SuppliesLeft,
                    CostPerUnit = result.CostPerUnit,
                    Total = result.Total,
                };
                return Response<IsSuppliesDto>.Success(suppliesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating");
                return Response<IsSuppliesDto>.Exception(ex);
            }

        }

        // DELETE
        public async Task<Response<IsSuppliesDto>> DeleteSuppliesAsync(Guid id)
        {
            try
            {
                var deleteRef = await _suppliesRepository.GetAsync(id);
                var result = await _suppliesRepository.DeleteAsync(deleteRef);
                var suppliesDto = new IsSuppliesDto
                {
                    Id = result.Id,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    SuppliesTaken = result.SuppliesTaken,
                    SuppliesLeft = result.SuppliesLeft,
                    CostPerUnit = result.CostPerUnit,
                    Total = result.Total,

                };
                return Response<IsSuppliesDto>.Success(suppliesDto);
            }
            catch (Exception ex)
            {
                {
                    Logger.LogError(ex, "Error occurred while deleting supply.");
                    return Response<IsSuppliesDto>.Exception(ex);
                }
            }
        }
    }
}
    