import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginDto} from '../dto/login.dto';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {RegisterDto} from '../dto/register.dto';
import {TokenService} from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);
  private readonly apiUrl = 'http://localhost:5000/api/Users';

  private isLoggedInSubject = new BehaviorSubject<boolean>(this.tokenService.hasToken());
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  login(data: LoginDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/Login/login`, data).pipe(
      tap((token: string) => {
        this.tokenService.setToken(token);
        this.isLoggedInSubject.next(true);
      })
    );
  }

  register(data: RegisterDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/Register/register`, data).pipe(
      tap((token: string) => {
        this.tokenService.setToken(token);
        this.isLoggedInSubject.next(true);
      })
    );
  }

  logout() {
    this.tokenService.clearToken();
    this.isLoggedInSubject.next(false);
  }

  isLoggedIn(): boolean {
    return this.isLoggedInSubject.value;
  }
}
