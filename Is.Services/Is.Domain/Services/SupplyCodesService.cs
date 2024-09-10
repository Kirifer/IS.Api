using AutoMapper;
using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Domain.Services.Interface;
using Is.Models;
using Microsoft.Extensions.Logging;
using Is.Datalayer.Interface;
using Is.Models.Entities.SupplyCodes;
using Is.Datalayer.Entities;
using Is.Core.Authentication;

namespace Is.Domain.Services
{
    public class SupplyCodesService : EntityService, ISupplyCodesService
    {
        // create a field for the repository
        private readonly ISupplyCodesRepository _supplyCodesRepository;
        private readonly IUserContext _userContext;
        //create a constructor for the service
        public SupplyCodesService(
            IMapper mapper,
            ILogger<SupplyCodesService> logger,
            IUserContext userContext,

            ISupplyCodesRepository supplyCodesRepository)
            : base(mapper, logger)
        {
            _supplyCodesRepository = supplyCodesRepository;
            _userContext = userContext;
        }

        // GET
        public async Task<Response<List<IsSupplyCodesDto>>> GetSupplyCodesAsync()
        {
            try
            {
                // get all supplies, if no filter is provided we passed it as true
                // if filter is provided, we will filter the supplies based on the filter
                var result = await _supplyCodesRepository.GetAllAsync(supplycodes => supplycodes.UserId == _userContext.UserId);

                // after fetching the supplies, we will map the supplies to the DOT
                var supplycodesDtoList = new List<IsSupplyCodesDto>();

                // loop through the supply codes data
                foreach (var supplycodes in result)
                {
                    // create an instance of IsSupplyCodesDto
                    // pass the values from the supplycodes to the DTO
                    var supplycodesDto = new IsSupplyCodesDto
                    {
                        Id = supplycodes.Id,
                        Code = supplycodes.Code,
                        SequenceCode = supplycodes.SequenceCode,
                        Category = supplycodes.Category,
                        Item = supplycodes.Item,
                        Color = supplycodes.Color,
                        Size = supplycodes.Size,
                        Quantity = supplycodes.Quantity,
                        SupplyTaken = supplycodes.SupplyTaken,

                    };

                    // add the DTO to the l ist
                    supplycodesDtoList.Add(supplycodesDto);
                }
                return Response<List<IsSupplyCodesDto>>.Success(supplycodesDtoList, supplycodesDtoList.Count);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while fetching supply codes");
                return Response<List<IsSupplyCodesDto>>.Exception(ex);
            }
        }

        // GET (LIST)
        public async Task<Response<IsSupplyCodesDto>> GetSupplyCodeAsync(Guid id)
        {
            try
            {
                var result = await _supplyCodesRepository.GetAsync(id);
                var supplycodesDto = new IsSupplyCodesDto
                {
                    Id = result.Id,
                    Code = result.Code,
                    SequenceCode = result.SequenceCode,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,

                };
                return Response<IsSupplyCodesDto>.Success(supplycodesDto);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Error occured while fetching supply codes with id.");
                return Response<IsSupplyCodesDto>.Exception(ex);
            }
        }

        // CREATE
        public async Task<Response<List<IsSupplyCodesDto>>> CreateSupplyCodesAsync(IsSupplyCodesCreateDto payload)
        {
            try
            {
                var supplycodesDtoList = new List<IsSupplyCodesDto>();

                var existingEntries = await _supplyCodesRepository.GetAllAsync(sc =>
                    sc.Code == payload.Code);

                // Determine the highest SequenceCode
                int maxSequenceCode = existingEntries.Any() ? existingEntries.Max(sc => sc.SequenceCode) : 0;
                // loop for the quantity
                for (int i = 1; i <= payload.Quantity; i++)
                {
                    var createRef = new SupplyCodes
                    {
                        Id = Guid.NewGuid(), // generate new id for each entry
                        Code = payload.Code,
                        SequenceCode = maxSequenceCode + i, // continue the sequence
                        Category = payload.Category,
                        Item = payload.Item,
                        Color = payload.Color,
                        Size = payload.Size,
                        Quantity = 1, 
                        UserId = _userContext.UserId
                    };

                    var result = await _supplyCodesRepository.AddAsync(createRef);

                    var supplycodesDto = new IsSupplyCodesDto
                    {
                        Id = result.Id,
                        Code = result.Code,
                        SequenceCode = result.SequenceCode,
                        Category = result.Category,
                        Item = result.Item,
                        Color = result.Color,
                        Size = result.Size,
                        Quantity = result.Quantity,
                    };

                    supplycodesDtoList.Add(supplycodesDto);
                }

                return Response<List<IsSupplyCodesDto>>.Success(supplycodesDtoList, supplycodesDtoList.Count);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating supply codes");
                return Response<List<IsSupplyCodesDto>>.Exception(ex);
            }
        }


        // UPDATE
        public async Task<Response<IsSupplyCodesDto>> UpdateSupplyCodesAsync(Guid id, IsSupplyCodesUpdateDto code)
        {
            try
            {
                var updateRef = await _supplyCodesRepository.GetAsync(id);
                updateRef.Id = code.Id;
                updateRef.Code = code.Code;
                updateRef.SequenceCode = code.SequenceCode;
                updateRef.Category = code.Category;
                updateRef.Item = code.Item;
                updateRef.Color = code.Color;
                updateRef.Size = code.Size;
                updateRef.Quantity = code.Quantity;
                updateRef.SupplyTaken = code.SupplyTaken;

                var result = await _supplyCodesRepository.UpdateAsync(updateRef);
                var supplycodesDto = new IsSupplyCodesDto
                {
                    Id = result.Id,
                    Code = result.Code,
                    SequenceCode = result.SequenceCode,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    SupplyTaken = result.SupplyTaken,

                };
                return Response<IsSupplyCodesDto>.Success(supplycodesDto);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while updating codes.");
                return Response<IsSupplyCodesDto>.Exception(ex);
            }
            
        }

        // DELETE
        public async Task<Response<IsSupplyCodesDto>> DeleteSupplyCodesAsync(Guid id)
        {
            try
            {
                var deleteRef = await _supplyCodesRepository.GetAsync(id);
                var result = await _supplyCodesRepository.DeleteAsync(deleteRef);
                var supplycodesDto = new IsSupplyCodesDto
                {
                    Id= result.Id,
                    Code = result.Code,
                    SequenceCode = result.SequenceCode,
                    Category = result.Category,
                    Item = result.Item,
                    Color = result.Color,
                    Size = result.Size,
                    Quantity = result.Quantity,
                    SupplyTaken = result.SupplyTaken,
                };
                return Response<IsSupplyCodesDto>.Success(supplycodesDto);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Error occured while deleting codes.");
                return Response<IsSupplyCodesDto>.Exception(ex);    
            }
        }
    }
}