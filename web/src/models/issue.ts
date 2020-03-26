export class Issue {
    id: Number;

    number: Number;

    arabicPdf: File;

    englishPdf: File;

    volumeId: Number;

    constructor(_id: Number, _number: Number, _arabicPdf: File, _engishPdf: File, _volumeId: Number) {
        this.id = _id;
        this.number = _number;
        this.arabicPdf = _arabicPdf;
        this.englishPdf = _engishPdf;
        this.volumeId = _volumeId;
    }
}