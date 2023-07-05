using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL
{
    public class NewsRepository : IGenericRepository<News>
    {
        private readonly SystemContext _context;

        public NewsRepository(SystemContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var toBeDeleted = _context.News.FirstOrDefault(n => n.Id == id);
            if (toBeDeleted != null)
            {
                _context.News.Remove(toBeDeleted);
                Save();
                //return true;
            }
            //return false;
        }

        public IEnumerable<News> GetAll()
        {
            return _context.Set<News>();
        }

        public News? GetById(int id)
        {
            return _context.News.FirstOrDefault(n => n.Id == id);
        }

        public void Insert(News obj)
        {
            _context.News.Add(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(News obj)
        {
            var toBeUpdated = _context.News.FirstOrDefault(n => n.Id == obj.Id);
            if (toBeUpdated != null)
            {
                toBeUpdated.news = obj.news;
                toBeUpdated.Title = obj.Title;
                toBeUpdated.PublicationDate = obj.PublicationDate;
                toBeUpdated.CreationDate = obj.CreationDate;
                toBeUpdated.Image = obj.Image;
                Save();
                //return true;
            }
            //return false;
        }
    }
}
