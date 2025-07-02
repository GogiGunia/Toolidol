import { Injectable, NgZone } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpService } from '../../core-services/data-provider/services/http.service';
import { HttpRequestOptions } from '../../core-services/data-provider/model/HttpRequestOptions';

declare const FB: any;
export interface FacebookConnectionStatus {
  isConnected: boolean;
  pages: { facebookPageId: string; name: string }[];
  pageCount: number;
}

export interface FacebookPage {
  facebookPageId: string;
  name: string;
}

@Injectable()
export class FacebookConnectService {
  private sdkReady = new ReplaySubject<void>(1);
  private isInitialized = false;

  constructor(private ngZone: NgZone, private httpService: HttpService) {
    console.log('%c[FacebookConnectService] Service initialized', 'color: purple; font-weight: bold;');
    this.initFacebookSdk();
  }

  /**
   * Get Facebook connection status from backend
   */
  public getConnectionStatus(): Observable<FacebookConnectionStatus> {
    const options = new HttpRequestOptions('Facebook/status', 'json', 'body');
    return this.httpService.Get<FacebookConnectionStatus>(options);
  }

  /**
   * Disconnect Facebook account (removes all tokens from backend)
   */
  public disconnect(): Observable<{ message: string }> {
    const options = new HttpRequestOptions('Facebook/disconnect', 'json', 'body');
    return this.httpService.Delete<{ message: string }>(options);
  }

  /**
   * Connect to Facebook and get short-lived token
   */
  public connect(): Observable<string> {
    console.log('%c[FacebookConnectService] Connect method called', 'color: blue; font-weight: bold;');

    return new Observable(observer => {
      this.waitForSDK().then(() => {
        console.log('%c[FacebookConnectService] SDK ready, attempting login', 'color: green; font-weight: bold;');

        const scope = 'public_profile,email,pages_show_list,pages_manage_posts';

        FB.login((response: any) => {
          console.log('%c[Facebook SDK] Raw Response:', 'color: blue; font-weight: bold;', response);

          this.ngZone.run(() => {
            if (response.authResponse && response.authResponse.accessToken) {
              console.log('%c[Service] Login successful. Status:', 'color: green;', response.status);

              const shortLivedToken = response.authResponse.accessToken;
              observer.next(shortLivedToken);
              observer.complete();
            } else {
              console.error('%c[Service] Login failed or was cancelled. Status:', 'color: red;', response.status);
              observer.error('Facebook login was cancelled or failed.');
            }
          });
        }, { scope });
      }).catch(error => {
        console.error('%c[FacebookConnectService] SDK initialization failed:', 'color: red; font-weight: bold;', error);
        observer.error(error);
      });
    });
  }

  /**
   * Send short-lived token to backend for processing
   */
  public sendTokenToBackend(token: string): Observable<{ message: string; pages: FacebookPage[] }> {
    const body = { shortLivedToken: token };
    const options = new HttpRequestOptions('Facebook/authenticate', 'json', 'body')
      .setBody(body);
    return this.httpService.Post<{ message: string; pages: FacebookPage[] }>(options);
  }

  private async waitForSDK(): Promise<void> {
    if (this.isInitialized) {
      return Promise.resolve();
    }

    return new Promise((resolve, reject) => {
      const timeout = setTimeout(() => {
        reject(new Error('Facebook SDK initialization timeout'));
      }, 15000);

      this.sdkReady.subscribe(() => {
        clearTimeout(timeout);
        resolve();
      });
    });
  }

  private initFacebookSdk(): void {
    console.log('%c[FacebookConnectService] Starting SDK initialization', 'color: orange; font-weight: bold;');

    if (!environment.facebookAppId) {
      console.error('%c[FacebookConnectService] Facebook App ID not found in environment!', 'color: red; font-weight: bold;');
      return;
    }

    const doInit = () => {
      console.log('%c[FacebookConnectService] Initializing FB SDK with App ID:', 'color: cyan;', environment.facebookAppId);

      try {
        FB.init({
          appId: environment.facebookAppId,
          cookie: true,
          xfbml: true,
          version: 'v23.0'
        });

        console.log('%c[FacebookConnectService] FB.init completed', 'color: green;');

        FB.getLoginStatus((response: any) => {
          console.log('%c[FacebookConnectService] Initial login status:', 'color: blue;', response);
          this.isInitialized = true;
          this.sdkReady.next();
          this.sdkReady.complete();
          console.log('%c[FacebookConnectService] SDK fully ready!', 'color: green; font-weight: bold;');
        });

      } catch (error) {
        console.error('%c[FacebookConnectService] Error during FB.init:', 'color: red; font-weight: bold;', error);
      }
    };

    if (typeof FB !== 'undefined') {
      console.log('%c[FacebookConnectService] FB object already exists', 'color: yellow;');
      doInit();
      return;
    }

    (window as any).fbAsyncInit = () => {
      console.log('%c[FacebookConnectService] fbAsyncInit callback triggered', 'color: green; font-weight: bold;');
      doInit();
    };

    const checkFB = setInterval(() => {
      if (typeof FB !== 'undefined' && !this.isInitialized) {
        console.log('%c[FacebookConnectService] FB detected via polling, initializing...', 'color: yellow;');
        clearInterval(checkFB);
        doInit();
      }
    }, 100);

    setTimeout(() => {
      clearInterval(checkFB);
      if (!this.isInitialized) {
        console.error('%c[FacebookConnectService] SDK failed to load within 10 seconds', 'color: red; font-weight: bold;');
      }
    }, 10000);
  }
}
