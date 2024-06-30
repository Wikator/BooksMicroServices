import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from "@angular/common/http";
import { BookListComponent } from './books/book-list/book-list.component';
import { BookFormComponent } from './books/book-form/book-form.component';
import {ReactiveFormsModule} from "@angular/forms";
import { BookNewComponent } from './books/book-new/book-new.component';
import { BookUpdateComponent } from './books/book-update/book-update.component';
import { OrdersListComponent } from './orders/orders-list/orders-list.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookFormComponent,
    BookNewComponent,
    BookUpdateComponent,
    OrdersListComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        ReactiveFormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
