import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisplayTimetableComponent } from './display-timetable/display-timetable.component';
import { EditTimetableComponent } from './edit-timetable/edit-timetable.component';
import { CreateTimetableComponent } from './create-timetable/create-timetable.component';
import { TimetableInfoComponent } from './timetable-info/timetable-info.component';

const routes: Routes = [
    {
        path: 'timetable', children: [
            {path: 'display', component: DisplayTimetableComponent},
            {path: 'edit', component: EditTimetableComponent},
            {path: 'create', component: CreateTimetableComponent},
            {path: 'info', component: TimetableInfoComponent}
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimetableRoutingModule { }
