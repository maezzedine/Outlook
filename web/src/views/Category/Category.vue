<template>
    <div class="Category" v-if="showCategory()">
        <div class="about">
            <div class="title" :style="[{ color: getCategoryColor() }]">
                {{Category.categoryName}}
            </div>
            <div class="row m-0 p-0">
                <b>{{$store.getters.Language.nbrOfArticles}}: </b>
                <p>{{Category.articlesCount}}</p>
            </div>
            <b>{{$store.getters.Language.juniorEditors}}:</b>
            <p>
                <ul>
                    <li v-for="editor in Category.juniorEditors">{{editor.name}}</li>
                </ul>
            </p>
        </div>
        <div class="articles">
            <article-thumbnail v-for="article in Articles" :article="article" />
        </div>
    </div>
    <!-- todo: add 404 incase of v-else -->
</template>

<script lang="ts" src="./Category.ts"></script>
<style lang="scss" src="./Category.scss" scoped></style>