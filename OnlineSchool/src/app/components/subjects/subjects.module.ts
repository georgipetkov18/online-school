import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";

import { CreateSubjectComponent } from "./create-subject/create-subject.component";
import { SubjectsRoutingModule } from "./subjects-routing.module";
import { SubjectsComponent } from "./subjects.component";
import { SubjectsListComponent } from './subjects-list/subjects-list.component';

@NgModule({
    declarations: [
        SubjectsComponent,
        CreateSubjectComponent,
        SubjectsListComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        SubjectsRoutingModule,
        SharedModule
    ],
    providers: [],
})
export class SubjectsModule { }