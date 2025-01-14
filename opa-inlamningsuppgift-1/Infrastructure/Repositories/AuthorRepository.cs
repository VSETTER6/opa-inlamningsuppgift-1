﻿using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _realDatabase;

        public AuthorRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task AddAuthor(Author author)
        {
            await _realDatabase.Authors.AddAsync(author);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Guid id)
        {
            var author = await _realDatabase.Authors.FindAsync(id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} was not found.");
            }

            _realDatabase.Authors.Remove(author);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _realDatabase.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            var author = await _realDatabase.Authors.FirstOrDefaultAsync(author => author.Id == id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} was not found.");
            }

            return author;
        }

        public async Task UpdateAuthor(Guid id, Author author)
        {
            var existingAuthor = await _realDatabase.Authors.FirstOrDefaultAsync(author => author.Id == id);

            if (existingAuthor == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} was not found.");
            }

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Category = author.Category;

            await _realDatabase.SaveChangesAsync();
        }
    }
}
