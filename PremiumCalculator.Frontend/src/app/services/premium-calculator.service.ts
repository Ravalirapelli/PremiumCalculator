import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Occupation } from '../models/occupation';
import { PremiumCalculationRequest } from '../models/premium-calculation-request';
import { PremiumCalculationResponse } from '../models/premium-calculation-response';

@Injectable({
  providedIn: 'root'
})
export class PremiumCalculatorService {
  private apiUrl = 'http://localhost:4112/api/premium';

  constructor(private http: HttpClient) { }

  getOccupations(): Observable<Occupation[]> {
    return this.http.get<Occupation[]>(`${this.apiUrl}/occupations`);
  }

  calculatePremium(request: PremiumCalculationRequest): Observable<PremiumCalculationResponse> {
    return this.http.post<PremiumCalculationResponse>(`${this.apiUrl}/calculate`, request);
  }
}

