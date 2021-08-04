using System;
using System.Collections.Generic;

#nullable disable

namespace UsingInMemoryCache.Models
{
    public partial class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
