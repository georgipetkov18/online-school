import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { CreateClassComponent } from '../classes/create-class/create-class.component';
import { ClassesRoutingModule } from "./classes-routing.module";
import { ClassesComponent } from "./classes.component";

@NgModule({
    declarations: [
        ClassesComponent,
        CreateClassComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ClassesRoutingModule,
    ],
    providers: [],
})
export class ClassesModule { }