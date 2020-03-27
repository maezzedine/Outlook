<template>
    <div id="app">
        <outlook-navbar @theme-toggled="toggleTheme" @language-toggled="toggleLang" @set-issue="setIssue"
                        :language="Language"/>

        <div class="window">
            <outlook-sidebar @theme-toggled="toggleTheme" :language="Language"></outlook-sidebar>

            <router-view class="route"></router-view>

        </div>



    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
