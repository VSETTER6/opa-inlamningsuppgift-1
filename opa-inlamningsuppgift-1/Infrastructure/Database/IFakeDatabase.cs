using Domain.Models;

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
        AuthorModel GetAuthorById(Guid id);
        void AddAuthor(AuthorModel newAuthor);
        void DeleteAuthor(Guid id);
        void UpdateAuthor(AuthorModel updatedAuthor);

        List<UserModel> Users { get; set; }
    }
}
