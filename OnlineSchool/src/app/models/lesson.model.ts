import { AutoComplete } from "./auto-complete.model";

export class Lesson implements AutoComplete {
    private _autoCompleteIdentifier = '';
    constructor(
        public from: string,
        public durationInMinutes: number
    ) { };

    get autoCompleteIdentifier() {
        return this._autoCompleteIdentifier;
    }
    set autoCompleteIdentifier(value: string) {
        this._autoCompleteIdentifier = value;
    }
}