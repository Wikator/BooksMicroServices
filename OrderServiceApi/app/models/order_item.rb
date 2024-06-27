class OrderItem < ApplicationRecord
  belongs_to :order

  validates :order_id, :book_id, :title, :author, :price, :quantity, presence: true

  after_save :update_order_total
  after_destroy :update_order_total

  private

  def update_order_total
    order.recalculate_total_amount
  end
end
