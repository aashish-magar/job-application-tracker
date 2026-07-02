import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { LoginRequest, LoginResponse } from '../../features/auth/login/model';
import { Observable } from 'rxjs';
import { RegisterRequest, RegisterResponse } from '../../features/auth/register/register-model';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private apiUrl = "https://localhost:7240/api/Auth"
  constructor(private http: HttpClient) {}
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  LoginUser(user:LoginRequest):Observable<LoginResponse> {
    return this.http.post<LoginResponse> (this.apiUrl + "/Login", user)
  } 
  RegisterUser(user:RegisterRequest):Observable<RegisterResponse> {
    return this.http.post<RegisterResponse> (this.apiUrl + "/Register", user)
  }
}
