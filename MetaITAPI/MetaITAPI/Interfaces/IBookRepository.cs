using MetaITAPI.Entities;

namespace MetaITAPI.Interfaces;

public interface IBookRepository
{
    Task<bool> IsBookIdExist(int bookId);
    Task<bool> IsBookExist(int authorId, string title);

    Task<List<Book>> GetAll();
    Task<Book?> GetById(int bookId);
    Task<Book?> GetByAuthorIdAndTitle(int authorid, string title);

    Task<bool> Add(Book book);
    Task<bool> AddRange(List<Book> book);

    Task<bool> Update(Book book);
    Task<bool> UpdateRange(List<Book> book);

    Task<bool> Delete(Book book);
    Task<bool> DeleteRange(List<Book> book);

    Task<bool> Save();
}
