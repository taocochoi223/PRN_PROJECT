using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Q2_BookManagement.Data.Repo
{
    public class BookRepo
    {
        private readonly PePrn25fallB523Context _context;

        public BookRepo(PePrn25fallB523Context context)
        {
            _context = context;
        }

        public IQueryable<Book> GetBooksQueryable()
        {
            return _context.Books
                .Include(b => b.Authors)
                .Include(g => g.Genre)
                .AsTracking();
        }

        public IQueryable<Author> GetAuthorsAsync()
        {
            return _context.Authors.AsTracking();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
