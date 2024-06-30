export default interface OrderItem {
  id: number;
  orderId: number;
  bookId: string;
  title: string;
  author: string;
  price: number;
  quantity: number;
}
