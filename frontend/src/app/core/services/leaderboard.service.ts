import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LeaderboardEntry, WeeklyLeaderboardEntry, UserStats } from '../../shared/models/leaderboard.models';

@Injectable({ providedIn: 'root' })
export class LeaderboardService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/v1/leaderboard`;

  getOverall(): Observable<LeaderboardEntry[]> {
    return this.http.get<LeaderboardEntry[]>(`${this.base}/overall`);
  }

  getWeekly(gameWeekId: string): Observable<WeeklyLeaderboardEntry[]> {
    return this.http.get<WeeklyLeaderboardEntry[]>(`${this.base}/weekly/${gameWeekId}`);
  }

  getUserStats(userId: string): Observable<UserStats> {
    return this.http.get<UserStats>(`${this.base}/user/${userId}`);
  }
}
