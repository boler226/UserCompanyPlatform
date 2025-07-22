import {Component, inject, OnInit} from '@angular/core';
import {CompanyService} from '../../core/services/company.service';
import {CompanyDto} from '../../core/models/company.dto';
import {CommonModule} from '@angular/common';
import {CompanyCardComponent} from './company-card/company-card';
import { ToolbarLayoutComponent} from '../../layout/toolbar-layout/toolbar-layout';
import {SortLayoutComponent} from '../../layout/sort-layout/sort-layout';

@Component({
  selector: 'app-companies',
  imports: [CommonModule, CompanyCardComponent, ToolbarLayoutComponent, SortLayoutComponent],
  templateUrl: './companies.html',
  standalone: true,
  styleUrl: './companies.scss'
})
export class CompaniesComponent implements OnInit {
  private companiesService = inject(CompanyService);
  allCompanies: CompanyDto[] = [];
  filteredCompanies: CompanyDto[] = [];
  searchText = '';


  ngOnInit() {
    this.companiesService.getAll().subscribe({
      next: (data) => {
        this.allCompanies = data;
        this.filteredCompanies = data;
      },
      error: err => {
        console.error('Помилка при завантаженні компаній:', err);
      }
    });
  }

  onSearch(query: string) {
    this.filteredCompanies = this.allCompanies.filter(c =>
      c.name.toLowerCase().includes(query.toLowerCase())
    );
    this.searchText = query;
  }

  onSort(value: string) {
    if (value === "none") {
      this.filteredCompanies = [...this.allCompanies];
      this.onSearch(this.searchText);
      return;
    }
    if (value === 'name_asc') {
      this.filteredCompanies = [...this.filteredCompanies].sort((a, b) =>
        a.name.localeCompare(b.name)
      );
    } else if (value === 'name_desc') {
      this.filteredCompanies = [...this.filteredCompanies].sort((a, b) =>
        b.name.localeCompare(a.name)
      );
    }
  }
}
