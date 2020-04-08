<template>
    <div class="main-menu">
        <div v-for="article in Articles" class="article">
            <template v-if="showArticle(article)" >
                <div class="category" :style="[{background: getCategoryColor(article.categoryTagName)}]">{{article.category}}</div>
                <div class="title">{{article.title}}</div>
                <div class="subtitle"><span>{{article.writer}}</span> {{article.subtitle}}</div>

                <img v-if="article.picturePath != null" :src="APP_URL + article.picturePath" />
            </template>
        </div>
    </div>
</template>

<script lang="ts" src="./MainMenu.ts"></script>
<style lang="scss" src="./MainMenu.scss" scoped></style>