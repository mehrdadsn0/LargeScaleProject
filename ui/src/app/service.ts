import { Injectable } from '@angular/core';
import { Product } from './models/product';
import { HttpClient, } from '@angular/common/http';
import { AddProductDto } from './models/addProductDto';
import { SignUpDto } from './models/signupDto';
import { TokenResult } from './models/tokenResult';
import { SignInDto } from './models/signinDto';
import { UserResult } from './models/userResult';
import { AddOrderDto } from './models/addorderDto';
import { OrderResult } from './models/orderResult';
import { AddPaymentDto } from './models/addPaymentDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Service {
  productsApiUrl: string = "http://localhost:8202/product"
  authApiUrl: string = "http://localhost:8201/auth"
  orderApiUrl: string = "http://localhost:8203/order"
  paymentApiUrl: string = "http://localhost:8204/payment"
  constructor(private http: HttpClient) { }

  getProducts(): Promise<Product[]>{
    return this.http.get<Product[]>(this.productsApiUrl).toPromise();
  }

  addProduct(product: AddProductDto): Promise<Product>{
    return this.http.post<Product>(this.productsApiUrl + "/addproduct", product).toPromise();
  }

  signup(SignUpDto: SignUpDto): Promise<TokenResult> {
    return this.http.post<TokenResult>(this.authApiUrl + "/signup", SignUpDto).toPromise();
  }
  signin(SignInDto: SignInDto): Promise<TokenResult> {
    return this.http.post<TokenResult>(this.authApiUrl + "/signin", SignInDto).toPromise();
  }

  getUser(): Promise<UserResult> {
    return this.http.get<UserResult>(this.authApiUrl + "/getuserbytoken/" + localStorage.getItem("acc")).toPromise()
  }

  addOrder(addorderDto: AddOrderDto): Promise<any>{
    return this.http.post(this.orderApiUrl, addorderDto).toPromise()
  }

  getOrders() {
    return this.http.get<OrderResult[]>(this.orderApiUrl).toPromise();
  }

  addpayment(addpaymentdto: AddPaymentDto): Promise<any> {
    return this.http.post(this.paymentApiUrl, addpaymentdto).toPromise();
  }
}
