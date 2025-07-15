import {Component, inject} from '@angular/core';
import {AuthService} from '../../../core/services/auth.service';
import {TokenService} from '../../../core/services/token.service';
import {LoginDto} from '../../../core/models/login.dto';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login-form',
  imports: [
    FormsModule
  ],
  templateUrl: './login-form.html',
  standalone: true,
  styleUrl: '../auth.scss'
})
export class LoginFormComponent {
  email = '';
  password = '';
  private authService = inject(AuthService);
  private tokenService = inject(TokenService);
  private router = inject(Router);

  onLogin() {
    const dto: LoginDto = {
      email: this.email,
      password: this.password
    };

    this.authService.login(dto).subscribe((token) => {
      this.tokenService.setToken(token);
      this.router.navigate(['/home']);
    });
  }
}
