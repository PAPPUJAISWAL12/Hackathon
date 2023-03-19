using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeSoft.Models;
using System.Runtime.CompilerServices;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace CollegeSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public FeesController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Fees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeeDetailsView>>> GetFees()
        {
            return await _context.FeeDetailsViews.ToListAsync();
        }

        // GET: api/Fees/5
        [HttpGet("{id}")]
        public ActionResult<FeeDetailsView> GetFee(int id)
        {
            var feeDetails = _context.FeeDetailsViews.Where(x => x.FeeId == id).FirstOrDefault();

            if (feeDetails == null)
            {
                return NotFound();
            }

            return feeDetails;
        }

        // PUT: api/Fees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<ActionResult<Fee>> PatchFee(int id, [FromBody] JsonPatchDocument<Fee> patchdoc)
        {
            Fee? f = _context.Fees.FirstOrDefault(a => a.FeeId == id);
            if (f == null)
            {
                return NotFound();
            }
            patchdoc.ApplyTo(f);
            await _context.SaveChangesAsync();            
            return Ok(f);
        }

        // POST: api/Fees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeePrintView>> PostFee(FeeEdit edit)
        {
            edit.EntryDate = DateTime.Today;
            edit.EntryTime = DateTime.UtcNow.AddMinutes(345).ToShortTimeString();
            Fee fee = new Fee
            {
                StdId=edit.StdId,
				MonthlyFeeAmt=edit.MonthlyFeeAmt,
				YearlyAmt=edit.YearlyAmt,
				YearlyDiscount=edit.YearlyDiscount,
				Examfee=edit.Examfee,
				FiscalYear=edit.FiscalYear,
				EntryBy=edit.EntryBy,
				EntryDate=edit.EntryDate,
				EntryTime=edit.EntryTime,
				
			};
            _context.Fees.Add(fee);
            await _context.SaveChangesAsync();

            FeeDetail details = new FeeDetail
            {
                FeeId=fee.FeeId,
				TotalAmt=edit.TotalAmt,
				PaidAmt=edit.PaidAmt,
				RemainingAmt=edit.RemainingAmt,
				FeeStatus=edit.FeeStatus
			};
            _context.FeeDetails.Add(details);
            await _context.SaveChangesAsync();

            FeePrint print = new FeePrint
            {
                DetailId=details.DetailId,
				PrintDate=fee.EntryDate,
				PrintTime=fee.EntryTime,
				PrintUserId= fee.EntryBy
            };
            _context.FeePrints.Add(print);
            await _context.SaveChangesAsync();

            return Ok(_context.FeePrintViews.Where(x=>x.PrintId==print.PrintId).FirstOrDefault());
        }

       /* // DELETE: api/Fees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            if (_context.Fees == null)
            {
                return NotFound();
            }
            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }

            _context.Fees.Remove(fee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
*/
        private bool FeeExists(int id)
        {
            return (_context.Fees?.Any(e => e.FeeId == id)).GetValueOrDefault();
        }
    }
}
