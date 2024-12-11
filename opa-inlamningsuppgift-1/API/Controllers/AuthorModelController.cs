﻿using Application.Author.Commands;
using Application.Author.Queries;
using Azure.Core;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorModelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorModelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(authors);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while getting the authors. {ex}");
            }
        }

        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _mediator.Send(new GetAuthorByIdQuery(id));
                return Ok(author);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while getting the author. {ex}");
            }
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetAuthorById), new {id = result.Id}, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while adding the author. {ex}");
            }
        }

        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok($"Author with ID {id} has been successfully updated.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while updating the author. {ex}");
            }
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteAuthorCommand(id));

                return Ok($"Author has been successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while deleting the author. {ex}");
            }
        }
    }
}
