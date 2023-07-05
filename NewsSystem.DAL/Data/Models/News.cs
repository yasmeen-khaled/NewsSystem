using NewsSystem.DAL.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL
{
    public class News
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string news { get; set; }= string.Empty;
        //TODO:check this
        [Required, WeekValidation] public DateTime PublicationDate { get; set; } = DateTime.Now;
        [Required] public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required] public byte[]? Image { get; set; } = { };

        //navigation properties
        public int AuthorId { get; set; }

        public Author? Author { get; set; }


        //TODO: handle warnings //= string.Empty; , image make sure it is uploaded
    }
}
