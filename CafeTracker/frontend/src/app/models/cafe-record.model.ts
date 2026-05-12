import { ProductCategory } from "../product-category";

export class CafeRecord {
    id: number = 0;
    productName: string = '';
    category: string = '';
    quantity: number= 1;
    dateConsumed: string = '';
    notes?: string = '';
    dateCreated: string = '';
    dateModified: string = '';
}
