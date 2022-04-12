import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisplayTimetableComponent } from './display-timetable/display-timetable.component';
import { EditTimetableComponent } from './edit-timetable/edit-timetable.component';
import { CreateTimetableComponent } from './create-timetable/create-timetable.component';
import { TimetableInfoComponent } from './timetable-info/timetable-info.component';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';
import { CommonUserAuthGuard } from 'src/app/guards/common-user.guard';

const routes: Routes = [
    {
        path: 'timetable', children: [
            { path: 'display', component: DisplayTimetableComponent, canActivate: [CommonUserAuthGuard] },
            { path: 'edit', component: EditTimetableComponent, canActivate: [AdminAuthGuard] },
            { path: 'create', component: CreateTimetableComponent, canActivate: [AdminAuthGuard] },
            { path: 'info', component: TimetableInfoComponent, canActivate: [CommonUserAuthGuard] }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimetableRoutingModule { }
