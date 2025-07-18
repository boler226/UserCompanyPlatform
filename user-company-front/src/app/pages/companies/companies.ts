import {Component, inject, OnInit} from '@angular/core';
import {CompanyService} from '../../core/services/company.service';
import {CompanyDto} from '../../core/models/company.dto';
import {CommonModule} from '@angular/common';
import {CompanyCardComponent} from './company-card/company-card';

@Component({
  selector: 'app-companies',
  imports: [CommonModule, CompanyCardComponent],
  templateUrl: './companies.html',
  standalone: true,
  styleUrl: './companies.scss'
})
export class CompaniesComponent implements OnInit {
  private companiesService = inject(CompanyService);
companies: CompanyDto[] = [];

  ngOnInit() {
    this.companiesService.getAll().subscribe({
      next: (data) => {
        this.companies = data;
      },
      error: err => {
        console.error('Помилка при завантаженні компаній:', err);
      }
    });
  }
}
