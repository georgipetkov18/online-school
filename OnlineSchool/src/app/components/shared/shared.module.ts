import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { AutocompleteLibModule } from "angular-ng-autocomplete";

import { FormComponent } from "./form/form.component";

@NgModule({
    declarations: [
        FormComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AutocompleteLibModule
    ],
    exports: [
        FormComponent,
        AutocompleteLibModule
    ],
    providers: [],
})
export class SharedModule { }