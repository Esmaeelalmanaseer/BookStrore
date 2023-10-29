using BookStrore.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStrore.ViewModel
{
    public class BookAuthurViewsModel
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Discription { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImageURL { get; set; }
    }
}
