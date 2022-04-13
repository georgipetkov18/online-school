import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { AutocompleteLibModule } from "angular-ng-autocomplete";
import { TranslatePipe } from "src/app/pipes/translate.pipe";

import { FormComponent } from "./form/form.component";

@NgModule({
    declarations: [
        FormComponent,
        TranslatePipe,
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AutocompleteLibModule
    ],
    exports: [
        FormComponent,
        TranslatePipe,
        AutocompleteLibModule
    ],
    providers: [],
})
export class SharedModule { }