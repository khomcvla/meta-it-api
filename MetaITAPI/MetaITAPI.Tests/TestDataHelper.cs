using AutoMapper;
using MetaITAPI.Data;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;
using MetaITAPI.Profiles;
using MetaITAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace MetaITAPI.Tests;

public static class TestDataHelper
{
    public const int ZERO = 0;

    public const int FIRST_INDEX = 3;
    public const int LAST_INDEX = 5;

    public const int EXISTED_INDEX = FIRST_INDEX;
    public const int NOT_EXISTED_INDEX = LAST_INDEX + 1;

    public const string FIRST_NAME_BASE = "FirstName";
    public const string LAST_NAME_BASE = "LastName";
    public const string TITLE_BASE = "Title";

    public const string UPDATED = $"Updated";

    public const string EMPTY_DATA = "";
    public const string NOT_EXISTED_DATA = "NOT_EXISTED_DATA";

    //-------------------------------------------------------------------------
    public static Author GetTestAuthor(int index) =>
        new()
        {
            AuthorId = index,
            FirstName = $"{FIRST_NAME_BASE}-{index}",
            LastName = $"{LAST_NAME_BASE}-{index}",
            Books = new List<Book> { GetTestBook(index) }
        };

    //-------------------------------------------------------------------------
    public static Book GetTestBook(int index) =>
        new()
        {
            AuthorId = index,
            BookId = index,
            Title = $"{TITLE_BASE}-{index}"
        };

    //-------------------------------------------------------------------------
    public static List<Book> GetAllTestBooks() =>
        GetRangeTestBooks(FIRST_INDEX, LAST_INDEX);

    //-------------------------------------------------------------------------
    public static List<Book> GetRangeTestBooks(int start, int end) =>
        Enumerable.Range(start, end - start + 1).Select(GetTestBook).ToList();

    //-------------------------------------------------------------------------
    public static IEnumerable<object[]> GetBookIdMemberData()
    {
        yield return new object[] { EXISTED_INDEX };
        yield return new object[] { NOT_EXISTED_INDEX };
    }

    //-------------------------------------------------------------------------
    public static IEnumerable<object?[]> AddBookMemberData()
    {
        var notExistedAuthorNewBook = GetTestBook(NOT_EXISTED_INDEX);
        notExistedAuthorNewBook.AuthorId = NOT_EXISTED_INDEX;
        var notExistedAuthorNewBookDto = GetMapper().Map<BookPostDto>(notExistedAuthorNewBook);

        var existedBookExistedAuthor = GetTestBook(EXISTED_INDEX);
        var existedBookExistedAuthorDto = GetMapper().Map<BookPostDto>(existedBookExistedAuthor);

        var newBookExistedAuthor = GetTestBook(NOT_EXISTED_INDEX);
        newBookExistedAuthor.AuthorId = EXISTED_INDEX;
        var newBookDto = GetMapper().Map<BookPostDto>(newBookExistedAuthor);

        yield return new object?[] { notExistedAuthorNewBookDto};
        yield return new object?[] { existedBookExistedAuthorDto};
        yield return new object?[] { newBookDto};
    }

    //-------------------------------------------------------------------------
    public static IEnumerable<object?[]> UpdateBookMemberData()
    {
        var existedBook = GetTestBook(EXISTED_INDEX);
        var existedBookDto = GetMapper().Map<BookPatchDto>(existedBook);
        existedBookDto.Title = $"{UPDATED}";

        var notExistedBook = GetTestBook(NOT_EXISTED_INDEX);
        var notExistedBookDto = GetMapper().Map<BookPatchDto>(notExistedBook);

        yield return new object?[] { EXISTED_INDEX, existedBookDto };
        yield return new object?[] { NOT_EXISTED_INDEX, notExistedBookDto };
    }

    //-------------------------------------------------------------------------
    public static IEnumerable<object?[]> DeleteBookMemberData()
    {
        yield return new object?[] { EXISTED_INDEX };
        yield return new object?[] { NOT_EXISTED_INDEX };
    }

    //-------------------------------------------------------------------------
    public static IMapper GetMapper()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
        return configuration.CreateMapper();
    }

    //-------------------------------------------------------------------------
    public static async Task<BookRepository> GetTestBookRepository()
    {
        return new BookRepository(await GetTestDataContext());
    }

    //-------------------------------------------------------------------------
    private static async Task<AppDbContext> GetTestDataContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dataContext = new AppDbContext(options);
        await dataContext.Database.EnsureCreatedAsync();

        if (await dataContext.Books.AnyAsync())
            return dataContext;

        for (var i = FIRST_INDEX; i <= LAST_INDEX; i++)
        {
            dataContext.Add(GetTestAuthor(i));
            await dataContext.SaveChangesAsync();
        }

        return dataContext;
    }
}
