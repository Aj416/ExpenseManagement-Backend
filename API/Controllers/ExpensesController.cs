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
        public async Task<IActionResult> GetAllExpense(DateTime? date)
        {
            try
            {
                if (!date.HasValue)
                {
                    date = DateTime.Now;
                }

                var expenses = await _repository.Expense.GetAllExpenseAsync(date.Value);
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
        [HttpGet("list/{date}/", Name = "ExpenseWithDetailsByDate")]
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

                    var result = _mapper.Map<IEnumerable<ExpenseWithDetailDto>>(expenses);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetExpenseWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("detail/{id}", Name = "ExpenseWithDetailsById")]
        public async Task<IActionResult> GetExpenseWithDetails(int id)
        {
            try
            {
                var expenses = await _repository.Expense.GetExpenseWithDetailsAsync(id);
                if (expenses == null)
                {
                    _logger.LogError($"Expenses for id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned expenses with details for id: {id}");

                    var result = _mapper.Map<ExpenseWithDetailDto>(expenses);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetExpenseWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateUpdateExpenseDetailDto expense)
        {
            try
            {
                if (expense == null)
                {
                    _logger.LogError("Expense object sent from client is null.");
                    return BadRequest("Expense object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid expense object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var expenseEntity = _mapper.Map<Expense>(expense);

                _repository.Expense.CreateExpense(expenseEntity);
                await _repository.SaveAsync();

                var createdExpense = _mapper.Map<ExpenseWithDetailDto>(expenseEntity);

                return CreatedAtRoute("ExpenseWithDetailsById", new { id = createdExpense.ExpenseId }, createdExpense);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateExpense action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] CreateUpdateExpenseDetailDto expense)
        {
            try
            {
                if (expense == null)
                {
                    _logger.LogError("Expense object sent from client is null.");
                    return BadRequest("Expense object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid expense object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var expenseEntity = await _repository.Expense.GetExpenseWithDetailsAsync(id);
                if (expenseEntity == null)
                {
                    _logger.LogError($"Expense with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(expense, expenseEntity);

                _repository.Expense.UpdateExpense(expenseEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateExpense action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Expenses/category-report/2020-05-02
        [HttpGet("category-report/{date}")]
        public async Task<IActionResult> GetCategoryWiseExpense(DateTime date)
        {
            try
            {
                var expenses = await _repository.Expense.GetMonthWiseExpense(date);
                _logger.LogInfo($"Returned month wise expense from database.");

                var result = expenses.GroupBy(exp => exp.Detail.Category.Category1)
                    .Select(dto => new CategoryReportDto { Category = dto.Key, Amount = dto.Sum(x => x.Detail.Amount) })
                    .OrderByDescending(dto => dto.Amount);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCategoryWiseExpense action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Expenses/payer-report/2020-05-02
        [HttpGet("payer-report/{date}")]
        public async Task<IActionResult> GetPayertWiseExpense(DateTime date)
        {
            try
            {
                var expenses = await _repository.Expense.GetMonthWiseExpense(date);
                _logger.LogInfo($"Returned month wise expense from database.");

                var result = expenses.GroupBy(exp => new { exp.Detail.Source.BankName, exp.Detail.Source.AccountHolderName })
                    .Select(dto => new PayerReportDto { Payer = dto.Key.AccountHolderName + " => " + dto.Key.BankName, Amount = dto.Sum(x => x.Detail.Amount) })
                    .OrderByDescending(dto => dto.Amount);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPayertWiseExpense action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
