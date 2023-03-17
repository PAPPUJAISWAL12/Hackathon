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
          
            var feeDetails = _context.FeeDetailsViews.Where(x=>x.FeeId==id).FirstOrDefault();

            if (feeDetails == null)
            {
                return NotFound();
            }

            return feeDetails;
        }

        // PUT: api/Fees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFee(int id, Fee fee)
        {
            if (id != fee.FeeId)
            {
                return BadRequest();
            }

            _context.Entry(fee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
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

        // POST: api/Fees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeeEdit>> PostFee(FeeEdit edit)
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
				CancelledDate=edit.CancelledDate,
				CancelledBy=edit.CancelledBy,
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
