import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DisplayTimetableComponent } from './display-timetable/display-timetable.component';
import { EditTimetableComponent } from './edit-timetable/edit-timetable.component';

const routes: Routes = [
    {
        path: 'timetable', children: [
            {path: 'display', component: DisplayTimetableComponent},
            {path: 'edit', component: EditTimetableComponent},
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimetableRoutingModule { }
