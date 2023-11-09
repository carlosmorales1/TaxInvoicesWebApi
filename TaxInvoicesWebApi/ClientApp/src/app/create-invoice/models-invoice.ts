export interface FixedProductModel {
    id: number;
    name: string;
    unitPrice: number,
    imageUrl: string;
}
  
export interface ProductsESModel { 
    id: number; 
    nombreProducto: string; 
    precioUnitario: number; 
    imagenProducto: string; 
}

export interface CartProductModel {
    product?: FixedProductModel;
    quantity: number;
    total: number
}

export interface CustomerESModel {
    id: number;
    razonSocial: string;
}

export interface CustomerModel {
    id: number;
    businessName: string;
}

export interface InvoicePost {
    CustomerId: number;
    InvoiceNumber: number;
    Products: InvoiceProductPost[];
  }
  
  export interface InvoiceProductPost {
    ProductId: number;
    Quantity: number;
  }