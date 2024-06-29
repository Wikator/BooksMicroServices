import { Injectable } from '@angular/core';
import {environment} from "../../environments/environemnt";
import {HttpClient} from "@angular/common/http";
import Book from "../_models/book";
import BookUpsert from "../_models/bookUpsert";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private baseUrl = environment.booksApiUrl + 'books/';

  constructor(private httpClient: HttpClient) { }

  getBooks() {
    return this.httpClient.get<Book[]>(this.baseUrl);
  }

  getBookById(id: number) {
    return this.httpClient.get<Book>(this.baseUrl + id);
  }

  addBook(book: BookUpsert) {
    return this.httpClient.post<Book>(this.baseUrl, book);
  }

  updateBook(id: number, book: BookUpsert) {
    return this.httpClient.put<Book>(this.baseUrl + id, book);
  }

  deleteBook(id: number) {
    return this.httpClient.delete(this.baseUrl + id);
  }
}
