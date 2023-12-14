using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreAPI.Entities;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookStoreAPI.Controllers; // BookStoreAPI est l'espace de nom racine de mon projet 


// this designe la classe dans laquelle on se trouve


// Ceci est une annotation, elle permet de définir des métadonnées sur une classe
// Dans ce contexte elle permet de définir que la classe BookController est un contrôleur d'API
// On parle aussi de decorator / décorateur
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    //private BookDto _listBooks;

    public BookController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        //_listBooks = listBooks;
    }


    // Ceci est une annotation, elle permet de définir des métadonnées sur une méthode
    // ActionResult designe le type de retour de la méthode de controller d'api
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetBooks()
    {
        var books = await _dbContext.Books.ToListAsync();

        var booksDto = new List<BookDto>();

        foreach (var book in books)
        {
            booksDto.Add(_mapper.Map<BookDto>(book));
        }


        return Ok(booksDto);

    }
    // POST: api/Book
    // BODY: Book (JSON)
    [Authorize]
    //[AllowAnonymous] // permet de ne pas avoir besoin d'être authentifié pour accéder à la méthode
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Book))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
    {
        // we check if the parameter is null
        if (book == null)
        {
            return BadRequest();
        }
        // we check if the book already exists
        Book? addedBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
        if (addedBook != null)
        {
            return BadRequest("Book already exists");
        }
        else
        {
            // we add the book to the database
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            // we return the book
            return Created("api/book", book);

        }
    }

    // TODO: Add PUT and DELETE methods
    // PUT: api/Book/5
    // BODY: Book (JSON)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }
        var bookToUpdate = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        bookToUpdate.Genre = book.Genre;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Abstract = book.Abstract;

        _dbContext.Entry(bookToUpdate).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("validationTest")]
    public ActionResult ValidationTest([FromBody] BookDto book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var bookToDelete = await _dbContext.Books.FindAsync(id);
        // var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

        if (bookToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Books.Remove(bookToDelete);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }


    // Rajout d'une recherche par auteur et affichage des livres uniquement écrit par l'auteur rechercher
    [Authorize]
    [HttpGet("{author}")]
    public async Task<ActionResult<List<BookDto>>> FindAuthor(string author)
    {
        // Récupération de tout les livres avec le même auteur
        var books = await _dbContext.Books.Where(b => b.Author == author).ToListAsync();
        
        //Vérification si l'auteur existe
        if (books == null)
        {
            return BadRequest("Auteur non trouver");
        }   
        else
        {
            //Affichage dans une liste des livre de l'auteur rechercher
            var booksDto = _mapper.Map<List<BookDto>>(books);
            return Ok(booksDto);
        }
    }

    // Rajout d'une recherche par genre et affichage des livres uniquement dans ce genre
    [Authorize]
    [HttpGet("Genre")]
    public async Task<ActionResult<List<BookDto>>> FindGenre(string genre)
    {
        // Récupération de tout les livres avec le même genre
        var books = await _dbContext.Books.Where(b => b.Genre == genre).ToListAsync();
        
        //Vérification si l'auteur existe
        if (books == null)
        {
            return BadRequest("Genre non trouver");
        }   
        else
        {
            //Affichage dans une liste des livre du genre rechercher
            var booksDto = _mapper.Map<List<BookDto>>(books);
            return Ok(booksDto);
        }
    }

    // Rajout d'un trie alphabetique des livre par les titres
    [Authorize]
    [HttpGet("sortBooksByTheTitle")]
    public async Task<ActionResult<List<BookDto>>> sortBooksByTheTitle()
    {
        // ------
        // -- Récupération de tout les livres en stocke
        var books = await _dbContext.Books.ToListAsync();
        var listofBooks = new List<BookDto>();

        foreach (var book in books)
        {
            listofBooks.Add(_mapper.Map<BookDto>(book));
        }
        // --
        // ------

        //Trie de la liste des livres par ordre alphabetique des titres
        listofBooks = listofBooks.OrderBy(b => b.Title).ToList();
        await _dbContext.SaveChangesAsync();
        return Ok(listofBooks); // affichage
    }

    // Rajout d'un trie alphabetique des livres par les auteurs
    [Authorize]
    [HttpGet("sortBooksByTheAuthor")]
    public async Task<ActionResult<List<BookDto>>> sortBooksByTheAuthor()
    {
        // ------
        // -- Récupération de tout les livres en stocke
        var books = await _dbContext.Books.ToListAsync();
        var listofBooks = new List<BookDto>();

        foreach (var book in books)
        {
            listofBooks.Add(_mapper.Map<BookDto>(book));
        }
        // --
        // ------

        //Trie de la liste des livres par ordre alphabetique des auteurs
        listofBooks = listofBooks.OrderBy(b => b.Author).ToList();
        await _dbContext.SaveChangesAsync();
        return Ok(listofBooks);
    }

    // Rajout d'un trie alphabetique des livres par le genre
    [Authorize]
    [HttpGet("sortBooksByTheGenre")]
    public async Task<ActionResult<List<BookDto>>> sortBooksByTheGenre()
    {
        // ------
        // -- Récupération de tout les livres en stocke
        var books = await _dbContext.Books.ToListAsync();
        var listofBooks = new List<BookDto>();

        foreach (var book in books)
        {
            listofBooks.Add(_mapper.Map<BookDto>(book));
        }
        // --
        // ------

        //Trie de la liste des livres par ordre alphabetique des genres
        listofBooks = listofBooks.OrderBy(b => b.Genre).ToList();
        await _dbContext.SaveChangesAsync();
        return Ok(listofBooks);
    }


}