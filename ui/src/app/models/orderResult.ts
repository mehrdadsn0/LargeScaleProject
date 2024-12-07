import { OrderDetailDto } from "./orderDetailDto";

export class OrderResult{
    constructor() {
        this.orderDetails = new Array<OrderDetailDto>();
    }
    id: number | undefined;
    userId : number | undefined;
    totalPrice : number | undefined;
    orderDetails : OrderDetailDto[] | undefined;
}