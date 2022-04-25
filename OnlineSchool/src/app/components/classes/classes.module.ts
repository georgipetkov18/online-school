import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { CreateClassComponent } from '../classes/create-class/create-class.component';
import { SharedModule } from "../shared/shared.module";
import { ClassesRoutingModule } from "./classes-routing.module";
import { ClassesComponent } from "./classes.component";
import { ClassesListComponent } from './classes-list/classes-list.component';
import { UpdateClassComponent } from './update-class/update-class.component';

@NgModule({
    declarations: [
        ClassesComponent,
        CreateClassComponent,
        ClassesListComponent,
        UpdateClassComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ClassesRoutingModule,
        SharedModule
    ],
    providers: [],
})
export class ClassesModule { }