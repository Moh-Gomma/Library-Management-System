using LiberaryManagementAPI.Models;

namespace LiberaryManagementAPI.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetOverdueLoans();
        Task<decimal> CalculateFine(int loanId);
        Task<bool> HasMemberReachedLoanLimit(int memberId);
    }
}
