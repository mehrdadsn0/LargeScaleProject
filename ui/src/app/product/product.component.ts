import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';
import { Service } from '../service';
import { AddProductDto } from '../models/addProductDto';
import { AddOrderDto } from '../models/addorderDto';
import { OrderDetailDto } from '../models/orderDetailDto';
import { OrderResult } from '../models/orderResult';
import { AddPaymentDto } from '../models/addPaymentDto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  title = 'shop';
  products: Array<Product>;
  addProductDto: AddProductDto;
  addOrderDto: AddOrderDto;
  orders: OrderResult[];

  constructor(private service: Service) {
    this.products = new Array<Product>();
    this.addProductDto = new AddProductDto("", 0);
    this.addOrderDto = new AddOrderDto();
    this.orders = new Array<OrderResult>();
  }


  ngOnInit(): void {
    this.service.getProducts().then(r => {
      this.products = r;
    });
    this.service.getOrders().then(o => {
      // this.orders = o;
      // o.map(v => {
      // })
      this.orders = o.map(parent => ({
        ...parent,
        children: parent.orderDetails!.map(child => ({
          ...child,
          productTitle: this.products.find(i => i.id == child.productId)
        }))
      }));

      console.log(this.orders)
    });
  }

  addProduct() {
    this.service.addProduct(this.addProductDto).then(s => {
      this.service.getProducts().then(r => {
        this.products = r;
      });
      console.log(s)
    });
  }

  addToCart(productId: number, price: number) {
    if (this.addOrderDto && this.addOrderDto.orderDetails && this.addOrderDto.orderDetails.find(i => i.productId == productId)) {
      let index = this.addOrderDto.orderDetails.findIndex(i => i.productId == productId)
      this.addOrderDto.orderDetails[index].count!++;
    } else {
      let detail: OrderDetailDto = new OrderDetailDto(productId, price, 1, this.products.find(p => p.id == productId)?.title!);
      this.addOrderDto.orderDetails?.push(detail)
    }
  }

  addOrder() {
    this.service.getUser().then(u => {
      this.addOrderDto.userId = u.id;
      this.service.addOrder(this.addOrderDto).then(i => {
        this.addOrderDto = new AddOrderDto();
        this.service.getOrders().then(o => {
          this.orders = o;
        });
      });
    })
  }

  payOrder(id: number, total: number) {
    this.service.addpayment(new AddPaymentDto(id, total)).then(s => {
      if (s.success) {
        alert("paid successfully")
      } else {
        alert("payment failed")
      }
    }).catch(c => {
      alert("payment failed")
    })
    
  }

}
