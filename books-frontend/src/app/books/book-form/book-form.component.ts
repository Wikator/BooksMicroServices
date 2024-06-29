import {Component, EventEmitter, Input, Output} from '@angular/core';
import Book from "../../_models/book";
import BookUpsert from "../../_models/bookUpsert";
import {FormBuilder, Validators} from "@angular/forms";

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent {
  @Input() initialBook: Book = { id: '', title: '', author: '',  price: 0, stock: 0 };
  @Output() onSubmit = new EventEmitter<BookUpsert>();

  constructor(private fb: FormBuilder) { }

  bookForm = this.fb.group({
    title: [this.initialBook.title, Validators.required],
    author: [this.initialBook.author, Validators.required],
    price: [this.initialBook.price],
    stock: [this.initialBook.stock]
  });

  submit() {
    const bookUpsert = this.bookForm.value as BookUpsert;
    this.onSubmit.emit(bookUpsert);
  }
}
