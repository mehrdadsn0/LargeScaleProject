export class OrderDetailDto{
    constructor(productId: number, productPrice: number, count: number, title: string) {
        this.productId = productId;
        this.productPrice = productPrice;
        this.count = count;
        this.productTitle = title;
    }
    productId: number | undefined;
    productPrice: number | undefined;
    productTitle: string | undefined;
    count: number | undefined;
}