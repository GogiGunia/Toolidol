import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppConnectionsComponent } from './app-connections.component';

describe('AppConnectionsComponent', () => {
  let component: AppConnectionsComponent;
  let fixture: ComponentFixture<AppConnectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppConnectionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppConnectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
