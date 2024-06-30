import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BookListComponent} from "./books/book-list/book-list.component";
import {BookNewComponent} from "./books/book-new/book-new.component";
import {BookUpdateComponent} from "./books/book-update/book-update.component";

const routes: Routes = [
  { path: '', component: BookListComponent },
  { path: 'books/new', component: BookNewComponent },
  { path: 'books/update/:id', component: BookUpdateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
