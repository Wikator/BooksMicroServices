using RabbitMQ.Client;

namespace BooksServiceApi.Publishers;

public class BookChangedPublisher(IConnection connection) : PublisherBase(connection, "book-changed");