<template>
    <div class="top-voted-articles">
        <span><i class="fas fa-trophy"></i> {{title}}:</span>

        <ol>
            <li v-for="article in articles" v-if="article.language == Language.num">
                {{article.title}}
            </li>
        </ol>
    </div>
</template>

<script lang="ts" src="./TopArticles.ts"></script>
<style lang="scss" src="./TopArticles.scss"></style>