import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SPA } from './SPA';

@NgModule({
    imports: [BrowserModule, ReactiveFormsModule, HttpClientModule],
    declarations: [SPA],
    bootstrap: [SPA]
})

export class AppModule { }
