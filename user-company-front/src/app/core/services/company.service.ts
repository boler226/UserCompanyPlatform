import { inject, Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CompanyDto} from '../models/company.dto';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5001/api/Companies';

  getAll(): Observable<CompanyDto[]> {
    return this.http.get<CompanyDto[]>(this.apiUrl + "/GetAll");
  }
}
