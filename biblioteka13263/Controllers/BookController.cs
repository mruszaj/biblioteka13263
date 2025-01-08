using biblioteka13263.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace biblioteka13263.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;



        public BookController(LibraryDbContext context)

        {

            _context = context;

        }
        public List<BookView> GetBooksWithDetails()
        {
          
            {
                var booksWithDetails = _context.Books
                    .Include(b => b.Client)               
                    .Include(b => b.BookGenre)           
                    .ThenInclude(bg => bg.Genre)          
                    .Select(b => new BookView             
                    {
                        Id = b.Id,
                        Name = b.Name,
                        ISBN = b.ISBN,
                        Client = b.Client,
                        Genres = String.Join(", ", b.BookGenre.Select(bg => bg.Genre.Name).ToList()),
                        Author = b.Author,
                        Description = b.Description,
                        IsAvilible = b.IsAvilible,
                        WhenAvilable = b.WhenAvilable
                    })
                    .ToList();

                return booksWithDetails;
            }
        }
        public BookView GetBookWithDetailsById(int id)
        {
           
            
                var bookWithDetails = _context.Books
                    .Include(b => b.Client)               
                    .Include(b => b.BookGenre)          
                    .ThenInclude(bg => bg.Genre)          
                    .Where(b => b.Id == id)              
                    .Select(b => new BookView        
                    {
                        Id = b.Id,
                        Name = b.Name,
                        ISBN = b.ISBN,
                        Client = b.Client,
                        Genres = String.Join(", ", b.BookGenre.Select(bg => bg.Genre.Name).ToList()),
                        Author = b.Author,
                        Description = b.Description,
                        IsAvilible = b.IsAvilible,
                        WhenAvilable = b.WhenAvilable
                    })
                    .FirstOrDefault();                    

                return bookWithDetails;
            
        }
        // GET: BookController
        public ActionResult Index()
        {
 
            return View(GetBooksWithDetails());
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            
            return View(GetBookWithDetailsById(id));
        }

        // GET: BookController/Create
        public ActionResult Create()
        {


var model = new BookCreate
        {
            // Populate Genres from the database
            Genres = _context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList()
        };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            //model.Genres = new List<SelectListItem> {null };
            //Debug.WriteLine($"Model received: ISBN={model.ISBN}, Name={model.SelectedGenreIds[0]}");

            //if (!ModelState.IsValid)
            //{
            //    // Log all the validation errors to see what's wrong
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Debug.WriteLine($"Validation error: {error.ErrorMessage}");
            //    }
            //}
            // if (ModelState.IsValid)
            if (model.SelectedGenreIds != null && model.SelectedGenreIds.Any())
            {
            //    Debug.WriteLine("model valid");
                var book = new Book
                {
                    Name = model.Name,
                    ISBN = model.ISBN,
                    Author = model.Author,
                    ClientId = null,  
                    Description = model.Description,
                    IsAvilible=true,
                    WhenAvilable=DateTime.Now



                };


                _context.Books.Add(book);

                _context.SaveChanges();  



                
                    var bookGenres = model.SelectedGenreIds.Select(genreId => new BookGenre
                    {
                        BookId = book.Id,
                        GenreId = genreId
                    }).ToList();

                    _context.BookGenres.AddRange(bookGenres);
                    _context.SaveChanges();  
                

                return RedirectToAction(nameof(Index));  
            }
           
            // Repopulate genres if the form was invalid
            model.Genres = _context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList();

            return View(model);  
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
