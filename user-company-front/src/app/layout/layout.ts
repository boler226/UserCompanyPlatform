import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-layout',
  imports: [
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './layout.html',
  standalone: true,
  styleUrl: './layout.scss'
})
export class LayoutComponent {

}
