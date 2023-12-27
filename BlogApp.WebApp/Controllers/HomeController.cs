using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private const int POSTS_IN_HOMEPAGE = 5;

        public async Task<IActionResult> Index(
            [FromServices] IPostRepository postRepository)
        {
            var posts = await postRepository.Posts.Take(POSTS_IN_HOMEPAGE)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            var hasMorePosts = await postRepository.Posts.CountAsync() > POSTS_IN_HOMEPAGE;

            ViewBag.HasMorePosts = hasMorePosts;

            return View(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Forbidden")]
        public IActionResult Forbidden()
        {
            return Content("Forbidden");
        }

        [HttpGet("/NotFound")]
        public IActionResult ErrorNotFound()
        {
            return Content("Not Found");
        }
    }
}
