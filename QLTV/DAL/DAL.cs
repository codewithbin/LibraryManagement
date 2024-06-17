using QLTV.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DAL
{
    class DAL : IDAL
    {
        private QuanLy db = new QuanLy();

        private static DAL _Instance;
        public static DAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL();
                }
                return _Instance;
            }
            private set { }
        }

        private DAL() { }
        public bool AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return true;
        }

        public bool DeleteBooks(List<string> IDs)
        {
            foreach (var ID in IDs)
            {
                var book = db.Books.FirstOrDefault(p => p.Sach_ID == ID);
                if (book == null)
                    return false;

                db.Books.Remove(book);
            }

            db.SaveChanges();
            return true;
        }

        public Book GetBookByID(string ID)
        {
            return db.Books.FirstOrDefault(p => p.Sach_ID == ID);
        }

        public List<string> GetBookProperty()
        {
            Book book = new Book();
            return book.GetType().GetProperties().Select(p => p.Name).ToList();
        }

        public List<Genre> GetGenreList()
        {
            return db.Genres.ToList();
        }

        public bool IsExist(string ID)
        {
            var book = db.Books.FirstOrDefault(p => p.Sach_ID == ID);
            if (book == null)
                return true;
            return false;
        }

        public bool UpdateBook(Book book)
        {
            var s = db.Books.FirstOrDefault(p => p.Sach_ID == book.Sach_ID);
            if (s == null)
                return false;

            s.Ten = book.Ten;
            s.NgayXuatBan = book.NgayXuatBan;
            s.TrangThai = book.TrangThai;
            s.TheLoai_ID = book.TheLoai_ID;

            db.SaveChanges();
            return true;
        }

        public List<Book> GetBooks(string theloai_id, string book_name)
        {
            List<Book> books = new List<Book>();

            if (theloai_id == "0")
            {
                if (string.IsNullOrEmpty(theloai_id))
                {
                    books = db.Books.ToList();
                }
                else
                {
                    books = db.Books.Where(p => p.Ten.Contains(book_name)).ToList();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(theloai_id))
                {
                    books = db.Books.Where(p => p.TheLoai_ID == theloai_id).ToList();
                }
                else
                {
                    books = db.Books.Where(p => p.TheLoai_ID == theloai_id && p.Ten.Contains(book_name)).ToList();
                }
            }

            return books;
        }

        public List<Book> SearchBook(string input)
        {
            return db.Books.Where(p => p.Ten == input).ToList();
        }

    }
}
