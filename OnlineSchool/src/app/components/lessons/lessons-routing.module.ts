import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';
import { LessonResolver } from 'src/app/resolvers/lesson.resolver';

import { CreateLessonsComponent } from './create-lessons/create-lessons.component';
import { LessonsListComponent } from './lessons-list/lessons-list.component';
import { UpdateLessonComponent } from './update-lesson/update-lesson.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateLessonsComponent },
            { path: 'edit/:id', component: UpdateLessonComponent, resolve: [LessonResolver] },
            { path: 'all', component: LessonsListComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LessonsRoutingModule { }
