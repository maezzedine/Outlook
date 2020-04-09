<template>
    <div class="top-articles">
        <top-stats :model="TopRatedArticles" />
        <top-stats :model="TopFavoritedArticles" />
        <top-stats :model="TopWriters" />
    </div>
</template>

<script lang="ts" src="./Stats.ts"></script>