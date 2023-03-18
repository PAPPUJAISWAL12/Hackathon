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
    public class UploadFilesController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public UploadFilesController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/UploadFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UploadFileView>>> GetUploadFiles()
        {
         
            return await _context.UploadFileViews.ToListAsync();
        }

        // GET: api/UploadFiles/5
        [HttpGet("{id}")]
        public ActionResult<UploadFileView> GetUploadFile(int id)
        {
         
            var uploadFile = _context.UploadFileViews.Where(x=>x.DocId==id).ToList();

            if (uploadFile == null)
            {
                return NotFound();
            }
            return Ok(uploadFile);
        }

        // PUT: api/UploadFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUploadFile(int id, UploadFile uploadFile)
        {
            if (id != uploadFile.UploadId)
            {
                return BadRequest();
            }

            _context.Entry(uploadFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadFileExists(id))
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

        // POST: api/UploadFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UploadFile>> PostUploadFile(UploadFile uploadFile)
        {
          
            _context.UploadFiles.Add(uploadFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUploadFile", new { id = uploadFile.UploadId }, uploadFile);
        }

        // DELETE: api/UploadFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUploadFile(int id)
        {
            if (_context.UploadFiles == null)
            {
                return NotFound();
            }
            var uploadFile = await _context.UploadFiles.FindAsync(id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            _context.UploadFiles.Remove(uploadFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UploadFileExists(int id)
        {
            return (_context.UploadFiles?.Any(e => e.UploadId == id)).GetValueOrDefault();
        }
    }
}
