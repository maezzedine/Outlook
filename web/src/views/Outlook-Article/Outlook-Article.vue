<template>
    <div class="article">
        <div v-if="!loading"  class="category" 
                :style="[{background: getCategoryColor(Article.categoryTagName)}]">
            {{Article.category}} <span></span>
        </div>

        <div class="article-body">
            <div class="title">
                <template v-if="!loading">{{Article.title}}</template>
            </div>
            <div class="subtitle">
                <template v-if="!loading">{{Article.subtitle}}</template>
            </div>

            <template v-if="!loading">
                <img v-if="Article.picturePath != null" :src="APP_URL + Article.picturePath" :class="$store.getters['language/Language']['article-image']"/>
            </template>

            <div class="writer">
                <template v-if="!loading">{{Article.writer}} | {{Article.writerPosition}}</template>
            </div>

            <div id="article-text-body"></div>

            <div v-if="!loading" class="footer">
                <span :class="[{ ratedUp : Article.ratedByUser == 1 }]"><i class="fas fa-angle-up"></i></span>
                {{Article.rate}}
                <span :class="[{ ratedDown : Article.ratedByUser == 2 }]"><i class="fas fa-angle-down"></i></span>
                <span>{{$store.getters['language/Language'].votes}}: {{Article.numberOfVotes}}</span>
                <span v-if="Article.comments != undefined">{{$store.getters['language/Language'].comments}}: {{Article.comments.length}}</span>
                <span class="row m-0 p-0"><span><i class="far fa-star"></i></span>{{Article.numberOfFavorites}}</span>
                <!-- <i class="fas fa-star"></i> -->
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Outlook-Article.ts"></script>
<style lang="scss" src="./Outlook-Article.scss" scoped></style>