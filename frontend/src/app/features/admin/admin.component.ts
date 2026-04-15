import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminService, CreateTournamentRequest, UpdateTournamentRequest, CreateGameWeekRequest, CreateMatchRequest, EnterResultRequest } from '../../core/services/admin.service';
import { MatchService } from '../../core/services/match.service';
import { TournamentDto, GameWeekDto, MatchDto } from '../../shared/models/match.models';

type AdminTab = 'tournaments' | 'gameweeks' | 'matches';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold text-gray-900 mb-4">Admin Panel</h2>

      <div class="flex border border-gray-200 rounded-lg mb-4 overflow-hidden">
        @for (t of tabs; track t.key) {
          <button (click)="tab.set(t.key)" class="flex-1 py-2 text-xs font-medium transition-colors"
                  [class.bg-blue-600]="tab() === t.key" [class.text-white]="tab() === t.key"
                  [class.text-gray-600]="tab() !== t.key">{{ t.label }}</button>
        }
      </div>

      @if (tab() === 'tournaments') {
        <div class="card mb-4">
          <h3 class="font-semibold text-gray-700 mb-3">{{ editingTournament ? 'Edit Tournament' : 'New Tournament' }}</h3>
          <div class="flex flex-col gap-2">
            <input class="input-field" placeholder="Name" [(ngModel)]="tForm.name" />
            <input class="input-field" placeholder="Season (e.g. 2024/25)" [(ngModel)]="tForm.season" />
            <input class="input-field" placeholder="External ID (optional)" [(ngModel)]="tForm.externalId" />
            <input class="input-field" type="date" [(ngModel)]="tForm.startDate" />
            <input class="input-field" type="date" [(ngModel)]="tForm.endDate" />
            <label class="flex items-center gap-2 text-sm text-gray-700">
              <input type="checkbox" [(ngModel)]="tForm.isActive" /> Active
            </label>
            <div class="flex gap-2">
              <button class="btn-primary flex-1" (click)="saveTournament()">{{ editingTournament ? 'Update' : 'Create' }}</button>
              @if (editingTournament) {
                <button class="btn-secondary" (click)="cancelTournamentEdit()">Cancel</button>
              }
            </div>
          </div>
        </div>

        <div class="flex flex-col gap-2">
          @for (t of tournaments(); track t.id) {
            <div class="card flex items-center justify-between">
              <div>
                <div class="font-semibold text-gray-900">{{ t.name }}</div>
                <div class="text-xs text-gray-400">{{ t.season }}{{ t.isActive ? ' ✓ Active' : '' }}</div>
              </div>
              <div class="flex gap-2">
                <button class="text-xs text-blue-600 font-medium" (click)="editTournament(t)">Edit</button>
                <button class="text-xs text-red-500 font-medium" (click)="deleteTournament(t.id)">Delete</button>
                <button class="text-xs text-green-600 font-medium" (click)="selectTournament(t)">Weeks</button>
              </div>
            </div>
          }
        </div>
      }

      @if (tab() === 'gameweeks') {
        @if (!selectedTournament()) {
          <div class="text-center text-gray-400 py-8">Select a tournament from the Tournaments tab first.</div>
        } @else {
          <div class="mb-3 p-3 bg-blue-50 rounded-lg text-sm text-blue-800 font-medium">{{ selectedTournament()!.name }}</div>

          <div class="card mb-4">
            <h3 class="font-semibold text-gray-700 mb-3">New Game Week</h3>
            <div class="flex flex-col gap-2">
              <input class="input-field" type="number" placeholder="Week number" [(ngModel)]="gwForm.weekNumber" />
              <input class="input-field" placeholder="Name (e.g. Week 1)" [(ngModel)]="gwForm.name" />
              <input class="input-field" type="date" [(ngModel)]="gwForm.startDate" />
              <input class="input-field" type="date" [(ngModel)]="gwForm.endDate" />
              <button class="btn-primary" (click)="createGameWeek()">Create</button>
            </div>
          </div>

          <div class="flex flex-col gap-2">
            @for (gw of gameWeeks(); track gw.id) {
              <div class="card flex items-center justify-between">
                <div>
                  <div class="font-semibold text-gray-900">{{ gw.name }}</div>
                  <div class="text-xs text-gray-400">Week {{ gw.weekNumber }}</div>
                </div>
                <button class="text-xs text-green-600 font-medium" (click)="selectGameWeek(gw)">Matches</button>
              </div>
            }
          </div>
        }
      }

      @if (tab() === 'matches') {
        @if (!selectedGameWeek()) {
          <div class="text-center text-gray-400 py-8">Select a game week from the Game Weeks tab first.</div>
        } @else {
          <div class="mb-3 p-3 bg-green-50 rounded-lg text-sm text-green-800 font-medium">{{ selectedGameWeek()!.name }}</div>

          <div class="card mb-4">
            <h3 class="font-semibold text-gray-700 mb-3">New Match</h3>
            <div class="flex flex-col gap-2">
              <input class="input-field" placeholder="Home Team" [(ngModel)]="mForm.homeTeam" />
              <input class="input-field" placeholder="Away Team" [(ngModel)]="mForm.awayTeam" />
              <input class="input-field" type="datetime-local" [(ngModel)]="mForm.kickoffTime" />
              <select class="input-field" [(ngModel)]="mForm.stage">
                <option [value]="1">Group Stage</option>
                <option [value]="2">Round of 16</option>
                <option [value]="3">Quarter-Finals</option>
                <option [value]="4">Semi-Finals</option>
                <option [value]="5">Final</option>
              </select>
              <button class="btn-primary" (click)="createMatch()">Create Match</button>
            </div>
          </div>

          <div class="mb-3">
            <button class="btn-secondary w-full text-sm" (click)="importMatches()" [disabled]="importing()">
              {{ importing() ? 'Importing...' : 'Import from API' }}
            </button>
            @if (importResult()) {
              <div class="mt-2 text-sm text-green-600 text-center">{{ importResult() }}</div>
            }
          </div>

          <div class="flex flex-col gap-2">
            @for (m of matches(); track m.id) {
              <div class="card">
                <div class="flex items-center justify-between mb-1">
                  <div class="font-semibold text-sm text-gray-900">{{ m.homeTeam }} vs {{ m.awayTeam }}</div>
                  <span class="text-xs text-gray-400">{{ m.stageName }}</span>
                </div>
                <div class="text-xs text-gray-400 mb-2">{{ kickoffDisplay(m.kickoffTime) }}</div>
                @if (m.isFinished) {
                  <div class="text-sm font-bold text-green-700">FT: {{ m.homeScore }} - {{ m.awayScore }}</div>
                } @else {
                  <div class="flex items-center gap-2 mt-2">
                    <input type="number" class="w-12 h-8 text-center border border-gray-300 rounded text-sm" [(ngModel)]="resultForms[m.id].home" min="0" />
                    <span class="text-gray-400">-</span>
                    <input type="number" class="w-12 h-8 text-center border border-gray-300 rounded text-sm" [(ngModel)]="resultForms[m.id].away" min="0" />
                    <button class="btn-primary text-xs py-1 px-3" (click)="enterResult(m.id)">Set Result</button>
                  </div>
                }
              </div>
            }
          </div>
        }
      }
    </div>
  `
})
export class AdminComponent implements OnInit {
  private readonly adminSvc = inject(AdminService);
  private readonly matchSvc = inject(MatchService);

  tabs: { key: AdminTab; label: string }[] = [
    { key: 'tournaments', label: 'Tournaments' },
    { key: 'gameweeks', label: 'Game Weeks' },
    { key: 'matches', label: 'Matches' }
  ];

  tab = signal<AdminTab>('tournaments');
  tournaments = signal<TournamentDto[]>([]);
  gameWeeks = signal<GameWeekDto[]>([]);
  matches = signal<MatchDto[]>([]);
  selectedTournament = signal<TournamentDto | null>(null);
  selectedGameWeek = signal<GameWeekDto | null>(null);
  importing = signal(false);
  importResult = signal<string | null>(null);

  editingTournament: TournamentDto | null = null;

  tForm: { name: string; season: string; externalId: string | null; startDate: string; endDate: string; isActive: boolean } =
    { name: '', season: '', externalId: null, startDate: '', endDate: '', isActive: false };

  gwForm: { tournamentId: string; weekNumber: number; name: string; startDate: string; endDate: string } =
    { tournamentId: '', weekNumber: 1, name: '', startDate: '', endDate: '' };

  mForm: { gameWeekId: string; homeTeam: string; awayTeam: string; kickoffTime: string; stage: number } =
    { gameWeekId: '', homeTeam: '', awayTeam: '', kickoffTime: '', stage: 1 };

  resultForms: Record<string, { home: number; away: number }> = {};

  ngOnInit(): void {
    this.adminSvc.getTournaments().subscribe({
      next: ts => this.tournaments.set(ts ?? [])
    });
  }

  kickoffDisplay(dt: string): string {
    return new Date(dt).toLocaleString(undefined, { weekday: 'short', month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit' });
  }

  saveTournament(): void {
    const req: CreateTournamentRequest = { ...this.tForm };
    if (this.editingTournament) {
      const upd: UpdateTournamentRequest = { ...this.tForm };
      this.adminSvc.updateTournament(this.editingTournament.id, upd).subscribe({
        next: updated => {
          this.tournaments.update(ts => ts.map(t => t.id === updated.id ? updated : t));
          this.cancelTournamentEdit();
        }
      });
    } else {
      this.adminSvc.createTournament(req).subscribe({
        next: created => {
          this.tournaments.update(ts => [...ts, created]);
          this.tForm = { name: '', season: '', externalId: null, startDate: '', endDate: '', isActive: false };
        }
      });
    }
  }

  editTournament(t: TournamentDto): void {
    this.editingTournament = t;
    this.tForm = { name: t.name, season: t.season, externalId: t.externalId, startDate: t.startDate.slice(0, 10), endDate: t.endDate.slice(0, 10), isActive: t.isActive };
  }

  cancelTournamentEdit(): void {
    this.editingTournament = null;
    this.tForm = { name: '', season: '', externalId: null, startDate: '', endDate: '', isActive: false };
  }

  deleteTournament(id: string): void {
    this.adminSvc.deleteTournament(id).subscribe({
      next: () => this.tournaments.update(ts => ts.filter(t => t.id !== id))
    });
  }

  selectTournament(t: TournamentDto): void {
    this.selectedTournament.set(t);
    this.gwForm.tournamentId = t.id;
    this.tab.set('gameweeks');
    this.matchSvc.getGameWeeksByTournament(t.id).subscribe({
      next: gws => this.gameWeeks.set(gws ?? [])
    });
  }

  createGameWeek(): void {
    const req: CreateGameWeekRequest = { ...this.gwForm };
    this.adminSvc.createGameWeek(req).subscribe({
      next: gw => {
        this.gameWeeks.update(gws => [...gws, gw]);
        this.gwForm = { tournamentId: this.gwForm.tournamentId, weekNumber: this.gwForm.weekNumber + 1, name: '', startDate: '', endDate: '' };
      }
    });
  }

  selectGameWeek(gw: GameWeekDto): void {
    this.selectedGameWeek.set(gw);
    this.mForm.gameWeekId = gw.id;
    this.tab.set('matches');
    this.matchSvc.getMatchesByGameWeek(gw.id).subscribe({
      next: ms => {
        const safeMs = ms ?? [];
        this.matches.set(safeMs);
        safeMs.forEach(m => {
          if (!this.resultForms[m.id]) this.resultForms[m.id] = { home: 0, away: 0 };
        });
      }
    });
  }

  createMatch(): void {
    const req: CreateMatchRequest = { ...this.mForm };
    this.adminSvc.createMatch(req).subscribe({
      next: m => {
        this.matches.update(ms => [...ms, m]);
        this.resultForms[m.id] = { home: 0, away: 0 };
        this.mForm = { gameWeekId: this.mForm.gameWeekId, homeTeam: '', awayTeam: '', kickoffTime: '', stage: 1 };
      }
    });
  }

  enterResult(matchId: string): void {
    const { home, away } = this.resultForms[matchId] ?? { home: 0, away: 0 };
    const req: EnterResultRequest = { homeScore: home, awayScore: away };
    this.adminSvc.enterResult(matchId, req).subscribe({
      next: updated => this.matches.update(ms => ms.map(m => m.id === updated.id ? updated : m))
    });
  }

  importMatches(): void {
    const gw = this.selectedGameWeek();
    if (!gw) return;
    this.importing.set(true);
    this.importResult.set(null);
    this.adminSvc.importMatches(gw.id).subscribe({
      next: res => {
        this.importResult.set(`Imported ${res.imported} matches.`);
        this.importing.set(false);
        this.matchSvc.getMatchesByGameWeek(gw.id).subscribe({
          next: ms => {
            const safeMs = ms ?? [];
            this.matches.set(safeMs);
            safeMs.forEach(m => {
              if (!this.resultForms[m.id]) this.resultForms[m.id] = { home: 0, away: 0 };
            });
          }
        });
      },
      error: () => { this.importResult.set('Import failed.'); this.importing.set(false); }
    });
  }
}
