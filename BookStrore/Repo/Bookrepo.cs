using BookStrore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStrore.Repo
{
    public class Bookrepo : IBookrepos<Book>
    {
        List<Book>Books;
        public Bookrepo()
        {
            Books = new List<Book>()
            {
                new Book{ 
                    Id=1,title="C# programing",Decription="no decriptcion",Author=new Author{id=1}
                },
                  new Book{
                    Id=2,title="Java programing",Decription="no deta",Author=new Author{id=2} 
                },
                    new Book{
                    Id=3,title="python programing",Decription="not found",Author=new Author{id=3}
                },
            };
        }
        public void Add(Book entity)
        {
            entity.Id = Books.Max(b => b.Id) + 1;
            Books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            Books.Remove(book);
        }

        public Book Find(int id)
        {
            var book=Books.SingleOrDefault(b=>b.Id==id);
            return book;
        }

        public IList<Book> List()
        {
            return Books;
        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);
            book.title = newBook.title;
            book.Decription = newBook.Decription;
            book.Author = newBook.Author;
            book.ImageURL = newBook.ImageURL;
        }
    }
}
