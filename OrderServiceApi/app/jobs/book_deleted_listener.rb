class BookDeletedListener < ApplicationJob
  queue_as :default

  def perform(*args)
    puts 'Subscribing...'
    connection = Bunny.new(
      hostname: ENV['RABBITMQ_HOST'],
      username: ENV['RABBITMQ_USERNAME'],
      password: ENV['RABBITMQ_PASSWORD']
    )
    connection.start
    channel = connection.create_channel
    queue = channel.queue('book-deleted', durable: true)

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