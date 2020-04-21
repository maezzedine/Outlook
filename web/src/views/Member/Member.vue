<template>
    <div class="member">
        <div class="title">{{member.name}}</div>
        <table>
            <tr>
                <th>{{$store.getters.Language['position']}}:</th>
                <td>{{member.positionName}}</td>
            </tr>
            <tr v-if="member.category != null">
                <th>{{$store.getters.Language['category']}}:</th>
                <td>{{member.category.categoryName}}</td>
            </tr>
            <tr>
                <th>{{$store.getters.Language['number-of-articles']}}:</th>
                <td>{{member.numberOfArticles}}</td>
            </tr>
        </table>
        <div class="articles">
            <article-thumbnail v-for="article in member.articles" :article="article" />
        </div>
    </div>
</template>

<script lang="ts" src="./Member.ts"></script>
<style lang="scss" src="./Member.scss" scoped></style>