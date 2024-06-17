using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DTO
{
    public class BookView
    {
        public string MaSach { get; set; }
        public string Ten { get; set; }
        public string TheLoai { get; set; }
        public DateTime NgayXuatBan { get; set; }
        public bool TrangThai { get; set; }
    }
}
