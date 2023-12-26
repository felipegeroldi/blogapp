namespace BlogApp.Models;

public class Post
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public virtual User Author { get; set; } = null!;

    public virtual IEnumerable<Commentary> Commentaries { get; set; } = null!;
}
