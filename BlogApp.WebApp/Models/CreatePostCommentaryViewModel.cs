namespace BlogApp.WebApp.Models
{
    public class CreatePostCommentaryViewModel
    {
        public int PostId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
