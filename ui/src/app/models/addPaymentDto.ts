export class AddPaymentDto{
    constructor(orderId: number, amount: number) {
        this.OrderId = orderId;
        this.Amount = amount;
    }
    OrderId: number | undefined;
    Amount: number | undefined;
}