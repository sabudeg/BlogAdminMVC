using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcHomework4.Models
{
    public class DataRepository : IRepository
    {
        private CRUD_DBContext crudContext;

        public DataRepository(CRUD_DBContext ctx) => crudContext = ctx;


        public IEnumerable<Blog> BlogData => crudContext.Blog.ToArray();
        public IEnumerable<Login> LoginData => crudContext.Login.ToArray();
        public IEnumerable<Post> PostData => crudContext.Post.ToArray();
        public IEnumerable<UserBlogMatch> userBlogMatchesData => crudContext.UserBlogMatch.ToArray();

        public Post GetPost(int key) => crudContext.Post.Find(key);

        public bool Validate(Login user)
        {
            var usr = this.crudContext.Login.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();

            if (usr != null) return true;
            else return false;
        }

        public void CreatePost(Post post)
        {
            this.crudContext.Post.Add(post);
            this.crudContext.SaveChanges();
        }

        public void EditPost(Post post)
        {
            //Post p = crudContext.Post.FirstOrDefault(x => x.PostId == post.PostId);
            Post p = GetPost(post.PostId);
            p.BlogId = post.BlogId;
            p.Content = post.Content;
            p.Title = post.Title;
            crudContext.SaveChanges();
        }


        public Post Delete(int postId) { 
          Post po = crudContext.Post
                .FirstOrDefault(p => p.PostId == postId);

            if(po != null )
        {
            crudContext.Post.Remove(po);
            crudContext.SaveChanges();
            }
            return po;
        }
    }
}
