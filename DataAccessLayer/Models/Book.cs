using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Book : Entity
    {
        public string Title { get; set; } = string.Empty;       
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
