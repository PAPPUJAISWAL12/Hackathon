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
    public class ProgramInfoesController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public ProgramInfoesController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/ProgramInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramInfo>>> GetProgramInfos()
        {
          if (_context.ProgramInfos == null)
          {
              return NotFound();
          }
            return await _context.ProgramInfos.ToListAsync();
        }

        // GET: api/ProgramInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramInfo>> GetProgramInfo(int id)
        {
          if (_context.ProgramInfos == null)
          {
              return NotFound();
          }
            var programInfo = await _context.ProgramInfos.FindAsync(id);

            if (programInfo == null)
            {
                return NotFound();
            }

            return programInfo;
        }

        // PUT: api/ProgramInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramInfo(int id, ProgramInfo programInfo)
        {
            if (id != programInfo.Pid)
            {
                return BadRequest();
            }

            _context.Entry(programInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramInfoExists(id))
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

        // POST: api/ProgramInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgramInfo>> PostProgramInfo(ProgramInfo programInfo)
        {
          if (_context.ProgramInfos == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.ProgramInfos'  is null.");
          }
            _context.ProgramInfos.Add(programInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramInfo", new { id = programInfo.Pid }, programInfo);
        }

        // DELETE: api/ProgramInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramInfo(int id)
        {
            if (_context.ProgramInfos == null)
            {
                return NotFound();
            }
            var programInfo = await _context.ProgramInfos.FindAsync(id);
            if (programInfo == null)
            {
                return NotFound();
            }

            _context.ProgramInfos.Remove(programInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramInfoExists(int id)
        {
            return (_context.ProgramInfos?.Any(e => e.Pid == id)).GetValueOrDefault();
        }
    }
}
