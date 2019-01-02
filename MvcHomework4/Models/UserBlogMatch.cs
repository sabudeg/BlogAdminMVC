using System;
using System.Collections.Generic;

namespace MvcHomework4.Models
{
    public partial class UserBlogMatch
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public Blog Blog { get; set; }
        public Login User { get; set; }
    }
}
