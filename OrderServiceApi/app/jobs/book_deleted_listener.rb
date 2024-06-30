class BookDeletedListener < ApplicationJob
  queue_as :default

  def perform(*args)
    connection = RabbitmqConnection.instance.connection
    channel = connection.create_channel

    exchange = channel.direct('book-delete-exchange', durable: true)
    queue = channel.queue('book-deleted-orders', durable: true)
    queue.bind(exchange, routing_key: 'book-deleted-orders')

    queue.subscribe(block: true) do |delivery_info, properties, body|
      puts delivery_info, properties, body
      process_message(body)
    end
  end

  private

  def process_message(body)
    order_items = OrderItem.where(book_id: body)
    count = order_items.count
    order_items.delete_all

    puts "Deleted #{count} order_items for book ID #{body}"
  end
end
