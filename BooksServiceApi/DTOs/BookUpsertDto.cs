namespace BooksServiceApi.DTOs;

public class BookUpsertDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
}