import { Component } from '@angular/core';
import { FacebookConnectComponent } from '../../external-components/facebook-connect/facebook-connect.component';

@Component({
  selector: 'app-app-connections',
  imports: [FacebookConnectComponent],
  templateUrl: './app-connections.component.html',
  styleUrl: './app-connections.component.scss'
})
export class AppConnectionsComponent {

}
