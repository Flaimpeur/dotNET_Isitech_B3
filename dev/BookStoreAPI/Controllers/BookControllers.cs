using System.Collections.Generic;
using BookStoreAPI.Entities;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreAPI.Controllers; // BookStoreAPI est l'espace de nom racine de mon projet 

// this c'est l'instance de la classe, c'est a dire la classe dans la quel on se trouve

// Ceci est une annotation, elle permet de définir des métadonnées sur une classe
// Dans ce contexte elle permet de définir que la classe BookController est un contrôleur d'API
// On parle aussi de decorator / décorateur
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    public static List<Book> books = new List<Book> //J'inisialise d'abord ma liste de livre en j'y ajoute 1 livre de base
    {
        new Book { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
    };

    [HttpGet] //J'utilise une méthode get pour récup se qui il y a
    public ActionResult<List<Book>> GetBooks()
    {
        return Ok(books);
    }

    [HttpPost] // La méthode post sert ici a pouvoir intéragir entre l'utilisateur et notre code, ici la liste donc pouvoir rajouter des livres dans la liste
    public ActionResult<Book> CreateBook(Book book)
    {
        books.Add(book); //Ajout du livre écrit par l'utilisateur dans la liste
        return CreatedAtAction(nameof(GetBooks), new{id = book.Id}, book);
    }
}