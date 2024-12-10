//using Application.Author.Commands;
//using Application.Author.Handlers;
//using Application.Author.Queries;
//using Domain.Models;
//using Infrastructure.Database;
//using Moq;

//namespace LibraryTests;

//public class AuthorTests
//{
//    [SetUp]
//    public void Setup()
//    {
//    }

//    [Test]
//    public async Task GetAllAuthorsHandler_ReturnsAllAuthors()
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();

//        var mockAuthorsList = new List<AuthorModel>
//        {
//            new AuthorModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
//            new AuthorModel { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
//        };

//        mockDatabase.Setup(datebase => datebase.GetAllAuthors()).Returns(mockAuthorsList);

//        var handler = new GetAllAuthorsHandler(mockDatabase.Object);

//        // Act
//        var result = await handler.Handle(new GetAllAuthorsQuery(), CancellationToken.None);

//        // Assert
//        Assert.NotNull(result);
//        Assert.That(result.Count, Is.EqualTo(2));
//        Assert.That(result[0].FirstName, Is.EqualTo("FirstName1"));
//        Assert.That(result[1].LastName, Is.EqualTo("LastName2"));
//        Assert.That(result[1].Category, Is.EqualTo("Category2"));
//    }

//    [Test]
//    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
//    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
//    public async Task GetAuthorByIdHandler_ShouldReturnAuthor_IfValidIdIsGiven(Guid id)
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();

//        var mockAuthorsList = new List<AuthorModel>
//        {
//            new AuthorModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
//            new AuthorModel { Id = new Guid("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295"), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
//        };

//        mockDatabase.Setup(datebase => datebase.GetAuthorById(It.IsAny<Guid>()))
//                    .Returns((Guid id) => mockAuthorsList.FirstOrDefault(author => author.Id == id));

//        var handler = new GetAuthorByIdHandler(mockDatabase.Object);

//        // Act
//        var result = await handler.Handle(new GetAuthorByIdQuery(id), CancellationToken.None);

//        // Assert
//        var expectedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);
//        Assert.NotNull(result);
//        Assert.That(result, Is.EqualTo(expectedAuthor));
//    }

//    [Test]
//    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
//    [TestCase("b44c7c5d-b0bb-4d8c-bbf9-779d1c7c1295")]
//    public void GetAuthorByIdHandler_ShouldThrowArgumentException_WhenIdIsInvalid(Guid id)
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();
//        var handler = new GetAuthorByIdHandler(mockDatabase.Object);

//        var query = new GetAuthorByIdQuery(id);

//        // Act & Assert
//        var expectedException = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
//                        await handler.Handle(query, CancellationToken.None));

//        Assert.That(expectedException.Message, Is.EqualTo($"No author found with ID {id}."));
//    }

//    [Test]
//    [TestCase("FirstName1", "LastName1", "Category1", true)]
//    [TestCase("", "Valid Description", "", false)]
//    [TestCase("Valid Title", "", "", false)]
//    [TestCase("", "", "", false)]
//    public async Task AddAuthorHandler_ShouldHandleAddingAuthor(string firstName, string lastName, string category, bool isValid)
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();

//        var mockAuthorsList = new List<AuthorModel>
//        {
//            new AuthorModel { Id = Guid.NewGuid(), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
//            new AuthorModel { Id = Guid.NewGuid(), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
//        };

//        mockDatabase.Setup(database => database.GetAllAuthors()).Returns(mockAuthorsList);
//        mockDatabase.Setup(database => database.AddAuthor(It.IsAny<AuthorModel>()))
//                    .Callback<AuthorModel>(author => mockAuthorsList.Add(author));

//        var handler = new AddAuthorHandler(mockDatabase.Object);
//        var command = new AddAuthorCommand(firstName, lastName, category);

//        // Act
//        if (isValid)
//        {
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.FirstName, Is.EqualTo(firstName));
//            Assert.That(result.LastName, Is.EqualTo(lastName));
//            Assert.That(result.Category, Is.EqualTo(category));
//            Assert.IsTrue(mockAuthorsList.Any(author => author.Id == result.Id));
//        }
//        else
//        {
//            // Assert
//            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await handler.Handle(command, CancellationToken.None));

//            Assert.That(exception.Message, Is.EqualTo("First name, last name and category cannot be empty."));
//        }
//    }

//    [Test]
//    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
//    [TestCase("a3c85b8e-0d7b-4f5a-9638-df4b7d720c3f")]
//    public async Task DeleteAuthorHandler_ShouldRemoveAuthor_IfValidIdIsGiven(Guid id)
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();

//        var mockAuthorsList = new List<AuthorModel>
//    {
//        new AuthorModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "FirstName1", LastName = "LastName1", Category = "Category1"},
//        new AuthorModel { Id = Guid.NewGuid(), FirstName = "FirstName2", LastName = "LastName2", Category = "Category2"}
//    };

//        mockDatabase.Setup(database => database.GetAllAuthors()).Returns(mockAuthorsList);
//        mockDatabase.Setup(database => database.DeleteAuthor(It.IsAny<Guid>()))
//                    .Callback<Guid>(authorId => mockAuthorsList.RemoveAll(author => author.Id == authorId));

//        var handler = new DeleteAuthorHandler(mockDatabase.Object);

//        var command = new DeleteAuthorCommand(id);

//        // Act & Assert
//        if (Guid.TryParse("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", out Guid parsedGuid) && id == parsedGuid)
//        {
//            var result = await handler.Handle(command, CancellationToken.None);

//            Assert.IsTrue(result);
//            Assert.That(mockAuthorsList.Count, Is.EqualTo(1));
//            Assert.IsNull(mockAuthorsList.FirstOrDefault(author => author.Id == parsedGuid));
//        }
//        else
//        {
//            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
//                await handler.Handle(command, CancellationToken.None));

//            Assert.That(exception.Message, Is.EqualTo($"Author with ID {id} not found."));
//        }
//    }

//    [Test]
//    [TestCase("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f", "Updated FirstName", "Updated LastName", "Updated Category")]
//    public async Task UpdateAuthorHandler_ShouldUpdateAuthor_WhenValidRequestIsGiven(Guid id, string newFirstName, string newLastName, string newCategory)
//    {
//        // Arrange
//        var mockDatabase = new Mock<IFakeDatabase>();
//        var mockAuthorsList = new List<AuthorModel>
//        {
//            new AuthorModel { Id = new Guid("d3c85b8e-0d7b-4f5a-9638-df4b7d720c3f"), FirstName = "Original FirstName", LastName = "Original LastName", Category = "Orginal Category" },
//            new AuthorModel { Id = Guid.NewGuid(), FirstName = "Another FirstName", LastName = "Another LastName", Category = "Another Category" }
//        };

//        mockDatabase.Setup(database => database.GetAuthorById(It.IsAny<Guid>()))
//                    .Returns((Guid authorId) => mockAuthorsList.FirstOrDefault(author => author.Id == authorId));

//        mockDatabase.Setup(datebase => datebase.UpdateAuthor(It.IsAny<AuthorModel>()))
//                    .Callback<AuthorModel>(updatedAuthor =>
//                    {
//                        var authorToUpdate = mockAuthorsList.FirstOrDefault(author => author.Id == updatedAuthor.Id);
//                        if (authorToUpdate != null)
//                        {
//                            authorToUpdate.FirstName = updatedAuthor.FirstName;
//                            authorToUpdate.LastName = updatedAuthor.LastName;
//                            authorToUpdate.Category = updatedAuthor.Category;
//                        }
//                    });

//        var handler = new UpdateAuthorHandler(mockDatabase.Object);
//        var command = new UpdateAuthorCommand(id, newFirstName, newLastName, newCategory);

//        // Act
//        var result = await handler.Handle(command, CancellationToken.None);

//        // Assert
//        var updatedAuthor = mockAuthorsList.FirstOrDefault(author => author.Id == id);
//        Assert.NotNull(result);
//        Assert.That(updatedAuthor.FirstName, Is.EqualTo(newFirstName));
//        Assert.That(updatedAuthor.LastName, Is.EqualTo(newLastName));
//        Assert.That(updatedAuthor.Category, Is.EqualTo(newCategory));
//    }
//}
