using biblioteka13263.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace biblioteka13263.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;
        //public int clientId;


        public BookController(LibraryDbContext context)

        {

            _context = context;
            //clientId = 1;
                
            // #### tu odkomentować żeby zrobić gatunki ####
                
            //var popularGenres = new List<Genre>
            //    {
            //        new Genre { Name = "Science Fiction" },
            //        new Genre { Name = "Fantasy" },
            //        new Genre { Name = "Mystery" },
            //        new Genre { Name = "Romance" },
            //        new Genre { Name = "Non-Fiction" },
            //        new Genre { Name = "Horror" },
            //        new Genre { Name = "Biography" }
            //    };

            //_context.Genres.AddRange(popularGenres);
            //_context.SaveChanges();

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
                        ClientEmail = b.Client.Email,
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
                        ClientEmail = b.Client.Email,
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            var book = _context.Books
                 .Where(b => b.Id == id)
                 .Select(b => new BookCreate
                 {
                     ISBN = b.ISBN,
                     Name = b.Name,
                     Description = b.Description,
                     Author = b.Author,
                     SelectedGenreIds = b.BookGenre.Select(bg => bg.GenreId).ToList(),
                     Genres = _context.Genres.Select(g => new SelectListItem
                     {
                         Value = g.Id.ToString(),
                         Text = g.Name
                     }).ToList()
                 })
                 .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, BookCreate bookCreate )
        {


            if (bookCreate.SelectedGenreIds == null || !bookCreate.SelectedGenreIds.Any())
            {
                bookCreate.Genres = _context.Genres.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList();
                return View(bookCreate);
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            // Update book entity with BookCreate data
            book.ISBN = bookCreate.ISBN;
            book.Name = bookCreate.Name;
            book.Description = bookCreate.Description;
            book.Author = bookCreate.Author;

            // Handle BookGenre
            var existingGenres = _context.BookGenres.Where(bg => bg.BookId == book.Id).ToList();

            // Remove genres not selected
            foreach (var existingGenre in existingGenres)
            {
                if (!bookCreate.SelectedGenreIds.Contains(existingGenre.GenreId))
                {
                    _context.BookGenres.Remove(existingGenre);
                }
            }

            // Add new genres
            foreach (var genreId in bookCreate.SelectedGenreIds)
            {
                if (!existingGenres.Any(bg => bg.GenreId == genreId))
                {
                    _context.BookGenres.Add(new BookGenre { BookId = book.Id, GenreId = genreId });
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: BookController/Delete/5
        // GET: Book/Delete/1
        public ActionResult Delete(int id)
        {
            var book = GetBookWithDetailsById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book); // Remove the book from the list (or database in real application)
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //// POST: BookController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        // GET: Book/Borrow/1 (Borrow Book)
        public ActionResult Borrow(int id)
        {
            var book = _context.Books
                .Where(b => b.Id == id)
                .FirstOrDefault();

            if (book == null || !book.IsAvilible)
            {
                return NotFound(); // Book is either not found or already borrowed
            }

            
            return View(book);  // Show the Borrow form for the selected book
        }

        // POST: Book/Borrow/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrow(int id, IFormCollection borow1)
        {
            Debug.WriteLine(id);
            var book = _context.Books
                .Where(b => b.Id == id)
                .FirstOrDefault();

            if (book == null || !book.IsAvilible)
            {
                return NotFound();  // Book is either not found or already borrowed
            }
            var client = _context.Clients
               .Where(b => b.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
               .FirstOrDefault();

            if (client == null)
            {
                client = new Client
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Email = User.Identity.Name

                };
                _context.Clients.Add(client);
                _context.SaveChanges();
            }
            //var client = _context.Clients
            //    .Where(c => c.Id == clientId)
            //    .FirstOrDefault();

            //if (client == null)
            //{
            //    return NotFound();  // Client not found
            //}

            // Assign the book to the client and mark it as not available
            book.ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            book.IsAvilible = false;
            book.WhenAvilable = DateTime.Now.AddMonths(1);

            // Add the book to the client's borrowed books list
            //client.Books.Add(book);

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));  // Redirect to the list of books
        }
        //public ActionResult Return(int id)
        //{
        //    var book = _context.Books
        //        .Where(b => b.Id == id && !b.IsAvilible)
        //        .FirstOrDefault();

        //    if (book == null)
        //    {
        //        return NotFound();  // Book not found or wasn't borrowed
        //    }

        //    return View(book);  // Show the Return view for the borrowed book
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id, IFormCollection collection)
        {

            var book = _context.Books
                .Where(b => b.Id == id && !b.IsAvilible)
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();  // Book not found or wasn't borrowed
            }


            // Find the client who borrowed the book
            //var client = _context.Clients
            //    .Where(c => c.Id == book.ClientId)
            //    .FirstOrDefault();

            //if (client == null)
            //{
            //    return NotFound();  // Client not found
            //}

            // Mark the book as available
            book.IsAvilible = true;
            book.ClientId = null;        
            book.WhenAvilable = DateTime.Now;
            // Remove the book from the client's borrowed books list
            //client.Books.Remove(book);

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction(nameof(BorrowedBooks), new { id = User.FindFirstValue(ClaimTypes.NameIdentifier)});
        }
        // GET: Client/BorrowedBooks/1 (List books borrowed by a client)
        [Authorize]
        public ActionResult BorrowedBooks(string id)
        {
            var client = _context.Clients
               .Where(b => b.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
               .FirstOrDefault();

            if (client == null)
            {
                client = new Client
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Email = User.Identity.Name

                };
                _context.Clients.Add(client);
                _context.SaveChanges();
            }
            // Retrieve the client by id, including the books they borrowed
                 client = _context.Clients
                .Where(c => c.Id == id)
                .Include(c => c.Books)  // Include the Books navigation property
                .FirstOrDefault();

            if (client == null)
            {
                return NotFound();  // Client not found
            }

            return View(client);  // Return the client with the list of borrowed books
        }

    }
}
