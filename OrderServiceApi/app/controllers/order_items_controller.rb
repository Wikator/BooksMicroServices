require 'bunny'

class OrderItemsController < ApplicationController
  include HTTParty
  before_action :set_order_item, only: %i[show update destroy]

  base_uri 'http://books-service-api.books-platform:8080/api/books'

  def index
    @order_items = OrderItem.all
    render json: @order_items
  end

  def show
    render json: @order_item
  end

  def create
    unless params[:book_id].present? && params[:quantity].present? && params[:user_id].present?
      render json: { error: "Missing required parameters" }, status: :unprocessable_entity
      return
    end

    response = self.class.put("/place-order",
                               body: {
                                 bookId: params[:book_id],
                                 quantity: params[:quantity]
                               }.to_json,
                               headers: { 'Content-Type' => 'application/json' }
    )

    if response.success?
      book = JSON.parse(response.body)
      order = Order.find_or_create_by(user_id: params[:user_id])

      order_item = order.order_items.create(
        book_id: book['id'],
        title: book['title'],
        author: book['author'],
        price: book['price'],
        quantity: params[:quantity]
      )

      if order_item.persisted?
        render json: order
      else
        render json: { error: "Failed to create order item" }, status: :unprocessable_entity
      end
    else
      render json: { error: response.code }, status: :bad_request
    end
  end

  def update
    unless params[:book_id].present? && params[:quantity].present?
      render json: { error: "Missing required parameters" }, status: :unprocessable_entity
      return
    end

    response = self.class.put("/place-order",
                              body: {
                                bookId: params[:book_id],
                                quantity: params[:quantity]
                              }.to_json,
                              headers: { 'Content-Type' => 'application/json' }
    )

    if response.success?
      if @order_item.update(quantity: params[:quantity])
        render json: order
      else
        render json: { error: "Failed to create order item" }, status: :unprocessable_entity
      end
    else
      render json: { error: response.code }, status: :bad_request
    end
  end

  def destroy
    event_payload = {
      book_id: @order_item.book_id,
      quantity_to_restore: @order_item.quantity
    }.to_json

    order = @order_item.order
    @order_item.destroy

    order.destroy if order.order_items.empty?

    rabbitmq_service = RabbitmqEventPublisherService.new('order-deleted')
    rabbitmq_service.publish_event(event_payload)
    rabbitmq_service.close

    head :no_content
  end

  private

  def set_order_item
    @order_item = OrderItem.find(params[:id])
  end

  def order_item_params
    params.require(:order_item).permit(:book_id, :quantity, :user_id)
  end

  def publish_order_event(queue_name, message)
    connection = Bunny.new(
      hostname: ENV['RABBITMQ_HOST'],
      username: ENV['RABBITMQ_USERNAME'],
      password: ENV['RABBITMQ_PASSWORD']
    )
    connection.start
    channel = connection.create_channel
    queue = channel.queue(queue_name)

    queue.publish(message, persistent: true)

    connection.close
  end
end
