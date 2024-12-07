export class AddProductDto{

    constructor(title: string, price: number) {
        this.Title = title;
        this.Price = price;
    }

    Title: string | undefined;
    Price: number | undefined;
}