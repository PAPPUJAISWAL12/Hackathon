using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeSoft.Models;

namespace CollegeSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public ReceptionsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Receptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reception>>> GetReceptions()
        {
          if (_context.Receptions == null)
          {
              return NotFound();
          }
            return await _context.Receptions.ToListAsync();
        }

        // GET: api/Receptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reception>> GetReception(int id)
        {
          if (_context.Receptions == null)
          {
              return NotFound();
          }
            var reception = await _context.Receptions.FindAsync(id);

            if (reception == null)
            {
                return NotFound();
            }

            return reception;
        }

        // PUT: api/Receptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReception(int id, Reception reception)
        {
            if (id != reception.Rid)
            {
                return BadRequest();
            }

            _context.Entry(reception).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Receptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reception>> PostReception(Reception reception)
        {
          if (_context.Receptions == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.Receptions'  is null.");
          }
            _context.Receptions.Add(reception);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReception", new { id = reception.Rid }, reception);
        }

        // DELETE: api/Receptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReception(int id)
        {
            if (_context.Receptions == null)
            {
                return NotFound();
            }
            var reception = await _context.Receptions.FindAsync(id);
            if (reception == null)
            {
                return NotFound();
            }

            _context.Receptions.Remove(reception);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceptionExists(int id)
        {
            return (_context.Receptions?.Any(e => e.Rid == id)).GetValueOrDefault();
        }
    }
}
