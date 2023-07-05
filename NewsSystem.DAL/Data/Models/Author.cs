using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL
{
    public class Author
    {
        public int Id { get; set; }
        [MinLength(3),MaxLength(20)]public string Name { get; set; } = string.Empty;

        //navigation properties
        public ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
