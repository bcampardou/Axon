import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';

import { SimplemdeComponent } from './simplemde.component';
import { SimplemdeConfig } from './simplemde.config';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  declarations: [SimplemdeComponent],
  exports: [SimplemdeComponent],
})
export class SimplemdeModule {
  static forRoot(config?: SimplemdeConfig): ModuleWithProviders {
    return {
      ngModule: SimplemdeModule,
      providers: [{ provide: SimplemdeConfig, useValue: config }],
    };
  }
}