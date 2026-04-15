import { Component, inject, signal, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { LeaderboardService } from '../../core/services/leaderboard.service';
import { UserStats } from '../../shared/models/leaderboard.models';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold text-gray-900 mb-4">Profile</h2>

      @if (auth.currentUser(); as user) {
        <div class="card mb-4">
          <div class="flex items-center gap-3 mb-3">
            <div class="w-12 h-12 rounded-full bg-blue-100 flex items-center justify-center text-xl font-bold text-blue-700">
              {{ user.username[0].toUpperCase() }}
            </div>
            <div>
              <div class="font-bold text-gray-900">{{ user.username }}</div>
              <div class="text-sm text-gray-500">{{ user.email }}</div>
              <div class="text-xs text-gray-400 capitalize">{{ user.role }}</div>
            </div>
          </div>
        </div>

        @if (loading()) {
          <div class="flex justify-center py-8"><div class="w-8 h-8 border-4 border-blue-500 border-t-transparent rounded-full animate-spin"></div></div>
        } @else if (stats()) {
          <div class="card mb-4">
            <h3 class="font-semibold text-gray-700 mb-3">Overall Stats</h3>
            <div class="grid grid-cols-2 gap-3">
              <div class="bg-blue-50 rounded-lg p-3 text-center">
                <div class="text-2xl font-bold text-blue-700">{{ stats()!.overallRank }}</div>
                <div class="text-xs text-gray-500">Rank</div>
              </div>
              <div class="bg-green-50 rounded-lg p-3 text-center">
                <div class="text-2xl font-bold text-green-700">{{ stats()!.totalPoints }}</div>
                <div class="text-xs text-gray-500">Total Points</div>
              </div>
              <div class="bg-purple-50 rounded-lg p-3 text-center">
                <div class="text-2xl font-bold text-purple-700">{{ stats()!.exactScores }}</div>
                <div class="text-xs text-gray-500">Exact Scores</div>
              </div>
              <div class="bg-orange-50 rounded-lg p-3 text-center">
                <div class="text-2xl font-bold text-orange-700">{{ stats()!.correctWinners }}</div>
                <div class="text-xs text-gray-500">Correct Winners</div>
              </div>
            </div>
            <div class="mt-3 grid grid-cols-3 gap-2 text-center text-sm">
              <div>
                <div class="font-semibold text-gray-700">{{ stats()!.predictionsCount }}</div>
                <div class="text-xs text-gray-400">Predictions</div>
              </div>
              <div>
                <div class="font-semibold text-gray-700">{{ stats()!.winnerPlusDiff }}</div>
                <div class="text-xs text-gray-400">Winner+Diff</div>
              </div>
              <div>
                <div class="font-semibold text-gray-700">{{ stats()!.misses }}</div>
                <div class="text-xs text-gray-400">Misses</div>
              </div>
            </div>
          </div>

          @if (stats()!.weeklyHistory.length > 0) {
            <div class="card">
              <h3 class="font-semibold text-gray-700 mb-3">Weekly History</h3>
              <div class="flex flex-col gap-2">
                @for (week of stats()!.weeklyHistory; track week.gameWeekId) {
                  <div class="flex items-center justify-between py-1.5 border-b border-gray-50 last:border-0">
                    <span class="text-sm text-gray-600">{{ week.gameWeekName }}</span>
                    <span class="font-semibold text-gray-900">{{ week.points }} pts</span>
                  </div>
                }
              </div>
            </div>
          }
        }
      }
    </div>
  `
})
export class ProfileComponent implements OnInit {
  readonly auth = inject(AuthService);
  private readonly lbSvc = inject(LeaderboardService);

  stats = signal<UserStats | null>(null);
  loading = signal(false);

  ngOnInit(): void {
    const userId = this.auth.currentUser()?.id;
    if (!userId) return;
    this.loading.set(true);
    this.lbSvc.getUserStats(userId).subscribe({
      next: s => { this.stats.set(s); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
