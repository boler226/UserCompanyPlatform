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
  private readonly apiUrl = 'http://localhost:5000/api/Users';

  login(data: LoginDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/Login/login`, data);
  }

  register(data: RegisterDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/Register/register`, data);
  }
}
