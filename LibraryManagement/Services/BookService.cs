using LibraryManagement.Models.DTOs;
using LibraryManagement.Models.Entities;
using LibraryManagement.Repositories.Interfaces;
using LibraryManagement.Services.Interfaces;

namespace LibraryManagement.Services;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Author> _authorRepository;

    public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        var authors = await _authorRepository.GetAllAsync();

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId,
            AuthorName = authors.FirstOrDefault(a => a.Id == b.AuthorId)?.Name
        });
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        var author = await _authorRepository.GetByIdAsync(book.AuthorId);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId,
            AuthorName = author?.Name
        };
    }

    public async Task<BookDto> CreateAsync(CreateBookDto bookDto)
    {
        var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
        if (author == null)
            throw new ArgumentException("Author not found");

        if (string.IsNullOrWhiteSpace(bookDto.Title))
            throw new ArgumentException("Title is required");

        var book = new Book
        {
            Title = bookDto.Title.Trim(),
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId
        };

        var createdBook = await _bookRepository.AddAsync(book);

        return new BookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            PublishedYear = createdBook.PublishedYear,
            AuthorId = createdBook.AuthorId,
            AuthorName = author.Name
        };
    }

    public async Task<BookDto?> UpdateAsync(int id, UpdateBookDto bookDto)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
        if (author == null)
            throw new ArgumentException("Author not found");

        if (string.IsNullOrWhiteSpace(bookDto.Title))
            throw new ArgumentException("Title is required");

        book.Title = bookDto.Title.Trim();
        book.PublishedYear = bookDto.PublishedYear;
        book.AuthorId = bookDto.AuthorId;

        var updatedBook = await _bookRepository.UpdateAsync(book);

        return new BookDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            PublishedYear = updatedBook.PublishedYear,
            AuthorId = updatedBook.AuthorId,
            AuthorName = author.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _bookRepository.DeleteAsync(id);
    }
}