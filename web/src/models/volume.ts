export class Volume {
    id: Number;

    number: Number;

    fall: Number;

    spring: Number;

    constructor(_id: Number, _number: Number, _fall: Number, _spring: Number) {
        this.id = _id;
        this.number = _number;
        this.fall = _fall;
        this.spring = _spring;
    }
}
