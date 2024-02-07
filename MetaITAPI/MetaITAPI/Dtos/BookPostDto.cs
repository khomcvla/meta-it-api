using System.ComponentModel.DataAnnotations;

namespace MetaITAPI.Dtos;

public class BookPostDto
{
    [Required(ErrorMessage = "AuthorId is required!")]
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Title is required!")]
    public string Title { get; set; }
}
