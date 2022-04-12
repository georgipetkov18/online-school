import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuardService } from 'src/app/guards/admin-auth-guard.service';

import { CreateClassComponent } from './create-class/create-class.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuardService], children: [
            { path: 'add', component: CreateClassComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClassesRoutingModule { }
