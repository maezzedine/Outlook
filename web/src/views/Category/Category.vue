<template>
    <div class="Category" v-if="showCategory()">
        <div class="about">
            <div class="title" :style="[{ color: getCategoryColor() }]">
                {{Category.name}}
            </div>
            <div class="row m-0 p-0">
                <b>{{$store.getters.Language.nbrOfArticles}}: </b>
                <p>{{Category.articles.length}}</p>
            </div>
            <b>{{$store.getters.Language.juniorEditors}}:</b>
            <p>
                <ul>
                    <li v-for="editor in Category.editors">{{editor.name}}</li>
                </ul>
            </p>
        </div>
        <div class="articles">
            <article-thumbnail v-for="article in Category.articles" :article="article" />
        </div>
    </div>
</template>

<script lang="ts" src="./Category.ts"></script>
<style lang="scss" src="./Category.scss" scoped></style>