export class CoffeeRecords {
    public id: number;
    public quantity: number;
    public date: Date;

    constructor(id: number, quantity: number, date: Date) {
        this.id = id;
        this.quantity = quantity;
        this.date = date;
    }
}
