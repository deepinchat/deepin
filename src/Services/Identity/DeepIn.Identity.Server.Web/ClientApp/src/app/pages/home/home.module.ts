import { NgModule } from '@angular/core';
import { HomeComponent } from './home.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageServiceModule } from 'ng-zorro-antd/message';

@NgModule({
  imports: [
    NzButtonModule,
    NzMessageServiceModule,
    SharedModule,
    HomeRoutingModule
  ],
  declarations: [HomeComponent]
})
export class HomeModule { }
