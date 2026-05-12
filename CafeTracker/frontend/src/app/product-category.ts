export enum ProductCategory {
    None = 0,
    Coffee = 1,
    Tea = 2,
    HotChocolate = 3,
    ColdDrink = 4,
    Pastry = 5,
    Sandwich = 6,
    Cake = 7,
    Other = 8
}

export const ProductCategoryLabels: Record<ProductCategory, string> = {
  [ProductCategory.None]: 'None',
  [ProductCategory.Coffee]: 'Coffee',
  [ProductCategory.Tea]: 'Tea',
  [ProductCategory.HotChocolate]: 'Hot Chocolate',
  [ProductCategory.ColdDrink]: 'Cold Drink',
  [ProductCategory.Pastry]: 'Pastry',
  [ProductCategory.Sandwich]: 'Sandwich',
  [ProductCategory.Cake]: 'Cake',
  [ProductCategory.Other]: 'Other'
};

export const ProductCategoryOptions = Object.entries(ProductCategory)
  .filter(([, value]) => typeof value === 'number' && value !== 0)
  .map(([key, value]) => ({
    value: value as ProductCategory,
    label: ProductCategoryLabels[value as ProductCategory] ?? key
  }));