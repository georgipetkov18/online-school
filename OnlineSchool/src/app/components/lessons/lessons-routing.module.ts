import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateLessonsComponent } from './create-lessons/create-lessons.component';

const routes: Routes = [
    {
        path: 'lessons', children: [
            { path: 'add', component: CreateLessonsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LessonsRoutingModule { }
