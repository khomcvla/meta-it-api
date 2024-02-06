using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaITAPI.Entities;

[Table("book")]
public class Book
{
    [Column("book_id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookId { get; set; }

    [Column("author_id")]
    [ForeignKey("author_id")]
    public Author Author { get; set; }
    public int AuthorId { get; set; }

    [Column("title")]
    [StringLength(100, MinimumLength = 2)]
    public string Title { get; set; }

}
