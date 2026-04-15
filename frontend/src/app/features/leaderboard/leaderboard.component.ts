import { Component, inject, signal, OnInit } from '@angular/core';
import { LeaderboardService } from '../../core/services/leaderboard.service';
import { MatchService } from '../../core/services/match.service';
import { AuthService } from '../../core/services/auth.service';
import { LeaderboardEntry, WeeklyLeaderboardEntry } from '../../shared/models/leaderboard.models';
import { GameWeekDto } from '../../shared/models/match.models';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold text-gray-900 mb-4">Leaderboard</h2>

      <div class="flex border border-gray-200 rounded-lg mb-4 overflow-hidden">
        <button (click)="tab.set('overall')" class="flex-1 py-2 text-sm font-medium transition-colors"
                [class.bg-blue-600]="tab() === 'overall'" [class.text-white]="tab() === 'overall'"
                [class.text-gray-600]="tab() !== 'overall'">Overall</button>
        <button (click)="tab.set('weekly')" class="flex-1 py-2 text-sm font-medium transition-colors"
                [class.bg-blue-600]="tab() === 'weekly'" [class.text-white]="tab() === 'weekly'"
                [class.text-gray-600]="tab() !== 'weekly'">Weekly</button>
      </div>

      @if (loading()) {
        <div class="flex justify-center py-12"><div class="w-8 h-8 border-4 border-blue-500 border-t-transparent rounded-full animate-spin"></div></div>
      } @else if (tab() === 'overall') {
        <div class="card p-0 overflow-hidden">
          <table class="w-full text-sm">
            <thead class="bg-gray-50 border-b border-gray-100">
              <tr>
                <th class="text-left px-3 py-2 text-gray-500 font-medium">#</th>
                <th class="text-left px-3 py-2 text-gray-500 font-medium">Player</th>
                <th class="text-right px-3 py-2 text-gray-500 font-medium">Pts</th>
                <th class="text-right px-3 py-2 text-gray-500 font-medium hidden sm:table-cell">Exact</th>
                <th class="text-right px-3 py-2 text-gray-500 font-medium hidden sm:table-cell">Picks</th>
              </tr>
            </thead>
            <tbody>
              @for (entry of overall(); track entry.userId) {
                <tr [class.bg-blue-50]="entry.userId === currentUserId()"
                    class="border-b border-gray-50 last:border-0">
                  <td class="px-3 py-2.5 font-bold text-gray-700">{{ entry.rank }}</td>
                  <td class="px-3 py-2.5">
                    <span class="font-medium text-gray-900">{{ entry.username }}</span>
                    @if (entry.userId === currentUserId()) {
                      <span class="ml-1 text-xs text-blue-500">(you)</span>
                    }
                  </td>
                  <td class="px-3 py-2.5 text-right font-bold text-gray-900">{{ entry.totalPoints }}</td>
                  <td class="px-3 py-2.5 text-right text-gray-500 hidden sm:table-cell">{{ entry.exactScores }}</td>
                  <td class="px-3 py-2.5 text-right text-gray-500 hidden sm:table-cell">{{ entry.predictionsCount }}</td>
                </tr>
              }
              @if (overall().length === 0) {
                <tr><td colspan="5" class="px-3 py-8 text-center text-gray-400">No data yet.</td></tr>
              }
            </tbody>
          </table>
        </div>
      } @else {
        <div class="mb-3">
          <select class="input-field" [(ngModel)]="selectedGameWeekId" (ngModelChange)="loadWeekly($event)">
            @for (gw of gameWeeks(); track gw.id) {
              <option [value]="gw.id">{{ gw.name }}</option>
            }
          </select>
        </div>
        <div class="card p-0 overflow-hidden">
          <table class="w-full text-sm">
            <thead class="bg-gray-50 border-b border-gray-100">
              <tr>
                <th class="text-left px-3 py-2 text-gray-500 font-medium">#</th>
                <th class="text-left px-3 py-2 text-gray-500 font-medium">Player</th>
                <th class="text-right px-3 py-2 text-gray-500 font-medium">Pts</th>
                <th class="text-right px-3 py-2 text-gray-500 font-medium">Bonus</th>
              </tr>
            </thead>
            <tbody>
              @for (entry of weekly(); track entry.userId) {
                <tr [class.bg-blue-50]="entry.userId === currentUserId()"
                    class="border-b border-gray-50 last:border-0">
                  <td class="px-3 py-2.5 font-bold text-gray-700">{{ entry.rank }}</td>
                  <td class="px-3 py-2.5">
                    <span class="font-medium text-gray-900">{{ entry.username }}</span>
                    @if (entry.userId === currentUserId()) {
                      <span class="ml-1 text-xs text-blue-500">(you)</span>
                    }
                  </td>
                  <td class="px-3 py-2.5 text-right font-bold text-gray-900">{{ entry.weeklyPoints }}</td>
                  <td class="px-3 py-2.5 text-right text-gray-500">{{ entry.bonusPoints }}</td>
                </tr>
              }
              @if (weekly().length === 0) {
                <tr><td colspan="4" class="px-3 py-8 text-center text-gray-400">No data yet.</td></tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>
  `
})
export class LeaderboardComponent implements OnInit {
  private readonly lbSvc = inject(LeaderboardService);
  private readonly matchSvc = inject(MatchService);
  private readonly authSvc = inject(AuthService);

  tab = signal<'overall' | 'weekly'>('overall');
  overall = signal<LeaderboardEntry[]>([]);
  weekly = signal<WeeklyLeaderboardEntry[]>([]);
  gameWeeks = signal<GameWeekDto[]>([]);
  loading = signal(false);
  selectedGameWeekId = '';

  currentUserId = () => this.authSvc.currentUser()?.id ?? '';

  ngOnInit(): void {
    this.loading.set(true);
    this.lbSvc.getOverall().subscribe({
      next: data => { this.overall.set(data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });

    this.matchSvc.getTournaments().subscribe({
      next: tournaments => {
        const active = tournaments.find(t => t.isActive) ?? tournaments[0];
        if (!active) return;
        this.matchSvc.getGameWeeksByTournament(active.id).subscribe({
          next: gws => {
            this.gameWeeks.set(gws);
            if (gws.length > 0) {
              this.selectedGameWeekId = gws[gws.length - 1].id;
            }
          }
        });
      }
    });
  }

  loadWeekly(gameWeekId: string): void {
    if (!gameWeekId) return;
    this.lbSvc.getWeekly(gameWeekId).subscribe({
      next: data => this.weekly.set(data ?? []),
      error: () => {}
    });
  }
}
