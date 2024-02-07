using MetaITAPI.Utils.Attributes;

namespace MetaITAPI.Dtos;

public class BookPatchDto
{
    public int? AuthorId { get; set; }

    [NullEmptyOrWhiteSpace(ErrorMessage = "Title must not be null, empty, or consist only of whitespace.")]
    public string? Title { get; set; }
}
