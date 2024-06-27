class Order < ApplicationRecord
  has_many :order_items, dependent: :destroy

  validates :user_id, :order_date, :total_amount, :status, presence: true

  before_validation :set_defaults

  def recalculate_total_amount
    self.total_amount = order_items.sum('price * quantity')
    save
  end

  private

  def set_defaults
    self.order_date ||= Time.current
    self.total_amount ||= 0
    self.status ||= 'pending'
  end
end
