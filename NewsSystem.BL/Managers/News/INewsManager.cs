using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.BL
{
    public interface INewsManager
    {
        IEnumerable<NewsReadDto> GetAll();
    }
}
