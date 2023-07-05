using NewsSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.BL
{
    public class NewsManager : INewsManager
    {
        private readonly IGenericRepository<News> _NewsRepo;

        public NewsManager(IGenericRepository<News> newsRepo)
        {
            _NewsRepo = newsRepo;
        }
        public IEnumerable<NewsReadDto> GetAll()
        {
            var newsFromDb = _NewsRepo.GetAll();
            return newsFromDb.Select(n => new NewsReadDto
            (
                Id: n.Id,
                Title:n.Title
            ));
        }
    }
}
