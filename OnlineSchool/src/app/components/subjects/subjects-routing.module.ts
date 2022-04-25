import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';
import { SubjectResolver } from 'src/app/resolvers/subject.resolver';

import { CreateSubjectComponent } from './create-subject/create-subject.component';
import { SubjectsListComponent } from './subjects-list/subjects-list.component';
import { UpdateSubjectComponent } from './update-subject/update-subject.component';

const routes: Routes = [
    {
        path: '', canActivate: [AdminAuthGuard], children: [
            { path: 'add', component: CreateSubjectComponent },
            { path: 'edit/:id', component: UpdateSubjectComponent, resolve: [SubjectResolver] },
            { path: 'all', component: SubjectsListComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SubjectsRoutingModule { }
