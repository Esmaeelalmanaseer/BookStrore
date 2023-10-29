using BookStrore.Model;
using BookStrore.Repo;
using BookStrore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStrore.Controle
{
   
    public class BookController : Controller
    {
        private readonly IBookrepos<Book> bookrepostory;
        private readonly IBookrepos<Author> auhtorRepo;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public BookController(IBookrepos<Book> bookrepostory,IBookrepos<Author>auhtorRepo,
            IHostingEnvironment Hosting)
        {
            this.bookrepostory = bookrepostory;
            this.auhtorRepo = auhtorRepo;
            this.hosting = Hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookrepostory.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books = bookrepostory.Find(id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthurViewsModel
            {
                //Authors=auhtorRepo.List().ToList()
                Authors = fillAuthur()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(BookAuthurViewsModel model)
        {
            if (ModelState.IsValid) 
            {
               
                var authour = auhtorRepo.Find(model.AuthorId);
                try
                {
                    string fileName = string.Empty;
                    if (model.File != null)
                    {
                        //ايجاد مسار الروت
                        string uploads = Path.Combine(hosting.WebRootPath, "uploade");
                        //اسم الملف عمل فيه ابلود
                        fileName = model.File.FileName;
                        //دمج
                        string fullpath = Path.Combine(uploads, fileName);
                        //حفظ الصور
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an authur from list!";
                     
                        return View(GetAllAuthur());
                    }

                    Book book = new Book
                    {
                        Id = model.BookId,
                        title = model.Title,
                        Decription = model.Discription,
                        Author = authour,
                        ImageURL=fileName                         
                    };
                    bookrepostory.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
            ModelState.AddModelError("","You Have to fill all required Fields");
            return View(GetAllAuthur());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookrepostory.Find(id);
            var authurId = book.Author == null ? book.Author.id = 0 : book.Author.id;
            var modelView = new BookAuthurViewsModel
            {
                BookId = book.Id,
                Title = book.title,
                Discription = book.Decription,
                AuthorId= authurId,
                Authors = auhtorRepo.List().ToList(),
                ImageURL=book.ImageURL
            };
            return View(modelView);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(int id, BookAuthurViewsModel model)
        {
            try
            {
                string fileName = string.Empty;
                if (model.File != null)
                {
                    //ايجاد مسار الروت
                    string uploads = Path.Combine(hosting.WebRootPath, "uploade");
                    //اسم الملف عمل فيه ابلود
                    fileName = model.File.FileName;
                    string fullpath = Path.Combine(uploads, fileName);
                    //دمج
                    //Delete image
                    string oldfileImage = bookrepostory.Find(model.BookId).ImageURL;
                    string fulloldfile = Path.Combine(uploads, oldfileImage);
                    if(fullpath!=oldfileImage)
                    {
                        System.IO.File.Delete(fulloldfile);
                        //حفظ الصور
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }

                }
                var authur = auhtorRepo.Find(model.AuthorId);
                Book book = new Book
                {
                    title = model.Title,
                    Decription = model.Discription,
                    Author = authur,
                    ImageURL = fileName
                };
                bookrepostory.Update(model.BookId, book);
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
            var book = bookrepostory.Find(id);
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiarmDelete(int id)
        {
            try
            {
                bookrepostory.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> fillAuthur()
        {
            var authurs = auhtorRepo.List().ToList();
            authurs.Insert(0,new Author { id = -1, FullName = "------Please Select an authur" });
            return authurs;
        }
        BookAuthurViewsModel GetAllAuthur()
        {
            var lmoder = new BookAuthurViewsModel
            {
                Authors = fillAuthur()
            };
            return lmoder;
        }
        
    }
}
