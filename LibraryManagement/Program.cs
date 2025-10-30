using LibraryManagement.Models;
using LibraryManagement.Models.Entities;
using LibraryManagement.Repositories;
using LibraryManagement.Repositories.Interfaces;
using LibraryManagement.Services;
using LibraryManagement.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<IRepository<Author>, InMemoryRepository<Author>>();
builder.Services.AddSingleton<IRepository<Book>, InMemoryRepository<Book>>();

// Register services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();