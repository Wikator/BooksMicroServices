class HealthController < ApplicationController
  def show

    connection = Bunny.new(
      hostname: ENV['RABBITMQ_HOST'],
      username: ENV['RABBITMQ_USERNAME'],
      password: ENV['RABBITMQ_PASSWORD']
    )
    connection.start

    if connection.open?
      render plain: 'Healthy', status: :ok
    else
      render plain: 'Unhealthy', status: :service_unavailable
    end
  rescue Bunny::TCPConnectionFailedForAllHosts => _e
    render plain: 'Unhealthy', status: :service_unavailable
  ensure
    connection.close if connection.open?
  end
end
