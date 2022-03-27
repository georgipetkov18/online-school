import { AutoComplete } from "../auto-complete.model";

export class LessonResponse implements AutoComplete {
    private _autoCompleteIdentifier = '';
    constructor(
        public id: string,
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