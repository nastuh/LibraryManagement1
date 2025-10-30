using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> CreateAsync(CreateBookDto bookDto);
    Task<BookDto?> UpdateAsync(int id, UpdateBookDto bookDto);
    Task<bool> DeleteAsync(int id);
}