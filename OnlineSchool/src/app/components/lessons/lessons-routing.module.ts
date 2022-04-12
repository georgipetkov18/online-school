import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuardService } from 'src/app/guards/admin-auth-guard.service';

import { CreateLessonsComponent } from './create-lessons/create-lessons.component';

const routes: Routes = [
    {
        path: '', canActivate:[AdminAuthGuardService], children: [
            { path: 'add', component: CreateLessonsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LessonsRoutingModule { }
