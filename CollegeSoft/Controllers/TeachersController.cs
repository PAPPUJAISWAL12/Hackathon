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
    public class TeachersController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public TeachersController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherView>>> GetTeachers()
        {
          
            return await _context.TeacherViews.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public ActionResult<TeacherView> GetTeacher(int id)
        {
          
            var teacher =  _context.TeacherViews.Where(x=>x.UserId==id).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound();
            }
            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Tid)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherEdit>> PostTeacher(TeacherEdit edit)
        {
            User u = new User
            {
                UserId = edit.UserId,
                UserAddress=edit.UserAddress,
                UserEmail=edit.UserEmail,
                Upassword=edit.Upassword,
                FullName=edit.FullName,
                LoginStatus=edit.LoginStatus
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();

            Teacher t = new Teacher
            {
                UserId=u.UserId,
                Tpost=edit.Tpost
            };
            _context.Teachers.Add(t);
            await _context.SaveChangesAsync();

			UserRole r = new UserRole
			{
				UserId = u.UserId,
				RoleId = edit.RoleId
			};
			_context.UserRoles.Add(r);
			await _context.SaveChangesAsync();
			return Ok();
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teachers == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(e => e.Tid == id)).GetValueOrDefault();
        }
    }
}
