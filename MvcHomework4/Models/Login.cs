using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcHomework4.Models
{
    public partial class Login
    {
        public Login()
        {
            UserBlogMatch = new HashSet<UserBlogMatch>();
        }

        public int UserId { get; set; }
        [Key]
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        public ICollection<UserBlogMatch> UserBlogMatch { get; set; }
    }
}
