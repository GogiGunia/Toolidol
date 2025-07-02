import { Component, OnDestroy, OnInit } from '@angular/core';
import { finalize, Subject, takeUntil } from 'rxjs';
import { FacebookConnectionStatus, FacebookConnectService } from '../../external-services/facebook/facebook-connect.service';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'facebook-connect',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatSnackBarModule,
    MatTooltipModule
  ],
  templateUrl: './facebook-connect.component.html',
  styleUrl: './facebook-connect.component.scss',
})
export class FacebookConnectComponent implements OnInit, OnDestroy {
  connectionStatus: FacebookConnectionStatus | null = null;
  isLoading = false;
  loadingMessage = '';

  private destroy$ = new Subject<void>();

  constructor(
    private facebookConnectService: FacebookConnectService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadConnectionStatus();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadConnectionStatus(): void {
    this.isLoading = true;
    this.loadingMessage = 'Checking connection status...';

    this.facebookConnectService.getConnectionStatus()
      .pipe(
        finalize(() => this.isLoading = false),
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (status) => {
          this.connectionStatus = status;
          console.log('Connection status loaded:', status);
        },
        error: (error) => {
          console.error('Failed to load connection status:', error);
          this.showMessage('Failed to load connection status', 'error');
          // Set default disconnected state on error
          this.connectionStatus = { isConnected: false, pages: [], pageCount: 0 };
        }
      });
  }

  connectAccount(): void {
    this.isLoading = true;
    this.loadingMessage = 'Connecting to Facebook...';

    this.facebookConnectService.connect()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (shortLivedToken) => {
          if (!shortLivedToken) {
            console.error('Facebook SDK did not return a token.');
            this.isLoading = false;
            this.showMessage('Facebook connection was cancelled or failed', 'error');
            return;
          }

          this.loadingMessage = 'Saving connection...';

          this.facebookConnectService.sendTokenToBackend(shortLivedToken)
            .pipe(
              finalize(() => this.isLoading = false),
              takeUntil(this.destroy$)
            )
            .subscribe({
              next: (response) => {
                this.showMessage(response.message || 'Facebook account connected successfully!', 'success');
                // Refresh status to show updated connection
                this.loadConnectionStatus();
              },
              error: (error) => {
                console.error('Backend connection error:', error);
                this.showMessage(
                  error.error?.message || 'Failed to save Facebook connection',
                  'error'
                );
              }
            });
        },
        error: (error) => {
          console.error('Facebook SDK error:', error);
          this.isLoading = false;
          this.showMessage('Facebook connection was cancelled or failed', 'error');
        }
      });
  }

  disconnectAccount(): void {
    this.isLoading = true;
    this.loadingMessage = 'Disconnecting...';

    this.facebookConnectService.disconnect()
      .pipe(
        finalize(() => this.isLoading = false),
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (response) => {
          this.showMessage(response.message || 'Facebook account disconnected successfully!', 'success');
          // Update status to show disconnected state
          this.connectionStatus = { isConnected: false, pages: [], pageCount: 0 };
        },
        error: (error) => {
          console.error('Disconnect error:', error);
          this.showMessage(
            error.error?.message || 'Failed to disconnect Facebook account',
            'error'
          );
        }
      });
  }

  refreshStatus(): void {
    this.loadConnectionStatus();
  }

  private showMessage(message: string, type: 'success' | 'error'): void {
    this.snackBar.open(message, 'Close', {
      duration: type === 'success' ? 3000 : 5000,
      panelClass: type === 'success' ? 'success-snackbar' : 'error-snackbar'
    });
  }
}
