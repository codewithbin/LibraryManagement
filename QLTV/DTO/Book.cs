using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DTO
{
    public class Book
    {
        [Key]
        public string Sach_ID { get; set; }
        public string Ten { get; set; }
        public DateTime NgayXuatBan { get; set; }
        public bool TrangThai { get; set; }

        public string TheLoai_ID { get; set; }
        [ForeignKey("TheLoai_ID")]
        public virtual Genre Genre { get; set; }
    }
}
