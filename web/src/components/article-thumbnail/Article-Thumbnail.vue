<template>
    <router-link :to="{name: 'article', params: { id: article.id} }" class="article" v-if="showArticle(article)">
        <div class="category" :style="[{background: getCategoryColor(article.category.tag)}]">{{article.category.categoryName}}</div>
        <div class="title">{{article.title}}</div>
        <div class="subtitle">-<span>{{article.member.name}}:</span> {{article.subtitle}}</div>

        <img v-if="article.picturePath != null" :src="APP_URL + article.picturePath" />
        <div class="footer">
            <svg-arrow-up />
            {{article.rate}}
            <svg-arrow-down />
            <span>{{$store.getters.Language.votes}}: {{article.numberOfVotes}}</span>
            <span>{{$store.getters.Language.comments}}: {{article.comments.length}}</span>
            <svg-star-empty class="mt-0" />{{article.numberOfFavorites}}
        </div>
    </router-link>
</template>

<script lang="ts" src="./Article-Thumbnail.ts"></script>
<style lang="scss" src="./Article-Thumbnail.scss" scoped></style>