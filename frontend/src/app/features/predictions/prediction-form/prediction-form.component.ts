import { Component, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject } from '@angular/core';

@Component({
  selector: 'app-prediction-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()" class="flex items-center gap-3 mt-3 pt-3 border-t border-gray-100">
      <div class="flex items-center gap-2 flex-1">
        <input type="number" formControlName="homeScore" min="0" max="9"
               class="w-12 h-10 text-center text-lg font-bold border-2 border-blue-300 rounded-lg focus:outline-none focus:border-blue-500" />
        <span class="text-gray-400 font-bold">-</span>
        <input type="number" formControlName="awayScore" min="0" max="9"
               class="w-12 h-10 text-center text-lg font-bold border-2 border-blue-300 rounded-lg focus:outline-none focus:border-blue-500" />
      </div>
      <button type="submit" class="btn-primary px-4 py-2 text-sm" [disabled]="form.invalid">
        {{ submitLabel() }}
      </button>
    </form>
  `
})
export class PredictionFormComponent {
  private readonly fb = inject(FormBuilder);

  submitLabel = input<string>('Save');

  submitted = output<{ homeScore: number; awayScore: number }>();

  form = this.fb.nonNullable.group({
    homeScore: [0, [Validators.required, Validators.min(0), Validators.max(9)]],
    awayScore: [0, [Validators.required, Validators.min(0), Validators.max(9)]]
  });

  setValues(home: number, away: number): void {
    this.form.setValue({ homeScore: home, awayScore: away });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    const { homeScore, awayScore } = this.form.getRawValue();
    this.submitted.emit({ homeScore, awayScore });
  }
}
