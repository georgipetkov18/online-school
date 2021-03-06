import { AutoComplete } from "../auto-complete.model";

export class SubjectResponse implements AutoComplete {
    private _autoCompleteIdentifier = '';
    constructor(
        public id: string,
        public name: string,
        public code: string,
    ) { };

    get autoCompleteIdentifier() {
        return this.name;
    }
    
    set autoCompleteIdentifier(value: string) {
        this._autoCompleteIdentifier = value;
    }
}