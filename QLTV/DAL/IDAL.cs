using QLTV.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV.DAL
{
    interface IDAL
    {
        List<Book> GetBooks(string theloai_id, string book_name);
        List<Genre> GetGenreList();
        bool AddBook(Book book);
        bool DeleteBooks(List<string> IDs);
        bool UpdateBook(Book book);
        Book GetBookByID(string ID);
        List<Book> SearchBook(string input);
        List<string> GetBookProperty();
        bool IsExist(string ID);
    }
}
