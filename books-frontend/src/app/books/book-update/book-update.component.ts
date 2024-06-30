import {Component, OnInit} from '@angular/core';
import BookUpsert from "../../_models/book-upsert";
import {BookService} from "../../_services/book.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-book-update',
  templateUrl: './book-update.component.html',
  styleUrl: './book-update.component.css'
})
export class BookUpdateComponent implements OnInit {
  initialBook: BookUpsert | undefined;
  id: number | undefined;

  constructor(private bookService: BookService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.bookService.getBookById(params['id']).subscribe(book => {
        console.log(book)
        if (!book)
          this.router.navigateByUrl('/');
        else
          this.initialBook = book;
      });
    })
  }

  updateBook(book: BookUpsert) {
    if (!this.id)
      return;

    this.bookService.updateBook(this.id, book).subscribe(() => {
      this.router.navigate(['/books']);
    });
  }
}
