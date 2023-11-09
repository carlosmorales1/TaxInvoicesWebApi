import { Component, OnInit } from '@angular/core';
import { InvoiceESInfo, ProductsModel } from './models-search-invoice';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { integerValidator } from '../utils';
import { ApiService } from '../api.service';
import { CustomerESModel, CustomerModel } from '../create-invoice/models-invoice';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-search-invoice-component',
  templateUrl: './search-invoice.component.html',
  styleUrls: ['./search-invoice.component.css']
})
export class SearchInvoiceComponent implements OnInit{

  constructor(private apiService: ApiService) {}

  radioOptions = [
    { value: 'customer', label: 'Cliente' },
    { value: 'numberInvoice', label: 'Número de Factura' }
  ];

  selectedOption: string = 'customer';
  customers: CustomerModel[] = [];
  products: ProductsModel[] = [];

  myForm = new FormGroup({
    invoiceNumber: new FormControl("", [Validators.required, Validators.min(1), integerValidator()]),
    customerId: new FormControl("", [Validators.required])
  });

  ngOnInit() {
    this.getCustomer();
  }

  onRadioChange(option: string) {
    this.selectedOption = option;
  }

  getCustomer() {
    this.apiService.get('customer').subscribe((data: CustomerESModel[]) => {
      this.customers = data.map((customer: CustomerESModel) => {
        return {
          id: customer.id,
          businessName: customer.razonSocial,
        };
      });
    });
  }
  
  public search() {
    const formData = this.myForm.value;
    var url = 'bills';
    if(this.selectedOption == 'customer'){
      if(formData.customerId == ""){
        Swal.fire('Selecciona un cliente', '', 'info');
        return;
      }
      url += "?cliente="+formData.customerId;
    } else {
      if(formData.invoiceNumber == ""){
        Swal.fire('Digita un número de factura', '', 'info');
        return;
      }
      url += "?numero="+formData.invoiceNumber;
    }
    this.apiService.get(url).subscribe((data: InvoiceESInfo[]) => {
      this.products = data.map((product: InvoiceESInfo) => {
        return {
          numberInvoice: product.numeroFactura,
          dateInvoice: product.fechaEmisionFactura,
          totalInvoice: product.totalFactura
        };
      });
    });
  }

  formatPrice(price: number): string {
    return price.toLocaleString('es-CO', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 });
  }
}
