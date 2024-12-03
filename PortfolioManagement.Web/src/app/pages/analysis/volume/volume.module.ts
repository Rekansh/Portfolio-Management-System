import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { VolumeComponent } from './volume.component';
import { VolumeRoutingModule } from './volumerouting';
import { NumberSuffixPipe } from './number-suffix.pipe';


@NgModule({
  declarations: [
    VolumeComponent,
    NumberSuffixPipe,
  ],
  imports: [
    CommonModule,
    VolumeRoutingModule,
    FormsModule,

  ]
})
export class VolumeModule { }
