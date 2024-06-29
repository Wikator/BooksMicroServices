import {Component, OnInit} from '@angular/core';
import Book from "../../_models/book";
import {BookService} from "../../_services/book.service";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  books: Book[] = [];

  constructor(private booksService: BookService) { }

  ngOnInit() {
    this.booksService.getBooks().subscribe((books: Book[]) => {
      this.books = books;
    });
  }
}
