import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivateaccountconfirmComponent } from './activateaccountconfirm.component';

describe('ActivateaccountconfirmComponent', () => {
  let component: ActivateaccountconfirmComponent;
  let fixture: ComponentFixture<ActivateaccountconfirmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActivateaccountconfirmComponent]
    });
    fixture = TestBed.createComponent(ActivateaccountconfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
