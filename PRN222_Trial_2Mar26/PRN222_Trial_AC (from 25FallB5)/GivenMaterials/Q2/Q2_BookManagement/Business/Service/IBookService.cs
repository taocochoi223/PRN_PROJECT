using Data.Models;

namespace Q2_BookManagement.Business.Service
{
    public interface IBookService
    {
        Task<List<Author>> GetAllAuthors();
        Task<(List<Book> Data, int TotalCount)> GetBooksPagedAsync(int? authorId, int page, int pageSize);
        Task<Book?> GetBookDetail(int id);
    }
}
