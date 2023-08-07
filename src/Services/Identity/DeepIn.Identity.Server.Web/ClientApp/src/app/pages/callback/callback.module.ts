import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CallbackComponent } from './callback.component';
import { RouterModule } from '@angular/router';
import { CallbackRoutingModule } from './callback-routing.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    CallbackRoutingModule
  ],
  declarations: [CallbackComponent]
})
export class CallbackModule { }
