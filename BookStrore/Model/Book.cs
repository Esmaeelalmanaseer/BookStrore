using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStrore.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string Decription { get; set; }
        public string ImageURL { get; set; }
        public Author Author { get; set; }

    }
}
