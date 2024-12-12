using Application.Authors.Commands;
using Application.Authors.Handlers;
using Application.Authors.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Azure.Core;
using Domain.Models;
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
        var mockDatabase = new Mock<IAuthorRepository>();

        List<Author> mockAuthorsList = new List<Author>
        {
            new Author { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new Author { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAllAuthors()).ReturnsAsync(mockAuthorsList);

        // Act
        var handler = new GetAllAuthorsHandler(mockDatabase.Object);
        var result = await handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Data.Count, Is.EqualTo(2));
        Assert.That(result.Data[0].FirstName, Is.EqualTo("FirstName1"));
        Assert.That(result.Data[1].LastName, Is.EqualTo("LastName2"));
        Assert.That(result.Data[1].Category, Is.EqualTo("Category2"));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public async Task GetAuthorByIdHandler_ShouldReturnAuthor_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IAuthorRepository>();

        var mockAuthorsList = new List<Author>
        {
            new Author { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new Author { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(datebase => datebase.GetAuthorById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid id) => mockAuthorsList.FirstOrDefault(author => author.Id == id));

        // Act
        var handler = new GetAuthorByIdHandler(mockDatabase.Object);
        var result = await handler.Handle(new GetAuthorByIdQuery(id), CancellationToken.None);
        var expectedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Data, Is.EqualTo(expectedAuthor));
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
    public async Task GetAuthorByIdHandler_ShouldReturnErrorMessage_WhenIdIsInvalid(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IAuthorRepository>();
        var handler = new GetAuthorByIdHandler(mockDatabase.Object);
        var query = new GetAuthorByIdQuery(id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.IsSuccessful, Is.False);
        Assert.That(result.ErrorMessage, Is.EqualTo($"Author with ID {id} was not found."));
    }

    [Test]
    [TestCase("FirstName1", "LastName1", "Category1", true)]
    [TestCase("", "Valid Description", "", false)]
    [TestCase("Valid Title", "", "", false)]
    [TestCase("", "", "", false)]
    public async Task AddAuthorHandler_ShouldHandleAddingAuthor(string firstName, string lastName, string category, bool isSucessful)
    {
        // Arrange
        var mockDatabase = new Mock<IAuthorRepository>();

        var mockAuthorsList = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(database => database.GetAllAuthors()).ReturnsAsync(mockAuthorsList);
        mockDatabase.Setup(database => database.AddAuthor(It.IsAny<Author>()))
                    .Callback<Author>(author => mockAuthorsList.Add(author));
        
        // Act
        var handler = new AddAuthorHandler(mockDatabase.Object);
        var command = new AddAuthorCommand(firstName, lastName, category);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        if (isSucessful)
        {
            Assert.NotNull(result);
            Assert.That(result.Data.FirstName, Is.EqualTo(firstName));
            Assert.That(result.Data.LastName, Is.EqualTo(lastName));
            Assert.That(result.Data.Category, Is.EqualTo(category));
            Assert.IsTrue(mockAuthorsList.Any(author => author.Id == result.Data.Id));
        }
        else
        {
            Assert.NotNull(result);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("None of first name, last name or category can be empty."));
        }
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    [TestCase("a3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
    public async Task DeleteAuthorHandler_ShouldRemoveAuthor_IfValidIdIsGiven(Guid id)
    {
        // Arrange
        var mockDatabase = new Mock<IAuthorRepository>();

        var mockAuthorsList = new List<Author>
        {
            new Author { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
        };

        mockDatabase.Setup(database => database.GetAuthorById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid authorId) => mockAuthorsList.FirstOrDefault(author => author.Id == authorId));
        mockDatabase.Setup(database => database.DeleteAuthor(It.IsAny<Guid>()))
                    .Callback<Guid>(authorId => mockAuthorsList.RemoveAll(author => author.Id == authorId));

        // Act
        var handler = new DeleteAuthorHandler(mockDatabase.Object);
        var command = new DeleteAuthorCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        if (Guid.TryParse("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", out Guid parsedGuid) && id == parsedGuid)
        {
            Assert.That(mockAuthorsList.Count, Is.EqualTo(1));
            Assert.IsNull(mockAuthorsList.FirstOrDefault(author => author.Id == parsedGuid));
        }
        else
        {
            Assert.NotNull(result);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo($"Author with ID {id} was not found."));
        }
    }

    [Test]
    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", "Updated FirstName", "Updated LastName", "Updated Category")]
    public async Task UpdateAuthorHandler_ShouldUpdateAuthor_WhenValidRequestIsGiven(Guid id, string newFirstName, string newLastName, string newCategory)
    {
        // Arrange
        var mockDatabase = new Mock<IAuthorRepository>();
        var mockAuthorsList = new List<Author>
        {
            new Author { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "Original FirstName", LastName = "Original LastName", Category = "Orginal Category" },
            new Author { Id = Guid.NewGuid(), FirstName = "Another FirstName", LastName = "Another LastName", Category = "Another Category" }
        };

        mockDatabase.Setup(database => database.GetAuthorById(It.IsAny<Guid>()))
                    .ReturnsAsync((Guid authorId) => mockAuthorsList.FirstOrDefault(author => author.Id == authorId));

        mockDatabase.Setup(datebase => datebase.UpdateAuthor(It.IsAny<Guid>(), It.IsAny<Author>()))
                    .Callback<Guid, Author>((authorId, updatedAuthor) =>
                    {
                        var authorToUpdate = mockAuthorsList.FirstOrDefault(author => author.Id == authorId);
                        if (authorToUpdate != null)
                        {
                            authorToUpdate.FirstName = updatedAuthor.FirstName;
                            authorToUpdate.LastName = updatedAuthor.LastName;
                            authorToUpdate.Category = updatedAuthor.Category;
                        }
                    });

        // Act
        var handler = new UpdateAuthorHandler(mockDatabase.Object);
        var command = new UpdateAuthorCommand(id, newFirstName, newLastName, newCategory);
        var result = await handler.Handle(command, CancellationToken.None);
        var updatedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);

        // Assert
        Assert.NotNull(result);
        Assert.That(updatedAuthor.FirstName, Is.EqualTo(newFirstName));
        Assert.That(updatedAuthor.LastName, Is.EqualTo(newLastName));
        Assert.That(updatedAuthor.Category, Is.EqualTo(newCategory));
    }
}
