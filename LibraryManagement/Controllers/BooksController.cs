using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound($"Book with ID {id} not found");

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(CreateBookDto bookDto)
    {
        try
        {
            var book = await _bookService.CreateAsync(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookDto>> Update(int id, UpdateBookDto bookDto)
    {
        try
        {
            var book = await _bookService.UpdateAsync(id, bookDto);
            if (book == null)
                return NotFound($"Book with ID {id} not found");

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _bookService.DeleteAsync(id);
        if (!result)
            return NotFound($"Book with ID {id} not found");

        return NoContent();
    }
}