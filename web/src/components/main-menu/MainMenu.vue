<template>
    <div class="main-menu">
        <div class="title">
            <div class="volume">{{VolumeToString()}}</div>
            <div class="issue">{{IssueToString()}}</div>
            <div class="theme">
                <template v-if="Language.lang == 'en'">{{Issue.englishTheme}}</template>
                <template v-if="Language.lang == 'ar'">{{Issue.arabicTheme}}</template>
            </div>
            
        </div>

        <template v-for="article in Articles">
            <article-thumbnail :article="article" />
        </template>
    </div>
</template>

<script lang="ts" src="./MainMenu.ts"></script>
<style lang="scss" src="./MainMenu.scss" scoped></style>