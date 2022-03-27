import { AutoComplete } from "../auto-complete.model";

export class TeacherResponse implements AutoComplete {
    private _autoCompleteIdentifier = '';
    constructor(
        public firstName: string,
        public lastName: string,
    ) { };

    get autoCompleteIdentifier() {
        return `${this.firstName} ${this.lastName}`;
    }
    set autoCompleteIdentifier(value: string) {
        this._autoCompleteIdentifier = value;
    }
}