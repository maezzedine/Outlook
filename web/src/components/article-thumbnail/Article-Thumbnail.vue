<template>
    <router-link :to="{name: 'article', params: { id: article.id} }" class="article" v-if="showArticle(article)">
        <div class="category" :style="[{background: getCategoryColor(article.categoryTagName)}]">{{article.category.categoryName}}</div>
        <div class="title">{{article.title}}</div>
        <div class="subtitle">-<span>{{article.member.name}}:</span> {{article.subtitle}}</div>

        <img v-if="article.picturePath != null" :src="APP_URL + article.picturePath" />
        <div class="footer">
            <span :class="[{ ratedUp : article.ratedByUser == 1 }]"><i class="fas fa-angle-up"></i></span>
            {{article.rate}}
            <span :class="[{ ratedDown : article.ratedByUser == 2 }]"><i class="fas fa-angle-down"></i></span>
            <span>{{$store.getters.Language.votes}}: {{article.numberOfVotes}}</span>
            <span>{{$store.getters.Language.comments}}: {{article.comments.length}}</span>
            <span class="row m-0 p-0"><span><i class="far fa-star"></i></span>{{article.numberOfFavorites}}</span>
            <!-- <i class="fas fa-star"></i> -->
        </div>
    </router-link>
</template>

<script lang="ts" src="./Article-Thumbnail.ts"></script>
<style lang="scss" src="./Article-Thumbnail.scss" scoped></style>