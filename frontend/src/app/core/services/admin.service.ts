import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TournamentDto, GameWeekDto, MatchDto } from '../../shared/models/match.models';

export interface CreateTournamentRequest { name: string; season: string; externalId: string | null; startDate: string; endDate: string; isActive: boolean; }
export interface UpdateTournamentRequest { name: string; season: string; externalId: string | null; startDate: string; endDate: string; isActive: boolean; }
export interface CreateGameWeekRequest { tournamentId: string; weekNumber: number; name: string; startDate: string; endDate: string; }
export interface UpdateGameWeekRequest { weekNumber: number; name: string; startDate: string; endDate: string; }
export interface CreateMatchRequest { gameWeekId: string; homeTeam: string; awayTeam: string; kickoffTime: string; stage: number; }
export interface UpdateMatchRequest { homeTeam: string; awayTeam: string; kickoffTime: string; stage: number; }
export interface EnterResultRequest { homeScore: number; awayScore: number; }

@Injectable({ providedIn: 'root' })
export class AdminService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/v1/admin`;

  getTournaments(): Observable<TournamentDto[]> {
    return this.http.get<TournamentDto[]>(`${this.base}/tournaments`);
  }

  createTournament(req: CreateTournamentRequest): Observable<TournamentDto> {
    return this.http.post<TournamentDto>(`${this.base}/tournaments`, req);
  }

  updateTournament(id: string, req: UpdateTournamentRequest): Observable<TournamentDto> {
    return this.http.put<TournamentDto>(`${this.base}/tournaments/${id}`, req);
  }

  deleteTournament(id: string): Observable<void> {
    return this.http.delete<void>(`${this.base}/tournaments/${id}`);
  }

  createGameWeek(req: CreateGameWeekRequest): Observable<GameWeekDto> {
    return this.http.post<GameWeekDto>(`${this.base}/gameweeks`, req);
  }

  updateGameWeek(id: string, req: UpdateGameWeekRequest): Observable<GameWeekDto> {
    return this.http.put<GameWeekDto>(`${this.base}/gameweeks/${id}`, req);
  }

  createMatch(req: CreateMatchRequest): Observable<MatchDto> {
    return this.http.post<MatchDto>(`${this.base}/matches`, req);
  }

  updateMatch(id: string, req: UpdateMatchRequest): Observable<MatchDto> {
    return this.http.put<MatchDto>(`${this.base}/matches/${id}`, req);
  }

  enterResult(id: string, req: EnterResultRequest): Observable<MatchDto> {
    return this.http.put<MatchDto>(`${this.base}/matches/${id}/result`, req);
  }

  importMatches(gameWeekId: string): Observable<{ imported: number }> {
    return this.http.post<{ imported: number }>(`${this.base}/matches/gameweek/${gameWeekId}/import`, {});
  }
}
