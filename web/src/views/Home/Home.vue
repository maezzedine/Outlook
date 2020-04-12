<template>
    <div class="Home" v-if="Language != undefined">
        <main-menu />
        <stats id="stats" class="d-none d-sm-block" :class="Language['stats-box-shadow']"/>
    </div>
</template>
<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss" scoped></style>
