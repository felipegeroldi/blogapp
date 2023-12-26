namespace BlogApp.Models;

public class Commentary
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public virtual User Author { get; set; } = null!;

    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
}
