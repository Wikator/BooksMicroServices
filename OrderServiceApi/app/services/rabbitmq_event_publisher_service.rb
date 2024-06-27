class RabbitmqEventPublisherService
  def initialize(queue_name)
    @connection = Bunny.new(
      hostname: ENV['RABBITMQ_HOST'],
      username: ENV['RABBITMQ_USERNAME'],
      password: ENV['RABBITMQ_PASSWORD']
    )
    @connection.start
    @channel = @connection.create_channel
    @queue = @channel.queue(queue_name)
    @queue_name = queue_name
  end

  def publish_event(payload)
    @queue.publish(payload, routing_key: @queue_name)
  end

  def close
    @connection.close
  end
end