import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="flex flex-col min-h-screen">
      <header class="bg-white border-b border-gray-200 sticky top-0 z-10">
        <div class="flex items-center justify-between px-4 py-3">
          <span class="text-lg font-bold text-gray-900">⚽ FootballPicks</span>
          <button (click)="logout()" class="text-sm text-gray-500 hover:text-gray-900 font-medium">Logout</button>
        </div>
      </header>

      <main class="flex-1 overflow-y-auto pb-20">
        <router-outlet />
      </main>

      <nav class="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 z-10">
        <div class="max-w-lg mx-auto flex">
          <a routerLink="/predictions" routerLinkActive="text-blue-600" [routerLinkActiveOptions]="{exact:false}"
             class="flex-1 flex flex-col items-center py-2 text-gray-400 hover:text-blue-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2"/></svg>
            <span class="text-xs mt-0.5">Picks</span>
          </a>
          <a routerLink="/leaderboard" routerLinkActive="text-blue-600" [routerLinkActiveOptions]="{exact:false}"
             class="flex-1 flex flex-col items-center py-2 text-gray-400 hover:text-blue-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"/></svg>
            <span class="text-xs mt-0.5">Table</span>
          </a>
          <a routerLink="/profile" routerLinkActive="text-blue-600" [routerLinkActiveOptions]="{exact:false}"
             class="flex-1 flex flex-col items-center py-2 text-gray-400 hover:text-blue-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"/></svg>
            <span class="text-xs mt-0.5">Profile</span>
          </a>
          @if (auth.isAdmin()) {
            <a routerLink="/admin" routerLinkActive="text-blue-600" [routerLinkActiveOptions]="{exact:false}"
               class="flex-1 flex flex-col items-center py-2 text-gray-400 hover:text-blue-600">
              <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z"/><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/></svg>
              <span class="text-xs mt-0.5">Admin</span>
            </a>
          }
        </div>
      </nav>
    </div>
  `
})
export class ShellComponent {
  readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
