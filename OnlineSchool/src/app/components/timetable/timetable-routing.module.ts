import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisplayTimetableComponent } from './display-timetable/display-timetable.component';
import { EditTimetableComponent } from './edit-timetable/edit-timetable.component';
import { CreateTimetableComponent } from './create-timetable/create-timetable.component';
import { TimetableInfoComponent } from './timetable-info/timetable-info.component';
import { AdminAuthGuardService } from 'src/app/guards/admin-auth-guard.service';
import { AuthGuardService } from 'src/app/guards/auth-guard.service';

const routes: Routes = [
    {
        path: 'timetable', children: [
            { path: 'display', component: DisplayTimetableComponent, canActivate: [AuthGuardService] },
            { path: 'edit', component: EditTimetableComponent, canActivate: [AdminAuthGuardService] },
            { path: 'create', component: CreateTimetableComponent, canActivate: [AdminAuthGuardService] },
            { path: 'info', component: TimetableInfoComponent, canActivate: [AuthGuardService] }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimetableRoutingModule { }
