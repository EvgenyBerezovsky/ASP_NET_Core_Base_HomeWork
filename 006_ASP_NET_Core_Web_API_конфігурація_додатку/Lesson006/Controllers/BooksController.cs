using ASP_NET_Core_Base_HomeWork_Lesson006.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ASP_NET_Core_Base_HomeWork_Lesson006.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBookValidationService _bookValidationService;

        public BooksController(IBookService bookService, IBookValidationService bookValidationService)
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
        public ActionResult AddNewBook([FromBody] Book book)
        {
            if (_bookValidationService.AddBookValidation(book))
            {
                _bookService.Add(book);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("UpdateBook")]
        public ActionResult UpdateBook([FromBody] Book book)
        {
            if (!_bookValidationService.AddBookValidation(book))
                return BadRequest();
            return _bookService.Update(book) ? Ok() : NotFound();
        }

        [HttpPost("DeleteBook/{id}")]
        public ActionResult DeleteBook([FromQuery] Guid id)
        {
            return _bookService.Delete(id) ? Ok() : NotFound();
        }
    }
}
