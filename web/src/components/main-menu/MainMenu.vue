<template>
    <div class="main-menu">
        <template v-for="article in Articles">
            <article-thumbnail :article="article" />
        </template>
    </div>
</template>

<script lang="ts" src="./MainMenu.ts"></script>
<style lang="scss" src="./MainMenu.scss" scoped></style>