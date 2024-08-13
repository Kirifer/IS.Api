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
using Is.Models.Entities.BudgetExpenses;

namespace Is.Domain.Services
{
    public class BudgetExpensesService : EntityService, IBudgetExpensesService
    {
        private readonly IBudgetExpensesRepository _budgetExpensesRepository;
        public BudgetExpensesService(
            IMapper mapper,
            ILogger<BudgetExpensesService> logger,
            IBudgetExpensesRepository budgetExpensesRepository)
            : base(mapper, logger)
        {
            _budgetExpensesRepository = budgetExpensesRepository;
        }
        public async Task<Response<List<IsBudgetExpensesDto>>> GetBudgetExpensesAsync()
        {
            try
            {
                var result = await _budgetExpensesRepository.GetAllAsync(u => true);
                var budgetExpensesDtoList = new List<IsBudgetExpensesDto>();
                foreach (var budgetExpenses in result)
                {
                    var budgetExpensesDto = new IsBudgetExpensesDto
                    {
                        Id = budgetExpenses.Id,
                        MonthCreated = budgetExpenses.MonthCreated,
                        YearCreated = budgetExpenses.YearCreated,
                        Budget = budgetExpenses.Budget,
                        Expenses = budgetExpenses.Expenses,
                        TotalBudget = budgetExpenses.TotalBudget
                    };
                    budgetExpensesDtoList.Add(budgetExpensesDto);
                }
                return Response<List<IsBudgetExpensesDto>>.Success(budgetExpensesDtoList, budgetExpensesDtoList.Count);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while fetching budget expenses");
                return Response<List<IsBudgetExpensesDto>>.Exception(ex);
            }
        }
        // GET
        public async Task<Response<IsBudgetExpensesDto>> GetBudgetExpenseAsync(Guid id)
        {
            try
            {
                var result = await _budgetExpensesRepository.GetAsync(id);
                var budgetExpensesDto = new IsBudgetExpensesDto
                {
                    Id = result.Id,
                    MonthCreated = result.MonthCreated,
                    YearCreated = result.YearCreated,
                    Budget = result.Budget,
                    Expenses = result.Expenses,
                    TotalBudget = result.TotalBudget
                };
                return Response<IsBudgetExpensesDto>.Success(budgetExpensesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while fetching budget expense with the id.");
                return Response<IsBudgetExpensesDto>.Exception(ex);
            }

        }

        // CREATE
        public async Task<Response<IsBudgetExpensesDto>> CreateBudgetExpensesAsync(IsBudgetExpensesCreateDto payload)
        {
            try
            {
                var createRef = new BudgetExpenses
                {
                    Id = payload.Id,
                    MonthCreated = payload.MonthCreated,
                    YearCreated = payload.YearCreated,
                    Budget = payload.Budget,
                    Expenses = payload.Expenses,
                    TotalBudget = payload.Budget - payload.Expenses
                };
                var result = await _budgetExpensesRepository.AddAsync(createRef);
                var budgetExpensesDto = new IsBudgetExpensesDto
                {
                    Id = result.Id,
                    MonthCreated = result.MonthCreated,
                    YearCreated = result.YearCreated,
                    Budget = result.Budget,
                    Expenses = result.Expenses,
                    TotalBudget = result.TotalBudget
                };
                return Response<IsBudgetExpensesDto>.Success(budgetExpensesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while creating budget expenses");
                return Response<IsBudgetExpensesDto>.Exception(ex);
            }

        }

        // UPDATE
        public async Task<Response<IsBudgetExpensesDto>> UpdateBudgetExpensesAsync(Guid id, IsBudgetExpensesUpdateDto payload)
        {
            try
            {
                var updateRef = await _budgetExpensesRepository.GetAsync(id);

                updateRef.MonthCreated = payload.MonthCreated;
                updateRef.YearCreated = payload.YearCreated;
                updateRef.Budget = payload.Budget;
                updateRef.Expenses = payload.Expenses;  
                updateRef.TotalBudget = payload.Budget - payload.Expenses;

                var result = await _budgetExpensesRepository.UpdateAsync(updateRef);

                var budgetExpensesDto = new IsBudgetExpensesDto
                {
                    Id = result.Id,
                    MonthCreated = result.MonthCreated,
                    YearCreated = result.YearCreated,
                    Budget = result.Budget,
                    Expenses = result.Expenses,
                    TotalBudget = result.TotalBudget
                };
                return Response<IsBudgetExpensesDto>.Success(budgetExpensesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while updating budget expenses");
                return Response<IsBudgetExpensesDto>.Exception(ex);
            }
        }

        // DELETE
        public async Task<Response<IsBudgetExpensesDto>> DeleteBudgetExpensesAsync(Guid id)
        {
            try
            {
                var deleteRef = await _budgetExpensesRepository.GetAsync(id);
                var result = await _budgetExpensesRepository.DeleteAsync(deleteRef);
                var budgetExpensesDto = new IsBudgetExpensesDto
                {
                    Id = result.Id,
                    MonthCreated = result.MonthCreated,
                    YearCreated = result.YearCreated,   
                    Budget = result.Budget,
                    Expenses = result.Expenses,
                    TotalBudget = result.TotalBudget
                };
                return Response<IsBudgetExpensesDto>.Success(budgetExpensesDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while deleting budget expenses");
                return Response<IsBudgetExpensesDto>.Exception(ex);
            }
        }
    }
}
