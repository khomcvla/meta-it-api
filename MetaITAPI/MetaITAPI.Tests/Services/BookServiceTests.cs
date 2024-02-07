using AutoMapper;
using FluentAssertions;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using MetaITAPI.Services;
using MetaITAPI.Utils.Constants;
using MetaITAPI.Utils.Responses;
using Microsoft.AspNetCore.Http;
using Xunit.Abstractions;
using static MetaITAPI.Tests.TestDataHelper;

namespace MetaITAPI.Tests.Services;

public class BookServiceTests
{
    private readonly ITestOutputHelper _console;
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookServiceTests(ITestOutputHelper testOutputHelper)
    {
        _console = testOutputHelper;

        var bookRepository = GetTestBookRepository().GetAwaiter().GetResult();
        _mapper = GetMapper();

        _bookService = new BookService(_mapper, bookRepository);
    }

    //-------------------------------------------------------------------------
    [Fact]
    public async void BookService_GetAll_Returns_OK_ListBookGetDto()
    {
        //Arrange
        var testBooks = GetAllTestBooks();
        var testBooksDtos = _mapper.Map<List<BookGetDto>>(testBooks);

        //Act
        var response = await _bookService.GetAll();

        //Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ServiceResponse>();

        response.StatusCode.Should().Be(StatusCodes.Status200OK);

        response.Value.Should().BeAssignableTo<ICollection<BookGetDto>>();
        (response.Value as IEnumerable<BookGetDto>).Should().NotBeNullOrEmpty();
        response.Value.Should().BeEquivalentTo(testBooksDtos);
    }

    //-------------------------------------------------------------------------
    [Theory]
    [MemberData(nameof(GetBookIdMemberData), MemberType = typeof(TestDataHelper))]
    public async void BookService_GetById_Returns_BookGetDto(
        int bookId,
        bool expectedResult)
    {
        //Arrange

        //Act
        var response = await _bookService.GetById(bookId);

        //Assert
        if (expectedResult)
        {
            response.Should().NotBeNull();
            response.Should().BeOfType<ServiceResponse>();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeAssignableTo<BookGetDto>();

            (response.Value as BookGetDto).BookId.Should().Be(bookId);
            (response.Value as BookGetDto).Title.Should().Be($"{TITLE_BASE}-{bookId}");
        }
        else
        {
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Value.Should().Be(StatusMessages.NotExist);
        }
    }

    //-------------------------------------------------------------------------
    [Theory]
    [MemberData(nameof(AddBookMemberData), MemberType = typeof(TestDataHelper))]
    public async void BookService_AddBook_Returns_Book(
        BookPostDto bookDto,
        bool expectedResult)
    {
        //Arrange

        //Act
        var response = await _bookService.Add(bookDto);

        //Assert
        if (expectedResult)
        {
            response.Should().NotBeNull();
            response.Should().BeOfType<ServiceResponse>();

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Value.Should().BeOfType<Book>();
            (response.Value as Book).BookId.Should().Be(NOT_EXISTED_INDEX);
        }
        else
        {
            response.StatusCode.Should().Be(StatusCodes.Status409Conflict);
            response.Value.Should().Be(StatusMessages.AlreadyExist);
        }
    }

    //-------------------------------------------------------------------------
    [Theory]
    [MemberData(nameof(UpdateBookMemberData), MemberType = typeof(TestDataHelper))]
    public async void BookService_UpdateBook_Returns_Book(
        int bookId,
        BookPatchDto bookDto,
        bool expectedResult)
    {
        //Arrange

        //Act
        var response = await _bookService.Update(bookId, bookDto);
        var book = await _bookService.GetById(bookId);

        //Assert
        if (expectedResult)
        {
            response.Should().NotBeNull();
            response.Should().BeOfType<ServiceResponse>();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeOfType<Book>();
            (response.Value as Book).Title.Should().Be($"{UPDATED}");
        }
        else
        {
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Value.Should().Be(StatusMessages.NotExist);
        }
    }

}
