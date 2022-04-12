import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';

import { CreateClassComponent } from './create-class/create-class.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateClassComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClassesRoutingModule { }
