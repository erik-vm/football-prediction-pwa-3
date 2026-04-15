import { Component, input, output, signal, computed } from '@angular/core';
import { MatchDto } from '../../../shared/models/match.models';
import { PredictionDto } from '../../../shared/models/prediction.models';
import { PredictionFormComponent } from '../prediction-form/prediction-form.component';

@Component({
  selector: 'app-match-card',
  standalone: true,
  imports: [PredictionFormComponent],
  template: `
    <div class="card cursor-pointer" [class.ring-2]="isOpen()" [class.ring-blue-400]="isOpen()" (click)="handleCardClick()">
      <div class="flex items-center justify-between mb-2">
        <span class="text-xs text-gray-400">{{ kickoffFormatted() }}</span>
        <div class="flex items-center gap-1">
          <span class="text-xs px-2 py-0.5 bg-gray-100 text-gray-600 rounded-full">{{ match().stageName }}</span>
          @if (isLocked() && !match().isFinished) {
            <span class="text-xs px-2 py-0.5 bg-orange-100 text-orange-600 rounded-full font-medium">LOCKED</span>
          }
          @if (match().isFinished) {
            <span class="text-xs px-2 py-0.5 bg-green-100 text-green-600 rounded-full font-medium">FT</span>
          }
        </div>
      </div>

      <div class="flex items-center justify-between">
        <div class="flex items-center gap-2 flex-1">
          @if (match().homeTeamCrest) {
            <img [src]="match().homeTeamCrest!" class="w-8 h-8 object-contain" [alt]="match().homeTeam" />
          } @else {
            <div class="w-8 h-8 rounded-full bg-blue-100 flex items-center justify-center text-xs font-bold text-blue-700">{{ initials(match().homeTeam) }}</div>
          }
          <span class="font-semibold text-gray-900 text-sm">{{ match().homeTeam }}</span>
        </div>

        <div class="px-3 text-center">
          @if (match().isFinished && match().homeScore !== null && match().awayScore !== null) {
            <div class="text-xl font-bold text-gray-900">{{ match().homeScore }} - {{ match().awayScore }}</div>
          } @else {
            <div class="text-lg font-bold text-gray-400">vs</div>
          }
        </div>

        <div class="flex items-center gap-2 flex-1 justify-end">
          <span class="font-semibold text-gray-900 text-sm">{{ match().awayTeam }}</span>
          @if (match().awayTeamCrest) {
            <img [src]="match().awayTeamCrest!" class="w-8 h-8 object-contain" [alt]="match().awayTeam" />
          } @else {
            <div class="w-8 h-8 rounded-full bg-red-100 flex items-center justify-center text-xs font-bold text-red-700">{{ initials(match().awayTeam) }}</div>
          }
        </div>
      </div>

      @if (prediction()) {
        <div class="mt-2 pt-2 border-t border-gray-100 flex items-center justify-between text-sm">
          <span class="text-gray-500">Your pick: <span class="font-semibold text-gray-800">{{ prediction()!.predictedHome }} - {{ prediction()!.predictedAway }}</span></span>
          @if (prediction()!.pointsEarned !== null) {
            <span class="font-bold" [class.text-green-600]="(prediction()!.pointsEarned ?? 0) > 0" [class.text-gray-400]="(prediction()!.pointsEarned ?? 0) === 0">
              {{ prediction()!.pointsEarned }} pts
            </span>
          }
        </div>
      }

      @if (isOpen()) {
        <div (click)="$event.stopPropagation()">
          <app-prediction-form
            #predForm
            [submitLabel]="prediction() ? 'Update' : 'Save'"
            (submitted)="onFormSubmit($event)" />
        </div>
      }
    </div>
  `
})
export class MatchCardComponent {
  match = input.required<MatchDto>();
  prediction = input<PredictionDto | null>(null);

  predictClicked = output<{ matchId: string; homeScore: number; awayScore: number }>();

  isOpen = signal(false);

  isLocked = computed(() => new Date(this.match().kickoffTime) <= new Date());

  kickoffFormatted = computed(() => {
    const d = new Date(this.match().kickoffTime);
    return d.toLocaleString(undefined, { weekday: 'short', month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit' });
  });

  initials(name: string): string {
    return name.split(' ').map(w => w[0]).join('').substring(0, 2).toUpperCase();
  }

  handleCardClick(): void {
    if (this.isLocked() || this.match().isFinished) return;
    this.isOpen.update(v => !v);
  }

  onFormSubmit(scores: { homeScore: number; awayScore: number }): void {
    this.predictClicked.emit({ matchId: this.match().id, ...scores });
    this.isOpen.set(false);
  }
}
