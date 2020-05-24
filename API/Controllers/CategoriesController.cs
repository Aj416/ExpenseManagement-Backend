using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var categories = await _repository.Category.GetAllCategoryAsync();
                
                if (categories == null)
                {
                    _logger.LogError($"Categories not found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned all categories from database.");
                    var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned category with id: {id}");

                    var result = _mapper.Map<CategoryDto>(category);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCategoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateUpdateCategoryDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null.");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = await _repository.Category.GetCategoryByIdAsync(id);
                if (categoryEntity == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(category, categoryEntity);
                categoryEntity.UpdatedDate = DateTime.Now;

                _repository.Category.UpdateCategory(categoryEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateUpdateCategoryDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null.");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = _mapper.Map<Category>(category);
                categoryEntity.CreatedDate = DateTime.Now;
                _repository.Category.CreateCategory(categoryEntity);
                await _repository.SaveAsync();

                var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

                return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.ExpenseDetail.ExpenseDetailByCategory(id).Any())
                {
                    _logger.LogError($"Cannot delete category with id: {id}. It has related expenseDetail. Delete those expenseDetail first");
                    return BadRequest("Cannot delete category. It has related expenseDetail. Delete those expenseDetail first");
                }

                _repository.Category.DeleteCategory(category);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
