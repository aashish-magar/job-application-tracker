import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Auth } from '../../../core/services/auth';
import { RegisterRequest } from './register-model';
import { Router } from '@angular/router';

// Cross-field validator: compares two sibling controls on the group
function passwordsMatch(group: AbstractControl): ValidationErrors | null {
  const password = group.get('password')?.value;
  const confirmPassword = group.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { passwordsMismatch: true };
}

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  constructor(private authService: Auth, private router: Router) {}

  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    confirmPassword: new FormControl('', [Validators.required]),
  }, { validators: passwordsMatch }); // <-- validator on the GROUP, not a single control

  onSubmit() {
    if (this.registerForm.invalid) {
      console.log("Form is invalid");
      return;
    }

    // Strip confirmPassword before sending — backend only expects RegisterRequest's shape
    const { confirmPassword, ...formValue } = this.registerForm.value;
    const registerRequest: RegisterRequest = formValue as RegisterRequest;

    this.authService.RegisterUser(registerRequest).subscribe({
      next: (response) => {
        console.log("Registration successful", response);
      },
      error: (error) => {
        console.error("Registration failed", error.error?.error);
      }
    });
  }
}