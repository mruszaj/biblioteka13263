using biblioteka13263.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace biblioteka13263.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;



        public BookController(LibraryDbContext context)

        {

            _context = context;

        }

        // GET: BookController
        public ActionResult Index()
        {



            
                var genre1 = new Genre { Name = "Fiction", Description = "ssdeee" };
                var genre2 = new Genre { Name = "Adventure", Description ="sadasd"  };

                var book = new Book
                {   
                    ISBN = "1231231231",
                    Name = "The Adventure of Learning",
                    Author = "John Doe",
                    Description ="sadasd",
                    IsAvilible=true,
                    WhenAvilable=new DateTime(),
                    BookGenre = new List<BookGenre>
                    
        {
            new BookGenre { Genre = genre1 },
            new BookGenre { Genre = genre2 }
        }
                };

                _context.Books.Add(book);
                _context.SaveChanges();
            
            return View(_context.Books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
