namespace BooksServiceApi.Models;

public class UpsertBookDto
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public float Price { get; init; }
    public int Stock { get; init; }
}