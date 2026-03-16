using Data.Models;
using Microsoft.EntityFrameworkCore;
using Q2_BookManagement.Data.Repo;

namespace Q2_BookManagement.Business.Service
{
    public class BookService : IBookService
    {
        private readonly BookRepo _bookRepo;

        public BookService(BookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Task<List<Author>> GetAllAuthors()
        {
            return _bookRepo.GetAuthorsAsync().ToListAsync();
        }

        public async Task<(List<Book> Data, int TotalCount)> GetBooksPagedAsync(int? authorId, int page, int pageSize)
        {
            var query = _bookRepo.GetBooksQueryable();

            if (authorId.HasValue && authorId.Value > 0)
            {
                query = query.Where(b => b.Authors.Any(a => a.AuthorId == authorId));
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var data = await query.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return (data, totalPages);
        }

        public async Task<Book?> GetBookDetail(int id)
        {
            return await _bookRepo.GetBookByIdAsync(id);
        }
    }
}
