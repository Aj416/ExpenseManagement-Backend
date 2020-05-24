using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using Entities.Models;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ExpensesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<IActionResult> GetAllExpense()
        {
            try
            {
                var expenses = await _repository.Expense.GetAllExpenseAsync();
                _logger.LogInfo($"Returned all expenses from database.");

                var result = expenses.GroupBy(exp => exp.Date.Date)
                    .Select(dto => new ExpenseDto { Date = dto.Key.Date, Amount = dto.Sum(x => x.Detail.Amount) });
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllExpense action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //// GET: api/Expenses/5
        [HttpGet("{date}/details")]
        public async Task<IActionResult> GetExpenseWithDetails(DateTime date)
        {
            try
            {
                var expenses = await _repository.Expense.GetExpenseWithDetailsAsync(date);
                if (expenses.Count() == 0)
                {
                    _logger.LogError($"Expenses on date: {date}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned expenses with details on date: {date}");

                    var result = _mapper.Map<IEnumerable<ExpenseDetailDto>>(expenses);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetExpenseWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
