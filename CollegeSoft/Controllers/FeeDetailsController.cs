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
    public class FeeDetailsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public FeeDetailsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/FeeDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeeDetail>>> GetFeeDetails()
        {
          if (_context.FeeDetails == null)
          {
              return NotFound();
          }
            return await _context.FeeDetails.ToListAsync();
        }

        // GET: api/FeeDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeeDetail>> GetFeeDetail(int id)
        {
          if (_context.FeeDetails == null)
          {
              return NotFound();
          }
            var feeDetail = await _context.FeeDetails.FindAsync(id);

            if (feeDetail == null)
            {
                return NotFound();
            }

            return feeDetail;
        }

        // PUT: api/FeeDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeeDetail(int id, FeeDetail feeDetail)
        {
            if (id != feeDetail.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(feeDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeDetailExists(id))
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

        // POST: api/FeeDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeeDetail>> PostFeeDetail(FeeDetail feeDetail)
        {
          if (_context.FeeDetails == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.FeeDetails'  is null.");
          }
            _context.FeeDetails.Add(feeDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeeDetail", new { id = feeDetail.DetailId }, feeDetail);
        }

        // DELETE: api/FeeDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeDetail(int id)
        {
            if (_context.FeeDetails == null)
            {
                return NotFound();
            }
            var feeDetail = await _context.FeeDetails.FindAsync(id);
            if (feeDetail == null)
            {
                return NotFound();
            }

            _context.FeeDetails.Remove(feeDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeeDetailExists(int id)
        {
            return (_context.FeeDetails?.Any(e => e.DetailId == id)).GetValueOrDefault();
        }
    }
}
