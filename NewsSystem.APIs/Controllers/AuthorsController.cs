using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsSystem.DAL;

namespace NewsSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericRepository<Author> _authorsRepository;

        public AuthorsController(IGenericRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        // GET: NewsController
        [HttpGet]
        public ActionResult<List<Author>> GetAll()
        {
            return _authorsRepository.GetAll().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Author> GetById(int id)
        {
            var x = _authorsRepository.GetById(id);
            if (x == null)
                return BadRequest();
            else
                return Ok(x);
        }
        [HttpPost]
        public ActionResult Add(Author author)
        {
            _authorsRepository.Insert(author);
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult delete(int id)
        {
            _authorsRepository.Delete(id);
            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id , Author author)
        {
            if (id != author.Id) return BadRequest();
            _authorsRepository.Update(author);
            return NoContent();
        }
    }
}
