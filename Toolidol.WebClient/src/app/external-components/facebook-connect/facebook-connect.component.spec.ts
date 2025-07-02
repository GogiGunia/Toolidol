import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacebookConnectComponent } from './facebook-connect.component';

describe('FacebookConnectComponent', () => {
  let component: FacebookConnectComponent;
  let fixture: ComponentFixture<FacebookConnectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FacebookConnectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FacebookConnectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
