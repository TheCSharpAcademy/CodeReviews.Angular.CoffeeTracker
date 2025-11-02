export interface SaleDto {
    id: number; 
    dateAndTimeOfSale: Date | null;
    total: number;
    coffeeName: string;
    coffeeId: number;
}

export interface CreateSaleDto {
    coffeeId: number;
    dateAndTimeOfSale?: string | null;
}

export interface UpdateSaleDto {
    coffeeId?: number;
    dateAndTimeOfSale?: string | null;
}

export interface PagedResponse<T> {
  data: T;
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  status: string;
  message?: string;
}