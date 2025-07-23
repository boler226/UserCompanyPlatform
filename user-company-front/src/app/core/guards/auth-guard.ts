import {CanActivate, Router, UrlTree} from '@angular/router';
import {AuthService} from '../services/auth.service';
import {map, Observable} from 'rxjs';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean | UrlTree> {
    return this.authService.isLoggedIn$.pipe(
      map(isLoggedIn => {
        return isLoggedIn ? true : this.router.createUrlTree(['/login']);
      })
    );
  }
}
