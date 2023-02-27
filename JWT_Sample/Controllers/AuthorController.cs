using JWT_Sample.Models;
using JWT_Sample.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace JWT_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public ActionResult<List<Author>> Get()
        {
            return Ok(_authorRepository.GetAuthor());
        }
        [HttpPost]
        public ActionResult Add(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            _authorRepository.AddAuthor(author);
            return Ok();
        }
        [HttpPut]
        public ActionResult Update(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            _authorRepository.UpdateAuthor(author);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(Author author)
        {
            _authorRepository.DeleteAuthor(author);
            return Ok();
        }
    }
}