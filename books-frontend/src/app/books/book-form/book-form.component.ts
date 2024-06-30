import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import Book from "../../_models/book";
import BookUpsert from "../../_models/book-upsert";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent implements OnInit {
  @Input() initialBook: BookUpsert = { title: '', author: '',  price: 0, stock: 0 };
  @Output() onSubmit = new EventEmitter<BookUpsert>();

  constructor(private fb: FormBuilder) { }

  bookForm: FormGroup = {} as FormGroup;

  ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.bookForm = this.fb.group({
      title: [this.initialBook.title, Validators.required],
      author: [this.initialBook.author, Validators.required],
      price: [this.initialBook.price],
      stock: [this.initialBook.stock]
    });
  }

  submit() {
    const bookUpsert = this.bookForm.value as BookUpsert;
    this.onSubmit.emit(bookUpsert);
  }
}
