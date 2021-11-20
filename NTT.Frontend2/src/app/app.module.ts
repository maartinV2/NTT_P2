import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';

import { AuthService } from './services/auth.service';
import { UserComponent } from './components/user/user.component';
import { PostDetailsComponent } from './components/post-details/post-details.component';
import { PostComponent } from './components/post/post.component';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Router } from '@angular/router';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UserComponent,
    PostDetailsComponent,
    PostComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,

    MatInputModule,
    FormsModule,
    ReactiveFormsModule,

    MatFormFieldModule,
    MatNativeDateModule,
    MatDatepickerModule,

    BrowserAnimationsModule,




  ],
  providers: [
    AuthService,
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
