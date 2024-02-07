
namespace MetaITAPI.Dtos;

public class AuthorGetDto
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<int> BookIds { get; set; }
}
