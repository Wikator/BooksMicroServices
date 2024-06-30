import {Component, OnInit} from '@angular/core';
import Book from "../../_models/book";
import {BookService} from "../../_services/book.service";
import {OrdersService} from "../../_services/orders.service";
import {OrderUpsert} from "../../_models/order-upsert";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  books: Book[] = [];

  constructor(private booksService: BookService,
              private ordersService: OrdersService) { }

  ngOnInit() {
    this.booksService.getBooks().subscribe((books: Book[]) => {
      this.books = books;
    });
  }

  deleteBook(id: string) {
    this.booksService.deleteBook(id).subscribe(_ =>
      this.books = this.books.filter(b => b.id !== id));
  }

  placeOrder(bookId: string) {
    const order: OrderUpsert = { book_id: bookId, quantity: 1, user_id: 1 }
    this.ordersService.create(order).subscribe({
      next: _ => this.books = this.books.map(b => b.id !== bookId ? b : { id: b.id, author: b.author, title: b.author, price: b.price, stock: b.stock - 1} )
    })
  }
}
