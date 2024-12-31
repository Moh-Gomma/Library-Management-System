using LiberaryManagementAPI.Models;

namespace LiberaryManagementAPI.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAvailableBook();
        Task<bool> IsAvailable(int id);
        Task UpdateBookQuantity(int BookId, int quantity);
    }
}
