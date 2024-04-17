using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentSystemApi.Models;

namespace UniversityTuitionPaymentSystemApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MobileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("query-tuition/{studentNumber}")]
        public IActionResult QueryTuition(string studentNumber)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
            if (student == null)
                return NotFound("Student not found");

            
            var tuition = _context.Tuitions
                                .Where(t => t.StudentId == student.Id && t.Amount != null)
                                .Sum(t => t.Amount);

            
            var payments = _context.Payments
                                    .Where(p => p.StudentId == student.Id && p.Amount != null)
                                    .Sum(p => p.Amount);

            
            var balance = tuition - payments;

            return Ok(new { tuition_total = tuition, balance });
        }
    }
}

