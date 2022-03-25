import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";

import { FormComponent } from "./form/form.component";

@NgModule({
    declarations: [
        FormComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule
    ],
    exports: [
        FormComponent
    ],
    providers: [],
})
export class SharedModule { }