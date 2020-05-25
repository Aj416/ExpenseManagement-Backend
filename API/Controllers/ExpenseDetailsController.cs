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
    public class ExpenseDetailsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ExpenseDetailsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ExpenseDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseDetailById(int id)
        {
            try
            {
                var expenseDetail = await _repository.ExpenseDetail.GetExpenseDetailByIdAsync(id);

                if (expenseDetail == null)
                {
                    _logger.LogError($"ExpenseDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ExpenseDetail with id: {id}");

                    var result = _mapper.Map<ExpenseDetailDto>(expenseDetail);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetExpenseDetailById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //// DELETE: api/ExpenseDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseDetail(int id)
        {
            try
            {
                var expenseDetail = await _repository.ExpenseDetail.GetExpenseDetailByIdAsync(id);
                if (expenseDetail == null)
                {
                    _logger.LogError($"Expense Detail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.ExpenseDetail.DeleteExpenseDetail(expenseDetail);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteExpenseDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
