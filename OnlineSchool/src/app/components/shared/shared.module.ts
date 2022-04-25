import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { AutocompleteLibModule } from "angular-ng-autocomplete";
import { TranslatePipe } from "src/app/pipes/translate.pipe";

import { FormComponent } from "./form/form.component";
import { ErrorContainerComponent } from './error-container/error-container.component';

@NgModule({
    declarations: [
        FormComponent,
        TranslatePipe,
        ErrorContainerComponent,
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AutocompleteLibModule
    ],
    exports: [
        FormComponent,
        ErrorContainerComponent,
        TranslatePipe,
        AutocompleteLibModule
    ],
    providers: [],
})
export class SharedModule { }