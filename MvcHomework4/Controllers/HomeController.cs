using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcHomework4.Models;

namespace MvcHomework4.Controllers
{
    public class HomeController : Controller
    {
        private readonly CRUD_DBContext _crudctx;

        private IRepository repository;

        public HomeController(IRepository repo) => repository = repo;

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(Login user)
        {
            if (ModelState.IsValid)
            {
                if (repository.Validate(user)) return View("Main");
                else return View();
            }

            else
            { return View(); }
        }

        public ViewResult Main()
        {
            return View();
        }

        public ViewResult PostList()
        {
            return View(repository.PostData);
        }

        public ViewResult CreateNewPost()
        {
            return View("CreateNewPost");
        }

        public ViewResult UpdatePost()
        {
            return View(repository.PostData);

        }

        public ViewResult DeletePost()
        {
            return View(repository.PostData);
        }

        public ViewResult EditPost(int postId) =>
           View(repository.PostData
               .FirstOrDefault(p => p.PostId == postId));

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            repository.EditPost(post);
            return RedirectToAction(nameof(PostList));
        }

        public IActionResult Create(Post post)
        {
            repository.CreatePost(post);
            return RedirectToAction(nameof(PostList));
        }

        [HttpPost]
        public IActionResult Delete(int postId)
        {
            Post deletedPost = repository.Delete(postId);
            repository.Delete(postId);
            return RedirectToAction(nameof(DeletePost));
        }


    }
}
