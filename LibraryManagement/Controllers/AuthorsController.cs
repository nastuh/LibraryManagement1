using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        var author = await _authorService.GetByIdAsync(id);
        if (author == null)
            return NotFound($"Author with ID {id} not found");

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto authorDto)
    {
        var author = await _authorService.CreateAsync(authorDto);
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuthorDto>> Update(int id, UpdateAuthorDto authorDto)
    {
        var author = await _authorService.UpdateAsync(id, authorDto);
        if (author == null)
            return NotFound($"Author with ID {id} not found");

        return Ok(author);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _authorService.DeleteAsync(id);
        if (!result)
            return NotFound($"Author with ID {id} not found");

        return NoContent();
    }
}