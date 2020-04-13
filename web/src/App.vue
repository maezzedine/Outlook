<template>
    <div id="app" :class="[{ 'sidebar-expanded': expanded }]">
        <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang" @expansion-toggled="toggleExpansion"/>

        <div class="window">
            <outlook-sidebar @theme-toggled="toggleTheme"></outlook-sidebar>

            <div id="route" :class="Language['window-expand']"><router-view @set-issue="setIssue" @set-volume="setVolume" /></div>
        </div>
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
