using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.Models;
using BlogApp.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.WebApp.Controllers;

[Route("Post")]
public class PostController : Controller
{
    private const int POSTS_PER_PAGE = 5;
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [Authorize(Roles = "Editor,Administrator")]
    [HttpGet("Create")]
    public IActionResult Create() => View();

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ViewAsync(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post is null)
            return RedirectToAction("ErrorNotFound", "Home");

        ViewBag.Post = post;
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/Posts")]
    public async Task<IActionResult> PaginateAsync([FromQuery] int page)
    {
        PagePostsViewModel pagination = new()
        {
            Page = page,
            PostsPerPage = POSTS_PER_PAGE,
        };

        pagination.Posts = await _postRepository.Posts
            .OrderByDescending(x => x.Id)
            .Skip(pagination.PostsPerPage * pagination.Page)
            .Take(pagination.PostsPerPage)
            .ToListAsync();

        if (pagination.Posts.Count() == 0)
            return RedirectToAction("NotFoundError", "Home");

        pagination.FinalPage = await _postRepository.Posts
            .CountAsync() > pagination.PostsPerPage * (pagination.Page + 1);

        return View(pagination);
    }

    [Authorize(Roles = "Editor,Administrator")]
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
            return RedirectToAction("View", new { post.Id });
        }

        return View("Create", model);
    }
}
