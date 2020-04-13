<template>
    <div class="Category" v-if="showCategory()">
        <div class="about">
            <div class="title" :style="[{ color: getCategoryColor() }]">
                {{Category.categoryName}}
            </div>
            <table>
                <tr>
                    <th>{{Language.nbrOfArticles}}:</th>
                    <td>{{Category.articlesCount}}</td>
                </tr>
                <tr>
                    <th>{{Language.juniorEditors}}:</th>
                    <td v-for="editor in Category.juniorEditors">{{editor.name}}</td>
                </tr>
            </table>
        </div>
        <div class="articles">
            <article-thumbnail v-for="article in Articles" :article="article" />
        </div>
    </div>
    <!-- todo: add 404 incase of v-else -->
</template>

<script lang="ts" src="./Category.ts"></script>
<style lang="scss" src="./Category.scss"></style>