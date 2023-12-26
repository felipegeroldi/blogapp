namespace BlogApp.Models;

public class User
{
    private string passwordHash = string.Empty;

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash
    {
        get => passwordHash;
        set => passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
    }
    public EUserRole UserRole { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = null!;
    public virtual ICollection<Commentary> Commentaries { get; set; } = null!;

    public bool VerifyPassword(string password) =>
        BCrypt.Net.BCrypt.Verify(password, PasswordHash);
}
