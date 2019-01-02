using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHomework4.Models
{
    public interface IRepository
    {
        IEnumerable<Blog> BlogData { get; }
        IEnumerable<Login> LoginData { get; }
        IEnumerable<Post> PostData { get; }
        IEnumerable<UserBlogMatch> userBlogMatchesData { get; }

        Post GetPost(int key);

        bool Validate(Login user);
        void CreatePost(Post post);
        void EditPost(Post post);
        Post Delete(int postId);
    }
}
