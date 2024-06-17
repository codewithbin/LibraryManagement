using QLTV.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.BLL
{
    class BLL
    {
        public static BLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static BLL _Instance;
        private BLL()
        {

        }
        public List<BookView> GetBooks(string theloai_id, string book_name)
        {
            List<BookView> bookViewModels = new List<BookView>();
            var books = DAL.DAL.Instance.GetBooks(theloai_id, book_name);
            foreach (var s in books)
            {
                bookViewModels.Add(new BookView
                {
                    MaSach = s.Sach_ID,
                    Ten = s.Ten,
                    TheLoai = s.Genre.Ten,
                    NgayXuatBan = s.NgayXuatBan,
                    TrangThai = s.TrangThai
                });
            }
            return bookViewModels;
        }
        public List<Genre> GetGenreList()
        {
            return DAL.DAL.Instance.GetGenreList();
        }
        public List<string> GetBookProperty()
        {
            return DAL.DAL.Instance.GetBookProperty();
        }
        public Book GetBookByID(string ID)
        {
            return DAL.DAL.Instance.GetBookByID(ID);
        }
        public List<Book> SearchBook(string input)
        {
            return DAL.DAL.Instance.SearchBook(input);
        }
        public bool AddBook(Book book)
        {
            return DAL.DAL.Instance.AddBook(book);
        }
        public bool UpdateBook(Book book)
        {
            return DAL.DAL.Instance.UpdateBook(book);
        }
        public bool DeleteBooks(List<string> IDs)
        {
            return DAL.DAL.Instance.DeleteBooks(IDs);
        }
        public bool IsExist(string ID)
        {
            return DAL.DAL.Instance.IsExist(ID);
        }
        public List<BookView> Sort(List<BookView> books)
        {
            List<BookView> sortedList = new List<BookView>();
            sortedList = books.OrderBy(p => p.Ten).ToList();

            return sortedList;
        }
    }
}
