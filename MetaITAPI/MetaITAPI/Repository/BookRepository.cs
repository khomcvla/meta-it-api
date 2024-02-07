using MetaITAPI.Data;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetaITAPI.Repository;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dataContext;

    public BookRepository(AppDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> IsBookIdExist(int bookId)
    {
        return await _dataContext.Books.AnyAsync(b => b.BookId == bookId);
    }

    public async Task<bool> IsBookExist(int authorId, string title)
    {
        return await _dataContext.Books.AnyAsync(
            b => b.AuthorId == authorId && b.Title.ToLower().Contains(title.ToLower()));
    }

    //GET
    public async Task<List<Book>> GetAll()
    {
        return await _dataContext.Books.AsNoTracking()
            .Select(b => new Book
            {
                BookId = b.BookId,
                AuthorId = b.Author.AuthorId,
                Title = b.Title
            })
            .ToListAsync();
    }

    public async Task<List<Author>> GetAllAuthors()
    {
        return await _dataContext.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetById(int bookId)
    {
        return await _dataContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == bookId);
    }

    public async Task<Book?> GetByAuthorIdAndTitle(int authorId, string title)
    {
        return await _dataContext.Books.AsNoTracking()
            .FirstOrDefaultAsync(b => b.AuthorId == authorId && b.Title.ToLower().Contains(title.ToLower()));
    }

    //ADD
    public async Task<bool> Add(Book book)
    {
        var author = await _dataContext.Authors.FindAsync(book.AuthorId);
        author.Books.Add(book);

        await _dataContext.AddAsync(book);
        return await Save();
    }

    public async Task<bool> AddRange(List<Book> books)
    {
        _dataContext.AddRange(books);
        return await Save();
    }

    //UPDATE
    public async Task<bool> Update(Book book)
    {
        _dataContext.Update(book);
        return await Save();
    }

    public async Task<bool> UpdateRange(List<Book> books)
    {
        _dataContext.UpdateRange(books);
        return await Save();
    }

    //DELETE
    public async Task<bool> Delete(Book book)
    {
        _dataContext.Remove(book);
        return await Save();
    }

    public async Task<bool> DeleteRange(List<Book> books)
    {
        _dataContext.RemoveRange(books);
        return await Save();
    }

    //SAVE
    public async Task<bool> Save()
    {
        var saved = await _dataContext.SaveChangesAsync();
        return saved > 0;
    }
}
