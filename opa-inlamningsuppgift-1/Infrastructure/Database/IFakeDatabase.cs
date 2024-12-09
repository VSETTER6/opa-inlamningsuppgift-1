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
        BookModel GetBookById(Guid id);
        void AddBook(BookModel newBook);
        void DeleteBook(Guid id);
        void UpdateBook(BookModel updatedBook);

        List<AuthorModel> GetAllAuthors();
        AuthorModel GetAuthorById(int id);
        void AddAuthor(AuthorModel newAuthor);
        void DeleteAuthor(int id);
        void UpdateAuthor(AuthorModel updatedAuthor);

        List<UserModel> Users { get; set; }
    }
}
