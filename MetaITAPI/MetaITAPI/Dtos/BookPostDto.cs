using System.ComponentModel.DataAnnotations;

namespace MetaITAPI.Dtos;

public class BookPostDto
{
    [Required(ErrorMessage = "Author is required!")]
    public string Author { get; set; }

    [Required(ErrorMessage = "Title is required!")]
    public string Title { get; set; }

}
