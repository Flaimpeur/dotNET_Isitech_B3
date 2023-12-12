using System.Collections.Generic;
using BookStoreAPI.Entities;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreAPI.Controllers; // BookStoreAPI est l'espace de nom racine de mon projet 



// Ceci est une annotation, elle permet de définir des métadonnées sur une classe
// Dans ce contexte elle permet de définir que la classe BookController est un contrôleur d'API
// On parle aussi de decorator / décorateur
[ApiController]
public class BookController : ControllerBase
{

    [HttpGet("books")]
    public ActionResult<List<Book>> GetBooks()
    {
        List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
        };
        return Ok(books);
    }

    [HttpPost("books")]
    public ActionResult<Book> AddBook(Book book)
    {
        List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
        };
        books.Add(book);
        return CreatedAtAction(nameof(GetBooks), new{id = book.Id}, book);

    }
}