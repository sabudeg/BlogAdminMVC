using System;
using System.Collections.Generic;

namespace MvcHomework4.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Post = new HashSet<Post>();
            UserBlogMatch = new HashSet<UserBlogMatch>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Post { get; set; }
        public ICollection<UserBlogMatch> UserBlogMatch { get; set; }
    }
}
