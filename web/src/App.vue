<template>
    <div id="app" :class="[{ 'sidebar-expanded': expanded }]">
        <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang" @set-issue="setIssue" @set-volume="setVolume" @expansion-toggled="toggleExpansion"
                        :language="Language"/>

        <div class="window">
            <outlook-sidebar @theme-toggled="toggleTheme" :language="Language"></outlook-sidebar>

            <router-view id="route" class="route expand-route"></router-view>

        </div>
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
