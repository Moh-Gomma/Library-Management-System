using LiberaryManagementAPI.Data;
using LiberaryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LiberaryManagementAPI.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private const int MaxLoansPerMember = 5;
        private const decimal FinePerDay = 1.00m;

        public LoanRepository(LiberaryDbContext context) : base(context) { }

        public async Task<IEnumerable<Loan>> GetOverdueLoans()
        {
            return await _context.loans
               .Where(l => !l.IsReturned && l.DueDate < DateTime.Now)
               .Include(a=>a.Book)
               .Include(l => l.Member)
               .ToListAsync();
        }
        public async Task<decimal> CalculateFine(int loanId)
        {
            var loan = await _context.loans.FindAsync(loanId);
            if (loan == null || !loan.ReturnDate.HasValue) return 0;

            var daysOverdue = (loan.ReturnDate.Value - loan.DueDate).Days;
            return daysOverdue > 0 ? daysOverdue * FinePerDay : 0;
        }


        public async Task<bool> HasMemberReachedLoanLimit(int memberId)
        {
            var activeLoans = await _context.loans
                .CountAsync(l => l.MemberId == memberId && !l.IsReturned);
            return activeLoans >= MaxLoansPerMember;
        }
    }
}
