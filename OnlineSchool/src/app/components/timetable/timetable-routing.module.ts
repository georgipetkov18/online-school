import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisplayTimetableComponent } from './display-timetable/display-timetable.component';
import { CreateTimetableComponent as ManageTimetableComponent } from './manage-timetable/manage-timetable.component';
import { TimetableInfoComponent } from './timetable-info/timetable-info.component';
import { AdminAuthGuard } from 'src/app/guards/admin.guard';
import { CommonUserAuthGuard } from 'src/app/guards/common-user.guard';
import { AuthGuard } from 'src/app/guards/auth.guard';

const routes: Routes = [
    {
        path: '', canActivate: [AuthGuard], children: [
            { path: 'display', component: DisplayTimetableComponent, canActivate: [CommonUserAuthGuard] },
            { path: 'manage', component: ManageTimetableComponent, canActivate: [AdminAuthGuard] },
            { path: 'info', component: TimetableInfoComponent, canActivate: [CommonUserAuthGuard] }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimetableRoutingModule { }
