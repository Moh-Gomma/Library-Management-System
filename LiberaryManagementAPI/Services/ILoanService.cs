using LiberaryManagementAPI.Models;

namespace LiberaryManagementAPI.Services
{
    public interface ILoanService
    {
        Task<Loan> CreateLoan(int bookId, int memberId);
        Task<Loan> ReturnBook(int loanId);
    }
}
