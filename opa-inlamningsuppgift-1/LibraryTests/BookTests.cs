using Application.Books.Commands;
using Application.Books.Handlers;
using Application.Books.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
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
        var mockDatabase = new Mock<IBookRepository>();

        var mockBooksList = new List<Book>
        {
            new Book { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1"},
            new Book { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAllBooks()).ReturnsAsync(mockBooksList);

        // Act
        var handler = new GetAllBooksHandler(mockDatabase.Object);
        var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Data.Count, Is.EqualTo(2));
        Assert.That(result.Data[0].Title, Is.EqualTo("BookTitle1"));
        Assert.That(result.Data[1].Description, Is.EqualTo("Description2"));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public async Task GetBookByIdHandler_ShouldReturnBook_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IBookRepository>();

        var bookId = Guid.NewGuid();
        var mockBooksList = new List<Book>
        {
            new Book { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1"},
            new Book { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), Title = "BookTitle2", Description = "Description2"}
        };

        mockDatabase.Setup(datebase => datebase.GetBookById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid id) => mockBooksList.FirstOrDefault(book => book.Id == id));

        // Act
        var handler = new GetBookByIdHandler(mockDatabase.Object);
        var result = await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None);
        var expectedBook = mockBooksList.FirstOrDefault(book => book.Id == id);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Data, Is.EqualTo(expectedBook));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public async Task GetBookByIdHandler_ShouldReturnErrorMessage_WhenIdIsInvalidAsync(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IBookRepository>();
        var handler = new GetBookByIdHandler(mockDatabase.Object);
        var query = new GetBookByIdQuery(id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.That(result.IsSuccessful, Is.False);
        Assert.That(result.ErrorMessage, Is.EqualTo($"Book with ID {id} was not found."));
    }

    [Test]
    [TestCase("Valid Title", "Valid Description", true)]
    [TestCase("", "Valid Description", false)]
    [TestCase("Valid Title", "", false)]
    [TestCase("", "", false)]
    public async Task AddBookHandler_ShouldHandleAddingBook(string title, string description, bool isSuccessful)
    {
        // Arrange
        var mockDatabase = new Mock<IBookRepository>();

        var mockBooksList = new List<Book>
    {
        new Book { Id = Guid.NewGuid(), Title = "Book 1", Description = "Description 1" },
        new Book { Id = Guid.NewGuid(), Title = "Book 2", Description = "Description 2" }
    };

        mockDatabase.Setup(database => database.GetAllBooks()).ReturnsAsync(mockBooksList);
        mockDatabase.Setup(database => database.AddBook(It.IsAny<Book>()))
                    .Callback<Book>(book => mockBooksList.Add(book));

        // Act
        var handler = new AddBookHandler(mockDatabase.Object);
        var command = new AddBookCommand(title, description);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        if (isSuccessful)
        {
            Assert.NotNull(result);
            Assert.That(result.Data.Title, Is.EqualTo(title));
            Assert.That(result.Data.Description, Is.EqualTo(description));
            Assert.IsTrue(mockBooksList.Any(book => book.Id == result.Data.Id));
        }
        else
        {
            Assert.NotNull(result);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("None of title or description can be empty."));
        }
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("a3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    public async Task DeleteBookHandler_ShouldRemoveBook_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IBookRepository>();

        var mockBooksList = new List<Book>
        {
            new Book { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "BookTitle1", Description = "Description1" },
            new Book { Id = Guid.NewGuid(), Title = "BookTitle2", Description = "Description2" }
        };

        mockDatabase.Setup(database => database.GetBookById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid bookId) => mockBooksList.FirstOrDefault(book => book.Id == bookId));
        mockDatabase.Setup(database => database.DeleteBook(It.IsAny<Guid>()))
                    .Callback<Guid>(bookId => mockBooksList.RemoveAll(book => book.Id == bookId));

        // Act
        var handler = new DeleteBookHandler(mockDatabase.Object);
        var command = new DeleteBookCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        if (Guid.TryParse("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", out Guid parsedGuid) && id == parsedGuid)
        {
            Assert.AreEqual(1, mockBooksList.Count);
            Assert.IsNull(mockBooksList.FirstOrDefault(book => book.Id == parsedGuid));
        }
        else
        {


            Assert.NotNull(result);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo($"Book with ID {id} was not found."));
        }
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", "Updated Title", "Updated Description")]
    public async Task UpdateBookHandler_ShouldUpdateBook_WhenValidRequestIsGiven(Guid id, string newTitle, string newDescription)
    {
        // Arrange
        var mockDatabase = new Mock<IBookRepository>();
        var mockBooksList = new List<Book>
        {
            new Book { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), Title = "Original Title", Description = "Original Description" },
            new Book { Id = Guid.NewGuid(), Title = "Another Title", Description = "Another Description" }
        };

        mockDatabase.Setup(database => database.GetBookById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid bookId) => mockBooksList.FirstOrDefault(book => book.Id == bookId));

        mockDatabase.Setup(datebase => datebase.UpdateBook(It.IsAny<Guid>(), It.IsAny<Book>()))
                    .Callback<Guid, Book>((bookId, updatedBook) =>
                    {
                        var bookToUpdate = mockBooksList.FirstOrDefault(book => book.Id == bookId);
                        if (bookToUpdate != null)
                        {
                            bookToUpdate.Title = updatedBook.Title;
                            bookToUpdate.Description = updatedBook.Description;
                        }
                    });

        // Act
        var handler = new UpdateBookHandler(mockDatabase.Object);
        var command = new UpdateBookCommand(id, newTitle, newDescription);
        var result = await handler.Handle(command, CancellationToken.None);
        var updatedBook = mockBooksList.FirstOrDefault(book => book.Id == id);

        // Assert
        Assert.NotNull(result);
        Assert.That(updatedBook.Title, Is.EqualTo(newTitle));
        Assert.That(updatedBook.Description, Is.EqualTo(newDescription));
    }
}
