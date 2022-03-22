import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CreateSubjectComponent } from "./create-subject/create-subject.component";
import { SubjectsRoutingModule } from "./subjects-routing.module";

import { SubjectsComponent } from "./subjects.component";

@NgModule({
    declarations: [
        SubjectsComponent,
        CreateSubjectComponent
    ],
    imports: [
        FormsModule,
        SubjectsRoutingModule
    ],
    providers: [],
})
export class SubjectsModule { }