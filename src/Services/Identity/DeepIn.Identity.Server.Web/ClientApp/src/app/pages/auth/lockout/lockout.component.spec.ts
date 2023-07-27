/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LockoutComponent } from './lockout.component';

describe('LockoutComponent', () => {
  let component: LockoutComponent;
  let fixture: ComponentFixture<LockoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LockoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LockoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
