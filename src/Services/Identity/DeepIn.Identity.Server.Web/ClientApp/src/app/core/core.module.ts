import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { ModelStateService } from './services/model-state.service';

@NgModule({
  imports: [
    CommonModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    ModelStateService
  ]
})
export class CoreModule { }
