using ASP_NET_Core_Base_HomeWork_Lesson005;
using ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ASP_NET_Core_Base_HomeWork_Lesson005.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBbookValidationService _bookValidationService;

        public BooksController(IBookService bookService, IBbookValidationService bookValidationService)
        {
            _bookService = bookService;
            _bookValidationService = bookValidationService;
        }

        [HttpGet("GetAllBooks")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookService.GetAll();
            if (books is null)
            {
                return BadRequest();
            }
            else return Ok(books);
        }

        [HttpGet("GetAllBooks/{filter}")]
        public ActionResult<IEnumerable<Book>> GetAllBooks([FromQuery]BookFilter filter)
        {
            var books = _bookService.GetAll(filter);
            if (books is null)
            {
                return BadRequest();
            }
            else return Ok(books);
        }

        [HttpGet("GetBookById/{id}")]
        public ActionResult<Book> GetBookById([FromQuery] Guid id)
        {
            var book = _bookService.GetById(id);
            if (book is null)
            {
                return BadRequest();
            }
            else return Ok(book);
        }

        [HttpPost("AddNewBook")]
        public IActionResult AddNewBook([FromBody] Book book)
        {
            if (_bookService.Add(book))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook([FromBody] Book book)
        {
            if (!_bookValidationService.AddBookValidation(book))
                return BadRequest();
            return _bookService.Update(book) ? Ok() : NotFound();
        }

        [HttpDelete("DeleteBook/{id}")]
        public ActionResult DeleteBook([FromQuery] Guid id)
        {
            return _bookService.Delete(id) ? Ok() : NotFound();
        }
    }
}
