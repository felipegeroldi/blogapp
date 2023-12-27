using BlogApp.Models;

namespace BlogApp.WebApp.Models
{
    public class PagePostsViewModel
    {
        public bool FinalPage { get; set; }
        public int Page { get; set; }
        public int PostsPerPage { get; set; }
        public IEnumerable<Post> Posts { get; set; } = null!;
    }
}
