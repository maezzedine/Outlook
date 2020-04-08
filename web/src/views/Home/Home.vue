<template>
    <div class="Home">
        <main-menu />
        <div class="top-articles">
            <top-articles :articles="TopRatedArticles" :title="Language.topRatedArticles"/>
            <top-articles :articles="TopFavoritedArticles" :title="Language.topFavoritedArticles"/>
        </div>
    </div>
</template>
<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss" scoped></style>
