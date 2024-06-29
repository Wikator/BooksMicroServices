using BooksServiceApi.Database;
using BooksServiceApi.Endpoints;
using BooksServiceApi.Models;
using BooksServiceApi.Publishers;
using BooksServiceApi.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookStoreDatabaseSettings)));

var connectionFactory = new ConnectionFactory
{
    HostName = "rabbitmq.books-platform",
    UserName = "admin",
    Password = "password"
};

builder.Services.AddSingleton(connectionFactory.CreateConnection());

builder.Services.AddSingleton<BooksService>();
builder.Services.AddSingleton<BookChangedPublisher>();
builder.Services.AddSingleton<BookDeletedPublisher>();
builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddHostedService<OrderDeletedBackgroundService>();

builder.Services.AddHealthChecks()
    .AddRabbitMQ(timeout: TimeSpan.FromSeconds(5));

builder.Services.AddCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedBooksAsync();
}

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.WithOrigins("http://localhost:4200", "http://192.168.49.2:31002");
});

app.MapBooksEndpoints();
app.MapHealthChecks("/healthz");

app.Run();
