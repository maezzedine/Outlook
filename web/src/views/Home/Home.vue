<template>
    <div class="Home" v-if="$store.getters.Language != undefined">
        <main-menu />
        <stats id="stats" class="d-none d-sm-block" :class="$store.getters.Language['stats-box-shadow']"/>
    </div>
</template>
<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss" scoped></style>
