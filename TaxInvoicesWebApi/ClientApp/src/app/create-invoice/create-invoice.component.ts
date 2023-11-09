import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ApiService } from '../api.service';
import { CartProductModel, FixedProductModel, ProductsESModel, CustomerESModel, CustomerModel, InvoicePost, InvoiceProductPost } from './models-invoice';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { integerValidator } from '../utils';

@Component({
  selector: 'app-create-invoice',
  templateUrl: './create-invoice.component.html',
  styleUrls: ['./create-invoice.component.css']
})

export class CreateInvoiceComponent implements OnInit{
  constructor(private apiService: ApiService) {}

  fixedProducts: FixedProductModel[] = [];
  customers: CustomerModel[] = [];

  //Datos
  cartProducts: CartProductModel[] = []; 

  myForm = new FormGroup({
    invoiceNumber: new FormControl("", [Validators.required, Validators.min(1), integerValidator()]),
    customerId: new FormControl("", [Validators.required])
  });

  ngOnInit() {
    this.getCustomer();
    this.getProductos();
  }

  getProductos() {
    this.apiService.get('products').subscribe((data: ProductsESModel[]) => {
      this.fixedProducts = data.map((product: ProductsESModel) => {
        return {
          id: product.id,
          name: product.nombreProducto,
          unitPrice: product.precioUnitario,
          imageUrl: product.imagenProducto
        };
      });
    });
  }

  getCustomer() {
    this.apiService.get('customer').subscribe((data: CustomerESModel[]) => {
      this.customers = data.map((product: CustomerESModel) => {
        return {
          id: product.id,
          businessName: product.razonSocial,
        };
      });
    });
  }

  clearCart() {
    this.myForm.reset();
    if (this.cartProducts.length > 0) {
      Swal.fire({
        title: 'Limpiar Carrito',
        text: '¿Estás seguro de que deseas eliminar todos los productos del carrito?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, Limpiar',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.isConfirmed) {
          this.cartProducts = [];
          Swal.fire('Carrito limpiado', '', 'success');
        }
      });
    } else {
      Swal.fire('Carrito Vacío', 'El carrito ya está vacío.', 'info');
    }
  }

  addProductToCart() {
    this.cartProducts.push({ quantity: 1, total: 0 });
  }

  calculateSubtotal() {
    let subtotal = 0;
    for (const cartProduct of this.cartProducts) {
      if (cartProduct.product != null) {
        subtotal += cartProduct.product.unitPrice * cartProduct.quantity;
      }
    }
    return subtotal;
  }

  calculateTaxes() {
    return this.calculateSubtotal() * 0.19;
  }

  calculateTotal() {
    return this.calculateSubtotal() + this.calculateTaxes();
  }

  onProductSelected(event: any, producto: CartProductModel) {
    producto.total = 0;
    const selectedProductId = parseInt(event.target.value, 10);
    const selectedProduct = this.fixedProducts.find(product => product.id === selectedProductId);
    producto.product = selectedProduct;
    producto.total = producto.quantity * selectedProduct!.unitPrice;
  }

  changeQuantity(event: any, producto: CartProductModel) {
    const quantity = parseInt(event.target.value, 10);
    producto.quantity = quantity;
    if (producto.product != null ) {
      producto.total = quantity * producto.product.unitPrice;
    }
  }

  formatPrice(price: number): string {
    return price.toLocaleString('es-CO', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 });
  }

  saveInvoice() {
    if(this.myForm.valid){
      if(this.cartProducts.length < 1){
        Swal.fire('Carrito Vacío', '','info');
      } else {
        const formData = this.myForm.value;

        var products: InvoiceProductPost[] = [];
        this.cartProducts.forEach(element => {
          products.push({
            ProductId: element.product?.id!,
            Quantity: element.quantity
          })
        });

        const params: InvoicePost = {
          CustomerId: parseInt(formData.customerId!),
          InvoiceNumber: parseInt(formData.invoiceNumber!),
          Products: products
        }
        this.apiService.post('bills', params).subscribe((data: boolean) => {
          if(data){
            Swal.fire('Factura guardada exitosamente', '', 'success');
            this.cartProducts = [];
          } else {
            Swal.fire('No se pudo guardar la factura', '', 'error');
          }
        });
      }
    }
  }
}

