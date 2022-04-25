import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LessonsComponent } from './lessons.component';
import { SharedModule } from '../shared/shared.module';
import { LessonsRoutingModule } from './lessons-routing.module';
import { CreateLessonsComponent } from './create-lessons/create-lessons.component';
import { HourPipe } from 'src/app/pipes/hour.pipe';
import { LessonsListComponent } from './lessons-list/lessons-list.component';
import { UpdateLessonComponent } from './update-lesson/update-lesson.component';

@NgModule({
  declarations: [
    LessonsComponent,
    CreateLessonsComponent,
    LessonsListComponent,
    UpdateLessonComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    LessonsRoutingModule
  ],
  providers: [HourPipe]
})
export class LessonsModule { }
