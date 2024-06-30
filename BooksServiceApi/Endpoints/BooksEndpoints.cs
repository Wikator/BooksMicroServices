using BooksServiceApi.DTOs;
using BooksServiceApi.Models;
using BooksServiceApi.Publishers;
using BooksServiceApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BooksServiceApi.Endpoints;

public static class BooksEndpoints
{
    public static void MapBooksEndpoints(this WebApplication app)
    {
        var booksApi = app.MapGroup("api/books");
        
        booksApi.MapGet("", async (BooksService service) => await service.GetAsync());
        booksApi.MapGet("{id}", GetAsync);
        booksApi.MapPost("", CreateAsync);
        booksApi.MapPut("{id}", UpdateAsync);
        booksApi.MapDelete("{id}", DeleteAsync);
        booksApi.MapPut("place-order", PlaceOrderAsync);
    }
    
    private static async Task<Results<Ok<Book>, NotFound>> GetAsync(BooksService service, string id)
    {
        var book = await service.GetAsync(id);
        return book is not null ? TypedResults.Ok(book) : TypedResults.NotFound();
    }
    
    private static async Task<IResult> CreateAsync(BooksService service, BookUpsertDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author,
            Price = bookDto.Price,
            Stock = bookDto.Stock
        };
        
        await service.CreateAsync(book);
        return Results.Created($"/api/books/{book.Id}", book);
    }
    
    private static async Task<Results<Ok<Book>, NotFound>> UpdateAsync(BooksService service, BookChangedPublisher publisher,
        string id, BookUpsertDto bookDto)
    {
        var book = await service.GetAsync(id);
        
        if (book is null)
            return TypedResults.NotFound();

        book.Title = bookDto.Title;
        book.Author = bookDto.Author;
        book.Price = bookDto.Price;
        book.Stock = bookDto.Stock;
        
        await service.UpdateAsync(id, book);
        
        publisher.Publish(book, "book-changed-orders", "book-changed-notifications");
        return TypedResults.Ok(book);
    }
    
    private static async Task<Results<NoContent, NotFound>> DeleteAsync(BooksService service,
        BookDeletedPublisher publisher, string id)
    {
        if (await service.GetAsync(id) is null)
            return TypedResults.NotFound();
        
        await service.RemoveAsync(id);
        
        publisher.Publish(id, "book-deleted-orders", "book-deleted-notifications");
        return TypedResults.NoContent();
    }

    private static async Task<Results<Ok<Book>, NotFound, BadRequest<string>>> PlaceOrderAsync(BooksService service,
        OrderDto orderDto)
    {
        var book = await service.GetAsync(orderDto.BookId);
        if (book is null)
            return TypedResults.NotFound();
        
        if (book.Stock < orderDto.Quantity)
            return TypedResults.BadRequest("Not enough stock");
        
        await service.UpdateStock(book, orderDto.Quantity);
        return TypedResults.Ok(book);
    }
}