import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { MessageClient, API_BASE_URL } from './chat-api-client.service';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    MessageClient,
    { provide: API_BASE_URL, useValue: environment.API_BASE_URL}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
