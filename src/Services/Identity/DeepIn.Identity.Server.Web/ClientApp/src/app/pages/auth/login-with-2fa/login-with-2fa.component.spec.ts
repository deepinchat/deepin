/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LoginWith2FaComponent } from './login-with-2fa.component';

describe('LoginWith-2faComponent', () => {
  let component: LoginWith2FaComponent;
  let fixture: ComponentFixture<LoginWith2FaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LoginWith2FaComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginWith2FaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
