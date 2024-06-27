using BooksServiceApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksServiceApi.Database;

public class DatabaseSeeder
{
    private readonly IMongoCollection<Book> _booksCollection;

    public DatabaseSeeder(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task SeedBooksAsync()
    {
        if (await _booksCollection.CountDocumentsAsync(_ => true) != 0)
            return;
        
        var books = new List<Book>
        {
            new()
            {
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                Price = 10.99f,
                Stock = 50
            },
            new()
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Price = 12.99f,
                Stock = 30
            },
            new()
            {
                Title = "1984",
                Author = "George Orwell",
                Price = 15.99f,
                Stock = 20
            },
            new()
            {
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Price = 9.99f,
                Stock = 40
            }
        };

        await _booksCollection.InsertManyAsync(books);
        Console.WriteLine("Successfully seeded books collection.");
    }
}