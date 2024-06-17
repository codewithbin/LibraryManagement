using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DTO
{
    class CreateDB : CreateDatabaseIfNotExists<QuanLy>
    {
        protected override void Seed(QuanLy context)
        {
            context.Books.AddRange(new Book[]
            {
                new Book {Sach_ID = "s1", Ten = "Lập trình .Net", NgayXuatBan = DateTime.Now, TrangThai = true, TheLoai_ID = "1"},
                new Book {Sach_ID = "s2", Ten = "Lập trình Java", NgayXuatBan = DateTime.Now, TrangThai = true, TheLoai_ID = "1"},
                new Book {Sach_ID = "s3", Ten = "Tư tưởng HCM", NgayXuatBan = DateTime.Now, TrangThai = true, TheLoai_ID = "2"},
            });

            context.Genres.AddRange(new Genre[]
            {
                new Genre {TheLoai_ID = "1", Ten = "Lập trình"},
                new Genre {TheLoai_ID = "2", Ten = "Đại cương"}
            });
        }
    }
}
