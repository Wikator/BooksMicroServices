using BooksServiceApi.Database;
using BooksServiceApi.Endpoints;
using BooksServiceApi.Models;
using BooksServiceApi.Publishers;
using BooksServiceApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookStoreDatabaseSettings)));

builder.Services.AddSingleton<BooksService>();
builder.Services.AddSingleton<BookChangedPublisher>();
builder.Services.AddSingleton<BookDeletedPublisher>();
builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddHostedService<OrderDeletedBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedBooksAsync();
}

app.MapBooksEndpoints();

app.Run();
