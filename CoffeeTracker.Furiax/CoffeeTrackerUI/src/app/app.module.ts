import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoffeeRecordComponent } from './coffee-record/coffee-record.component';
import { HttpClientModule } from '@angular/common/http';
import { CoffeeDetailComponent } from './coffee-detail/coffee-detail.component';
import { AddCoffeeComponent } from './add-coffee/add-coffee.component';

@NgModule({
  declarations: [
    AppComponent,
    CoffeeRecordComponent,
    CoffeeDetailComponent,
    AddCoffeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
