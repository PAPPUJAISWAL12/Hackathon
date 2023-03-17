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
    public class UploadsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public UploadsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Uploads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Upload>>> GetUploads()
        {
          if (_context.Uploads == null)
          {
              return NotFound();
          }
            return await _context.Uploads.ToListAsync();
        }

        // GET: api/Uploads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Upload>> GetUpload(int id)
        {
          if (_context.Uploads == null)
          {
              return NotFound();
          }
            var upload = await _context.Uploads.FindAsync(id);

            if (upload == null)
            {
                return NotFound();
            }

            return upload;
        }

        // PUT: api/Uploads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpload(int id, Upload upload)
        {
            if (id != upload.UploadId)
            {
                return BadRequest();
            }

            _context.Entry(upload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(id))
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

        // POST: api/Uploads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Upload>> PostUpload(Upload upload)
        {
          if (_context.Uploads == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.Uploads'  is null.");
          }
            _context.Uploads.Add(upload);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpload", new { id = upload.UploadId }, upload);
        }

        // DELETE: api/Uploads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpload(int id)
        {
            if (_context.Uploads == null)
            {
                return NotFound();
            }
            var upload = await _context.Uploads.FindAsync(id);
            if (upload == null)
            {
                return NotFound();
            }

            _context.Uploads.Remove(upload);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UploadExists(int id)
        {
            return (_context.Uploads?.Any(e => e.UploadId == id)).GetValueOrDefault();
        }
    }
}
