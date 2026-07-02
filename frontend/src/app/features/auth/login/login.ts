import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule,Validators,FormGroup,FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from './model';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  constructor( private loginService:Auth,private router:Router){
   
  }
  errorMessage:string = '';
  
  loginForm = new FormGroup(
    {
      email:new FormControl('',[Validators.required,Validators.email]),
      password:new FormControl('',[Validators.required,Validators.minLength(6)])
    }
  )

  onLogin(){
    if(this.loginForm.invalid){
      console.log("Form is invalid");
    }
    const loginRequest:LoginRequest = this.loginForm.value as LoginRequest;
    this.loginService.LoginUser(loginRequest).subscribe({
      next:(response)=>{
        console.log("Login successful",response);
        localStorage.setItem('user', JSON.stringify(response.email));
        localStorage.setItem('token', response.token);
        this.router.navigate(['']);
      },
      error:(error)=>{
        console.log(error.backendError);
        console.error("Login failed",error.error?.error);
        this.errorMessage = error.error?.error || 'An error occurred during login.';
      }
    })
  }
}
