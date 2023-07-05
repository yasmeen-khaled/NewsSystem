using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsSystem.DAL;
//using NewsSystem.BL;

namespace NewsSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IGenericRepository<News> _newsRepository;

        public NewsController(IGenericRepository<News> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // GET: NewsController
        [HttpGet]
        public ActionResult<List<News>> GetAll()
        {
            return _newsRepository.GetAll().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<News> GetById(int id)
        {
            var x = _newsRepository.GetById(id);
            if (x == null)
                return BadRequest();
            else
                return Ok(x);
        }
        [HttpPost, Authorize]
        public ActionResult Add(News news)
        {
            _newsRepository.Insert(news);
            return NoContent();
        }
        [HttpDelete, Authorize]
        [Route("{id}")]
        public ActionResult delete(int id)
        {
            _newsRepository.Delete(id);
            return NoContent();
        }
        [HttpPut, Authorize]
        [Route("{id}")]
        public ActionResult Update(News news)
        {
            _newsRepository.Update(news);
            return NoContent();
        }

    }    
}
