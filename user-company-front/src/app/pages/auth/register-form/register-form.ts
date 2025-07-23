import {Component, inject} from '@angular/core';
import {RegisterDto} from '../../../core/dto/register.dto';
import {AuthService} from '../../../core/services/auth.service';
import {TokenService} from '../../../core/services/token.service';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-form',
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './register-form.html',
  standalone: true,
  styleUrl: '../auth.scss'
})
export class RegisterFormComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private tokenService = inject(TokenService);
  private router = inject(Router);


  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    firstname: ['', Validators.required],
    lastname: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required],
  });

  onRegister() {
    if (this.form.invalid)
      return;

    const { email, firstname, lastname, password, confirmPassword } = this.form.value;

    if (password !== confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    const dto: RegisterDto = {
      email: email!,
      firstname: firstname!,
      lastname: lastname!,
      password: password!
    };

    this.authService.register(dto).subscribe((token) => {
      this.tokenService.setToken(token);
      this.router.navigate(['/home']);
    });
  }
}
