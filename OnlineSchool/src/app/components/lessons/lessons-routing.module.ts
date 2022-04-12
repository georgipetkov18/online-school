import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';

import { CreateLessonsComponent } from './create-lessons/create-lessons.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateLessonsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LessonsRoutingModule { }
