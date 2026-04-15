import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PredictionDto, SubmitPredictionRequest, UpdatePredictionRequest } from '../../shared/models/prediction.models';

@Injectable({ providedIn: 'root' })
export class PredictionService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/v1/predictions`;

  getMyPredictions(): Observable<PredictionDto[]> {
    return this.http.get<PredictionDto[]>(`${this.base}/my`);
  }

  getMatchPredictions(matchId: string): Observable<PredictionDto[]> {
    return this.http.get<PredictionDto[]>(`${this.base}/match/${matchId}`);
  }

  submit(req: SubmitPredictionRequest): Observable<PredictionDto> {
    return this.http.post<PredictionDto>(this.base, req);
  }

  update(id: string, req: UpdatePredictionRequest): Observable<PredictionDto> {
    return this.http.put<PredictionDto>(`${this.base}/${id}`, req);
  }
}
