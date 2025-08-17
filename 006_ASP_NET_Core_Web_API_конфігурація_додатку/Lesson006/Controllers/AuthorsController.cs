using ASP_NET_Core_Base_HomeWork_Lesson006.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_Base_HomeWork_Lesson006.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("AddNewAuthor")]
        public ActionResult AddNewAuthor([FromBody] string author)
        {
            if (_authorService.IsAuthorExist(author))
                return BadRequest();

            _authorService.Add(author);
            return Ok();
        }

        [HttpPost("DeleteAuthor")]
        public ActionResult DeleteAuthor([FromBody] string author)
        {
            if (!_authorService.IsAuthorExist(author))
                return BadRequest();

            _authorService.Delete(author);
            return Ok();
        }
    }
}
