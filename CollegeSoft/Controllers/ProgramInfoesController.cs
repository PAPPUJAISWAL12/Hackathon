using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeSoft.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

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
        public async Task<ActionResult<IEnumerable<PrograInfoView>>> GetProgramInfos()
        {
         
            return await _context.PrograInfoViews.ToListAsync();
        }

        // GET: api/ProgramInfoes/5
        [HttpGet("{id}")]
        public ActionResult<PrograInfoView> GetProgramInfo(int id)
        {         
            PrograInfoView? programInfo =  _context.PrograInfoViews.Where(x=>x.Pid==id).FirstOrDefault();

            if (programInfo == null)
            {
                return NotFound();
            }
            return programInfo;
        }

		// PUT: api/ProgramInfoes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

		[HttpPatch("{id}")]
		public IActionResult PatchReception(int id, [FromBody] JsonPatchDocument<ProgramInfo> patchdoc)
		{
			ProgramInfo? p = _context.ProgramInfos.FirstOrDefault(x => x.Pid == id);
			if (p == null)
			{
				return NotFound();
			}
			patchdoc.ApplyTo(p);
			_context.SaveChanges();
			return Ok(p);
		}
		// POST: api/ProgramInfoes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<ProgramInfoEdit>> PostProgramInfo(ProgramInfoEdit info)
        {
            info.EntryDate = DateTime.Today;
            ProgramInfo p = new ProgramInfo
            {
               Pname=info.Pname,
               Pdescription=info.Pdescription,
               Venue=info.Venue,
               StartDate=info.StartDate,
               StartTime=info.StartTime,
               EndDate=info.EndDate,
               EndTime=info.EndTime,
               UserId=info.UserId,
               EntryDate=info.EntryDate
            };
            _context.ProgramInfos.Add(p);
            await _context.SaveChangesAsync();

            return Ok(info);
        }

       

        private bool ProgramInfoExists(int id)
        {
            return (_context.ProgramInfos?.Any(e => e.Pid == id)).GetValueOrDefault();
        }
    }
}
