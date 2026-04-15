import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TournamentDto, GameWeekDto, MatchDto } from '../../shared/models/match.models';

@Injectable({ providedIn: 'root' })
export class MatchService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/v1`;

  getTournaments(): Observable<TournamentDto[]> {
    return this.http.get<TournamentDto[]>(`${this.base}/tournaments`);
  }

  getActiveTournament(): Observable<TournamentDto> {
    return this.http.get<TournamentDto>(`${this.base}/tournaments/active`);
  }

  getTournamentById(id: string): Observable<TournamentDto> {
    return this.http.get<TournamentDto>(`${this.base}/tournaments/${id}`);
  }

  getGameWeeksByTournament(tournamentId: string): Observable<GameWeekDto[]> {
    return this.http.get<GameWeekDto[]>(`${this.base}/tournaments/${tournamentId}/gameweeks`);
  }

  getMatchesByGameWeek(gameWeekId: string): Observable<MatchDto[]> {
    return this.http.get<MatchDto[]>(`${this.base}/matches/gameweek/${gameWeekId}`);
  }

  getMatchById(id: string): Observable<MatchDto> {
    return this.http.get<MatchDto>(`${this.base}/matches/${id}`);
  }

  getUpcomingMatches(gameWeekId?: string): Observable<MatchDto[]> {
    const url = gameWeekId
      ? `${this.base}/matches/upcoming?gameWeekId=${gameWeekId}`
      : `${this.base}/matches/upcoming`;
    return this.http.get<MatchDto[]>(url);
  }
}
