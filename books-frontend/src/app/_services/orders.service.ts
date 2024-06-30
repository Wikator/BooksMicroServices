import { Injectable } from '@angular/core';
import {environment} from "../../environments/environemnt";
import {HttpClient} from "@angular/common/http";
import OrderItem from "../_models/order-item";
import {OrderUpsert} from "../_models/order-upsert";

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  private baseUrl = environment.ordersApiUrl + 'order_items/';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<OrderItem[]>(this.baseUrl);
  }

  create(order: OrderUpsert) {
    // const pascalCaseData = this.convertKeysToPascalCase(order);
    return this.http.post<OrderItem>(this.baseUrl, order);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }


  // private toPascalCase(str: string): string {
  //   return str.replace(/(^\w|_\w)/g, match => match.replace('_', '').toUpperCase());
  // }
  //
  // private convertKeysToPascalCase(obj: any): any {
  //   if (Array.isArray(obj)) {
  //     return obj.map(value => this.convertKeysToPascalCase(value));
  //   } else if (obj !== null && typeof obj === 'object') {
  //     return Object.keys(obj).reduce((result, key) => {
  //       const pascalKey = this.toPascalCase(key);
  //       result[pascalKey] = this.convertKeysToPascalCase(obj[key]);
  //       return result;
  //     }, {} as any);
  //   }
  //   return obj;
  // }
}
