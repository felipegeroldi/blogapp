using System.ComponentModel.DataAnnotations;

namespace BlogApp.WebApp.Models;

public class PostCreateViewModel
{
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Title is required.")]
    [DataType(DataType.Text)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Content")]
    [Required(ErrorMessage = "Content is required.")]
    [DataType(DataType.EmailAddress)]
    public string Content { get; set; } = string.Empty;
}
