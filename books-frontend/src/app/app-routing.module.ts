import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BookListComponent} from "./books/book-list/book-list.component";
import {BookNewComponent} from "./books/book-new/book-new.component";

const routes: Routes = [
  { path: '', component: BookListComponent },
  { path: 'books/new', component: BookNewComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
