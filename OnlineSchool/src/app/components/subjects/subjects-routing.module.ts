import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuardService } from 'src/app/guards/admin-auth-guard.service';

import { CreateSubjectComponent } from './create-subject/create-subject.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuardService], children: [
            { path: 'add', component: CreateSubjectComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SubjectsRoutingModule { }
