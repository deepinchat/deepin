import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class ModelStateService {
  private modelStateErrorSubject = new Subject<any>();
  public modelStateError = this.modelStateErrorSubject.asObservable();
  constructor() { }

  set(error: any) {
    if (error) {
      this.modelStateErrorSubject.next(error);
    }
  }

  clear() {
    this.modelStateErrorSubject.next(null);
  }

}
