using Microsoft.AspNetCore.Mvc;
using Q2_BookManagement.Business.Service;

namespace Q2_BookManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Trong BooksController.cs
        public async Task<IActionResult> Index(int? authorId, int page = 1)
        {
            int pageSize = 5;
            var (books, totalPages) = await _bookService.GetBooksPagedAsync(authorId, page, pageSize);

            ViewBag.Authors = await _bookService.GetAllAuthors();
            ViewBag.SelectedAuthorId = authorId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(books);
        }


        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookDetail(id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
