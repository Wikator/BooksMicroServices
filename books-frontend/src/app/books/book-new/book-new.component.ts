import { Component } from '@angular/core';
import {BookService} from "../../_services/book.service";
import {Router} from "@angular/router";
import BookUpsert from "../../_models/book-upsert";

@Component({
  selector: 'app-book-new',
  templateUrl: './book-new.component.html',
  styleUrl: './book-new.component.css'
})
export class BookNewComponent {

  constructor(private bookService: BookService,
              private router: Router) { }


  addBook(book: BookUpsert) {
    this.bookService.addBook(book).subscribe({
      next: () => this.router.navigateByUrl('/'),
      error: (error) => console.error(error)
    })
  }
}
