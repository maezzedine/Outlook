<template>
    <div class="main-menu">
        <div v-for="article in Articles" class="article">
            <template v-if="showArticle(article)" >
                <div class="category" :style="[{background: getCategoryColor(article.categoryTagName)}]">{{article.category}}</div>
                <div class="title">{{article.title}}</div>
                <div class="subtitle"><span>{{article.writer}}</span> {{article.subtitle}}</div>

                <img v-if="article.picturePath != null" :src="APP_URL + article.picturePath" />
                <div class="footer">
                    <span :class="[{ ratedUp : article.ratedByUser == 1 }]"><i class="fas fa-angle-up"></i></span>
                    {{article.rate}}
                    <span :class="[{ ratedDown : article.ratedByUser == 2 }]"><i class="fas fa-angle-down"></i></span>
                    <span>{{Language.votes}}: {{article.numberOfVotes}}</span>
                    <span>{{Language.comments}}: {{article.comments.length}}</span>
                    <span>{{Language.favorites}}: {{article.numberOfFavorites}}</span>
                </div>
            </template>
        </div>
    </div>
</template>

<script lang="ts" src="./MainMenu.ts"></script>
<style lang="scss" src="./MainMenu.scss" scoped></style>