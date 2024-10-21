using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        //---------------------------------- GET -----------------------------//
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        //---------------------------------- POST -----------------------------//
        [HttpPost]

        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        //---------------------------------- PUT -----------------------------//
        [HttpPut("{id}")]
        public IActionResult updateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
                return BadRequest();

            book.GenreId = updatedBook != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
        }

        //---------------------------------- DELETE -----------------------------//
        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
