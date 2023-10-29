using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStrore.Model;

namespace BookStrore.Repo
{
    public class AuthorRepo : IBookrepos<Author>
    {
        IList<Author> authors;
        public AuthorRepo()
        {
            authors = new List<Author>()
            { new Author { id=1,FullName="Esmaeel Almanaseer" } ,
              new Author { id=2,FullName="Salem Almherat" } ,
              new Author { id=1,FullName="Ibrahem Maher" } ,
            };
        }

        public void Add(Author entity)
        {
            entity.id = authors.Max(a => a.id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var authour = Find(id);
            authors.Remove(authour);
        }

        public Author Find(int id)
        {
            var authour = authors.SingleOrDefault(a => a.id == id);
            return authour;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author newAuthor)
        {
            var authour = Find(id);
            authour.FullName = newAuthor.FullName;
        }
    }
}
