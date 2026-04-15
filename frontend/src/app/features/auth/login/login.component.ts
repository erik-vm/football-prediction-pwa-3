import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  template: `
    <div class="min-h-screen flex items-center justify-center p-4 bg-gray-50">
      <div class="w-full max-w-sm">
        <div class="text-center mb-8">
          <div class="text-5xl mb-2">⚽</div>
          <h1 class="text-2xl font-bold text-gray-900">FootballPicks</h1>
          <p class="text-gray-500 mt-1">Sign in to your account</p>
        </div>
        <div class="card">
          @if (error()) {
            <div class="mb-4 p-3 bg-red-50 border border-red-200 text-red-700 rounded-lg text-sm">{{ error() }}</div>
          }
          <form [formGroup]="form" (ngSubmit)="onSubmit()">
            <div class="mb-4">
              <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
              <input type="email" formControlName="email" class="input-field" placeholder="you@example.com" />
            </div>
            <div class="mb-6">
              <label class="block text-sm font-medium text-gray-700 mb-1">Password</label>
              <input type="password" formControlName="password" class="input-field" placeholder="••••••••" />
            </div>
            <button type="submit" class="btn-primary w-full" [disabled]="loading() || form.invalid">
              {{ loading() ? 'Signing in...' : 'Sign in' }}
            </button>
          </form>
          <p class="text-center text-sm text-gray-500 mt-4">
            Don't have an account? <a routerLink="/register" class="text-blue-600 font-medium">Register</a>
          </p>
        </div>
      </div>
    </div>
  `
})
export class LoginComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);
  private readonly fb = inject(FormBuilder);

  form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  loading = signal(false);
  error = signal<string | null>(null);

  onSubmit(): void {
    if (this.form.invalid) return;
    this.loading.set(true);
    this.error.set(null);
    const { email, password } = this.form.getRawValue();
    this.auth.login({ email, password }).subscribe({
      next: () => this.router.navigate(['/predictions']),
      error: (err) => {
        this.error.set(err.error?.message ?? 'Login failed. Please try again.');
        this.loading.set(false);
      }
    });
  }
}
