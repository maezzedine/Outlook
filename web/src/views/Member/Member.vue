<template>
    <div class="member">
        <div class="title">{{member.name}}</div>
        <table>
            <tr>
                <th>{{$store.getters.Language['position']}}:</th>
                <td>{{member.position}}</td>
            </tr>
            <tr v-if="member.category != null">
                <th>{{$store.getters.Language['category']}}:</th>
                <td>{{member.category.categoryName}}</td>
            </tr>
            <tr v-if="member.articles != undefined">
                <th>{{$store.getters.Language['number-of-articles']}}:</th>
                <td>{{member.articles.length}}</td>
            </tr>
        </table>
        <div class="articles">
            <article-thumbnail v-for="article in member.articles" :article="article" :language-specific="false"/>
        </div>
    </div>
</template>

<script lang="ts" src="./Member.ts"></script>
<style lang="scss" src="./Member.scss" scoped></style>