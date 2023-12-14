using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; init; } = default!;

    [MaxLength(15)]
    public string? Author { get; set; }
}