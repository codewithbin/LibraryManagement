using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DTO
{
    public class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public string TheLoai_ID { get; set; }
        public string Ten { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
