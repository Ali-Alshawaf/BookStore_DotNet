using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DbOnlineStore _context;

        public BooksController(DbOnlineStore context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();

                if (books.Count == 0 || books == null)
                {
                    return NotFound("No books found.");
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching books.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {
                    return NotFound("No book found with this id.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the book.");
            }
        }

        //[HttpPost]
        //public ActionResult AddBook([FromForm] Book book, [FromForm] IFormFile FormFile)
        //{
        //    try
        //    {
        //        if (FormFile != null)
        //        {
        //            string fileName = Guid.NewGuid() + Path.GetExtension(FormFile.FileName);
        //            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

        //            Directory.CreateDirectory(Path.GetDirectoryName(path)); // Ensure the directory exists
        //            using (Stream stream = new FileStream(path, FileMode.Create))
        //            {
        //                FormFile.CopyTo(stream);
        //            }
        //            book.Image = fileName;
                    

        //            _context.Books.Add(book);
        //            _context.SaveChanges();

        //            return Ok( "Book added successfully");
        //        }
        //        else
        //        {
        //            return BadRequest("Image file is required.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        //    }
        //}
    }
}
