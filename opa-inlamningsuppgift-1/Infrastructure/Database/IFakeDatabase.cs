using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public interface IFakeDatabase
    {
        List<BookModel> GetAllBooks();
        BookModel GetBookById(int id);
        void AddBook(BookModel newBook);
        void DeleteBook(int id);

        List<AuthorModel> GetAllAuthors();
    }
}
