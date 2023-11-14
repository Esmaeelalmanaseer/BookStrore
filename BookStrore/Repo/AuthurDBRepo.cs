using BookStrore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStrore.Repo
{
    public class AuthurDBRepo : IBookrepos<Author>
    {
        BookStoreDBContext _DB;
       
        public AuthurDBRepo(BookStoreDBContext DB)
        {
            this._DB = DB;
        }

        public void Add(Author entity)
        {
            _DB.Authors.Add(entity);
            _DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var authour = Find(id);
            _DB.Authors.Remove(authour);
            _DB.SaveChanges();

        }

        public Author Find(int id)
        {
            var authour = _DB.Authors.SingleOrDefault(a => a.id == id);
            return authour;
        }

        public IList<Author> List()
        {
            return _DB.Authors.ToList();
        }

        public List<Author> Serch(string trem)
        {
            var result = _DB.Authors.Where(a => a.FullName.Contains(trem)).ToList();
            return result;
        }

        public void Update(int id, Author newAuthor)
        {
            _DB.Update(newAuthor);
            _DB.SaveChanges();

        }
    }
}
