<template>
    <div class="meet-outlook">
        <div class="editor-in-chief">
            {{getMemberName(EnglishBoard['Editor In Chief'])}}
        </div>
        <div class="english-board">
            <div class="board">English Board</div>
            <ul v-for="position in Object.keys(EnglishBoard)" v-if="position != 'Editor In Chief' && EnglishBoard[position].length > 0">
                {{position}}
                <li v-for="member in EnglishBoard[position]">
                    <router-link :to="{ name: 'member', params: { id: member.id } }">
                        {{member.name}}
                        <span v-if="position == 'Junior Editor'">
                            ({{member.category.categoryName}})
                        </span>
                    </router-link>
                </li>
            </ul>
        </div>
        <div class="arabic-board">
            <div class="board">Arabic Board</div>
            <ul v-for="position in Object.keys(ArabicBoard)" v-if="position != '\u0631\u0626\u064a\u0633 \u0627\u0644\u062a\u062d\u0631\u064a\u0631' && ArabicBoard[position].length > 0">
                {{position}}
                <li v-for="member in ArabicBoard[position]">
                    <router-link :to="{ name: 'member', params: { id: member.id } }">
                        {{member.name}}
                        <span v-if="position == '\u0631\u0626\u064a\u0633 \u0642\u0633\u0645'">
                            <!-- junior editors -->
                            ({{member.category.categoryName}})
                        </span>
                    </router-link>
                </li>
            </ul>
        </div>
        <div class="footer">
            <div class="row m-0 text-center">
                <div class="col-12 col-sm-4">
                    Find us on social media:
                </div>
                <div class="col-12 col-sm-4">
                    To contact us:
                </div>
                <div class="col-12 col-sm-4">
                    2020 &copy; AUB Outlook
                </div>
            </div>
            <div class="signature">
                Outlook is a weekly publication of the American University of Beirut (AUB) and represents the voice of the student body. It is an independent, non-affiliated publication that favors no ethic,
                religious or political group. All commons, articles and reports are the property of Outlook. And not neccesary represents the views of outlook or the AUB community. Outlook  welcomes all contributions.
                No part of this publication may be reproduces in any way, shape or form without the written  consent of Outlook and/or higher authorities. Outlook reserves the right to edit all material.
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Meet-Outlook.ts"></script>
<style lang="scss" src="./Meet-Outlook.scss" scoped></style>