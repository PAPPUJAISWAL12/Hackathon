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
    public class RoleListsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public RoleListsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/RoleLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleList>>> GetRoleLists()
        {
          if (_context.RoleLists == null)
          {
              return NotFound();
          }
            return await _context.RoleLists.ToListAsync();
        }

        // GET: api/RoleLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleList>> GetRoleList(int id)
        {
          if (_context.RoleLists == null)
          {
              return NotFound();
          }
            var roleList = await _context.RoleLists.FindAsync(id);

            if (roleList == null)
            {
                return NotFound();
            }

            return roleList;
        }

        // PUT: api/RoleLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleList(int id, RoleList roleList)
        {
            if (id != roleList.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(roleList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleListExists(id))
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

        // POST: api/RoleLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleList>> PostRoleList(RoleList roleList)
        {
          if (_context.RoleLists == null)
          {
              return Problem("Entity set 'NamunaCollegeContext.RoleLists'  is null.");
          }
            _context.RoleLists.Add(roleList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleList", new { id = roleList.RoleId }, roleList);
        }

        // DELETE: api/RoleLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleList(int id)
        {
            if (_context.RoleLists == null)
            {
                return NotFound();
            }
            var roleList = await _context.RoleLists.FindAsync(id);
            if (roleList == null)
            {
                return NotFound();
            }

            _context.RoleLists.Remove(roleList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleListExists(int id)
        {
            return (_context.RoleLists?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
