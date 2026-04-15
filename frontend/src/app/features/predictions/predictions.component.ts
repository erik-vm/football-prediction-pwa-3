import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { MatchService } from '../../core/services/match.service';
import { PredictionService } from '../../core/services/prediction.service';
import { TournamentDto, GameWeekDto, MatchDto } from '../../shared/models/match.models';
import { PredictionDto, SubmitPredictionRequest, UpdatePredictionRequest } from '../../shared/models/prediction.models';
import { MatchCardComponent } from './match-card/match-card.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-predictions',
  standalone: true,
  imports: [MatchCardComponent, FormsModule],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold text-gray-900 mb-4">Predictions</h2>

      @if (loading()) {
        <div class="flex justify-center py-12"><div class="w-8 h-8 border-4 border-blue-500 border-t-transparent rounded-full animate-spin"></div></div>
      } @else if (error()) {
        <div class="p-4 bg-red-50 text-red-700 rounded-lg">{{ error() }}</div>
      } @else {
        <div class="mb-4 flex gap-2">
          <select class="input-field flex-1" [(ngModel)]="selectedTournamentId" (ngModelChange)="onTournamentChange($event)">
            @for (t of tournaments(); track t.id) {
              <option [value]="t.id">{{ t.name }} ({{ t.season }})</option>
            }
          </select>
          <select class="input-field flex-1" [(ngModel)]="selectedGameWeekId" (ngModelChange)="onGameWeekChange($event)">
            @for (gw of gameWeeks(); track gw.id) {
              <option [value]="gw.id">{{ gw.name }}</option>
            }
          </select>
        </div>

        @if (matches().length === 0) {
          <div class="text-center text-gray-400 py-12">No matches found for this game week.</div>
        } @else {
          <div class="flex flex-col gap-3">
            @for (match of sortedMatches(); track match.id) {
              <app-match-card
                [match]="match"
                [prediction]="predictionFor(match.id)"
                (predictClicked)="onPredict($event)" />
            }
          </div>
        }
      }
    </div>
  `
})
export class PredictionsComponent implements OnInit {
  private readonly matchSvc = inject(MatchService);
  private readonly predSvc = inject(PredictionService);

  tournaments = signal<TournamentDto[]>([]);
  gameWeeks = signal<GameWeekDto[]>([]);
  matches = signal<MatchDto[]>([]);
  predictions = signal<PredictionDto[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  selectedTournamentId = '';
  selectedGameWeekId = '';

  sortedMatches = computed(() =>
    [...this.matches()].sort((a, b) => new Date(a.kickoffTime).getTime() - new Date(b.kickoffTime).getTime())
  );

  ngOnInit(): void {
    this.loading.set(true);
    this.matchSvc.getTournaments().subscribe({
      next: tournaments => {
        this.tournaments.set(tournaments);
        const active = tournaments.find(t => t.isActive) ?? tournaments[0];
        if (!active) { this.loading.set(false); return; }
        this.selectedTournamentId = active.id;
        this.loadGameWeeks(active.id);
      },
      error: () => { this.error.set('Failed to load tournaments.'); this.loading.set(false); }
    });

    this.predSvc.getMyPredictions().subscribe({
      next: preds => this.predictions.set(preds ?? []),
      error: () => {}
    });
  }

  predictionFor(matchId: string): PredictionDto | null {
    return this.predictions().find(p => p.matchId === matchId) ?? null;
  }

  onTournamentChange(id: string): void {
    this.loadGameWeeks(id);
  }

  onGameWeekChange(id: string): void {
    this.loadMatches(id);
  }

  onPredict(event: { matchId: string; homeScore: number; awayScore: number }): void {
    const existing = this.predictionFor(event.matchId);
    if (existing) {
      const req: UpdatePredictionRequest = { homeScore: event.homeScore, awayScore: event.awayScore };
      this.predSvc.update(existing.id, req).subscribe({
        next: updated => this.predictions.update(preds => preds.map(p => p.id === updated.id ? updated : p))
      });
    } else {
      const req: SubmitPredictionRequest = { matchId: event.matchId, homeScore: event.homeScore, awayScore: event.awayScore };
      this.predSvc.submit(req).subscribe({
        next: created => this.predictions.update(preds => [...preds, created])
      });
    }
  }

  private loadGameWeeks(tournamentId: string): void {
    this.matchSvc.getGameWeeksByTournament(tournamentId).subscribe({
      next: gws => {
        this.gameWeeks.set(gws);
        if (gws.length > 0) {
          const now = new Date();
          const current = gws.find(gw => new Date(gw.startDate) <= now && now <= new Date(gw.endDate))
            ?? gws[gws.length - 1];
          this.selectedGameWeekId = current.id;
          this.loadMatches(current.id);
        } else {
          this.matches.set([]);
          this.loading.set(false);
        }
      },
      error: () => { this.error.set('Failed to load game weeks.'); this.loading.set(false); }
    });
  }

  private loadMatches(gameWeekId: string): void {
    this.loading.set(true);
    this.matchSvc.getMatchesByGameWeek(gameWeekId).subscribe({
      next: matches => { this.matches.set(matches ?? []); this.loading.set(false); },
      error: () => { this.error.set('Failed to load matches.'); this.loading.set(false); }
    });
  }
}
