using BookStrore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStrore.Repo
{
    public  class BookDBRepo : IBookrepos<Book>
    {
        BookStoreDBContext _DB;
        public BookDBRepo(BookStoreDBContext DB)
        {
            this._DB = DB;
        }
        public void Add(Book entity)
        {
            _DB.Add(entity);
            _DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var book = Find(id);
            _DB.Remove(book);
            _DB.SaveChanges();

        }

        public Book Find(int id)
        {
            var book = _DB.Books.Include(a=>a.Author).SingleOrDefault(b => b.Id == id);
            return book;
            
        }

        public IList<Book> List()
        {
            return _DB.Books.Include(a=>a.Author).ToList();

        }

        public void Update(int id, Book newBook)
        {
            _DB.Books.Update(newBook);
            _DB.SaveChanges();
        }
     
    }
    
}
