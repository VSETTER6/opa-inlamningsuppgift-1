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
        }

        public List<BookModel> GetAllBooks()
        {
            return bookModelList;
        }

        public List<AuthorModel> GetAllAuthors()
        {
            return authorModelList;
        }
    }
}
