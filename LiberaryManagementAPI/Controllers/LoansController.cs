using LiberaryManagementAPI.Models;
using LiberaryManagementAPI.Repositories;
using LiberaryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiberaryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILoanRepository _loanRepository;

        public LoansController(ILoanService loanService, ILoanRepository loanRepository)
        {
            _loanService = loanService;
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _loanRepository.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetOverdueLoans()
        {
            var loans = await _loanRepository.GetOverdueLoans();
            return Ok(loans);
        }

        [HttpPost("borrow")]
        public async Task<ActionResult<Loan>> BorrowBook(int bookId, int memberId)
        {
            try
            {
                var loan = await _loanService.CreateLoan(bookId, memberId);
                return CreatedAtAction(nameof(GetLoan), new { id = loan.LoanId }, loan);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [HttpPost("{id}/return")]
        public async Task<ActionResult<Loan>> ReturnBook(int id)
        {
            try
            {
                var loan = await _loanService.ReturnBook(id);
                return Ok(loan);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
