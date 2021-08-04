using System;
using System.Collections.Generic;

#nullable disable

namespace UsingInMemoryCache.Models
{
    public partial class Author
    {
        public Author()
        {
            Courses = new HashSet<Course>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string MainCategory { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
