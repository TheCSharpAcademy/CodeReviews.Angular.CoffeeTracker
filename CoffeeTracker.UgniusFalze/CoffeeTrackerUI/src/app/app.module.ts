import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoffeeRecordsComponent } from './coffee-record/coffee-records.component';
import { HttpClientModule } from '@angular/common/http';
import { CoffeeRecordEditComponent } from './coffee-record-edit/coffee-record-edit.component';
import { FormsModule } from '@angular/forms';
import { TableFilterComponent } from './table-filter/table-filter.component';

@NgModule({
  declarations: [
    AppComponent,
    CoffeeRecordsComponent,
    CoffeeRecordEditComponent,
    TableFilterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
