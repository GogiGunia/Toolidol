import { TestBed } from '@angular/core/testing';

import { FacebookConnectService } from './facebook-connect.service';

describe('FacebookConnectService', () => {
  let service: FacebookConnectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FacebookConnectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
