using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooks();

        Task<BookModel> GetBookById(Guid id);

        Task<BookModel> AddBook(BookModel book);

        Task<string> DeleteBook(Guid id);

        Task<BookModel> UpdateBook(Guid id, BookModel book);
    }
}
