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

        //// GET: api/Sources/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Source>> GetSource(int id)
        //{
        //    var source = await _context.Source.FindAsync(id);

        //    if (source == null)
        //    {
        //        return NotFound();
        //    }

        //    return source;
        //}

        //// PUT: api/Sources/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSource(int id, Source source)
        //{
        //    if (id != source.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(source).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SourceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Sources
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Source>> PostSource(Source source)
        //{
        //    _context.Source.Add(source);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSource", new { id = source.Id }, source);
        //}

        //// DELETE: api/Sources/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Source>> DeleteSource(int id)
        //{
        //    var source = await _context.Source.FindAsync(id);
        //    if (source == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Source.Remove(source);
        //    await _context.SaveChangesAsync();

        //    return source;
        //}

        //private bool SourceExists(int id)
        //{
        //    return _context.Source.Any(e => e.Id == id);
        //}
    }
}
