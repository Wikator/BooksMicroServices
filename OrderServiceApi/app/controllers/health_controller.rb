class HealthController < ApplicationController
  def show
    begin
      channel = RabbitmqConnection.instance.connection.create_channel
      queue = channel.queue('', exclusive: true)

      if queue
        render plain: 'Healthy', status: :ok
      else
        render plain: 'Unhealthy', status: :service_unavailable
      end
    rescue Bunny::TCPConnectionFailed, Bunny::NetworkFailure, Timeout::Error => _
      render plain: 'Unhealthy', status: :service_unavailable
    ensure
      RabbitmqConnection.instance.connection.close if RabbitmqConnection.instance.connection&.open?
    end
  end
end
