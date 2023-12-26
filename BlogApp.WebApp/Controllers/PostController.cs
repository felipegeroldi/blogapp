using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.Models;
using BlogApp.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.WebApp.Controllers;

[Route("Post")]
[Authorize(Roles = "Editor,Administrator")]
public class PostController : Controller
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet("Create")]
    public IActionResult Create() => View();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ViewAsync(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post is null)
            return RedirectToAction("ErrorNotFound", "Home");

        return View(post);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> RegisterNewPostAsync(
        [FromServices] IUserRepository userRepository,
        [FromForm] PostCreateViewModel model)
    {
        if(ModelState.IsValid)
        {
            var currentUser = await userRepository.GetUserByEmailAsync(
                User.Claims.First(x => x.Type == ClaimTypes.Email).Value);

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                Author = currentUser!,
            };

            await _postRepository.AddPostAsync(post);
            return RedirectToAction("ViewAsync", new { post.Id });
        }

        return View("Create", model);
    }
}
