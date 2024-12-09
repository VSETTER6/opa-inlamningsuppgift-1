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

        var mockBooksList = new List<BookModel>
        {
            new BookModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1"},
            new BookModel { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAllBooks()).Returns(mockBooksList);

        var handler = new GetAllBooksHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.AreEqual("BookTitle1", result[0].Title);
        Assert.That(result[1].Description, Is.EqualTo("Description2"));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public async Task GetBookByIdHandler_ShouldReturnBook_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var bookId = Guid.NewGuid();
        var mockBooksList = new List<BookModel>
        {
            new BookModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1"},
            new BookModel { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetBookById(It.IsAny<Guid>()))
                    .Returns((Guid id) => mockBooksList.FirstOrDefault(book => book.Id == id));

        var handler = new GetBookByIdHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None);

        // Assert
        var expectedBook = mockBooksList.FirstOrDefault(book => book.Id == id);
        Assert.NotNull(result);
        Assert.That(result, Is.EqualTo(expectedBook));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public void GetBookByIdHandler_ShouldThrowArgumentException_WhenIdIsInvalid(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var handler = new GetBookByIdHandler(mockDatabase.Object);

        var query = new GetBookByIdQuery(id);

        // Act & Assert
        var expectedException = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                                await handler.Handle(query, CancellationToken.None));

        Assert.That(expectedException.Message, Is.EqualTo($"No book found with ID {id}."));
    }

    [Test]
    [TestCase("Valid Title", "Valid Description", true)]
    [TestCase("", "Valid Description", false)]          
    [TestCase("Valid Title", "", false)]                
    [TestCase("", "", false)]                           
    public async Task AddBookHandler_ShouldHandleAddingBook(string title, string description, bool isValid)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockBooksList = new List<BookModel>
    {
        new BookModel { Id = Guid.NewGuid(), Title = "Book 1", Description = "Description 1" },
        new BookModel { Id = Guid.NewGuid(), Title = "Book 2", Description = "Description 2" }
    };

        mockDatabase.Setup(database => database.GetAllBooks()).Returns(mockBooksList);
        mockDatabase.Setup(database => database.AddBook(It.IsAny<BookModel>()))
                    .Callback<BookModel>(book => mockBooksList.Add(book));

        var handler = new AddBookHandler(mockDatabase.Object);
        var command = new AddBookCommand(title, description);

        // Act
        if (isValid)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.Description, Is.EqualTo(description));
            Assert.IsTrue(mockBooksList.Any(book => book.Id == result.Id));
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
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("a3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    public async Task DeleteBookHandler_ShouldRemoveBook_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockBooksList = new List<BookModel>
        {
            new BookModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1" },
            new BookModel { Id = Guid.NewGuid(), Title = "BookTitle2", Description = "Description2" }
        };

        mockDatabase.Setup(database => database.GetAllBooks()).Returns(mockBooksList);
        mockDatabase.Setup(database => database.DeleteBook(It.IsAny<Guid>()))
                    .Callback<Guid>(bookId => mockBooksList.RemoveAll(book => book.Id == bookId));

        var handler = new DeleteBookHandler(mockDatabase.Object);

        var command = new DeleteBookCommand(id);

        // Act & Assert
        if (Guid.TryParse("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", out Guid parsedGuid) && id == parsedGuid)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(1, mockBooksList.Count);
            Assert.IsNull(mockBooksList.FirstOrDefault(book => book.Id == parsedGuid));
        }
        else
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await handler.Handle(command, CancellationToken.None));

            Assert.That(exception.Message, Is.EqualTo($"Book with ID {id} not found."));
        }
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", "Updated Title", "Updated Description")]
    public async Task UpdateBookHandler_ShouldUpdateBook_WhenValidRequestIsGiven(Guid id, string newTitle, string newDescription)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var mockBooksList = new List<BookModel>
        {
            new BookModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "Original Title", Description = "Original Description" },
            new BookModel { Id = Guid.NewGuid(), Title = "Another Title", Description = "Another Description" }
        };

        mockDatabase.Setup(database => database.GetBookById(It.IsAny<Guid>()))
                    .Returns((Guid bookId) => mockBooksList.FirstOrDefault(book => book.Id == bookId));

        mockDatabase.Setup(datebase => datebase.UpdateBook(It.IsAny<BookModel>()))
                    .Callback<BookModel>(updatedBook =>
                    {
                        var bookToUpdate = mockBooksList.FirstOrDefault(book => book.Id == updatedBook.Id);
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
        var updatedBook = mockBooksList.FirstOrDefault(book => book.Id == id);
        Assert.NotNull(result);
        Assert.AreEqual(newTitle, updatedBook.Title);
        Assert.AreEqual(newDescription, updatedBook.Description);
    }
}
