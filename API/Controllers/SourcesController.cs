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
    public class SourcesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public SourcesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Sources
        [HttpGet]
        public async Task<IActionResult> GetAllSource()
        {
            try
            {
                var sources = await _repository.Source.GetAllSourceAsync();
                _logger.LogInfo($"Returned all sources from database.");

                var result = _mapper.Map<IEnumerable<SourceDto>>(sources);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSource action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Sources/5
        [HttpGet("{id}", Name = "SourceById")]
        public async Task<IActionResult> GetSourceById(int id)
        {
            try
            {
                var source = await _repository.Source.GetSourceByIdAsync(id);

                if (source == null)
                {
                    _logger.LogError($"Source with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned source with id: {id}");

                    var result = _mapper.Map<SourceDto>(source);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSourceById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Sources/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSource(int id, [FromBody] CreateUpdateSourceDto source)
        {
            try
            {
                if (source == null)
                {
                    _logger.LogError("Source object sent from client is null.");
                    return BadRequest("Source object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid source object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var sourceEntity = await _repository.Source.GetSourceByIdAsync(id);
                if (sourceEntity == null)
                {
                    _logger.LogError($"Source with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(source, sourceEntity);
                sourceEntity.UpdatedDate = DateTime.Now;

                _repository.Source.UpdateSource(sourceEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSource action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Sources
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> CreateSource([FromBody] CreateUpdateSourceDto source)
        {
            try
            {
                if (source == null)
                {
                    _logger.LogError("Source object sent from client is null.");
                    return BadRequest("Source object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid source object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var sourceEntity = _mapper.Map<Source>(source);
                sourceEntity.CreatedDate = DateTime.Now;
                _repository.Source.CreateSource(sourceEntity);
                await _repository.SaveAsync();

                var createdSource = _mapper.Map<SourceDto>(sourceEntity);

                return CreatedAtRoute("SourceById", new { id = createdSource.Id }, createdSource);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSource action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Sources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSource(int id)
        {
            try
            {
                var source = await _repository.Source.GetSourceByIdAsync(id);
                if (source == null)
                {
                    _logger.LogError($"Source with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.ExpenseDetail.ExpenseDetailBySource(id).Any())
                {
                    _logger.LogError($"Cannot delete source with id: {id}. It has related expenseDetail. Delete those expenseDetail first");
                    return BadRequest("Cannot delete source. It has related expenseDetail. Delete those expenseDetail first");
                }

                _repository.Source.DeleteSource(source);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteSource action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
