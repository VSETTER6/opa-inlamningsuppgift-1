using Domain.Models;

namespace Infrastructure.Database
{
    public interface IFakeDatabase
    {
        List<Book> GetAllBooks();
        Book GetBookById(Guid id);
        void AddBook(Book newBook);
        void DeleteBook(Guid id);
        void UpdateBook(Book updatedBook);

        List<Author> GetAllAuthors();
        Author GetAuthorById(Guid id);
        void AddAuthor(Author newAuthor);
        void DeleteAuthor(Guid id);
        void UpdateAuthor(Author updatedAuthor);

        List<User> Users { get; set; }
    }
}
