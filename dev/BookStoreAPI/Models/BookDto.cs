using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; init; } = default!;

    [MaxLength(15)]
    public string? Author { get; set; }

    public string? Genre {get; set;}

    public bool IsAvailable { get; set; } = true; // Ajout de la propriété IsAvailable pour savoir si un livre est disponible pour le louer ou non
}