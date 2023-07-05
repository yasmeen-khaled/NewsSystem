using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL
{
    public class AuthorsRepository : IGenericRepository<Author>
    {
        private readonly SystemContext _context;
        public AuthorsRepository(SystemContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var toBeDeleted = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (toBeDeleted != null)
            {
                _context.Authors.Remove(toBeDeleted);
                Save();
                //return true;
            }
            //return false;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Set<Author>();
        }

        public Author? GetById(int id)
        {
            var requiredAuthor = _context.Authors.FirstOrDefault(a => a.Id == id);
            return requiredAuthor;
        }

        public void Insert(Author obj)
        {
            _context.Authors.Add(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Author obj)
        {
            var toBeUpdated = _context.Authors.FirstOrDefault(a => obj.Id == a.Id);
            if (toBeUpdated != null)
            {
                toBeUpdated.Name = obj.Name;
                Save();
                //return true;
            }
            //return false;
        }
    }
}
