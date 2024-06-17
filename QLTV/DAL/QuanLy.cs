using System;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DTO
{
    public class QuanLy : DbContext
    {
        public QuanLy()
            : base("name=QLTV")
        {
            Database.SetInitializer(new CreateDB());
        }
        
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
    }
}