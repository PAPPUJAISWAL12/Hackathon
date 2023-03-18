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
    public class StudentsController : ControllerBase
    {
        private readonly NamunaCollegeContext _context;

        public StudentsController(NamunaCollegeContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentView>>> GetStudents()
        {          
            return await _context.StudentViews.ToListAsync();
        }


        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentView>> GetStudent(int id)
        {          
            var student =  _context.StudentViews.Where(x=>x.StdId==id).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }
            return student;
        }


        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StdId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentEdit>> PostStudent(StudentEdit edit)
        {
            User u=new User
            {
                UserEmail=edit.UserEmail,
                FullName=edit.FullName,
                UserAddress=edit.UserAddress,
                Phone=edit.Phone,
                Upassword=edit.Upassword,
                LoginStatus=edit.LoginStatus
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();

            Student std = new Student
            {
                UserId=u.UserId,
                Cid=edit.Cid
            };
            _context.Students.Add(std);
            await _context.SaveChangesAsync();

            UserRole r = new UserRole
            {
                UserId=u.UserId,
                RoleId=edit.RoleId
            };
            _context.UserRoles.Add(r);
            await _context.SaveChangesAsync();
            return Ok(edit);
        }

       
        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.StdId == id)).GetValueOrDefault();
        }
    }
}
