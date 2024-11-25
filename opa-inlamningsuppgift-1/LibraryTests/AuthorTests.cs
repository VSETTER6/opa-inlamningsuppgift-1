using Application.Author.Commands;
using Application.Author.Handlers;
using Application.Author.Queries;
using Domain.Models;
using Infrastructure.Database;
using Moq;

namespace LibraryTests;

public class AuthorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetAllAuthorsHandler_ReturnsAllAuthors()
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockAuthorsList = new List<AuthorModel>
        {
            new AuthorModel { Id = 1, FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new AuthorModel { Id = 2, FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAllAuthors()).Returns(mockAuthorsList);

        var handler = new GetAllAuthorsHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("FirstName1", result[0].FirstName);
        Assert.AreEqual("LastName2", result[1].LastName);
        Assert.AreEqual("Category2", result[1].Category);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public async Task GetAuthorByIdHandler_ShouldReturnAuthor_IfValidIdIsGiven(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockAuthorsList = new List<AuthorModel>
        {
            new AuthorModel { Id = 1, FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new AuthorModel { Id = 2, FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAuthorById(It.IsAny<int>()))
                    .Returns((int id) => mockAuthorsList.FirstOrDefault(author => author.Id == id));

        var handler = new GetAuthorByIdHandler(mockDatabase.Object);

        // Act
        var result = await handler.Handle(new GetAuthorByIdQuery(id), CancellationToken.None);

        // Assert
        var expectedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);
        Assert.NotNull(result);
        Assert.AreEqual(expectedAuthor, result);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    public void GetAuthorByIdHandler_ShouldThrowArgumentException_WhenIdIsInvalid(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var handler = new GetAuthorByIdHandler(mockDatabase.Object);

        var query = new GetAuthorByIdQuery(id);

        // Act & Assert
        var expectedException = Assert.ThrowsAsync<ArgumentException>(async () =>
                        await handler.Handle(query, CancellationToken.None));

        Assert.That(expectedException.Message, Is.EqualTo("ID must be a positive number."));
    }

    [Test]
    [TestCase("FirstName1", "LastName1", "Category1", true)]
    [TestCase("", "Valid Description", "", false)]
    [TestCase("Valid Title", "", "", false)]
    [TestCase("", "", "", false)]
    public async Task AddAuthorHandler_ShouldHandleAddingAuthor(string firstName, string lastName, string category, bool isValid)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockAuthorsList = new List<AuthorModel>
        {
            new AuthorModel { Id = 1, FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new AuthorModel { Id = 2, FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(database => database.GetAllAuthors()).Returns(mockAuthorsList);
        mockDatabase.Setup(database => database.AddAuthor(It.IsAny<AuthorModel>()))
                    .Callback<AuthorModel>(author => mockAuthorsList.Add(author));

        var handler = new AddAuthorHandler(mockDatabase.Object);
        var command = new AddAuthorCommand(firstName, lastName, category);

        // Act
        if (isValid)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(firstName, result.FirstName);
            Assert.AreEqual(lastName, result.LastName);
            Assert.AreEqual(category, result.Category);
            Assert.AreEqual(mockAuthorsList.Count, result.Id);
        }
        else
        {
            // Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                await handler.Handle(command, CancellationToken.None));

            Assert.That(exception.Message, Is.EqualTo("First name, last name and category cannot be empty."));
        }
    }

    [Test]
    [TestCase(1)]
    [TestCase(99)]
    public async Task DeleteAuthorHandler_ShouldRemoveAuthor_IfValidIdIsGiven(int id)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();

        var mockAuthorsList = new List<AuthorModel>
    {
        new AuthorModel { Id = 1, FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
        new AuthorModel { Id = 2, FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
    };

        mockDatabase.Setup(database => database.GetAllAuthors()).Returns(mockAuthorsList);
        mockDatabase.Setup(database => database.DeleteAuthor(It.IsAny<int>()))
                    .Callback<int>(authorId => mockAuthorsList.RemoveAll(author => author.Id == authorId));

        var handler = new DeleteAuthorHandler(mockDatabase.Object);

        var command = new DeleteAuthorCommand(id);

        // Act & Assert
        if (id == 1)
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual(1, mockAuthorsList.Count);
            Assert.IsNull(mockAuthorsList.FirstOrDefault(author => author.Id == 1));
        }
        else
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await handler.Handle(command, CancellationToken.None));

            Assert.That(exception.Message, Is.EqualTo($"Author with ID {id} not found."));
        }
    }

    [Test]
    [TestCase(1, "Updated FirstName", "Updated LastName", "Updated Category")]
    public async Task UpdateAuthorHandler_ShouldUpdateAuthor_WhenValidRequestIsGiven(int id, string newFirstName, string newLastName, string newCategory)
    {
        // Arrange
        var mockDatabase = new Mock<IFakeDatabase>();
        var mockAuthorsList = new List<AuthorModel>
        {
            new AuthorModel { Id = 1, FirstName = "Original FirstName", LastName = "Original LastName", Category = "Orginal Category" },
            new AuthorModel { Id = 2, FirstName = "Another FirstName", LastName = "Another LastName", Category = "Another Category" }
        };

        mockDatabase.Setup(database => database.GetAuthorById(It.IsAny<int>()))
                    .Returns((int authorId) => mockAuthorsList.FirstOrDefault(author => author.Id == authorId));

        mockDatabase.Setup(datebase => datebase.UpdateAuthor(It.IsAny<AuthorModel>()))
                    .Callback<AuthorModel>(updatedAuthor =>
                    {
                        var authorToUpdate = mockAuthorsList.FirstOrDefault(author => author.Id == updatedAuthor.Id);
                        if (authorToUpdate != null)
                        {
                            authorToUpdate.FirstName = updatedAuthor.FirstName;
                            authorToUpdate.LastName = updatedAuthor.LastName;
                            authorToUpdate.Category = updatedAuthor.Category;
                        }
                    });

        var handler = new UpdateAuthorHandler(mockDatabase.Object);
        var command = new UpdateAuthorCommand(id, newFirstName, newLastName, newCategory);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);
        Assert.NotNull(result);
        Assert.AreEqual(newFirstName, updatedAuthor.FirstName);
        Assert.AreEqual(newLastName, updatedAuthor.LastName);
        Assert.AreEqual(newCategory, updatedAuthor.Category);
    }
}
