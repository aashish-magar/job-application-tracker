import { Component } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { Auth } from '../../core/services/auth';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.css',
})

export class Header {
  constructor(private authService: Auth,private router: Router) {}
  isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
    logout(): void {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.router.navigate(['/login']);
    }

}
