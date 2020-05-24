<template>
    <div id="app" :class="[{ 'sidebar-expanded': expanded }]">
        <template v-if="loading">
            <svg-spinner />
        </template>

        <template v-else>
            <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang" @expansion-toggled="toggleExpansion" />

            <div class="window">
                <outlook-sidebar @theme-toggled="toggleTheme" @expansion-toggled="toggleExpansion"></outlook-sidebar>

                <div id="route"><router-view @set-issue="setIssue" @set-volume="setVolume" /></div>
            </div>
        </template>
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
