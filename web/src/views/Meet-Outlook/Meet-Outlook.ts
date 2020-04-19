import { Component, Vue } from "vue-property-decorator";
import { api } from '../../services/api';
import { ApiObject } from '../../models/apiObject';

@Component
export default class About extends Vue {
    private ArabicBoard = new Array();
    private EnglishBoard = new Array();
    private EditorInChief = new Object();

    created() {
        this.getBoardMembers();
    }

    getBoardMembers() {
        api.getBoardMembers().then(d => {
            this.ArabicBoard = d['arabicBoard'];
            console.log(d['arabicBoard']);
            console.log(this.ArabicBoard);
            this.EnglishBoard = d['englishBoard'];
        })
    }

    getMemberName(member: Array<ApiObject>) {
        if (member != undefined) {
            return member[0].name + " | " + member[0].positionName;
        }
    }
}