import {Component, Input} from '@angular/core';
import {CompanyDto} from '../../../core/models/company.dto';

@Component({
  selector: 'app-company-card',
  imports: [],
  templateUrl: './company-card.html',
  standalone: true,
  styleUrl: './company-card.scss'
})
export class CompanyCardComponent {
  @Input() company!: CompanyDto;
}
