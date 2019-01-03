using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcHomework4.Models;
using System.Web;


namespace MvcHomework4.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo) => repository = repo;

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        int blogId { get; set; }

        [HttpPost]
        public ViewResult Index(Login user)
        {
            if (ModelState.IsValid)
            {
                if (repository.Validate(user)){

                    HttpContext.Session.SetString("user", user.UserName);
                    HttpContext.Session.SetInt32("id", user.UserId);

                    ViewData["userName"] = HttpContext.Session.GetString("user");
                    ViewData["uid"] = HttpContext.Session.GetString("user").Substring(4);

                    return View("Main");
                }

                else return View();
            }

            else
            { return View(); }
        }

        public ViewResult Main()
        {
            ViewData["uid"] = HttpContext.Session.GetString("user").Substring(4);
            return View("Main");
        }

        public ViewResult PostList()
        {
            blogId = Int32.Parse(HttpContext.Session.GetString("user").Substring(4));
            return View(repository.PostData.Where(b => b.BlogId == blogId));
        }

        public ViewResult CreateNewPost()
        {
            @ViewData["uid"] = HttpContext.Session.GetString("user").Substring(4);
            return View("CreateNewPost");
        }

        public ViewResult UpdatePost()
        {
            blogId = Int32.Parse(HttpContext.Session.GetString("user").Substring(4));
            return View(repository.PostData.Where(b => b.BlogId == blogId));
        }

        public ViewResult DeletePost()
        {
            blogId = Int32.Parse(HttpContext.Session.GetString("user").Substring(4));
            return View(repository.PostData.Where(b => b.BlogId == blogId));
        }

        public ViewResult EditPost(int postId) =>
           View(repository.PostData
               .FirstOrDefault(p => p.PostId == postId));

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            repository.EditPost(post);
            return RedirectToAction("PostList");
        }

        public IActionResult Create(Post post)
        {
            ViewData["uid"] = blogId;
            repository.CreatePost(post);
            return RedirectToAction("PostList");
        }

        [HttpPost]
        public IActionResult Delete(int postId)
        {
            Post deletedPost = repository.Delete(postId);
            repository.Delete(postId);
            return RedirectToAction("DeletePost");
        }

    }
}
