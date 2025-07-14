import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginDto} from '../models/login.dto';
import {Observable} from 'rxjs';
import {RegisterDto} from '../models/register.dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private readonly apiUrl = '/api/auth';

  login(data: LoginDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/login`, data);
  }

  register(data: RegisterDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/register`, data);
  }
}
