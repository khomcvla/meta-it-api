using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaITAPI.Entities;

[Table("author")]
public class Author
{
    [Column("author_id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuthorId { get; set; }

    [Column("first_name")]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Column("last_name")]
    [MaxLength(100)]
    public string LastName { get; set; }

    public ICollection<Book> Books { get; set; }
}
