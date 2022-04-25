import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';
import { ClassResolver } from 'src/app/resolvers/class.resolver';
import { ClassesListComponent } from './classes-list/classes-list.component';
import { CreateClassComponent } from './create-class/create-class.component';
import { UpdateClassComponent } from './update-class/update-class.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateClassComponent },
            { path: 'all', component: ClassesListComponent },
            { path: 'edit/:id', component: UpdateClassComponent, resolve: [ClassResolver] },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClassesRoutingModule { }
