using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentSystemApi.Models;

namespace UniversityTuitionPaymentSystemApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("query-tuition/{studentNumber}")]
        [Authorize]
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


        [HttpPost("pay-tuition")]
        public IActionResult PayTuition([FromBody] PayTuitionModel model)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == model.StudentNumber);
            if (student == null)
                return NotFound("Student not found");

          
            var tuition = _context.Tuitions
                                .Where(t => t.StudentId == student.Id && t.Term == model.Term && t.Amount != null)
                                .Sum(t => t.Amount);

            
            var payments = _context.Payments
                                    .Where(p => p.StudentId == student.Id && p.Term == model.Term && p.Amount != null)
                                    .Sum(p => p.Amount);

          
            var balance = tuition - payments;

           
            var remainingAmount = balance;

          
            var paymentStatus = balance == 0 ? "Successful.You already paid tuition." : "Error.Your tuition not paid fully.";

         
            if (remainingAmount > 0)
            {
                _context.Payments.Add(new Payment
                {
                    StudentId = student.Id,
                    Term = model.Term,
                    Amount = (float)remainingAmount
                });

                
                _context.SaveChanges();
            }

            return Ok(paymentStatus);
        }


        public class PayTuitionModel
        {
        public string StudentNumber { get; set; }
        public string Term { get; set; }

        }

       


    }


}
