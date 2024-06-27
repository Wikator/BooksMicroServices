namespace BooksServiceApi.DTOs;

public class OrderDto
{
    public required string BookId { get; init; }
    public int Quantity { get; init; }
}