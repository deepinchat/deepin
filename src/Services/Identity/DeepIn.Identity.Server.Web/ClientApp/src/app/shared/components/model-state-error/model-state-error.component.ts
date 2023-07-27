import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ModelStateService } from 'src/app/core/services/model-state.service';

@Component({
  selector: 'app-model-state-error',
  templateUrl: './model-state-error.component.html',
  styleUrls: ['./model-state-error.component.scss']
})
export class ModelStateErrorComponent implements OnInit {
  errors?: { key: string, values?: string[] }[];
  private errorChanged?: Subscription;
  constructor(
    private modelStateService: ModelStateService
  ) { }

  ngOnInit() {
    this.errorChanged = this.modelStateService.modelStateError.subscribe(res => {
      this.errors = [];
      for (const key in res) {
        if (res.hasOwnProperty(key)) {
          const element = res[key];
          this.errors.push({ key, values: element });
        }
      }
    });
  }

  ngOnDestroy(): void {
    if (this.errorChanged)
      this.errorChanged.unsubscribe();
  }
}
