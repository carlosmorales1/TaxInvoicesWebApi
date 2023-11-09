import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CreateInvoiceComponent } from './create-invoice/create-invoice.component';
import { SearchInvoiceComponent } from './search-invoice/search-invoice.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CreateInvoiceComponent,
    SearchInvoiceComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: CreateInvoiceComponent, pathMatch: 'full' },
      { path: 'search-invoice', component: SearchInvoiceComponent },
    ]),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
