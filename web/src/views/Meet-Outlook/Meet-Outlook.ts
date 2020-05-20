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
        var boardFromSession = sessionStorage.getItem('outlook-board');
        if (boardFromSession != null) {
            var boardMembers = JSON.parse(boardFromSession);
            this.getBoardSections(boardMembers);
        }
        else {
            api.Get('members/board').then(d => {
                sessionStorage.setItem('outlook-board', JSON.stringify(d));
                this.getBoardSections(d);
            })
        }
    }

    getMemberName(member: Array<ApiObject>) {
        if (member != undefined) {
            return member[0].name + " | " + member[0].position;
        }
    }

    getBoardSections(boardMembers: any) {
        this.ArabicBoard = boardMembers['arabicBoard'];
        this.EnglishBoard = boardMembers['englishBoard'];
    }
}