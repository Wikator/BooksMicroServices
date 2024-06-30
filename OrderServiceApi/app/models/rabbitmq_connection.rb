require 'singleton'
require 'bunny'

class RabbitmqConnection
  include Singleton

  attr_reader :connection

  def initialize
    @connection = Bunny.new(
      hostname: ENV['RABBITMQ_HOST'],
      username: ENV['RABBITMQ_USERNAME'],
      password: ENV['RABBITMQ_PASSWORD']
    )
    @connection.start
  end

  def self.reconnect!
    instance.connection.close if instance.connection&.open?
    instance.instance_variable_set(:@connection, Bunny.new)
    instance.connection.start
  end
end