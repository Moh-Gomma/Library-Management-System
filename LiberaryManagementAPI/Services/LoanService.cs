using LiberaryManagementAPI.Models;
using LiberaryManagementAPI.Repositories;

namespace LiberaryManagementAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private const int LoanPeriodDays = 10;

        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }
        public async Task<Loan> CreateLoan(int bookId, int memberId)
        {
            if (!await _bookRepository.IsAvailable(bookId))
                throw new BusinessRuleException("Book is not available for loan");

            if (await _loanRepository.HasMemberReachedLoanLimit(memberId))
                throw new BusinessRuleException("Member has reached maximum loan limit");

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(LoanPeriodDays),
                IsReturned = false
            };

            await _loanRepository.AddAsync(loan);

            var book = await _bookRepository.GetByIdAsync(bookId);
            book.AvailableCopies--;
            await _bookRepository.UpdateAsync(book);

            return loan;
        }

        public async Task<Loan> ReturnBook(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new BusinessRuleException("Loan not found");

            if (loan.IsReturned)
                throw new BusinessRuleException("Book already returned");

            loan.ReturnDate = DateTime.Now;
            loan.IsReturned = true;

            await _loanRepository.UpdateAsync(loan);
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            book.AvailableCopies++;
            await _bookRepository.UpdateAsync(book);

            return loan;
        }
    }
}
