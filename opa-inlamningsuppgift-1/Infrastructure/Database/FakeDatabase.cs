using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class FakeDatabase : IFakeDatabase
    {
        private List<BookModel> bookModelList = new();
        private List<AuthorModel> authorModelList = new();
        private static List<UserModel> userModelList = new();

        public FakeDatabase()
        {
            bookModelList.Add(new BookModel { Id = 1, Title = "The Book of Fun", Description = "Funny" });
            bookModelList.Add(new BookModel { Id = 2, Title = "The Book of Mystery", Description = "Mystery" });
            bookModelList.Add(new BookModel { Id = 3, Title = "The Book of Action", Description = "Action" });
            bookModelList.Add(new BookModel { Id = 4, Title =  "The Book of Fantasy", Description = "Fantasy" });

            authorModelList.Add(new AuthorModel { Id = 1, FirstName =  "John", LastName = "Dough",  Category = "Comedy" } );
            authorModelList.Add(new AuthorModel { Id = 2, FirstName = "Sue", LastName = "Storm", Category = "Mystery" } );
            authorModelList.Add(new AuthorModel { Id = 3, FirstName = "Dick", LastName = "Doncaster", Category = "Action" });
            authorModelList.Add(new AuthorModel { Id = 4, FirstName = "Peter", LastName = "Pele", Category = "Fantasy" });

            userModelList.Add(new UserModel { Id = Guid.NewGuid(), UserName = "Ville" });
            userModelList.Add(new UserModel { Id = Guid.NewGuid(), UserName = "Hans" });
            userModelList.Add(new UserModel { Id = Guid.NewGuid(), UserName = "Tuva" });
        }

        public List<BookModel> GetAllBooks()
        {
            return bookModelList;
        }

        public BookModel GetBookById(int id)
        {
            return bookModelList.FirstOrDefault(book => book.Id == id);
        }

        public void AddBook(BookModel newBook)
        {
            bookModelList.Add(newBook);
        }

        public void DeleteBook(int id)
        {
            var bookToDelete = bookModelList.FirstOrDefault(book => book.Id == id);

            if (bookToDelete != null)
            {
                bookModelList.Remove(bookToDelete);
            }
            else
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
        }

        public void UpdateBook(BookModel updatedBook)
        {
            var bookToUpdate = bookModelList.FirstOrDefault(book => book.Id == updatedBook.Id);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with ID {updatedBook.Id} not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Description))
            {
                throw new ArgumentException("Title and description cannot be empty.");
            }

            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Description = updatedBook.Description;
        }

        public List<AuthorModel> GetAllAuthors()
        {
            return authorModelList;
        }

        public AuthorModel GetAuthorById(int id)
        {
            return authorModelList.FirstOrDefault(author => author.Id == id);
        }

        public void AddAuthor(AuthorModel newAuthor)
        {
            authorModelList.Add(newAuthor);
        }

        public void DeleteAuthor(int id)
        {
            var authorToDelete = bookModelList.FirstOrDefault(author => author.Id == id);

            if (authorToDelete != null)
            {
                bookModelList.Remove(authorToDelete);
            }
            else
            {
                throw new KeyNotFoundException($"Author with ID {id} not found.");
            }
        }

        public void UpdateAuthor(AuthorModel updatedAuthor)
        {
            var authorToUpdate = bookModelList.FirstOrDefault(author => author.Id == updatedAuthor.Id);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {updatedAuthor.Id} not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedAuthor.FirstName) || string.IsNullOrWhiteSpace(updatedAuthor.LastName) || string.IsNullOrWhiteSpace(updatedAuthor.Category))
            {
                throw new ArgumentException("First name, last name and category cannot be empty.");
            }

            authorToUpdate.Title = updatedAuthor.FirstName;
            authorToUpdate.Description = updatedAuthor.LastName;
            authorToUpdate.Description = updatedAuthor.Category;
        }

        public List<UserModel> Users
        {
            get { return userModelList; }
            set { userModelList = value; }
        }
    }
}
