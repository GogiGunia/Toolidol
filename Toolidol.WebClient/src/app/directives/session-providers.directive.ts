import { Directive, OnDestroy, OnInit } from '@angular/core';
import { sessionProviders } from '../../service-provider-map';

@Directive({
  selector: '[sessionProviders]',
  providers: [sessionProviders]
})
export class SessionProvidersDirective implements OnInit, OnDestroy {

  private static exists = false;

  public ngOnInit(): void {
    if (SessionProvidersDirective.exists) {
      // This check ensures that your session scope is only created once.
      console.error("Error: Only one SessionProviderDirective should exist at any time.");
      throw new Error("Only one SessionProviderDirective should exist at all times");
    }
    SessionProvidersDirective.exists = true;
    console.log('SessionProviderDirective Initialized: Session services are now available.');
  }

  public ngOnDestroy(): void {
    SessionProvidersDirective.exists = false;
    console.log('SessionProviderDirective Destroyed: Session services have been cleaned up.');
  }

}
