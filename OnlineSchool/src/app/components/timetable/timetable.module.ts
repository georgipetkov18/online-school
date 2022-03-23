import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { DisplayTimetableComponent } from "./display-timetable/display-timetable.component";
import { EditTimetableComponent } from "./edit-timetable/edit-timetable.component";

import { TimetableRoutingModule } from "./timetable-routing.module";
import { TimetableComponent } from "./timetable.component";

@NgModule({
    declarations: [
        TimetableComponent,
        DisplayTimetableComponent,
        EditTimetableComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        TimetableRoutingModule,
    ],
    providers: [],
})
export class TimetableModule { }