using Application.Book.Handlers;
using Application.Book.Queries;
using Domain.Models;
using Infrastructure.Database;
using Moq;

namespace LibraryTests;

public class BookTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Handle_ReturnsAllBooks()
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
}
