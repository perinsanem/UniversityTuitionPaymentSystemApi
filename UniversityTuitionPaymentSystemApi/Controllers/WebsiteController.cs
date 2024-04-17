using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using UniversityTuitionPaymentSystemApi.Models;

namespace UniversityTuitionPaymentSystemApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebsiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-tuition")]
        [Authorize]
        public IActionResult AddTuition([FromBody] AddTuitionModel model)
        {
            
            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == model.StudentNumber);
            if (student == null)
                return NotFound("Student not found");



           
            _context.Tuitions.Add(new Tuition
            {
                StudentId = student.Id,
                Term = model.Term,
                Amount = 5000 
            });

            
            _context.SaveChanges();

            return Ok("Tuition added successfully");
        }

        public class AddTuitionModel
        {
            public string StudentNumber { get; set; }
            public string Term { get; set; }
        }

        [HttpGet("unpaid-tuition-status/{term}")]
        [Authorize]
        public IActionResult GetUnpaidTuitionStatus(string term, int page = 1, int pageSize = 2)
        {
            var unpaidStudents = _context.Students
        .Where(s => _context.Tuitions.Any(t => t.StudentId == s.Id && t.Term == term))
        .Select(s => new
        {
            StudentId = s.Id,
            StudentNo = s.StudentNumber,
            UnpaidAmount = _context.Tuitions
                .Where(t => t.StudentId == s.Id && t.Term == term)
                .Sum(t => t.Amount) -
                _context.Payments
                .Where(p => p.StudentId == s.Id && p.Term == term)
                .Sum(p => p.Amount)
        })
        .Where(s => s.UnpaidAmount > 0)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

            return Ok(unpaidStudents);
        }


    }
}
