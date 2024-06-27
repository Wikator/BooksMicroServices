class CreateOrders < ActiveRecord::Migration[7.1]
  def change
    create_table :orders do |t|
      t.integer :user_id
      t.timestamp :order_date
      t.decimal :total_amount
      t.string :status

      t.timestamps
    end
  end
end
