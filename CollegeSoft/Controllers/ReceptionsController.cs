using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeSoft.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using NuGet.Protocol.Plugins;

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
        public async Task<ActionResult<IEnumerable<ReceptionView>>> GetReceptions()
        {
         
            return await _context.ReceptionViews.ToListAsync();
        }

        // GET: api/Receptions/5
        [HttpGet("{id}")]
        public ActionResult<ReceptionView> GetReception(int id)
        {
          
            var reception =  _context.ReceptionViews.Where(x=>x.Rid==id).FirstOrDefault();

            if (reception == null)
            {
                return NotFound();
            }

            return reception;
        }

        // PUT: api/Receptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public IActionResult PatchReception(int id, [FromBody] JsonPatchDocument<Reception> patchdoc)
        {
            Reception? reception = _context.Receptions.FirstOrDefault(x=>x.Rid==id);
            if (reception == null)
            {
                return NotFound();
            }
            patchdoc.ApplyTo(reception);
            _context.SaveChanges();
            return Ok(reception);
        }

        // POST: api/Receptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceptionEdit>> PostReception(ReceptionEdit reception)
        {
            reception.EntryDate = DateTime.Today;
            reception.EntryTime = DateTime.UtcNow.AddMinutes(345).ToShortTimeString();

            Reception r = new Reception
            {
                EntryDate=reception.EntryDate,
                EntryTime=reception.EntryTime,
                Purpose=reception.Purpose,
                PersonAddress=reception.PersonAddress,
                Phone=reception.Phone,
                UserId=reception.UserId,
                FiscalYear=reception.FiscalYear
            };
            _context.Receptions.Add(r);
            await _context.SaveChangesAsync();

            return Ok(r);
        }

       /* // DELETE: api/Receptions/5
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
*/
        private bool ReceptionExists(int id)
        {
            return (_context.Receptions?.Any(e => e.Rid == id)).GetValueOrDefault();
        }
    }
}
