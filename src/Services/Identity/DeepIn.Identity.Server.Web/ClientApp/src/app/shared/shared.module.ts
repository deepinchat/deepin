import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService } from './services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { ModelStateErrorComponent } from './components/model-state-error/model-state-error.component';

const SHARDD_MODULES = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule,
  NzAlertModule
];
const SHARDD_COMPONENTS = [
  ModelStateErrorComponent
];

@NgModule({
  imports: [
    SHARDD_MODULES
  ],
  declarations:[
    SHARDD_COMPONENTS
  ],
  exports: [
    SHARDD_MODULES,
    SHARDD_COMPONENTS
  ],
  providers: [
    UserService
  ]
})
export class SharedModule { }
