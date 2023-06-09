﻿using System;
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
    public class DocumentsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public DocumentsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UploadFileView>>> GetDocuments()
        {
            return await _context.UploadFileViews.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public ActionResult<UploadFileView> GetDocument(int id)
        {
            var document = _context.UploadFileViews.Where(x => x.DocId == id).FirstOrDefault();      
			return Ok(document);
		}

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.DocId)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentEdit>> PostDocument(DocumentEdit DocEdit)
        {
            Document document = new Document
            {
                UserId=DocEdit.UserId
            };
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            foreach(UploadFile f in DocEdit.UploadFiles)
            {
                if (f.DocFile != null)
                {
                    f.DocId = document.DocId;
                    _context.UploadFiles.Add(f);
                }
                else
                {
                    return BadRequest();
                }
            }
            await _context.SaveChangesAsync();
            return Ok("Insert Successfully");
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return (_context.Documents?.Any(e => e.DocId == id)).GetValueOrDefault();
        }
    }
}
