﻿namespace LibraryManagement.Models.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<Book> Books { get; set; } = new();
}