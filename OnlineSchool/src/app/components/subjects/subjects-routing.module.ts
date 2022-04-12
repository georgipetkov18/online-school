import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';

import { CreateSubjectComponent } from './create-subject/create-subject.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateSubjectComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SubjectsRoutingModule { }
