import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent } from './components/products/products.component';
import { FingerprintService } from './services/fingerprint-service.service';
import { ButtonColorService } from './services/button-color.service';
import { HttpClientModule } from '@angular/common/http';
import { ResultsTableComponent } from './components/results-table/results-table.component'; // HERE
import { UserDataService } from './services/user-data.service';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    ResultsTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [
    FingerprintService, 
    ButtonColorService,
    UserDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
