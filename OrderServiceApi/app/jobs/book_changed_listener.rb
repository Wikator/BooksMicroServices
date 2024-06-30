class BookChangedListener < ApplicationJob
  queue_as :default

  def perform(*args)
    connection = RabbitmqConnection.instance.connection
    channel = connection.create_channel

    exchange = channel.direct('book-changed-exchange', durable: true)
    queue = channel.queue('book-changed-orders', durable: true)
    queue.bind(exchange, routing_key: 'book-changed-orders')

    queue.subscribe(block: true) do |delivery_info, properties, body|
      puts delivery_info, properties, body
      process_message(body)
    end
  end

  private

  def process_message(body)
    message = JSON.parse(body)
    book_id = message['Id']
    title = message['Title']
    author = message['Author']
    price = message['Price']

    order_items = OrderItem.where(book_id: book_id)
    order_items.each do |order_item|
      order_item.update(title: title, author: author, price: price)
    end

    puts "Updated #{order_items.count} order_items for book ID #{book_id}"
  end
end