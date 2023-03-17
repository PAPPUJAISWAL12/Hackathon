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
    public class FeePrintsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public FeePrintsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/FeePrints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeePrint>>> GetFeePrints()
        {
          if (_context.FeePrints == null)
          {
              return NotFound();
          }
            return await _context.FeePrints.ToListAsync();
        }

        // GET: api/FeePrints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeePrint>> GetFeePrint(int id)
        {
          if (_context.FeePrints == null)
          {
              return NotFound();
          }
            var feePrint = await _context.FeePrints.FindAsync(id);

            if (feePrint == null)
            {
                return NotFound();
            }

            return feePrint;
        }

        // PUT: api/FeePrints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeePrint(int id, FeePrint feePrint)
        {
            if (id != feePrint.PrintId)
            {
                return BadRequest();
            }

            _context.Entry(feePrint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeePrintExists(id))
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

        // POST: api/FeePrints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeePrint>> PostFeePrint(FeePrint feePrint)
        {
          if (_context.FeePrints == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.FeePrints'  is null.");
          }
            _context.FeePrints.Add(feePrint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeePrint", new { id = feePrint.PrintId }, feePrint);
        }

        // DELETE: api/FeePrints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeePrint(int id)
        {
            if (_context.FeePrints == null)
            {
                return NotFound();
            }
            var feePrint = await _context.FeePrints.FindAsync(id);
            if (feePrint == null)
            {
                return NotFound();
            }

            _context.FeePrints.Remove(feePrint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeePrintExists(int id)
        {
            return (_context.FeePrints?.Any(e => e.PrintId == id)).GetValueOrDefault();
        }
    }
}
