import { OrderDetailDto } from "./orderDetailDto";

export class AddOrderDto{
    constructor() {
        this.orderDetails = new Array<OrderDetailDto>();
    }
    orderDetails: OrderDetailDto[] | undefined;
    userId: number | undefined;
}