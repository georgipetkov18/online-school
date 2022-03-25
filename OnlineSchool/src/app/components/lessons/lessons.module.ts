import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LessonsComponent } from './lessons.component';
import { SharedModule } from '../shared/shared.module';
import { LessonsRoutingModule } from './lessons-routing.module';
import { CreateLessonsComponent } from './create-lessons/create-lessons.component';

@NgModule({
  declarations: [
    LessonsComponent,
    CreateLessonsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    LessonsRoutingModule
  ]
})
export class LessonsModule { }
