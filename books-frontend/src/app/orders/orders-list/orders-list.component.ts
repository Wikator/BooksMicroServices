import {Component, OnInit} from '@angular/core';
import OrderItem from "../../_models/order-item";
import {OrdersService} from "../../_services/orders.service";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css'
})
export class OrdersListComponent implements OnInit {
  orders: OrderItem[] = [];

  constructor(private ordersService: OrdersService) { }

  ngOnInit() {
    this.ordersService.getAll().subscribe({
      next: orders => this.orders = orders,
      error: err => console.log(err)
    });
  }

  delete(id: number) {
    this.ordersService.delete(id).subscribe(_ =>
      this.orders = this.orders.filter(o => o.id !== id));
  }
}
