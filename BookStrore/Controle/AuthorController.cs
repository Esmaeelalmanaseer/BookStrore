using BookStrore.Model;
using BookStrore.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStrore.Controle
{
    public class AuthorController : Controller
    {
        private readonly IBookrepos<Author> authorrepos;

        public AuthorController(IBookrepos<Author> authorrepos)
        {
            this.authorrepos = authorrepos;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authrs = authorrepos.List();
            return View(authrs);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var authrs = authorrepos.Find(id);
            return View(authrs);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorrepos.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorrepos.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Author author)
        {
            try
            {
                authorrepos.Update(id,author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var authur = authorrepos.Find(id);
            return View(authur);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Author author)
        {
            authorrepos.Delete(id);
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
