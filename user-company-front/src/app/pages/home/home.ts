import { Component } from '@angular/core';
import {CompaniesComponent} from '../companies/companies';

@Component({
  selector: 'app-home',
  imports: [
    CompaniesComponent
  ],
  templateUrl: './home.html',
  standalone: true,
  styleUrl: './home.css'
})
export class HomeComponent {

}
