using Application.Book.Commands;
using Application.Book.Handlers;
using Application.Book.Queries;
using Domain.Models;
using Infrastructure.Database;
using Moq;
using NUnit.Framework.Internal;

namespace LibraryTests;

public class BookTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetAllBooksHandler_ReturnsAllBooks()
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockBooks = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "BookTitle1", Description = "Description1"},
            new BookModel { Id = 2, Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAllBooks()).Returns(mockBooks);

        var handler = new GetAllBooksHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("BookTitle1", result[0].Title);
        Assert.AreEqual("Description2", result[1].Description);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public async Task GetBookByIdHandler_ShouldReturnBook_IfValidIdIsGiven(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockBooks = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "BookTitle1", Description = "Description1"},
            new BookModel { Id = 2, Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetBookById(It.IsAny<int>()))
                    .Returns((int id) => mockBooks.FirstOrDefault(book => book.Id == id));

        var handler = new GetBookByIdHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None);

        // Assert
        var expectedBook = mockBooks.FirstOrDefault(book => book.Id == id);
        Assert.NotNull(result);
        Assert.AreEqual(expectedBook, result);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    public void GetBookByIdHandler_ShouldThrowArgumentException_WhenIdIsInvalid(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var handler = new GetBookByIdHandler(mockDatabase.Object);

        var query = new GetBookByIdQuery(id);

        // Act & Assert
        var expectedException = Assert.ThrowsAsync<ArgumentException>(async () =>
                        await handler.Handle(query, CancellationToken.None));

        Assert.That(expectedException.Message, Is.EqualTo("ID must be a positive number."));
    }

    [Test]
    [TestCase("Valid Title", "Valid Description", true)] // Giltig inmatning
    [TestCase("", "Valid Description", false)]          // Tom titel
    [TestCase("Valid Title", "", false)]                // Tom beskrivning
    [TestCase("", "", false)]                           // Både titel och beskrivning tomma
    public async Task AddBookHandler_ShouldHandleAddingBook(string title, string description, bool isValid)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var bookModelList = new List<BookModel>
    {
        new BookModel { Id = 1, Title = "Book 1", Description = "Description 1" },
        new BookModel { Id = 2, Title = "Book 2", Description = "Description 2" }
    };

        mockDatabase.Setup(database => database.GetAllBooks()).Returns(bookModelList);
        mockDatabase.Setup(database => database.AddBook(It.IsAny<BookModel>()))
                    .Callback<BookModel>(book => bookModelList.Add(book));

        var handler = new AddBookHandler(mockDatabase.Object);
        var command = new AddBookCommand(title, description);

        // Act
        if (isValid)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(bookModelList.Count, result.Id);
        }
        else
        {
            // Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                await handler.Handle(command, CancellationToken.None));

            Assert.That(exception.Message, Is.EqualTo("Title and description cannot be empty."));
        }
    }

    [Test]
    [TestCase(1)]
    [TestCase(99)]
    public async Task DeleteBookHandler_ShouldRemoveBook_IfValidIdIsGiven(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockBooks = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "BookTitle1", Description = "Description1" },
            new BookModel { Id = 2, Title = "BookTitle2", Description = "Description2" }
        };

        mockDatabase.Setup(database => database.GetAllBooks()).Returns(mockBooks);
        mockDatabase.Setup(database => database.DeleteBook(It.IsAny<int>()))
                    .Callback<int>(bookId => mockBooks.RemoveAll(book => book.Id == bookId));

        var handler = new DeleteBookHandler(mockDatabase.Object);

        var command = new DeleteBookCommand(id);

        // Act & Assert
        if (id == 1)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(1, mockBooks.Count); // Kontrollera att bara en bok är kvar
            Assert.IsNull(mockBooks.FirstOrDefault(book => book.Id == 1)); // Kontrollera att rätt bok togs bort
        }
        else
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await handler.Handle(command, CancellationToken.None));

            Assert.That(exception.Message, Is.EqualTo($"Book with ID {id} not found."));
        }
    }

    [Test]
    [TestCase(1, "Updated Title", "Updated Description")]
    public async Task UpdateBookHandler_ShouldUpdateBook_WhenValidRequestIsGiven(int id, string newTitle, string newDescription)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var mockBooks = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "Original Title", Description = "Original Description" },
            new BookModel { Id = 2, Title = "Another Title", Description = "Another Description" }
        };

        mockDatabase.Setup(database => database.GetBookById(It.IsAny<int>()))
                    .Returns((int bookId) => mockBooks.FirstOrDefault(book => book.Id == bookId));

        mockDatabase.Setup(datebase => datebase.UpdateBook(It.IsAny<BookModel>()))
                    .Callback<BookModel>(updatedBook =>
                    {
                        var bookToUpdate = mockBooks.FirstOrDefault(book => book.Id == updatedBook.Id);
                        if (bookToUpdate != null)
                        {
                            bookToUpdate.Title = updatedBook.Title;
                            bookToUpdate.Description = updatedBook.Description;
                        }
                    });

        var handler = new UpdateBookHandler(mockDatabase.Object);
        var command = new UpdateBookCommand(id, newTitle, newDescription);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedBook = mockBooks.FirstOrDefault(book => book.Id == id);
        Assert.NotNull(result);
        Assert.AreEqual(newTitle, updatedBook.Title);
        Assert.AreEqual(newDescription, updatedBook.Description);
    }

}
