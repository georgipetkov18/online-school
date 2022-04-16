import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { DisplayTimetableComponent } from "./display-timetable/display-timetable.component";

import { TimetableRoutingModule } from "./timetable-routing.module";
import { TimetableComponent } from "./timetable.component";
import { ManageTimetableComponent } from './manage-timetable/manage-timetable.component';
import { SharedModule } from "../shared/shared.module";
import { HourPipe } from "src/app/pipes/hour.pipe";
import { TimetableInfoComponent } from './timetable-info/timetable-info.component';

@NgModule({
    declarations: [
        TimetableComponent,
        DisplayTimetableComponent,
        ManageTimetableComponent,
        HourPipe,
        TimetableInfoComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        TimetableRoutingModule,
        SharedModule
    ],
    providers: [],
})
export class TimetableModule { }