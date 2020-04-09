<template>
    <div class="Home">
        <main-menu />
        <div class="top-articles">
            <top-stats :model="TopRatedArticles" />
            <top-stats :model="TopFavoritedArticles" />
        </div>
    </div>
</template>
<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss" scoped></style>
