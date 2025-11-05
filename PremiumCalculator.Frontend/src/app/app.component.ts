import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PremiumCalculatorService } from './services/premium-calculator.service';
import { Occupation } from './models/occupation';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  premiumForm: FormGroup;
  occupations: Occupation[] = [];
  monthlyPremium: number | null = null;
  occupationRating: string = '';
  occupationRatingFactor: number = 0;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private premiumService: PremiumCalculatorService
  ) {
    this.premiumForm = this.fb.group({
      name: ['', Validators.required],
      ageNextBirthday: ['', [Validators.required, Validators.min(1), Validators.max(120)]],
      dateOfBirth: ['', [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{4}$/)]],
      usualOccupation: ['', Validators.required],
      deathSumInsured: ['', [Validators.required, Validators.min(0.01)]]
    });

    // Optional: Subscribe to occupation changes to trigger calculation (commented out - using submit button instead)
    // this.premiumForm.get('usualOccupation')?.valueChanges.subscribe(() => {
    //   if (this.premiumForm.valid) {
    //     this.calculatePremium();
    //   }
    // });
  }

  ngOnInit(): void {
    this.loadOccupations();
  }

  loadOccupations(): void {
    this.premiumService.getOccupations().subscribe({
      next: (occupations) => {
        this.occupations = occupations;
      },
      error: (error) => {
        console.error('Error loading occupations:', error);
      }
    });
  }

  calculatePremium(): void {
    // Mark all fields as touched to show validation errors
    if (!this.premiumForm.valid) {
      Object.keys(this.premiumForm.controls).forEach(key => {
        this.premiumForm.get(key)?.markAsTouched();
      });
      return;
    }

    this.loading = true;
    const formValue = this.premiumForm.value;

    this.premiumService.calculatePremium({
      name: formValue.name,
      ageNextBirthday: formValue.ageNextBirthday,
      dateOfBirth: formValue.dateOfBirth,
      usualOccupation: formValue.usualOccupation,
      deathSumInsured: formValue.deathSumInsured
    }).subscribe({
      next: (response) => {
        this.monthlyPremium = response.monthlyPremium;
        this.occupationRating = response.occupationRating;
        this.occupationRatingFactor = response.occupationRatingFactor;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error calculating premium:', error);
        this.loading = false;
        alert('Error calculating premium. Please check your inputs.');
      }
    });
  }

  getErrorMessage(fieldName: string): string {
    const field = this.premiumForm.get(fieldName);
    if (field?.hasError('required')) {
      return `${this.getFieldLabel(fieldName)} is required`;
    }
    if (field?.hasError('min')) {
      return `${this.getFieldLabel(fieldName)} must be greater than ${field.errors!['min'].min}`;
    }
    if (field?.hasError('max')) {
      return `${this.getFieldLabel(fieldName)} must be less than ${field.errors!['max'].max}`;
    }
    if (field?.hasError('pattern')) {
      return `${this.getFieldLabel(fieldName)} must be in mm/YYYY format`;
    }
    return '';
  }

  getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      name: 'Name',
      ageNextBirthday: 'Age Next Birthday',
      dateOfBirth: 'Date of Birth',
      usualOccupation: 'Usual Occupation',
      deathSumInsured: 'Death Sum Insured'
    };
    return labels[fieldName] || fieldName;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.premiumForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }
}

