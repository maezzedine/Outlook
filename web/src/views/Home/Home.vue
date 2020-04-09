<template>
    <div class="Home">
        <main-menu />
        <div class="top-articles d-none d-sm-block">
            <top-stats :model="TopRatedArticles" />
            <top-stats :model="TopFavoritedArticles" />
            <top-stats :model="TopWriters" />
        </div>
    </div>
</template>
<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss" scoped></style>
