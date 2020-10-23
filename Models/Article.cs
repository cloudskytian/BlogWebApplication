using System;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}
