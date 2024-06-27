class CreateOrderItems < ActiveRecord::Migration[7.1]
  def change
    create_table :order_items do |t|
      t.references :order, null: false, foreign_key: true
      t.string :book_id
      t.string :title
      t.string :author
      t.decimal :price
      t.integer :quantity

      t.timestamps
    end
  end
end
