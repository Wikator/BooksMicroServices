using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksServiceApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public required string Title { get; set; }
    public required string Author { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
}