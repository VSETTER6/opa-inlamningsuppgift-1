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
            bookModelList.Add(new BookModel { Id = Guid.NewGuid(), Title = "The Book of Fun", Description = "Funny" });
            bookModelList.Add(new BookModel { Id = Guid.NewGuid(), Title = "The Book of Mystery", Description = "Mystery" });
            bookModelList.Add(new BookModel { Id = Guid.NewGuid(), Title = "The Book of Action", Description = "Action" });
            bookModelList.Add(new BookModel { Id = Guid.NewGuid(), Title =  "The Book of Fantasy", Description = "Fantasy" });

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

        public BookModel GetBookById(Guid id)
        {
            return bookModelList.FirstOrDefault(book => book.Id == id);
        }

        public void AddBook(BookModel newBook)
        {
            bookModelList.Add(newBook);
        }

        public void DeleteBook(Guid id)
        {
            try
            {
                var bookToDelete = bookModelList.FirstOrDefault(book => book.Id == id);

                if (bookToDelete == null)
                {
                    throw new KeyNotFoundException($"Book with ID {id} was not found.");
                }

                bookModelList.Remove(bookToDelete);
                Console.WriteLine($"Book {bookToDelete} was deleted.");
            }
            catch (KeyNotFoundException ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateBook(BookModel updatedBook)
        {
            try
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
                Console.WriteLine($"Book with ID {updatedBook.Id} was updated successfully.");
            }
            catch (Exception ex)
            { 
                if(ex is KeyNotFoundException || ex is ArgumentException)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                else
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
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
            try
            {
                var authorToDelete = authorModelList.FirstOrDefault(author => author.Id == id);

                if (authorToDelete == null)
                {
                    throw new KeyNotFoundException($"Author with ID {id} was not found.");
                }

                authorModelList.Remove(authorToDelete);
                Console.WriteLine($"Author {authorToDelete} was deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateAuthor(AuthorModel updatedAuthor)
        {
            try
            {
                var authorToUpdate = authorModelList.FirstOrDefault(author => author.Id == updatedAuthor.Id);

                if (authorToUpdate == null)
                {
                    throw new KeyNotFoundException($"Author with ID {updatedAuthor.Id} not found.");
                }

                if (string.IsNullOrWhiteSpace(updatedAuthor.FirstName) || string.IsNullOrWhiteSpace(updatedAuthor.LastName) || string.IsNullOrWhiteSpace(updatedAuthor.Category))
                {
                    throw new ArgumentException("None of first name, last name or category can be empty.");
                }

                authorToUpdate.FirstName = updatedAuthor.FirstName;
                authorToUpdate.LastName = updatedAuthor.LastName;
                authorToUpdate.Category = updatedAuthor.Category;
                Console.WriteLine($"Author with ID {updatedAuthor.Id} was updated successfully.");
            } 
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException || ex is ArgumentException)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                else
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
        }

        public List<UserModel> Users
        {
            get { return userModelList; }
            set { userModelList = value; }
        }
    }
}
