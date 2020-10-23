using System;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int Floor { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}
